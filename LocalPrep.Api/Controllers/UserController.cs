using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LocalPrep.Entity;
using LocalPrep.Services;


namespace LocalPrep.Api.Controllers
{
    public class UserController : ApiController
    {
       
       
        [HttpPost]
        public HttpResponseMessage Registration([FromBody] UserEntity userEntity)
        {
            UserModel objUserModel = new UserModel();
            APIResponsecs objresponse = new APIResponsecs();
            string UserId = string.Empty;

            try
            {

                UserService userService = new UserService();
                objUserModel = userService.CreateUser(userEntity);
                if (!string.IsNullOrEmpty(objUserModel.Id))
                {
                    objresponse.Error = false;
                    objresponse.Message = "New Record Inserted Successfully";
                    //objresponse.Id = UserId;
                    return Request.CreateResponse(HttpStatusCode.Created, objUserModel);
                }
                else
                {
                    objresponse.Error = true;
                    objresponse.Message = "Internal Server Error";
                    objresponse.Id = "";
                   
                }


                return Request.CreateResponse(HttpStatusCode.InternalServerError, objresponse);

            }
            catch (Exception ex)
            {
                //foreach (var eve in ex.EntityValidationErrors)
                //{

                //    foreach (var ve in eve.ValidationErrors)
                //    {
                //        objresponse.Message = ve.ErrorMessage;
                //    }
                //}
                return Request.CreateResponse(HttpStatusCode.InternalServerError, objresponse);

            }
        }

        

        //public HttpResponseMessage Login([FromBody] LoginModel loginModel)
        //{
        //    try
        //    {
        //        UserService userService = new UserService();
        //        var user = userService.GetValidUser(loginModel.UserName, loginModel.Password);
        //        if (user != null)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.OK, value: TokenManager.GenerateToken(loginModel.UserName));
        //        }
        //        else
        //        {
        //            return Request.CreateErrorResponse(HttpStatusCode.BadGateway, message: "User name and password is invalid");

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.InternalServerError, "Some Things Went Wroung");

        //    }

        //}

        [HttpPut]
        public HttpResponseMessage EditRegistration([FromBody] UserEntity user)
        {
            UserModel objUserModel = new UserModel();
            APIResponsecs objresponse = new APIResponsecs();

            try
            {

                UserService userService = new UserService();
                objUserModel = userService.UpdateUser(user);
                if (!string.IsNullOrEmpty(objUserModel.Id))
                {
                    objresponse.Error = false;
                    objresponse.Message = "User Record Updated Successfully";
                    objresponse.Id = objUserModel.Id;
                    return Request.CreateResponse(HttpStatusCode.Created, objUserModel);
                }
                else
                {
                    objresponse.Error = true;
                    objresponse.Message = "Internal Server Error";
                    objresponse.Id = "";

                }


                return Request.CreateResponse(HttpStatusCode.InternalServerError, objresponse);

            }
            catch (Exception ex)
            {
                //foreach (var eve in ex.EntityValidationErrors)
                //{

                //    foreach (var ve in eve.ValidationErrors)
                //    {
                //        objresponse.Message = ve.ErrorMessage;
                //    }
                //}
                return Request.CreateResponse(HttpStatusCode.InternalServerError, objresponse);

            }
        }

       

       
    }
}
