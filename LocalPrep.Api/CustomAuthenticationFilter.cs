using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace LocalPrep.Api
{
    public class CustomAuthenticationFilter : AuthorizeAttribute, IAuthenticationFilter
    {
       
            public bool AllowMultiple
            {
                get { return false; }
            }
            public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
            {
                string authparameter = string.Empty;
                HttpRequestMessage request = context.Request;
                AuthenticationHeaderValue authorization = request.Headers.Authorization;
                string[] TokenAndUser = null;
                if (authorization == null)
                {
                    context.ErrorResult = new AuthenticationFailureResult(reasonPhrase: "Missing Authorization Header", request);
                    return;
                }
                if (authorization.Scheme != "Bearer")
                {
                    context.ErrorResult = new AuthenticationFailureResult(reasonPhrase: "Invalid Authorization Schema", request);
                    return;
                }
                TokenAndUser = authorization.Parameter.Split(':');
                string Token = TokenAndUser[0];
                string userName = TokenAndUser[1];
                if (string.IsNullOrEmpty(Token))
                {
                    context.ErrorResult = new AuthenticationFailureResult(reasonPhrase: "Missing Token", request);
                    return;

                }
                string validUserName = TokenManager.ValidateToken(Token);
                if (userName != validUserName)
                {
                    context.ErrorResult = new AuthenticationFailureResult(reasonPhrase: "Invalid Token for user", request);
                    return;
                }
                context.Principal = TokenManager.GetPrincipal(authorization.Parameter);
            }

            public async Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
            {
                var result = await context.Result.ExecuteAsync(cancellationToken);
                if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    result.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue(scheme: "Basic", parameter: "realm=localhost"));

                }
                context.Result = new ResponseMessageResult(result);
            }
        
        public class AuthenticationFailureResult : IHttpActionResult
        {
            public string ReasonPharse;
            public HttpRequestMessage Request { get; set; }
            public AuthenticationFailureResult(string reasonPhrase, HttpRequestMessage request)
            {
                ReasonPharse = reasonPhrase;
                Request = request;
            }
            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                return Task.FromResult(Execute());
            }
            public HttpResponseMessage Execute()
            {
                HttpResponseMessage responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                responseMessage.RequestMessage = Request;
                responseMessage.ReasonPhrase = ReasonPharse;
                return responseMessage;
            }
        }
    }
}