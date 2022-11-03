using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using LocalPrep.Web.Models;
using RestSharp;
using System.Collections.Generic;
using System.IO;

namespace LocalPrep.Web.Models
{
    public class General
    {
        public IRestResponse Apistatelist()
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/state");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            // request.AddHeader("Auth-Token", "AwGQIZdk8vQYO9cWLpCMQWExmxsHTEMOHkCougRryZCq%2BrEaGdLoyS1HDinp5Sd4Fc6iWQRwRoDxuQi8JsxFH6zHfJ9aLiMWB7js9qcug7yYr7Cy661gBzkqyy5JCZuCYBRaPW%2FMoLOHks811UjSlGU2LCz2wXePlbiTxe7HauPgdedr39Hn%2BkmsPPoI%2F13OvjI3%2FxxYzTBnxHwx3rqt5m17BjjmjksBySSyyMvtdDDrgxANXQtKp9nZIJgjXJcBmhiAgZDT6vt1AsURxqtjYdoJztllleBfXBGjzCa6uaEZfQ%3D%3D");
            request.AddHeader("Content-Type", "application/json");
            var response = client.Execute(request);
            return (response);

        }
        public IRestResponse ApiResdietspecialities(string access_token)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/prepper/diet-specialities");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            IRestResponse response = client.Execute(request);
            return (response);
        }

        public IRestResponse ApiRescuisinespecialities(string access_token)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/prepper/cuisine-specialities");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            IRestResponse response = client.Execute(request);
            return (response);
        }
        public IRestResponse ApiGetOtp(string email)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/otp?email=" + email + "");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            return (response);

        }

        public IRestResponse ApiVerifyOtp(string email, string code)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/otp/verify?email=" + email + "&code=" + code + "");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            return (response);

        }
        public IRestResponse ApiPurchasePlan(PurchasePlan Model, string access_token)
        {
            string cardnumber = Model.card_number;
            cardnumber = cardnumber.Replace(" ", "");
            ///Model.expiration_date
            string expiration_date = "2025-12";
            //string[] test = Model.expiration_date.Split();
            //foreach (string s in test)
            //{
            //    Console.WriteLine(s);
            //}

            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/prepper/plans/3/subscribe");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{    \r\n    \"card_number\": \"" + cardnumber + "\",\r\n    \"expiration_date\": \"" + expiration_date + "\",\r\n    \"card_code\": \"" + Model.card_code + "\",\r\n    \"auto_renew\": true,\r\n    \"save_card\": false,\r\n    \"subscription_type\": \"ONE-TIME\"\r\n}", ParameterType.RequestBody);
            //request.AddParameter("application/json", "{\r\n    \"card_number\": \"4111111111111111\",\r\n    \"expiration_date\": \"2025-12\",\r\n    \"card_code\": \"122\",\r\n    \"auto_renew\": true,\r\n    \"save_card\": false,\r\n    \"subscription_type\": \"ONE-TIME\"    \r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);
        }


        public IRestResponse ApicustomerSavecards(string CustomercardNumber, string CustomercardYear, string CustomercardCVV, string access_token)
        {
            string cardnumber = CustomercardNumber;
            cardnumber = cardnumber.Replace(" ", "");

            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/customer/cards");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{    \r\n    \"card_number\": \"" + cardnumber + "\",\r\n    \"expiration_date\": \"" + CustomercardYear + "\",\r\n    \"card_code\": \"" + CustomercardCVV + "\"\r\n}", ParameterType.RequestBody);
            //request.AddParameter("application/json", "{\r\n    \"card_number\": \"4111111111111111\",\r\n    \"expiration_date\": \"2025-12\",\r\n    \"card_code\": \"122\",\r\n    \"auto_renew\": true,\r\n    \"save_card\": false,\r\n    \"subscription_type\": \"ONE-TIME\"    \r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);
        }

        public IRestResponse ApiGetcustomercards(string access_token)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/customer/cards");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            var body = @"";
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);
        }
        public IRestResponse ApiGetProfile(string access_token)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/auth/prepper/profile");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            var body = @"";
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);
        }


        public IRestResponse ApiOrdersbyId(string access_token, int Id)
        {

            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/prepper/orders/" + Id);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);
        }

        public IRestResponse ApiCustomerOrdersbyId(string access_token, int Id)
        {

            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/customer/orders/" + Id);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);
        }

        public IRestResponse ApiOrdersprepper(string access_token)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/prepper/orders/active");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);
        }

        public IRestResponse ApiOrdersprepperhistory(string access_token)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/prepper/orders/history");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);
        }

        public IRestResponse ApiOrdersCustomerhistory(string access_token)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/customer/orders/history");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);
        }

        public IRestResponse ApiOrders(string access_token)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/customer/orders");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);
        }

        public IRestResponse ApiOrderAccepted(string access_token, int Id)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/prepper/orders/status");
            client.Timeout = -1;
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\r\n    \"id\": " + Id + ",\r\n    \"status\": \"ACCEPTED\"\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);
        }

        public IRestResponse ApiOrderRejected(string access_token, int Id)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/prepper/orders/status");
            client.Timeout = -1;
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\r\n    \"id\": " + Id + ",\r\n    \"status\": \"REJECTED\"\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);
        }

        public IRestResponse ApiForgetPasswordCheck(string email)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/auth/forgot-password/check");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var body = "{\r\n    \"email\": \"" + email + "\"\r\n}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);
        }

        public IRestResponse ApiCreateNewPassword(ResetPasswordViewModel model)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/auth/forgot-password/update");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var body = "{\r\n    \"email\": \"" + model.Email + "\"\r\n,\r\n    \"password\": \"" + model.Password + "\"\r\n,\r\n    \"code\": \"1234\"\r\n}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);
        }

        public IRestResponse ApiResetPassword(ResetPasswordViewModel model, string access_token)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/auth/reset-password");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\r\n    \"email\": \"" + model.Email + "\",\r\n    \"password\": \"" + model.Password + "@\",\r\n    \"code\": \"1234\"\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);
        }

        public IRestResponse ApiAddBankAccount(BankAccount model, string access_token)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/prepper/banks");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\r\n    \"customer_payment_profile_id\": \"901448791\",\r\n    \"name_on_account\": \"" + model.name_on_account + "\",\r\n    \"bank_account_number\": \"" + model.bank_account_number + "\",\r\n    \"routing_number\": \"" + model.routing_number + "\",\r\n    \"account_type\": \"savings\",\r\n    \"bank_name\": \"" + model.bank_name + "\"\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);
        }

        public IRestResponse ApiUpdateBankAccount(BankAccount model, string access_token)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/prepper/banks");
            client.Timeout = -1;
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\r\n    \"customer_payment_profile_id\": \"901448791\",\r\n    \"name_on_account\": \"" + model.name_on_account + "\",\r\n    \"bank_account_number\": \"" + model.bank_account_number + "\",\r\n    \"routing_number\": \"" + model.routing_number + "\",\r\n    \"account_type\": \"savings\",\r\n    \"bank_name\": \"" + model.bank_name + "\"\r\n}", ParameterType.RequestBody);
            //request.AddParameter("application/json", "{\r\n    \"customer_payment_profile_id\": \"901448791\",\r\n    \"name_on_account\": \"Prakash Sharma\",\r\n    \"bank_account_number\": \"1234165498787\",\r\n    \"routing_number\": \"072403004\",\r\n    \"account_type\": \"savings\",\r\n    \"bank_name\": \"Wells Fargo Bank NA\"\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);
        }

        public IRestResponse ApiDeleteBankAccount(string customerpaymentprofileid, string access_token)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/prepper/banks");
            client.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddHeader("Content-Type", "application/json");
            var body = "{\r\n    \"customer_payment_profile_id\": \"" + customerpaymentprofileid + "\"\r\n}";
            //var body = @"{" + "\n" +  @"    ""customer_payment_profile_id"": """+ customerpaymentprofileid + "" ""+ "\n" +  @"}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);
        }

        public IRestResponse ApiGetBankAccount(string access_token)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/prepper/banks");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            var body = @"";
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);
        }

        public IRestResponse ApiHelpRequest(HelpRequest model, string access_token)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/prepper/help-request");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\r\n    \"subject\": \"" + model.subject + "\",\r\n    \"message\": \"" + model.message + "\"\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);
        }

        public IRestResponse ApiGetNotifications(string access_token)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/prepper/notifications");
            //var client = new RestClient("http://127.0.0.1:8000/");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            var body = @"";
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);
        }

        public IRestResponse ApiGetNotificationsPreppercount(string access_token)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/prepper/notifications/count");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            var body = @"";
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);
        }

        public IRestResponse ApiGetNotificationscustomer(string access_token)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/customer/notifications");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            var body = @"";
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);
        }


        public IRestResponse ApiGetNotificationsCustomercount(string access_token)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/customer/notifications/count");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            var body = @"";
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);
        }






        public IRestResponse ApiRegisterfinalstep(string registration_data, string Code)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/auth/customer/registration/final");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var body = "{\r\n    \"registration_data\": \"" + registration_data + "\"\r\n,\r\n    \"verification_code\": \"" + Code + "\"\r\n}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);
        }

        public IRestResponse Apicustomersearch(string search, string Code, string miles, string mini_price, string max_price, string access_token)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/search?search_by=" + search + "&keyword=" + Code + "&miles=" + miles + "&mini_price=" + mini_price + "&max_price=" + max_price + "");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            //request.AddHeader("Authorization", "Bearer" + access_token + "");
            var body = @"";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);
        }
        public IRestResponse Apilocaldata(string access_token)
        {

            ResponseLocaldata RLD = new ResponseLocaldata();
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/customer/local_data");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);

        }

        public IRestResponse Apidashboard(string access_token)
        {
            ResponseLocaldata RLD = new ResponseLocaldata();
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/customer/dashboard");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);

        }

        public IRestResponse ApiCustomerMealId(string access_token, int? meal_id)
        {
            ResponseLocaldata RLD = new ResponseLocaldata();
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/meals/" + meal_id + "");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);

        }
        public IRestResponse ApiPreppersbyPrepperId(string access_token, int prepper_id)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/preppers/" + prepper_id + "");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);

        }





        public IRestResponse ApiAddCart(string access_token, int meal_id, int quantity)
        {
            ResponseLocaldata RLD = new ResponseLocaldata();
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/customer/carts");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddParameter("application/json", "{\r\n    \"meal_id\": " + meal_id + ",\r\n    \"quantity\":" + quantity + "\r\n}", ParameterType.RequestBody);
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);

        }

        public IRestResponse ApiGetCart(string access_token)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/customer/carts");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);

        }

        public IRestResponse ApiDeletCart(string customerpaymentprofileid, string access_token)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/customer/cards");
            client.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddHeader("Content-Type", "application/json");
            var body = "{\r\n    \"customer_payment_profile_id\": \"" + customerpaymentprofileid + "\"\r\n}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);

        }

        public IRestResponse ApiDeleteCart(string access_token, int Id)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/customer/carts/" + Id + "");
            client.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);

        }

        public IRestResponse Apiprofile(string access_token)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/auth/prepper/profile");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            var body = @"";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);

        }

        public IRestResponse ApiCustomerProfile(string access_token)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/auth/customer/profile");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            var body = @"";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);

        }

        public IRestResponse ApiCustomerreviews(string access_token, int prepper_id)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/preppers/" + prepper_id + "/reviews");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            var body = @"";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);

        }

        public IRestResponse ApiCustomeraddresses(string access_token)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/customer/addresses");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            var body = @"";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);

        }

        public IRestResponse ApiSaveCustomeraddresses(string access_token, string address_line_1, string address_line_2, string state, string city, string zip_code)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/customer/addresses");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddHeader("Content-Type", "application/json");
            var body = @"{" + "\n" + @"    ""address_line_1"":  """ + address_line_1 + "\"," + "\n" + @"    ""address_line_2"": """ + address_line_2 + "\", " + "\n" + @"    ""state"": """ + state + "\"," + "\n" + @"    ""city"": """ + city + "\"," + "\n" + @"    ""zip_code"": """ + zip_code + "\"" + "\n" + @"}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);

        }






        public IRestResponse Apiorderscheckout(string access_token, string cardNumber, string cardYear, string cardCVV, int id, string customer_payment_profile_id, string pickupDelivery, string pickupDeliveryDt, bool paymentFlag)
        {
            
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/customer/orders/checkout");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddHeader("Content-Type", "application/json");
            if (!paymentFlag)
            {
                var body = @"{" + "\n" + @"    ""delivery_address"": {" + "\n" + @"  ""id"":" + id + "" + "\n" + @"    },         " + "\n" + @"    ""delivery_type"": """ + pickupDelivery + "\"," + "\n" +
            @"    ""payments"": {" + "\n" + @"        ""card_number"": """ + cardNumber + "\"," + "\n" + @"  ""expiration_date"": """ + cardYear + "\"," + "\n" + @"  ""card_code"": """ + cardCVV + "\"," + "\n" + @"   ""save_card"": false" + "\n" + @"    },
            " + "\n" + @"    ""delivery_slot"": """ + pickupDeliveryDt + "\" "+ "\n" + @"}";
                request.AddParameter("application/json", body, ParameterType.RequestBody);
            }
            else
            {
                var body = @"{" + "\n" + @"    ""delivery_address"": {" + "\n" + @"  ""id"":" + id + "" + "\n" + @"    },         " + "\n" + @"    ""delivery_type"": """ + pickupDelivery + "\"," + "\n" +
          @"    ""payments"": {" + "\n" + @"        ""customer_payment_profile_id"": """ + customer_payment_profile_id
          + "\"," + "\n" + @"  ""card_code"": """ + cardCVV + "\"" + "\n" + @"    },
            " + "\n" + @"    ""delivery_slot"": """ + pickupDeliveryDt + "\" "+ "\n" + @"}";
                request.AddParameter("application/json", body, ParameterType.RequestBody);
            }

            IRestResponse response = client.Execute(request);
            return (response);

        }


        /// <summary>
        /// Admin Api's
        /// </summary>
        /// <returns></returns>
        /// 
        public IRestResponse ApiGetAdminCustomers(string access_token)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/admin/customers");
            //var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/admin/customers");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);

        }

        public IRestResponse ApiGetAdminCustomersDetails(string access_token, int customer_id)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/admin/customers/" + customer_id + "/details");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);

        }

        public IRestResponse ApiGetAdminRecentOrders(string access_token, int customer_id)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/admin/customers/" + customer_id + "/details/recent-orders?page=pageNo");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);

        }

        public IRestResponse ApiGetAdminOrdersHistory(string access_token, int customer_id)
        {
            //var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/admin/customers/" + customer_id + "/details/recent-orders");
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/admin/customers/" + customer_id + "/details/orders-history");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);

        }


        /// <summary>
        /// Admin Api's
        /// </summary>
        /// <returns></returns>
        /// 
        public IRestResponse ApiGetAdminSearchCustomers(string access_token, string url)
        {

            var client = new RestClient("" + url + "");
            //var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/admin/customers");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);

        }


        public IRestResponse ApiGetAdminpreppers(string access_token)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/admin/preppers");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);

        }

        public IRestResponse ApiGetAdminPrepperSearchCustomers(string access_token, string url)
        {

            var client = new RestClient("" + url + "");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);

        }


        public IRestResponse ApiGetAdminpreppersDetails(string access_token, int customer_id)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/admin/preppers/" + customer_id + "/details");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);

        }

        public IRestResponse ApiGetAdminPreppersRecentOrders(string access_token, int customer_id)
        {
            //var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/admin/customers/" + 6 + "/details/recent-orders");
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/admin/preppers/" + customer_id + "/details/recent-orders");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);

        }

        public IRestResponse ApiGetAdminPreppersOrdersHistory(string access_token, int customer_id)
        {
            //var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/admin/customers/" + 6 + "/details/recent-orders");
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/admin/preppers/" + customer_id + "/details/orders-history");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);

        }

        public IRestResponse ApiGetAdminCustomersfilter(string access_token)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/admin/customers/filter");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            IRestResponse response = client.Execute(request);
            return (response);

        }

        public IRestResponse ApiGetAdminDashboard(string access_token)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/auth/admin/dashboard");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            IRestResponse response = client.Execute(request);
            return (response);

        }

        public IRestResponse ApiGetAdminPreppersEarnings(string access_token)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/admin/preppers/earnings?start_date=2022-01-01&end_date=2022-04-12&keyword=ss");
            //var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/admin/preppers/earnings");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            IRestResponse response = client.Execute(request);
            return (response);

        }

        public IRestResponse ApiGetAdminEarningSearch(string access_token, string url)
        {

            var client = new RestClient("" + url + "");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);

        }

        public IRestResponse ApiGetAdminprofile(string access_token)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/auth/admin/profile");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            IRestResponse response = client.Execute(request);
            return (response);

        }

        public IRestResponse ApiGetAdminNotifications(string access_token)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/admin/notifications");
            //var client = new RestClient("http://127.0.0.1:8000/");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            var body = @"";
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);
        }

        public IRestResponse ApiGetAdminNotificationscount(string access_token)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/admin/notifications/count");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            var body = @"";
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);
        }







        public List<SelectListItem> LoadState()
        {
            List<SelectListItem> statelist = new List<SelectListItem>();
            statelist.Add(new SelectListItem { Text = "Select", Value = "" });
            //var state =
            ResponseStates RS = new ResponseStates();
            var responseState = Apistatelist();
            RS = JsonConvert.DeserializeObject<ResponseStates>(responseState.Content);
            foreach (var item in RS.response)
            {
                statelist.Add(new SelectListItem { Text = item.short_code, Value = item.short_code });
            }

            return statelist;
        }

        public List<CheckBoxListItem> Loadcuisinespecialities(string token)
        {
            List<CheckBoxListItem> cuisinespecialitieslist = new List<CheckBoxListItem>();
            List<CheckBoxListItem> cuisinespecialities = new List<CheckBoxListItem>();
            //var state =
            Responsecuisinespecialities cuisine_specialities = new Responsecuisinespecialities();
            var rescuisinespecialities = ApiRescuisinespecialities(token);
            cuisine_specialities = JsonConvert.DeserializeObject<Responsecuisinespecialities>(rescuisinespecialities.Content);
            foreach (var item in cuisine_specialities.response)
            {
                cuisinespecialitieslist.Add(new CheckBoxListItem()
                {
                    ID = item.id,
                    Display = item.name,
                    IsChecked = false
                });
            }
            cuisinespecialities.Add(new CheckBoxListItem()
            {
                ID = (cuisine_specialities.response.Count) + 1,
                Display = "Other",
                IsChecked = false
            });
            cuisinespecialitieslist.AddRange(cuisinespecialities);
            return cuisinespecialitieslist;
        }

        public List<CheckBoxListItem> Loadcuisinespecialitiesedits(List<cuisine_specialities> cuisinespecialitieslistedit, string token)
        {
            List<CheckBoxListItem> cuisinespecialitieslist = new List<CheckBoxListItem>();
            List<CheckBoxListItem> cuisinespecialities = new List<CheckBoxListItem>();
            //var state =
            Responsecuisinespecialities cuisine_specialities = new Responsecuisinespecialities();
            var rescuisinespecialities = ApiRescuisinespecialities(token);
            cuisine_specialities = JsonConvert.DeserializeObject<Responsecuisinespecialities>(rescuisinespecialities.Content);
            foreach (var item in cuisine_specialities.response)
            {

                var existID = cuisinespecialitieslistedit.Where(a => a.id == item.id).FirstOrDefault();
                if (existID == null)
                {
                    CheckBoxListItem objchk1 = new CheckBoxListItem();
                    objchk1.ID = item.id;
                    objchk1.Display = item.name;

                    objchk1.IsChecked = false;
                    cuisinespecialitieslist.Add(objchk1);
                }
                else
                {
                    CheckBoxListItem objchk = new CheckBoxListItem();
                    objchk.ID = item.id;
                    objchk.Display = item.name;
                    objchk.IsChecked = true;
                    cuisinespecialitieslist.Add(objchk);
                }
            }
            cuisinespecialities.Add(new CheckBoxListItem()
            {
                ID = (cuisine_specialities.response.Count) + 1,
                Display = "Other",
                IsChecked = false
            });
            cuisinespecialitieslist.AddRange(cuisinespecialities);




            return cuisinespecialitieslist;

        }

        public List<CheckBoxListItem> Loaddietspecialities(string token)
        {
            List<CheckBoxListItem> dietspecialitiesList = new List<CheckBoxListItem>();
            List<CheckBoxListItem> dietspecialities = new List<CheckBoxListItem>();

            //var state =
            Responsedietspecialities diet_specialities = new Responsedietspecialities();
            var resdietspecialities = ApiResdietspecialities(token);
            diet_specialities = JsonConvert.DeserializeObject<Responsedietspecialities>(resdietspecialities.Content);
            foreach (var item in diet_specialities.response)
            {
                dietspecialitiesList.Add(new CheckBoxListItem()
                {
                    ID = item.id,
                    Display = item.name,
                    IsChecked = false
                });
            }
            dietspecialities.Add(new CheckBoxListItem()
            {
                ID = (diet_specialities.response.Count) + 1,
                Display = "Other",
                IsChecked = false
            });
            dietspecialitiesList.AddRange(dietspecialities);

            return dietspecialitiesList;
        }
        public List<CheckBoxListItem> Loaddietspecialitiesedit(List<diet_specialities> diet_specialitiesedit, string token)
        {
            List<CheckBoxListItem> dietspecialitiesList = new List<CheckBoxListItem>();
            List<CheckBoxListItem> dietspecialities = new List<CheckBoxListItem>();

            //var state =
            Responsedietspecialities diet_specialities = new Responsedietspecialities();
            var resdietspecialities = ApiResdietspecialities(token);
            diet_specialities = JsonConvert.DeserializeObject<Responsedietspecialities>(resdietspecialities.Content);
            foreach (var item in diet_specialities.response)
            {
                var existID = diet_specialitiesedit.Where(a => a.id == item.id).FirstOrDefault();

                if (existID != null)
                {
                    dietspecialitiesList.Add(new CheckBoxListItem()
                    {
                        ID = item.id,
                        Display = item.name,
                        IsChecked = true
                    });
                }
                else
                {
                    dietspecialitiesList.Add(new CheckBoxListItem()
                    {
                        ID = item.id,
                        Display = item.name,
                        IsChecked = false
                    });
                }

            }
            dietspecialities.Add(new CheckBoxListItem()
            {
                ID = (diet_specialities.response.Count) + 1,
                Display = "Other",
                IsChecked = false
            });
            dietspecialitiesList.AddRange(dietspecialities);


            return dietspecialitiesList;
        }

        public List<AddOn> GetDefaultAddON()
        {
            List<AddOn> lstaddon = new List<AddOn>();
            AddOn objAddOn = new AddOn();
            objAddOn.add_on_name = "";
            objAddOn.add_on_price = "";
            lstaddon.Add(objAddOn);
            return lstaddon;
        }

        public List<AddIngredient> GetDefaultIngredient()
        {
            List<AddIngredient> lstAddIngredient = new List<AddIngredient>();
            AddIngredient objIngredient = new AddIngredient();
            objIngredient.IngredientName = "";
            lstAddIngredient.Add(objIngredient);
            return lstAddIngredient;
        }

        public List<AddOn> AddDefaultAddON(List<AddOn> adds)
        {
            AddOn objAddOn = new AddOn();
            objAddOn.add_on_name = "";
            objAddOn.add_on_price = "";
            adds.Add(objAddOn);
            return adds;
        }

        public List<AddIngredient> AddDefaultIngredient(List<AddIngredient> AddIngredients)
        {
            AddIngredient objAddIngredient = new AddIngredient();
            objAddIngredient.IngredientName = "";
            AddIngredients.Add(objAddIngredient);
            return AddIngredients;
        }

        public List<string> ConvertStringList(List<AddIngredient> lstaddingredients)
        {
            List<string> Ingredient = new List<string>();
            foreach (var item in lstaddingredients)
            {
                Ingredient.Add(item.IngredientName);
            }
            return Ingredient;
        }

        public string RemoveSpecialChars(string str)
        {
            // Create  a string array and add the special characters you want to remove
            string[] chars = new string[] { ",", ".", "/", "!", "@", "#", "$", "%", "-", "&", "*", "'", "\"", ";", "_", "(", ")", ":", "|", "[", "]" };
            //Iterate the number of times based on the String array length.
            for (int i = 0; i < chars.Length; i++)
            {
                if (str.Contains(chars[i]))
                {
                    str = str.Replace(chars[i], "");
                }
            }
            return str;
        }







    }
}