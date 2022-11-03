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
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace LocalPrep.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;      

        General g = new General();

        public AccountController()
        {
            
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        #region API's Call
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// API's Call by shat
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////




        #region API register check
        public IRestResponse ApiRegisterCheck(RegisterViewModel model)
        {
            //string phone = model.PhoneNumber;
            //General g = new General();

            string ReturnedString = g.RemoveSpecialChars(model.PhoneNumber);
            string Phone = ReturnedString.Replace(" ", "");
            var imgresponse = UploadImageToServer(model);
            var imgresponseDL = UploadImageToServerDL(model);
            var apiimgresponse = JsonConvert.DeserializeObject<ResponseImageUpload>(imgresponse.Content);
            var apiimgresponseDL = JsonConvert.DeserializeObject<ResponseImageUpload>(imgresponseDL.Content);
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/auth/prepper/registration/check");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\r\n    \"user\": {\r\n        \"first_name\": \"" + model.FirstName + "\",\r\n        \"last_name\": \"" + model.LastName + "\",\r\n        \"email\": \"" + model.Email + "\",\r\n        \"phone\": \"" + Phone + "\",\r\n        \"password\": \"" + model.Password + "\"\r\n    },\r\n    \"account\": {\r\n        \"photo\": \"" + apiimgresponse.response.file_name + "\",\r\n        \"state_id_or_drivers_license\": \"" + model.DriversLicense + "\",\r\n        \"state_id_or_drivers_license_image\": \"" + apiimgresponseDL.response.file_name + "\",\r\n        \"social_security_number\": \"" + model.SocialSecurityNumber + "\"\r\n    },\r\n    \"address\": {\r\n        \"address_line_1\": \"" + model.Address1 + "\",\r\n        \"address_line_2\": \"" + model.Address2 + "\",\r\n        \"state\": \"" + model.short_code + "\",\r\n        \"city\": \"" + model.City + "\",\r\n        \"zip_code\": \"" + model.Zip + "\"\r\n    }\r\n}", ParameterType.RequestBody);
            //request.AddParameter("application/json", "{\r\n  \"user\": {\r\n    \"first_name\": \"" + model.FirstName + "\",\r\n    \"last_name\": \"" + model.LastName + "\",\r\n    \"email\": \"" + model.Email + "\",\r\n    \"phone\": \"" + Phone + "\",\r\n    \"photo\": \""+ apiimgresponse.response.file_name+ "\",\r\n    \"password\": \"" + model.Password + "\"\r\n  },\r\n  \"address\": {\r\n    \"address_line_1\": \"" + model.Address1 + "\",\r\n    \"address_line_2\": \"" + model.Address2 + "\",\r\n    \"state\": \""+model.short_code+"\",\r\n    \"city\": \"" + model.City + "\",\r\n    \"zip_code\": \"" + model.Zip + "\"\r\n  }\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);

        }

        public IRestResponse ApiRegister(RegisterViewModel model)
        {
            string ProfilePic = null;
            string ReturnedString = g.RemoveSpecialChars(model.PhoneNumber);
            string Phone = ReturnedString.Replace(" ", "");
            var imgresponse = UploadImageToServer(model);            
            var apiimgresponse = JsonConvert.DeserializeObject<ResponseImageUpload>(imgresponse.Content);
            if (apiimgresponse.response != null)
                ProfilePic = apiimgresponse.response.file_name;
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/auth/customer/registration/check");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\r\n    \"user\": {\r\n        \"first_name\": \"" + model.FirstName + "\",\r\n        \"last_name\": \"" + model.LastName + "\",\r\n        \"email\": \"" + model.Email + "\",\r\n        \"phone\": \"" + Phone + "\",\r\n        \"password\": \"" + model.Password + "\"\r\n    },\r\n    \"account\": {\r\n        \"photo\": \"" + ProfilePic + "\"\r\n    },\r\n    \"address\": {\r\n        \"address_line_1\": \"" + model.Address1 + "\",\r\n        \"address_line_2\": \"" + model.Address2 + "\",\r\n        \"state\": \"" + model.short_code + "\",\r\n        \"city\": \"" + model.City + "\",\r\n        \"zip_code\": \"" + model.Zip + "\"\r\n    }\r\n}", ParameterType.RequestBody);
            //request.AddParameter("application/json", "{\r\n  \"user\": {\r\n    \"first_name\": \"" + model.FirstName + "\",\r\n    \"last_name\": \"" + model.LastName + "\",\r\n    \"email\": \"" + model.Email + "\",\r\n    \"phone\": \"" + Phone + "\",\r\n    \"photo\": \""+ apiimgresponse.response.file_name+ "\",\r\n    \"password\": \"" + model.Password + "\"\r\n  },\r\n  \"address\": {\r\n    \"address_line_1\": \"" + model.Address1 + "\",\r\n    \"address_line_2\": \"" + model.Address2 + "\",\r\n    \"state\": \""+model.short_code+"\",\r\n    \"city\": \"" + model.City + "\",\r\n    \"zip_code\": \"" + model.Zip + "\"\r\n  }\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);

        }


       

        #endregion

        #region API states List
        //public IRestResponse Apistatelist(RegisterViewModel model)
        //{
        //    var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/state");
        //    var request = new RestRequest(Method.GET);           
        //    request.AddHeader("cache-control", "no-cache");
        //    // request.AddHeader("Auth-Token", "AwGQIZdk8vQYO9cWLpCMQWExmxsHTEMOHkCougRryZCq%2BrEaGdLoyS1HDinp5Sd4Fc6iWQRwRoDxuQi8JsxFH6zHfJ9aLiMWB7js9qcug7yYr7Cy661gBzkqyy5JCZuCYBRaPW%2FMoLOHks811UjSlGU2LCz2wXePlbiTxe7HauPgdedr39Hn%2BkmsPPoI%2F13OvjI3%2FxxYzTBnxHwx3rqt5m17BjjmjksBySSyyMvtdDDrgxANXQtKp9nZIJgjXJcBmhiAgZDT6vt1AsURxqtjYdoJztllleBfXBGjzCa6uaEZfQ%3D%3D");
        //    request.AddHeader("Content-Type", "application/json");
        //    var response = client.Execute(request);
        //    return (response);

        //}

        #endregion

        #region API upload image
        public IRestResponse UploadImageToServer(RegisterViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}
            string fileName = string.Empty;
            string imgUrl = string.Empty;
            if (model.ImageFile != null)
            {
                var validImageTypes = new string[]
                {
                        "image/gif",
                        "image/jpeg",
                        "image/pjpeg",
                        "image/png"
                };

                if (!validImageTypes.Contains(model.ImageFile.ContentType))
                {
                    ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
                }

                //imgUrl = ImageRepo.UploadImage(viewModel.ImageFile, "Meals");

                fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                string extension = Path.GetExtension(model.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;




                string fileNamePath = Path.Combine(Server.MapPath("~/img/"), fileName);
                model.ImageFile.SaveAs(fileNamePath);

            }
            

            

            string path = Server.MapPath("~/img/") + fileName;
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/file/upload");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            if (model.ImageFile != null)
                request.AddFile("image", @"" + path + "");
            IRestResponse response = client.Execute(request);
            return response;

        }
        public IRestResponse UploadImageToServerDL(RegisterViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}
            string fileName = string.Empty;
            string imgUrl = string.Empty;
            if (model.ImageFileDriversLicense != null)
            {
                var validImageTypes = new string[]
                {
                        "image/gif",
                        "image/jpeg",
                        "image/pjpeg",
                        "image/png"
                };

                if (!validImageTypes.Contains(model.ImageFileDriversLicense.ContentType))
                {
                    ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
                }

                //imgUrl = ImageRepo.UploadImage(viewModel.ImageFile, "Meals");

                fileName = Path.GetFileNameWithoutExtension(model.ImageFileDriversLicense.FileName);
                string extension = Path.GetExtension(model.ImageFileDriversLicense.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                string fileNamePath = Path.Combine(Server.MapPath("~/img/"), fileName);
                model.ImageFileDriversLicense.SaveAs(fileNamePath);

            }

            string path = Server.MapPath("~/img/") + fileName;
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/file/upload");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddFile("image", @"" + path + "");
            IRestResponse response = client.Execute(request);
            return response;

        }


        #endregion

       



       

        #endregion




        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            LoginViewModel model = new LoginViewModel();
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/auth/login");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\r\n    \"user_name\": \"" + model.UserName + "\",\r\n    \"password\": \"" + model.Password + "\"\r\n}", ParameterType.RequestBody);
            var response = client.Execute(request);
            ResponseLogin RL = new ResponseLogin();
            RL = JsonConvert.DeserializeObject<ResponseLogin>(response.Content);
            if (!RL.error)
            {
                g.ApiGetOtp(RL.response.user.email);
                Session["email"] = RL.response.user.email;
                Session["IsLogin"] = RL.error;
                if((RL.response.user.subscriptions)!=null)
                   Session["subscriptions"] = RL.response.user.subscriptions.Count;
                //role = RL.response.user.account.role;
                Session["role"] = RL.response.user.account.role;
               
                                
                 Session["Fullname"] = RL.response.user.first_name + " " + RL.response.user.last_name;
                Session["Photo"] = RL.response.user.account.photo;
                Session["access_token"] = RL.response.token_info.access_token;
                return RedirectToAction("VerifyCode", "Account");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }
        }





        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public ActionResult VerifyCode()
        {
            Session["HideMenu"] = true;
            bool IsLogin = Convert.ToBoolean(Session["IsLogin"]);
            // Require that the user has already logged in via username/password or external login
            if (IsLogin)
            {
                return View("Error");
            }
            return View();
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult VerifyCode(VerifyCodeViewModel model)         
        {
            string role=string.Empty;
            string accesstoken = string.Empty;
            ResponseVerifycode rvc = new ResponseVerifycode();
            ResponseLogin RL = new ResponseLogin();
            
            string forgotpassword = "true";
            if(Session["role"]!=null)
                role = Session["role"].ToString();
            if (Session["access_token"] != null)
                accesstoken = Session["access_token"].ToString();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (Session["registration_data"] != null)
            {
                string registration_data = Session["registration_data"].ToString();
                var responseRegisterfinal = g.ApiRegisterfinalstep(registration_data, model.Code);
                RL = JsonConvert.DeserializeObject<ResponseLogin>(responseRegisterfinal.Content);
                if (!RL.error)
                {
                    Session["access_token"] = RL.response.token_info.access_token;                    
                    Session["IsLogin"] = false;
                    Session["HideMenu"] = false;
                    Session["role"] = RL.response.user.account.role;
                    Session["Photo"] = RL.response.user.account.photo;
                    Session["Fullname"]= RL.response.user.first_name + " " + RL.response.user.last_name;
                    return RedirectToAction("Index", "Home");
                }
            }


                string email = Session["email"].ToString();
            if (Session["forgotpassword"] != null)
                forgotpassword = Session["forgotpassword"].ToString();

            var response = g.ApiVerifyOtp(email, model.Code);
            
            rvc = JsonConvert.DeserializeObject<ResponseVerifycode>(response.Content);
            if (!rvc.error)
            {
                if (forgotpassword == "False")
                {
                    Session.Remove("forgotpassword");
                    return RedirectToAction("CreatePassword", "Account");

                }
                int subscription = Convert.ToInt32(Session["subscriptions"]);
                Session["HideMenu"] = false;
                if (subscription > 0)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                if(role == "CUSTOMER")
                       
                    return RedirectToAction("Index", "Home");
                    Session["HideMenu"] = true;
                    return RedirectToAction("Plan", "Vendor");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid code.");
                return View(model);
            }

            //return View(model);
        }

        [AllowAnonymous]
        public ActionResult BecomePrepper()
        {
            if (Session["access_token"] != null)
            {
                Session["IsLogin"] = true;
                Session.Abandon();
                Session.Clear();
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                RegisterViewModel vm = new RegisterViewModel();
                General g = new General();
                ViewData["State"] = g.LoadState();
                return View();
                //return RedirectToAction("BecomePrepper", "Account");               
            }
            else
            {
                RegisterViewModel vm = new RegisterViewModel();
                General g = new General();
                ViewData["State"] = g.LoadState();
                return View();
            }



            
        }

        [AllowAnonymous]
        public JsonResult GetLocalData()
        {
            string role = string.Empty;

            if (Session["role"] != null)
                role = Session["role"].ToString();

            ResponseLocaldata RLD = new ResponseLocaldata();
            if (Session["access_token"] != null)
            {
                if (role == "CUSTOMER")
                {
                    string accesstoken = Session["access_token"].ToString();
                    var responseToken = g.Apilocaldata(accesstoken);
                    RLD = JsonConvert.DeserializeObject<ResponseLocaldata>(responseToken.Content);
                    return Json(RLD);
                }
                    
            }
            return Json(RLD);
        }

        [AllowAnonymous]
        public JsonResult Getdashboard()
        {

            string role = string.Empty;
           
            if (Session["role"] != null)
                role = Session["role"].ToString();

            ResponseSearch RS = new ResponseSearch();
            if (Session["access_token"] != null)
            {
                if (role == "CUSTOMER")
                {
                    string accesstoken = Session["access_token"].ToString();
                    var responseToken = g.Apidashboard(accesstoken);
                    RS = JsonConvert.DeserializeObject<ResponseSearch>(responseToken.Content);
                    return Json(RS.response.best_dishes.data, JsonRequestBehavior.AllowGet);
                }
                    
               
            }
           

            return Json(RS, JsonRequestBehavior.AllowGet);


            
        }



        




        [AllowAnonymous]
        public ActionResult PurchasePlan()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Earnings()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Earnings(BankAccount model)
        {
            General g = new General();
            ResRegFirst rrf = new ResRegFirst();
            string Token = Session["access_token"].ToString();
            if (ModelState.IsValid)
            {
                var ResponseAddBankAccount = g.ApiAddBankAccount(model, Token);
                rrf = JsonConvert.DeserializeObject<ResRegFirst>(ResponseAddBankAccount.Content);
                if (!rrf.error)
                {
                    ModelState.Clear();
                    ViewBag.SuccessMessage = rrf.message;
                    return View();

                }

            }

            AddErrors(rrf.message);
            return View();
        }






        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            if(Session["access_token"] != null)
            {
                Session["IsLogin"] = true;
                Session.Abandon();
                Session.Clear();
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                RegisterViewModel vm = new RegisterViewModel();
                General g = new General();
                //vm.States = RS.response;
                ViewData["State"] = g.LoadState();
                return View(vm);

                //return RedirectToAction("Login", "Account");
            }
            else
            {
                RegisterViewModel vm = new RegisterViewModel();
                General g = new General();

                //vm.States = RS.response;
                ViewData["State"] = g.LoadState();
                return View(vm);
            }
            
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            General g = new General();
            ResponseStates RS = new ResponseStates();
            var responseState = g.Apistatelist();
            if (ModelState.IsValid)
            {
                RegistCheck RC = new RegistCheck();
                var responseRegister = ApiRegister(model);
                var apiresponseRegister = JsonConvert.DeserializeObject<ResponseRegistCheck>(responseRegister.Content);

                RS = JsonConvert.DeserializeObject<ResponseStates>(responseState.Content);

                model.States = RS.response;
                if (!apiresponseRegister.error)
                {
                    RC.registration_data = apiresponseRegister.response.registration_data;
                    Session["registration_data"] = RC.registration_data;

                    //Session["role"] = RS.response.FirstOrDefault()..account.role;
                    //return RedirectToAction("SignUp", "Vendor", new { RC.registration_data });
                    return RedirectToAction("VerifyCode", "Account");
                }

                AddErrors(apiresponseRegister.message);
            }
            //responseState = g.Apistatelist();
            //RS = JsonConvert.DeserializeObject<ResponseStates>(responseState.Content);
            ViewData["State"] = g.LoadState();
            return View(model);
        }


        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult BecomePrepper(RegisterViewModel model)
        {
            General g = new General();
            ResponseStates RS = new ResponseStates();
            var responseState = g.Apistatelist();
            if (ModelState.IsValid)
            {
                RegistCheck RC = new RegistCheck();
                var responseRegister = ApiRegisterCheck(model);
                var apiresponseRegister = JsonConvert.DeserializeObject<ResponseRegistCheck>(responseRegister.Content);

                RS = JsonConvert.DeserializeObject<ResponseStates>(responseState.Content);

                model.States = RS.response;
                if (!apiresponseRegister.error)
                {
                    RC.registration_data = apiresponseRegister.response.registration_data;
                    return RedirectToAction("SignUp", "Vendor", new { RC.registration_data });

                }

                AddErrors(apiresponseRegister.message);
            }
            //responseState = g.Apistatelist();
            //RS = JsonConvert.DeserializeObject<ResponseStates>(responseState.Content);
            ViewData["State"] = g.LoadState();
            return View(model);
        }





        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterCheck(RegisterViewModel model)
        {
            ResponseRegistCheck RRC = new ResponseRegistCheck();
            if (ModelState.IsValid)
            {
                var response = ApiRegisterCheck(model);
                RRC = JsonConvert.DeserializeObject<ResponseRegistCheck>(response.Content);
                //if (result.Succeeded)
                //{


                //    return RedirectToAction("Index", "Home");
                //}

                //AddErrors(result);
            }
            return View(model);
        }


        


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterFianl(RegisterViewModel model)
        {

            //string imagepath=UploadImageToServer(model);
            //var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/file/upload");
            //client.Timeout = -1;
            //var request = new RestRequest(Method.POST);
            //request.AddFile("image", @"C:\Users\shat\Pictures\org_club_account.png");           
            //IRestResponse response = client.Execute(request);
            //var apiResponse = JsonConvert.DeserializeObject<ResponseImageUpload>(response.Content);

            return View();
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            ResponseRegistCheck RRC = new ResponseRegistCheck();

            if (ModelState.IsValid)
            {
                var response = g.ApiForgetPasswordCheck(model.Email);
                RRC = JsonConvert.DeserializeObject<ResponseRegistCheck>(response.Content);
                //var user = await UserManager.FindByEmailAsync(model.Email);
                if (RRC.error == true) // || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    AddErrors(RRC.message);
                    // Don't reveal that the user does not exist or is not confirmed
                    return View(model);
                }
                Session["forgotpassword"] = RRC.error;
                Session["email"] = model.Email;
                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                //string code = await UserManager.GeneratePasswordResetTokenAsync("10");
                //var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = "10", code = code }, protocol: Request.Url.Scheme);
                //await UserManager.SendEmailAsync("10", "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                //return RedirectToAction("ForgotPasswordConfirmation", "Account");
                return RedirectToAction("VerifyCode", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        ////[AllowAnonymous]
        ////public ActionResult ResetPassword(string code)
        ////{
        ////    return code == null ? View("Error") : View();
        ////}

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword()
        {
            return View();
        }

        


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            ResponseRegistCheck RRC = new ResponseRegistCheck();
            model.Code = "1234";

            string Token = Session["access_token"].ToString();
            var response = g.ApiResetPassword(model, Token);
            RRC = JsonConvert.DeserializeObject<ResponseRegistCheck>(response.Content);
            if (!RRC.error)
            {
                // Don't reveal that the user does not exist or is not confirmed
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }

            AddErrors(RRC.message);
            return View();

        }



        [AllowAnonymous]
        public ActionResult CreatePassword()
        {
            return View();
        }



        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePassword(ResetPasswordViewModel model)
        {
            ResponseRegistCheck RRC = new ResponseRegistCheck();
            model.Email = Session["email"].ToString();
            model.Code = "1234";
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var response = g.ApiCreateNewPassword(model);
            RRC = JsonConvert.DeserializeObject<ResponseRegistCheck>(response.Content);
            //var user = await UserManager.FindByEmailAsync(model.Email);
            if (!RRC.error) // || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
            {
                ViewBag.SuccessMessage = RRC.message;
                return View(model);
                //return RedirectToAction("Login", "Account");
            }



            ViewBag.FailureMessage = RRC.message;
            //AddErrors(RRC.message);            
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff

        [AllowAnonymous]
        public ActionResult LogOff()
        {
            Session["IsLogin"] = true;
            Session.Abandon();
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public void AddErrors(string result)
        {

            ModelState.AddModelError("", result);

        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
            //return RedirectToAction("Index", "Vendor");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}