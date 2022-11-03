using LocalPrep.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static LocalPrep.Web.Models.Utilities;

namespace LocalPrep.Web.Controllers
{
    public class VendorController : Controller
    {
        General g = new General();
        // GET: Vendor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PurchasePlan()
        {
            return View();
        }
        public ActionResult CardInfo()
        {
            ResponseCardInfo RCI = new ResponseCardInfo();
                if (Session["access_token"] != null)
                {                    
                        string accesstoken = Session["access_token"].ToString();
                        var responseCard = g.ApiGetcustomercards(accesstoken);
                        RCI = JsonConvert.DeserializeObject<ResponseCardInfo>(responseCard.Content);

                if (!RCI.error)
                {
                    return View(RCI.response);
                }
                //AddErrors(RCI.message);
                //return View(RCI.response);                                    
                }
            return View(RCI.response);   
        }


        //
        // GET: /Account/Notifications
        [AllowAnonymous]
        public ActionResult Notifications()
        {
            //NotificationsCount();
            if (Session["access_token"] != null)
            {
                string Role = string.Empty;
                if (Session["role"] != null)
                    Role = Session["role"].ToString();
                string Token = Session["access_token"].ToString();

                ResponseNotifications RN = new ResponseNotifications();
                if (Role != "CUSTOMER")
                {
                    var responseNotifications = g.ApiGetNotifications(Token);
                    RN = JsonConvert.DeserializeObject<ResponseNotifications>(responseNotifications.Content);

                    if (!RN.error)
                    {
                        return View(RN);

                    }

                    AddErrors(RN.message);
                    return View(RN);

                }
                else
                {   
                    var responseNotifications = g.ApiGetNotificationscustomer(Token);
                    RN = JsonConvert.DeserializeObject<ResponseNotifications>(responseNotifications.Content);

                    if (!RN.error)
                    {
                        return View(RN);

                    }

                    AddErrors(RN.message);
                    return View(RN);
                }
               
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }




        }

        [AllowAnonymous]
        public ActionResult NotificationsCount()
        {

            if (Session["access_token"] != null)
            {
                string Role = string.Empty;
                if (Session["role"] != null)
                    Role = Session["role"].ToString();
                string Token = Session["access_token"].ToString();

                ResponseNotificationsCount RNC = new ResponseNotificationsCount();
                if (Role != "CUSTOMER")
                {
                    var responseNotifications = g.ApiGetNotificationsPreppercount(Token);
                    RNC = JsonConvert.DeserializeObject<ResponseNotificationsCount>(responseNotifications.Content);

                    if (!RNC.error)
                    {
                        return View(RNC.response.count);

                    }

                    AddErrors(RNC.message);
                    return View(RNC.response.count);

                }
                else
                {
                    var responseNotifications = g.ApiGetNotificationscustomer(Token);
                    RNC = JsonConvert.DeserializeObject<ResponseNotificationsCount>(responseNotifications.Content);

                    if (!RNC.error)
                    {
                        return View(RNC.response.count);

                    }

                    AddErrors(RNC.message);
                    return View(RNC.response.count);
                }

            }
            else
            {
                return RedirectToAction("Login", "Account");

            }




        }



        [HttpPost]
        public ActionResult PurchasePlan(PurchasePlan model,string Token)
        {
            if(Session["access_token"]!=null)
            {
                ResponsePurchasePlan rpp = new ResponsePurchasePlan();
                Token = Session["access_token"].ToString();
                var Response = g.ApiPurchasePlan(model, Token);
                rpp = JsonConvert.DeserializeObject<ResponsePurchasePlan>(Response.Content);
                if (!rpp.error)
                {
                    Session["HideMenu"] = false;
                    Session["IsLogin"] = false;
                    return RedirectToAction("Meals", "Manage");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            

        
        }
        public ActionResult Plan()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Banks()
        {
            string Token = Session["access_token"].ToString();
            ResponseBankAccount rba = new ResponseBankAccount();
            var ResponseAddBankAccount = g.ApiGetBankAccount(Token);
            rba = JsonConvert.DeserializeObject<ResponseBankAccount>(ResponseAddBankAccount.Content);
            if (Session["access_token"] != null)
            {
                if (!rba.error)
                {
                    return View(rba.response.bankProfile);
                    //return RedirectToAction("Plan", "Vendor");
                }
                AddErrors(rba.message);
                return View(rba);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }          
            
        }

        [AllowAnonymous]
        public ActionResult EditBanks()
        {
            string Token = Session["access_token"].ToString();
            ResponseBankAccount rba = new ResponseBankAccount();
            var ResponseAddBankAccount = g.ApiGetBankAccount(Token);
            rba = JsonConvert.DeserializeObject<ResponseBankAccount>(ResponseAddBankAccount.Content);
            if (Session["access_token"] != null)
            {
                if (!rba.error)
                {
                    rba.response.bankProfile.FirstOrDefault().bank_account_number = rba.response.bankProfile.FirstOrDefault().account_number;
                    rba.response.bankProfile.FirstOrDefault().re_type_bank_account_number = rba.response.bankProfile.FirstOrDefault().account_number;
                    return View(rba.response.bankProfile.FirstOrDefault());
                    //return RedirectToAction("Plan", "Vendor");
                }
                AddErrors(rba.message);
                return View(rba);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }           
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult EditBanks(BankAccount model)
        {           
            
            General g = new General();
            string Token = Session["access_token"].ToString();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                ResRegFirst rrf = new ResRegFirst();
                var ResponseAddBankAccount = g.ApiUpdateBankAccount(model, Token);
                rrf = JsonConvert.DeserializeObject<ResRegFirst>(ResponseAddBankAccount.Content);
                if (!rrf.error)
                {
                    ViewBag.SuccessMessage = rrf.message;
                    return View();
                    //return RedirectToAction("Plan", "Vendor");
                }
                ViewBag.FailureMessage = rrf.message;
                //AddErrors(rrf.message);
                return View();
            }
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult Banks(BankAccount model)
        //{
        //    General g = new General();
        //    ResRegFirst rrf = new ResRegFirst();
        //    string Token = Session["access_token"].ToString();
        //    if (ModelState.IsValid)
        //    {                
        //        var ResponseAddBankAccount = g.ApiAddBankAccount(model, Token);
        //        rrf = JsonConvert.DeserializeObject<ResRegFirst>(ResponseAddBankAccount.Content);
        //        if (!rrf.error)
        //        {
        //            return View();
                    
        //        }

        //    }
            
        //     AddErrors(rrf.message);
        //    return RedirectToAction("Earnings", "Account");

        //}


        [AllowAnonymous]
        public ActionResult DeleteBankAccount(string customerpaymentprofileid)
        {
            General g = new General();
            string Token = Session["access_token"].ToString();
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                ResponseBankAccount rba = new ResponseBankAccount();
                var ResponseAddBankAccount = g.ApiDeleteBankAccount(customerpaymentprofileid, Token);
                rba = JsonConvert.DeserializeObject<ResponseBankAccount>(ResponseAddBankAccount.Content);
                if (!rba.error)
                {
                    //return View();
                    return RedirectToAction("Banks", "Vendor");
                }
                AddErrors(rba.message);
                return View();
            }
        }


        //[HttpPost]
        //[AllowAnonymous]
        //public ActionResult EditBankAccount(BankAccount model)
        //{
        //    General g = new General();
        //    string Token = Session["access_token"].ToString();
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    else
        //    {
        //        ResRegFirst rrf = new ResRegFirst();
        //        var ResponseAddBankAccount = g.ApiAddBankAccount(model, Token);
        //        rrf = JsonConvert.DeserializeObject<ResRegFirst>(ResponseAddBankAccount.Content);
        //        if (!rrf.error)
        //        {
        //            return View();
        //            //return RedirectToAction("Plan", "Vendor");
        //        }
        //        AddErrors(rrf.message);
        //        return View();
        //    }
        //}


        public ActionResult SignUp(RegistCheck RC)
        {

            var ResRegFirstResponse = ApiRegisterFirst(RC);
            ResRegFirst rrf = new ResRegFirst();
            rrf = JsonConvert.DeserializeObject<ResRegFirst>(ResRegFirstResponse.Content);
            Session["access_token"] = rrf.response.token_info.access_token;
            //Vendor vendor = new Vendor();
            Vendor model = new Vendor();            
            ViewData["State"] = g.LoadState();
            model.Cuisines = g.Loadcuisinespecialities(rrf.response.token_info.access_token);

            model.Diets = g.Loaddietspecialities(rrf.response.token_info.access_token);
            return View(model);
            
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SignUp(Vendor model)
        {
            General g = new General();           
            string Token = Session["access_token"].ToString();
            if (!ModelState.IsValid)
            {                
                ViewData["State"] = g.LoadState();
                model.Cuisines = g.Loadcuisinespecialities(Token);
                model.Diets = g.Loaddietspecialities(Token);
                return View(model);                
            }
            else
            {
                ResRegFirst rrf = new ResRegFirst();
                var responseRegisterFinal = ApiRegisterFianl(model, Token);
                rrf = JsonConvert.DeserializeObject<ResRegFirst>(responseRegisterFinal.Content);
                    if (!rrf.error)
                    {
                    Session["Isregistered"] = false;
                    Session["HideMenu"] = false;
                    Session["IsLogin"] = false;
                    Session["Fullname"] = rrf.response.user.first_name + " " + rrf.response.user.last_name;
                    Session["role"] = rrf.response.user.account.role;
                    return RedirectToAction("Plan", "Vendor");
                    }
                AddErrors(rrf.message);

            }

            ViewData["State"] = g.LoadState();
            model.Cuisines = g.Loadcuisinespecialities(Token);
            model.Diets = g.Loaddietspecialities(Token);
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Profile()
        {
                    
            if (Session["access_token"] != null)
            {
                string Role = string.Empty;
                if (Session["role"] != null)
                    Role = Session["role"].ToString();
                string Token = Session["access_token"].ToString();
                ResponseStates rs = new ResponseStates();
                General g = new General();
                var responseState = g.Apistatelist();
                rs = JsonConvert.DeserializeObject<ResponseStates>(responseState.Content);
                ResponseProfile rp = new ResponseProfile();
                if (Role != "CUSTOMER")
                {
                    var responseProfile = Apiprofile(Token);
                    rp = JsonConvert.DeserializeObject<ResponseProfile>(responseProfile.Content);

                }
                else
                {
                    var responseProfile = g.ApiCustomerProfile(Token);
                    rp = JsonConvert.DeserializeObject<ResponseProfile>(responseProfile.Content);
                }
                return View(rp);
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
            
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult EditProfile()
        {
            
            if (Session["access_token"] != null)
            {
                string Role = string.Empty;
                if (Session["role"] != null)
                    Role = Session["role"].ToString();

                string Token = Session["access_token"].ToString();
                ResponseStates rs = new ResponseStates();
                General g = new General();
                var responseState = g.Apistatelist();
                rs = JsonConvert.DeserializeObject<ResponseStates>(responseState.Content);

                ResponseProfile rp = new ResponseProfile();
                if (Role != "CUSTOMER")
                {
                    var responseProfile = Apiprofile(Token);
                    rp = JsonConvert.DeserializeObject<ResponseProfile>(responseProfile.Content);
                    VendorProfileViewModel model = new VendorProfileViewModel();
                }
                else
                {
                    var responseProfile = Apiprofilecustomer(Token);
                    rp = JsonConvert.DeserializeObject<ResponseProfile>(responseProfile.Content);
                    VendorProfileViewModel model = new VendorProfileViewModel();
                }
                return View(rp);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult EditProfile(ResponseProfile viewmodel)
        {          

            if (Session["access_token"] != null)
            {
                string Role = string.Empty;
                if (Session["role"] != null)
                    Role = Session["role"].ToString();

                string Token = Session["access_token"].ToString();
                ResponseStates rs = new ResponseStates();
                General g = new General();
                var responseState = g.Apistatelist();
                rs = JsonConvert.DeserializeObject<ResponseStates>(responseState.Content);
                ResponseProfile rp = new ResponseProfile();
                if (Role != "CUSTOMER")
                {
                    var responseProfile = ApiprofileUpdate(Token, viewmodel);
                    rp = JsonConvert.DeserializeObject<ResponseProfile>(responseProfile.Content);
                    VendorProfileViewModel model = new VendorProfileViewModel();
                    ViewBag.SuccessMessage = rp.message;
                }
                else
                {
                    var responseProfile = ApiprofileCustUpdate(Token, viewmodel);
                    rp = JsonConvert.DeserializeObject<ResponseProfile>(responseProfile.Content);
                    VendorProfileViewModel model = new VendorProfileViewModel();
                    ViewBag.SuccessMessage = rp.message;
                }
                return View(rp);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }

        public ActionResult LeaveReview(int? mealId)
        {
            //if (vendorId == null)
            //    return RedirectToAction("Index", "Home");

            using (var context = new localprepdbEntities())
            {
                ReviewsViewModel model = new ReviewsViewModel();
                model.vendor = new Vendor();

                //model.vendor = context.Vendors.Where(x => x.VendorId == vendorId).SingleOrDefault();
                model.review = new MealRating();
                //model.meals = context.Meals.Where(x => x.VendorId == vendorId).ToList();

                return View(model);
            }
        }

        [HttpPost]
        public ActionResult LeaveReview(ReviewsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var context = new localprepdbEntities())
            {
                MealRating rating = new MealRating();
                rating.IsActive = true;
                rating.MealId = model.review.MealId;
                rating.RatingComments = model.review.RatingComments;
                rating.StarRating = model.review.StarRating;
                rating.UserId = User.Identity.GetUserId();
                rating.CreateDt = DateTime.Now;

                context.MealRatings.Add(rating);
                context.SaveChanges();

                return RedirectToAction("Profile", "Vendor", new { vendorId = model.vendor.VendorId });
            }
        }

        public ActionResult SendMessage()
        {
            //int vendorIdInt = vendorId ?? 0;

            //if (vendorIdInt == 0)
            //    RedirectToAction("Index", "Home");

            //using (var context = new localprepdbEntities())
            //{
            //    VendorProfileViewModel model = new VendorProfileViewModel();

            //    model.Vendor = context.Vendors.Include("Meals").Where(x => x.VendorId == vendorIdInt).FirstOrDefault();
            //    model.Meals = context.Meals.Where(x => x.VendorId == vendorIdInt).ToList();
            //    model.TotalMealRatings = context.MealRatings.Include("Meals").Where(x => x.Meal.VendorId == vendorIdInt).Count();
            //    model.Cuisines = context.Meals.Include("Cuisines").Where(x => x.VendorId == vendorIdInt).Select(x => x.Cuisine.CuisineName).ToList().Distinct();

            //    if (model.TotalMealRatings == 0)
            //    {
            //        model.MealRating = 0;
            //    }
            //    else
            //    {
            //        model.MealRating = context.MealRatings.Include("Meals").Where(x => x.Meal.VendorId == vendorIdInt).Select(x => x.StarRating).Average();
            //    }

            //    return View(model);
            //}
            return View();
        }

        [HttpPost]
        [AllowAnonymous]        
        public ActionResult SendMessage(HelpRequest model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            ResponseRegistCheck RRC = new ResponseRegistCheck();
            

            string Token = Session["access_token"].ToString();
            var response = g.ApiHelpRequest(model, Token);
            RRC = JsonConvert.DeserializeObject<ResponseRegistCheck>(response.Content);
            if (!RRC.error)
            {
                ViewBag.SuccessMessage = RRC.message;
                ModelState.Clear();
                // Don't reveal that the user does not exist or is not confirmed
                return View();
            }

            AddErrors(RRC.message);
            return View(model);

        }



        // [HttpGet]
        //public JsonResult Get(string diet, string cuisine)
        //{
        //    localprepdbEntities model = new localprepdbEntities();
        //    model.Configuration.ProxyCreationEnabled = false;
        //    var preppers = model.Vendors.Include("Meals").AsNoTracking().Select(x => new PrepperHomeViewModel
        //    {
        //        VendorId = x.VendorId,
        //        VendorName = x.VendorName,
        //        Latitude = x.Latitude,
        //        Longitude = x.Longitude,
        //        FormattedAddress = x.FormattedAddress,
        //        PhoneNumber = x.PhoneNumber,
        //        DeliveryAvailable = x.DeliveryAvailable,
        //        PickupAvailable = x.PickupAvailable,
        //        ImgSrc = x.ImgSrc,
        //        meals = x.Meals.ToList()
        //    }).ToList();

        //    foreach (var prepper in preppers)
        //    {
        //        if (diet != string.Empty || cuisine != string.Empty)
        //        {
        //            foreach (var prepperMeal in prepper.meals)
        //            {
        //                if (diet != string.Empty)
        //                {

        //                }
        //            }
        //        }
        //    }

        //    return Json(preppers, JsonRequestBehavior.AllowGet);
        //}

        [HttpGet]
        public JsonResult Get(string Search, string code,string miles,string mini_price,string max_price)
        {
            ResponseSearch RS = new ResponseSearch();
            //if (Session["access_token"] != null)
            //{
            if (code != "")
            {
                //string accesstoken = Session["access_token"].ToString();
                var responseToken = g.Apicustomersearch(Search, code, miles, mini_price, max_price, null);
                RS = JsonConvert.DeserializeObject<ResponseSearch>(responseToken.Content);
                return Json(RS.response.data, JsonRequestBehavior.AllowGet);
            }
            else 
                {
                RedirectToAction("Index", "Home");
            }
                    
            //}
            //return Json(RLD);

            return Json(RS, JsonRequestBehavior.AllowGet);
        }

        

        [HttpPost]
        public JsonResult CardSave(string CustomercardNumber, string CustomercardYear, string CustomercardCVV)
        {
            /*"expiration_date": "2025-12"*/
            string CustomercardYearNew = "2025-12";
            ResponseSearch RS = new ResponseSearch();
            if (Session["access_token"] != null)
            {
                {
                    string accesstoken = Session["access_token"].ToString();
                    var responseToken = g.ApicustomerSavecards(CustomercardNumber, CustomercardYearNew, CustomercardCVV, accesstoken);
                    RS = JsonConvert.DeserializeObject<ResponseSearch>(responseToken.Content);
                    //return Json(RS.response.data, JsonRequestBehavior.AllowGet);
                }

            }


            return Json("");
        }

        [AllowAnonymous]
        public ActionResult DeleteCard(string Id)
        {
            ResponseSearch RS = new ResponseSearch();
            if (Session["access_token"] != null)
            {

                {
                    string accesstoken = Session["access_token"].ToString();
                    var responseToken = g.ApiDeletCart(Id, accesstoken);
                    RS = JsonConvert.DeserializeObject<ResponseSearch>(responseToken.Content);
                    return RedirectToAction("CardInfo","Vendor");
                    //return Json(RS.response.data, JsonRequestBehavior.AllowGet);
                }

            }


            return Json("");
        }

        [HttpGet]
        public JsonResult Getcustomercards()
        {
            ResponseSearch RS = new ResponseSearch();
            if (Session["access_token"] != null)
            {

                {
                    string accesstoken = Session["access_token"].ToString();
                    var responseToken = g.ApiGetcustomercards(accesstoken);
                    RS = JsonConvert.DeserializeObject<ResponseSearch>(responseToken.Content);
                    //return Json(RS.response.data, JsonRequestBehavior.AllowGet);
                }

            }


            return Json("");
        }



        [AllowAnonymous]
        public JsonResult Getreviews(int mealId)
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
                    var responseToken = g.ApiCustomerreviews(accesstoken, mealId);
                    RS = JsonConvert.DeserializeObject<ResponseSearch>(responseToken.Content);
                    return Json(RS.response.data, JsonRequestBehavior.AllowGet);
                }


            }


            return Json(RS, JsonRequestBehavior.AllowGet);



        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult PrepperDetails(int prepper_id,string img, string rating,string name)
        {
            ResponseSearch RS = new ResponseSearch();
            if (Session["access_token"] != null)
            {
              
                string accesstoken = Session["access_token"].ToString();
                var responseToken = g.ApiPreppersbyPrepperId(accesstoken, prepper_id);
                RS = JsonConvert.DeserializeObject<ResponseSearch>(responseToken.Content);
                RS.response.name = name;
                RS.response.image = img;
                RS.response.rating = rating;
                return View(RS);
                //return Json(RS.response, JsonRequestBehavior.AllowGet);
               

            }
            //return Json(RLD);

            return View(RS);
        }

        //[AllowAnonymous]
        //public JsonResult GetLocalData()
        //{

        //}

        #region API register check
        public IRestResponse ApiRegisterFirst(RegistCheck RC)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/auth/prepper/registration/step/first");
            var request = new RestRequest(Method.POST);            
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n    \"registration_data\": \""+RC.registration_data+"\",\r\n    \"verification_code\": \"123456\"\r\n}\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);

        }
        public IRestResponse ApiRegisterFianl(Vendor model,string access_token)
        {
            string PickupAvailable = "";
            string DeliveryAvailable = "";            
            string ReturnedString = g.RemoveSpecialChars(model.PhoneNumber);
            string Phone = ReturnedString.Replace(" ", "");
            List<dietspecialities> lstdietspecialities = new List<dietspecialities>();
            List<cuisinespecialities> lstcuisinespecialities = new List<cuisinespecialities>();

            if (model.PickupAvailable == true)
            {
                PickupAvailable = "Yes";
            }
            else
            {
                PickupAvailable = "No";
            }
            if (model.DeliveryAvailable == true)
            {
                DeliveryAvailable = "Yes";
            }
            else
            {
                DeliveryAvailable = "No";
            }

            if (model.Diets != null)
            {
                foreach (var muldiets in model.Diets.Where(a => a.IsChecked == true))
                {
                    dietspecialities objdietspecialities = new dietspecialities();
                    objdietspecialities.name = muldiets.Display;
                    objdietspecialities.id = muldiets.ID;
                    lstdietspecialities.Add(objdietspecialities);
                }
                
            }
            model.diet_specialities = lstdietspecialities;
            if (model.Cuisines != null)
            {
                foreach (var mulcuisines in model.Cuisines.Where(a => a.IsChecked == true))
                {
                    cuisinespecialities objcuisinespecialities = new cuisinespecialities();
                    objcuisinespecialities.name = mulcuisines.Display;
                    objcuisinespecialities.id = mulcuisines.ID;
                    lstcuisinespecialities.Add(objcuisinespecialities);
                }

            }
            model.cuisine_specialities = lstcuisinespecialities;

            List<dietspecialitieswithoutId> dcw = new List<dietspecialitieswithoutId>();
            List<cuisinespecialitieswithoutId> csw = new List<cuisinespecialitieswithoutId>();
            dcw = model.diet_specialities
            .Select(m => new dietspecialitieswithoutId
            {
                name = m.name
            }).ToList();
            if (!string.IsNullOrEmpty(model.Otherdiet))
            {
                //dcw.Where(w => w.name == "Other").Select(w =>  w.name== viewModel.Otherdiet).ToList();
                dcw.Where(c => c.name == "Other").Select(c => { c.name = model.Otherdiet; return c; }).ToList();
            }
            csw = model.cuisine_specialities
            .Select(m => new cuisinespecialitieswithoutId
            {
                name = m.name
            }).ToList();

            if (!string.IsNullOrEmpty(model.Othercuisine))
            {
                csw.Where(c => c.name == "Other").Select(c => { c.name = model.Othercuisine; return c; }).ToList();
                //csw.Where(w => w.name == "Other").Select(w => w.name == viewModel.Othercuisine).ToList();
            }
            var diets = JsonConvert.SerializeObject(dcw);
            var cuisines = JsonConvert.SerializeObject(csw);


            //var diets = JsonConvert.SerializeObject(model.diet_specialities);
            //var cuisines = JsonConvert.SerializeObject(model.cuisine_specialities);
            

            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/auth/prepper/registration/step/final");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer" + access_token + "");            
            request.AddHeader("cache-control", "no-cache");
            request.AddParameter("application/json", "{\r\n    \"address\": {\r\n        \"email\": \""+ model.EmailAddress+"\",\r\n        \"phone\": \""+Phone+"\",\r\n        \"address_line_1\": \""+model.PhoneNumber+"\",\r\n        \"address_line_2\": \""+model.Address2+"\",\r\n        \"state\": \""+model.short_code+"\",\r\n        \"city\": \""+model.City+"\",\r\n        \"zip_code\": \""+model.Zip+"\"\r\n    },\r\n    \"business\": {\r\n        \"vendor_name\": \""+model.VendorName+"\",\r\n        \"company_name\": \""+model.CompanyName+ "\",\r\n        \"licence_number\": \"" + model.LicenseNo + "\",\r\n        \"diet_specialities\": " + diets+",\r\n        \"cuisine_specialities\":"+cuisines+" ,\r\n        \"website\": \""+model.Website+"\",\r\n        \"about_yourself\": \"" + model.AboutYourself+"\",\r\n        \"delivery_available\": \""+ DeliveryAvailable + "\",\r\n        \"pickup_available\": \""+ PickupAvailable + "\",\r\n        \"favorite_cuisine_to_cook\": \""+model.FavoriteThingsToCook+"\",\r\n        \"describe_cooking_style\": \""+model.AboutYourself+"\",\r\n        \"describe_business\": \""+model.AboutYourself+"\"\r\n    }\r\n}", ParameterType.RequestBody);
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

        public IRestResponse ApiprofileUpdate(string access_token, ResponseProfile viewmodel)
        {
            string first_name = viewmodel.response.user.first_name;
            string last_name = viewmodel.response.user.last_name;
            string photo = viewmodel.response.user.account.photo;
            string state_id_or_drivers_license = viewmodel.response.user.account.photo;
            string social_security_number = viewmodel.response.user.account.photo;
            int id = viewmodel.response.user.addresses.FirstOrDefault().id;
            string address_line_1 = viewmodel.response.user.addresses.FirstOrDefault().address_line_1;
            string address_line_2 = viewmodel.response.user.addresses.FirstOrDefault().address_line_2;
            string city = viewmodel.response.user.addresses.FirstOrDefault().city;
            string zip_code = viewmodel.response.user.addresses.FirstOrDefault().zip_code;
            string state = viewmodel.response.user.addresses.FirstOrDefault().state;

            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/auth/prepper/profile");
            client.Timeout = -1;
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddHeader("Content-Type", "text/plain");
            request.AddParameter("text/plain", "{\r\n    \"user\": {\r\n\"first_name\":"+ first_name + ",\r\n\"last_name\":"+last_name+"\r\n },\r\n\"account\": {\r\n\"state_id_or_drivers_license\": \"ASD123123\",\r\n\"social_security_number\": \"123123123123\"\r\n},\r\n    \"addresses\": [\r\n{\r\n\"id\": "+id+",\r\n\"type\": \"PROFILE_ADDRESS\",\r\n\"email\": null,\r\n\"phone\": null,\r\n\"address_line_1\":"+address_line_1+",\r\n\"address_line_2\":"+address_line_2+",\r\n\"state\":"+state+",\r\n\"city\":"+city+",\r\n \"zip_code\":"+zip_code+"\r\n},\r\n {\r\n\"id\": 6,\r\n \"type\": \"PREPPER_COMPANY_ADDRESS\",\r\n\"email\": \"tlang@gmail.com\",\r\n \"phone\": \"1-950-201-1075\",\r\n\"address_line_1\": \"639 Marge Skyway update 2\",\r\n            \"address_line_2\": \"517 Jay Crossroad Suite 762 update 2\",\r\n            \"state\": \"CA\",\r\n            \"city\": \"Millsshire\",\r\n            \"zip_code\": \"50603\"\r\n        }\r\n    ],\r\n    \"business\": {\r\n        \"vendor_name\": \"Prakash Sharma update\",\r\n        \"company_name\": \"Splitree LLp\",\r\n        \"diet_specialities\": [\r\n            {\r\n                \"id\": 1,\r\n                \"name\": \"Ketogenic\"\r\n            }\r\n        ],\r\n        \"cuisine_specialities\": [\r\n            {\r\n                \"id\": 1,\r\n                \"name\": \"American\"\r\n            }\r\n        ],\r\n        \"website\": \"splitreef.com\",\r\n        \"about_yourself\": \"asdas dasd asd asd asd asd asda sd asdas das dasd asd asd asd asd sad asda sd\",\r\n        \"delivery_available\": \"Yes\",\r\n        \"pickup_available\": \"No\",\r\n        \"favorite_cuisine_to_cook\": \"asdas dasd asd asd asd asd asda sd asdas das dasd asd asd asd asd sad asda sd\",\r\n        \"describe_cooking_style\": \"asdas dasd asd asd asd asd asda sd asdas das dasd asd asd asd asd sad asda sd\",\r\n        \"describe_business\": \"asdas dasd asd asd asd asd asda sd asdas das dasd asd asd asd asd sad asda sd\"\r\n    }\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);
        }

       


        public IRestResponse Apiprofilecustomer(string access_token)
        {
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/auth/customer/profile");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            var body = @"";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);

        }

        public IRestResponse ApiprofileCustUpdate(string access_token, ResponseProfile viewmodel)
        {
            string first_name = viewmodel.response.user.first_name;
            string last_name = viewmodel.response.user.last_name;
            string photo = viewmodel.response.user.account.photo;
            int id = viewmodel.response.user.addresses.FirstOrDefault().id;
            string address_line_1 = viewmodel.response.user.addresses.FirstOrDefault().address_line_1;
            string address_line_2 = viewmodel.response.user.addresses.FirstOrDefault().address_line_2;
            string city = viewmodel.response.user.addresses.FirstOrDefault().city;
            string zip_code = viewmodel.response.user.addresses.FirstOrDefault().zip_code;
            string state = viewmodel.response.user.addresses.FirstOrDefault().state;
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/auth/customer/profile");
            client.Timeout = -1;           
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Authorization", "Bearer" + access_token + "");
            request.AddHeader("Content-Type", "text/plain");
            request.AddParameter("text/plain", "{\r\n  \"user\": {\r\n    \"first_name\":"+ first_name + ",\r\n    \"last_name\":"+last_name+"\r\n  },\r\n  \"account\":{\r\n      \"photo\" :"+ photo + "\r\n  },\r\n  \"address\": {\r\n      \"id\": "+ id + ",\r\n      \"address_line_1\": "+address_line_1+",\r\n      \"address_line_2\":"+address_line_2+",\r\n      \"state\":"+ state + ",\r\n      \"city\": "+city+",\r\n      \"zip_code\": "+zip_code+"\r\n    }\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);
        }








        #endregion

        public void AddErrors(string result)
        {

            ModelState.AddModelError("", result);

        }


    }
}
