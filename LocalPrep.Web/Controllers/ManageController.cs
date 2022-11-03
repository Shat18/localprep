using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using LocalPrep.Web.Models;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using static LocalPrep.Web.Models.Utilities;
using System.IO;
using RestSharp;

namespace LocalPrep.Web.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        General g = new General();
        public List<MealAddOn> MealAddOns;



        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

      

        //
        // GET: /Manage/Index
        [AllowAnonymous]
        public ActionResult Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            var userId = "5b6e4743-4156-421e-ac00-bc85d6351835";
            //var userId = "4d928c1f-6386-4778-8908-46d044f039fe";.

            var model = new IndexViewModel();
            //{
            //    HasPassword = HasPassword(),
            //    PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
            //    TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
            //    Logins = await UserManager.GetLoginsAsync(userId),
            //    BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            //};
            return View(model);
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Generate the token and send it
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };
                await UserManager.SmsService.SendAsync(message);
            }
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");
            return View(model);
        }

        //
        // POST: /Manage/RemovePhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Cuisines

        [Authorize(Roles = "Admin")]
        public ActionResult Cuisines()
        {
            List<Cuisine> cuisines = new List<Cuisine>();

            using (var model = new localprepdbEntities())
            {
                cuisines = model.Cuisines.ToList();
            }

            return View(cuisines);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddCuisine()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddCuisine(Cuisine cuisine)
        {
            if (!ModelState.IsValid)
            {
                return View(cuisine);
            }

            using (var model = new localprepdbEntities())
            {
                cuisine.CreateDt = DateTime.Now;
                cuisine.IsActive = true;
                model.Cuisines.Add(cuisine);
                model.SaveChanges();
            }

            ViewBag.SuccessMessage = "Cuisine was successfully added.";

            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditCuisine(int cuisineId)
        {
            using (var model = new localprepdbEntities())
            {
                Cuisine cuisine = model.Cuisines.Where(x => x.CuisineId == cuisineId).FirstOrDefault();

                ViewBag.SuccessMessage = "Cuisine was successfully updated.";

                return View(cuisine);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EditCuisine(Cuisine cuisine)
        {
            if (!ModelState.IsValid)
            {
                return View(cuisine);
            }

            using (var model = new localprepdbEntities())
            {
                var nCuisine = model.Cuisines.Single(x => x.CuisineId == cuisine.CuisineId);
                nCuisine.CuisineName = cuisine.CuisineName;
                nCuisine.IsActive = cuisine.IsActive;
                model.SaveChanges();
            }

            ViewBag.SuccessMessage = "Cuisine was successfully added.";

            return RedirectToAction("Cuisines");
        }

        #endregion

        #region Diets

        [Authorize(Roles = "Admin")]
        public ActionResult Diets()
        {
            List<Diet> diets = new List<Diet>();

            using (var model = new localprepdbEntities())
            {
                diets = model.Diets.ToList();
            }

            return View(diets);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddDiet()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddDiet(Diet diet)
        {
            if (!ModelState.IsValid)
            {
                return View(diet);
            }

            using (var model = new localprepdbEntities())
            {
                diet.CreateDt = DateTime.Now;
                diet.IsActive = true;
                model.Diets.Add(diet);
                model.SaveChanges();
            }

            ViewBag.SuccessMessage = "Diet was successfully added.";

            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditDiet(int dietId)
        {
            using (var model = new localprepdbEntities())
            {
                Diet diet = model.Diets.Where(x => x.DietId == dietId).FirstOrDefault();

                ViewBag.SuccessMessage = "Diet was successfully updated.";

                return View(diet);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EditDiet(Diet diet)
        {
            string imgUrl = string.Empty;
            if (diet.ImageFile != null)
            {
                var validImageTypes = new string[]
                {
                    "image/gif",
                    "image/jpeg",
                    "image/pjpeg",
                    "image/png"
                };

                if (!validImageTypes.Contains(diet.ImageFile.ContentType))
                {
                    ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
                }

                if (diet.ImgSrc != null && diet.ImgSrc != string.Empty)
                {
                    ImageRepo.RemoveImage(diet.ImgSrc);
                }

                imgUrl = ImageRepo.UploadImage(diet.ImageFile, "Diets");
            }

            if (!ModelState.IsValid)
            {
                return View(diet);
            }

            using (var model = new localprepdbEntities())
            {
                var nDiet = model.Diets.Single(x => x.DietId == diet.DietId);
                nDiet.DietShortName = diet.DietShortName;
                nDiet.DietLongName = diet.DietLongName;
                nDiet.DietDescription = diet.DietDescription;
                if (diet.ImageFile != null)
                    nDiet.ImgSrc = imgUrl;

                nDiet.IsActive = diet.IsActive;
                try
                {
                    model.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewBag.SuccessMessage = "Diet failed to update.";
                }
            }

            ViewBag.SuccessMessage = "Diet was successfully added.";

            return RedirectToAction("Diets");
        }

        #endregion

        #region Meals
        [AllowAnonymous]
        public ActionResult Meals()        
        {
            if (Session["access_token"] != null)
            {
                var response = ApiGetMeals();
                var meals = JsonConvert.DeserializeObject<ResponseMeals>(response.Content);
                return View(meals.response.data);
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
           
            
        }

        [AllowAnonymous]
        public ActionResult AddMeal()
        {
            if(Session["access_token"]!=null)
            {
                string Token = Session["access_token"].ToString();
                List<MealIngredient> MealIngredientsVal = new List<MealIngredient>();
                List<MealAddOn> MealAddOnsVal = new List<MealAddOn>();
                Meal mealVal = new Meal();
                using (var model = new localprepdbEntities())
                {
                    MealViewModel viewModel = new MealViewModel();
                    viewModel.meal = mealVal;
                    viewModel.Diets = g.Loaddietspecialities(Token);
                    viewModel.Cuisines = g.Loadcuisinespecialities(Token);
                    viewModel.AddON = g.GetDefaultAddON();
                    viewModel.AddIngredient = g.GetDefaultIngredient();
                    return View(viewModel);
                }

            }
            {
                return RedirectToAction("Login", "Account");
            }

        }

       

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddMeal(MealViewModel viewModel)
        {
            ResponseRegistCheck rrc = new ResponseRegistCheck();
            List<AddOn> lstaddon=new List<AddOn>();
            List<AddIngredient> lstaddIngredient = new List<AddIngredient>();

            string Token = Session["access_token"].ToString();
            if (!ModelState.IsValid)
            {
                
             viewModel.Diets = g.Loaddietspecialities(Token);
             viewModel.Cuisines = g.Loadcuisinespecialities(Token);
             viewModel.AddON = g.AddDefaultAddON(lstaddon);
             viewModel.AddIngredient= g.AddDefaultIngredient(lstaddIngredient);            
             return View(viewModel);
               
            }
            else {

                List<dietspecialities> lstdietspecialities = new List<dietspecialities>();
                List<cuisinespecialities> lstcuisinespecialities = new List<cuisinespecialities>();
                viewModel.AddON = Session["AddOnList"] as List<AddOn>;
                viewModel.AddIngredient = Session["IngredientsList"] as List<AddIngredient>;
                if (viewModel.AddON !=null)
                {
                    viewModel.AddON = viewModel.AddON.Where(a => a.add_on_name != "" && a.add_on_price != "").ToList();
                    
                }
                else {
                    viewModel.AddON = g.AddDefaultAddON(lstaddon);
                }
                if (viewModel.AddIngredient != null)
                {
                    viewModel.AddIngredient = viewModel.AddIngredient.Where(a => a.IngredientName != "").ToList();

                }
                else
                {
                    viewModel.AddIngredient = g.AddDefaultIngredient(lstaddIngredient);
                }


                if (viewModel.Diets != null)
                {
                    foreach (var muldiets in viewModel.Diets.Where(a => a.IsChecked == true))
                    {
                        dietspecialities objdietspecialities = new dietspecialities();
                        objdietspecialities.name = muldiets.Display;
                        objdietspecialities.id = muldiets.ID;
                        lstdietspecialities.Add(objdietspecialities);
                    }

                }
                viewModel.diet_specialities = lstdietspecialities;
                if (viewModel.Cuisines != null)
                {
                    foreach (var mulcuisines in viewModel.Cuisines.Where(a => a.IsChecked == true))
                    {
                        cuisinespecialities objcuisinespecialities = new cuisinespecialities();
                        objcuisinespecialities.name = mulcuisines.Display;
                        objcuisinespecialities.id = mulcuisines.ID;
                        lstcuisinespecialities.Add(objcuisinespecialities);
                    }

                }
                viewModel.cuisine_specialities = lstcuisinespecialities;
                if(!viewModel.meal.IsActive)
                {
                    viewModel.Active = "";
                }
                else
                {
                    viewModel.Active = "ACTIVE";
                }

                var response = ApiAddMeal(viewModel);
                rrc = JsonConvert.DeserializeObject<ResponseRegistCheck>(response.Content);
                if (!rrc.error)
                {
                    ViewBag.SuccessMessage = rrc.message;
                    //return View(viewModel);
                    //    ViewBag.SuccessMessage = meals.message;
                    return RedirectToAction("Meals", "Manage");

                    //var responseApi = ApiGetMeals();
                    //var meals = JsonConvert.DeserializeObject<ResponseMeals>(responseApi.Content);
                    //return View(meals.response.data);
                }
                //}
                else
                {
                    AddErrors(rrc.message);
                    return View(viewModel);
                }

            }
           

            

        }

        [AllowAnonymous]
        public ActionResult EditMeal(int mealId)
        {
            string Token = Session["access_token"].ToString();
            ResponseMeals rm = new ResponseMeals();
            var response = ApiGetMealbyId(mealId);
            rm = JsonConvert.DeserializeObject<ResponseMeals>(response.Content);
            List<AddIngredient> lstIngre = new List<AddIngredient>();
            List<MealIngredient> MealIngredientsVal = new List<MealIngredient>();
            List<MealAddOn> MealAddOnsVal = new List<MealAddOn>();

            if (Session["access_token"] != null)
            {
                rm.response.lstdietspecialities = g.Loaddietspecialitiesedit(rm.response.diet_specialities, Token);
                var resultdiet = rm.response.diet_specialities.Where(x => !rm.response.lstdietspecialities.Any(y => y.Display == x.name));
                foreach (var muldiets in rm.response.lstdietspecialities)
                {
                    if (rm.response.diet_specialities != null)
                    {
                        foreach (var muldiet_specialities in rm.response.diet_specialities)
                        {
                            if (muldiets.Display != "Other")
                            {
                                if (muldiets.Display == muldiet_specialities.name)
                                {
                                    muldiets.IsChecked = true;
                                }
                            }
                            else
                            {
                                if (resultdiet.Count() > 0)
                                {
                                    if (resultdiet.FirstOrDefault().name == muldiet_specialities.name)
                                    {
                                        muldiets.IsChecked = true;
                                        rm.response.Otherdiet = resultdiet.FirstOrDefault().name;
                                    }
                                }

                            }


                        }

                    }
                }

                rm.response.lstcuisinespecialities = g.Loadcuisinespecialitiesedits(rm.response.cuisine_specialities, Token);
                var resultcuisine = rm.response.cuisine_specialities.Where(x => !rm.response.lstcuisinespecialities.Any(y => y.Display == x.name));
                foreach (var mulcuisines in rm.response.lstcuisinespecialities)
                {
                    if (rm.response.diet_specialities != null)
                    {
                        foreach (var mulcuisine_specialities in rm.response.cuisine_specialities)
                        {
                            if (mulcuisines.Display != "Other")
                            {
                                if (mulcuisines.Display == mulcuisine_specialities.name)
                                {
                                    mulcuisines.IsChecked = true;
                                }
                            }
                            else
                            {
                                if (resultcuisine.Count() > 0)
                                {
                                    if (resultcuisine.FirstOrDefault().name == mulcuisine_specialities.name)
                                    {
                                        mulcuisines.IsChecked = true;
                                        rm.response.Othercuisine = resultcuisine.FirstOrDefault().name;
                                    }
                                }

                            }

                        }

                    }
                }

                Session.Add("AddonList", rm.response.add_on);
                Session.Add("IngredientsList", rm.response.ingredients);             
                foreach (var a in rm.response.ingredients)
                {
                    AddIngredient objAddIngredient = new AddIngredient();
                    objAddIngredient.IngredientName = a;
                    lstIngre.Add(objAddIngredient);

                }
                if (rm.response.status == "ACTIVE")
                {
                    rm.response.active = true;
                }
                else
                {
                    rm.response.active = false;
                }
                rm.response.ingredientsl = lstIngre;                
                return View(rm.response);            

            }
            else
            {
                return RedirectToAction("Login", "Account");

            }

        }



        [HttpPost]
        [AllowAnonymous]
        public ActionResult EditMeal(responsedata viewModel)
        {
            string Token = Session["access_token"].ToString();
            List<dietspecialities> lstdietspecialities = new List<dietspecialities>();
            List<cuisinespecialities> lstcuisinespecialities = new List<cuisinespecialities>();
            ResponseRegistCheck rrc = new ResponseRegistCheck();
            viewModel.add_on = Session["AddOnList"] as List<AddOn>;
            viewModel.ingredients = Session["IngredientsList"] as string[];



            if (viewModel.lstdietspecialities != null)
            {
                foreach (var muldiets in viewModel.lstdietspecialities.Where(a => a.IsChecked == true))
                {
                    dietspecialities objdietspecialities = new dietspecialities();
                    objdietspecialities.name = muldiets.Display;
                    objdietspecialities.id = muldiets.ID;
                    lstdietspecialities.Add(objdietspecialities);
                }

            }
            viewModel.diet_specialities_respo = lstdietspecialities;

            if (viewModel.lstcuisinespecialities != null)
            {
                foreach (var mulcuisines in viewModel.lstcuisinespecialities.Where(a => a.IsChecked == true))
                {
                    cuisinespecialities objcuisinespecialities = new cuisinespecialities();
                    objcuisinespecialities.name = mulcuisines.Display;
                    objcuisinespecialities.id = mulcuisines.ID;
                    lstcuisinespecialities.Add(objcuisinespecialities);
                }

            }
            viewModel.cuisine_specialities_respo = lstcuisinespecialities;

            if (!viewModel.active)
            {
                viewModel.status = "IN-ACTIVE";
            }
            else
            {
                viewModel.status = "ACTIVE";
            }

            var response = ApiEditMeal(viewModel);
            rrc = JsonConvert.DeserializeObject<ResponseRegistCheck>(response.Content);
            if (!rrc.error)
            {
                ViewBag.SuccessMessage = rrc.message;
                //return View(viewModel);
                //    ViewBag.SuccessMessage = meals.message;
                return RedirectToAction("Meals", "Manage");
                
            }
            //}
            else
            {
                AddErrors(rrc.message);
                return View(viewModel);
            }

        }



        [AllowAnonymous]
        public ActionResult DeleteMeal(int mealId)
        {
            string Token = Session["access_token"].ToString();
            //List<MealIngredient> MealIngredientsVal = new List<MealIngredient>();
            //List<MealAddOn> MealAddOnsVal = new List<MealAddOn>();
            //Meal mealVal = new Meal();
         

            var response = ApiDeleteMeal(mealId);
            var responseApi = JsonConvert.DeserializeObject<ResponseRegistCheck>(response.Content);            
            var meals = JsonConvert.DeserializeObject<ResponseMeals>(response.Content);
            if (!meals.error)
            {
                ViewBag.SuccessMessage = meals.message;
                return RedirectToAction("Meals", "Manage");
                //var responseMeals = ApiGetMeals();
                //meals = JsonConvert.DeserializeObject<ResponseMeals>(responseMeals.Content);
                //return View(meals.response.data);
            }
            else
            {
                AddErrors(meals.message);
                return View();
            }
            
            

        }

        [HttpPost]
        public JsonResult AddIngredient(int mealId, string ingredientName)
        {
            using (var model = new localprepdbEntities())
            {
                MealIngredient mi = new MealIngredient();
                mi.MealId = mealId;
                mi.IngredientName = ingredientName;

                model.MealIngredients.Add(mi);
                model.SaveChanges();

                return Json(mi);
            }
        }

        [HttpPost]
        public JsonResult RemoveIngredient(int mealIngredientId)
        {
            using (var model = new localprepdbEntities())
            {

                MealIngredient mi = model.MealIngredients.Where(x => x.MealIngredientId == mealIngredientId).FirstOrDefault();

                if (mi != null)
                {
                    model.MealIngredients.Remove(mi);
                    model.SaveChanges();

                    return Json("success");
                }
                else
                {
                    return Json("fail");
                }
            }
        }

        [HttpPost]
        public JsonResult AddAddOn(int mealId, string addOnName, string addOnPrice)
        {
            using (var model = new localprepdbEntities())
            {
                MealAddOn mao = new MealAddOn();
                mao.MealId = mealId;
                mao.MealAddOnName = addOnName;
                mao.MealAddOnPrice = Convert.ToDecimal(addOnPrice);

                model.MealAddOns.Add(mao);
                model.SaveChanges();

                return Json(mao);
            }
        }

        [HttpPost]
        public JsonResult RemoveAddOn(int mealAddOnId)
        {
            using (var model = new localprepdbEntities())
            {

                MealAddOn mao = model.MealAddOns.Where(x => x.MealAddOnId == mealAddOnId).FirstOrDefault();

                if (mao != null)
                {
                    model.MealAddOns.Remove(mao);
                    model.SaveChanges();

                    return Json("success");
                }
                else
                {
                    return Json("fail");
                }
            }
        }

        #endregion

        #region Vendors
        public ActionResult EditVendor()
        {
            ResponseStates rs = new ResponseStates();
            General g = new General();
            var responseState = g.Apistatelist();
            rs = JsonConvert.DeserializeObject<ResponseStates>(responseState.Content);


            using (var model = new localprepdbEntities())
            {
                string userId = User.Identity.GetUserId();

                var vendor = model.Vendors.Where(x => x.UserId == userId).SingleOrDefault();
                vendor.States = rs.response;

                return View(vendor);
            }
        }

        [HttpPost]
        public ActionResult EditVendor(Vendor vendor)
        {
            string imgUrl = string.Empty;
            if (vendor.ImageFile != null)
            {
                var validImageTypes = new string[]
                {
                    "image/gif",
                    "image/jpeg",
                    "image/pjpeg",
                    "image/png"
                };

                if (!validImageTypes.Contains(vendor.ImageFile.ContentType))
                {
                    ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
                }

                if (vendor.ImgSrc != null && vendor.ImgSrc != string.Empty)
                {
                    ImageRepo.RemoveImage(vendor.ImgSrc);
                }

                imgUrl = ImageRepo.UploadImage(vendor.ImageFile, "Profile");
                vendor.ImgSrc = imgUrl;
            }

            if (!ModelState.IsValid)
            {
                return View(vendor);
            }

            using (var model = new localprepdbEntities())
            {
                string userId = User.Identity.GetUserId();
                int vendorId = model.Vendors.Where(x => x.UserId == userId).Select(x => x.VendorId).SingleOrDefault();

                vendor.VendorId = vendorId;
                vendor.UserId = userId;

                //Google Maps Information
                string stateName = model.States.Where(x => x.StateId == vendor.StateId).Select(x => x.StateShortName).First();
                string addressLookup = vendor.Address1 + (vendor.Address2 == null ? "" : " " + vendor.Address2) + " " + vendor.City + ", " + stateName + " " + vendor.Zip;

                var request = WebRequest.Create(string.Format("https://maps.googleapis.com/maps/api/geocode/json?address={0}&key=AIzaSyBdFxZ8uWWp8TVVkbv3TUsiNrI0Vbk44uU", Uri.EscapeDataString(addressLookup)));
                request.ContentType = "application/json; charset=utf-8";

                string jsonGeoCode;
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        jsonGeoCode = sr.ReadToEnd();
                        var rootObject = JsonConvert.DeserializeObject<RootObject>(jsonGeoCode);

                        foreach (var result in rootObject.results)
                        {
                            vendor.Latitude = Convert.ToDecimal(result.geometry.location.lat);
                            vendor.Longitude = Convert.ToDecimal(result.geometry.location.lng);
                            vendor.PlaceId = result.place_id;
                            vendor.FormattedAddress = result.formatted_address;
                        }
                    }
                }

                var restOfFields = model.Vendors.Where(x => x.VendorId == vendor.VendorId).Select(x => new { x.IsApproved, x.ApprovedById, x.SubmitDt }).SingleOrDefault();

                vendor.IsApproved = restOfFields.IsApproved;
                vendor.ApprovedById = restOfFields.ApprovedById;
                vendor.SubmitDt = restOfFields.SubmitDt;

                model.Entry(vendor).State = System.Data.Entity.EntityState.Modified;
                model.SaveChanges();

                return RedirectToAction("EditVendor");
            }
        }
        #endregion

        #region Contact Us

        [Authorize(Roles = "Admin")]
        public ActionResult ContactUs()
        {
            List<Contact> contacts = new List<Contact>();

            using (var model = new localprepdbEntities())
            {
                contacts = model.Contacts.OrderByDescending(x => x.SubmitDt).ToList();
            }

            return View(contacts);
        }

        #endregion

        #region Orders
        [AllowAnonymous]
        public ActionResult Orders()
        {
            string Role = string.Empty;
            if (Session["access_token"] != null)
            { 
                if (Session["role"] != null)
                Role = Session["role"].ToString();

            General g = new General();
            string Token = Session["access_token"].ToString();
            ResponseMeals rm = new ResponseMeals();

            if (Role != "CUSTOMER")
            {
                var response = g.ApiOrdersprepper(Token);
                rm = JsonConvert.DeserializeObject<ResponseMeals>(response.Content);
                rm.response.roleResponse = Role;
            }
            else
            {
                var response = g.ApiOrders(Token);
                rm = JsonConvert.DeserializeObject<ResponseMeals>(response.Content);
                rm.response.roleResponse = Role;
            }

            if (!rm.error)
            {
                return View(rm.response);
            }
            return View();
        }
             else
            {
                return RedirectToAction("Login", "Account");

            }

        }

        /// <summary>
        /// Preppers Order History List
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public PartialViewResult _PreppersOrderHistoryList()
        {
            string Role = string.Empty;
            General g = new General();
            if (Session["role"] != null)
                Role = Session["role"].ToString();
            string Token = Session["access_token"].ToString();
            ResponseMeals rm = new ResponseMeals();
            if (Role != "CUSTOMER")
            {              
                var response = g.ApiOrdersprepperhistory(Token);
                rm = JsonConvert.DeserializeObject<ResponseMeals>(response.Content);
                return PartialView("_PrepperHistoryOrdersList", rm.response);
            }
            else
            {              
                var response = g.ApiOrdersCustomerhistory(Token);
                rm = JsonConvert.DeserializeObject<ResponseMeals>(response.Content);
                return PartialView("_PrepperHistoryOrdersList", rm.response);
            }

        }

        [AllowAnonymous]
        public ActionResult OrdersDetails(int mealId)
        {           
            if (Session["access_token"] != null)
            {
                string Role = string.Empty;
                General g = new General();
                if (Session["role"] != null)
                    Role = Session["role"].ToString();
                string Token = Session["access_token"].ToString();
                ResponseMeals rm = new ResponseMeals();
                if (Role != "CUSTOMER")
                {                  
                    
                    var response = g.ApiOrdersbyId(Token, mealId);
                    rm = JsonConvert.DeserializeObject<ResponseMeals>(response.Content);
                    if (!rm.error)
                    {
                        List<AddIngredient> lstIngre = new List<AddIngredient>();
                        List<MealIngredient> MealIngredientsVal = new List<MealIngredient>();
                        List<MealAddOn> MealAddOnsVal = new List<MealAddOn>();
                        Session.Add("AddonList", rm.response.add_on);
                        Session.Add("IngredientsList", rm.response.ingredients);

                        if (rm.response.status == "ACTIVE")
                        {
                            rm.response.active = true;
                        }
                        else
                        {
                            rm.response.active = false;
                        }
                        rm.response.roleResponse = "PREPPER";
                        return View(rm.response);
                    }
                    ViewBag.FailureMessage = rm.message;
                    return View(rm.response);
                }
                else
                {
                    var response = g.ApiCustomerOrdersbyId(Token, mealId);
                    rm = JsonConvert.DeserializeObject<ResponseMeals>(response.Content);
                    if (!rm.error)
                    {
                        List<AddIngredient> lstIngre = new List<AddIngredient>();
                        List<MealIngredient> MealIngredientsVal = new List<MealIngredient>();
                        List<MealAddOn> MealAddOnsVal = new List<MealAddOn>();
                        Session.Add("AddonList", rm.response.add_on);
                        Session.Add("IngredientsList", rm.response.ingredients);

                        if (rm.response.status == "ACTIVE")
                        {
                            rm.response.active = true;
                        }
                        else
                        {
                            rm.response.active = false;
                        }
                        rm.response.roleResponse = "CUSTOMER";
                        return View(rm.response);
                    }
                    ViewBag.FailureMessage = rm.message;
                    return View(rm.response);
                }



                

                
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }





        }

        [AllowAnonymous]
        public ActionResult CustomerOrdersDetails(int mealId)
        {
            General g = new General();

            string Token = Session["access_token"].ToString();
            ResponseMeals rm = new ResponseMeals();
            var response = g.ApiCustomerOrdersbyId(Token, mealId);

            rm = JsonConvert.DeserializeObject<ResponseMeals>(response.Content);
            List<AddIngredient> lstIngre = new List<AddIngredient>();



            List<MealIngredient> MealIngredientsVal = new List<MealIngredient>();
            List<MealAddOn> MealAddOnsVal = new List<MealAddOn>();            
            Session.Add("AddonList", rm.response.add_on);
            Session.Add("IngredientsList", rm.response.ingredients);           
          
            if (rm.response.status == "ACTIVE")
            {
                rm.response.active = true;
            }
            else
            {
                rm.response.active = false;
            }
            
            return View(rm.response);



        }

        [AllowAnonymous]
        public ActionResult OrderAccepted(int Id)
        {
            ResponseMeals rm = new ResponseMeals();
            string Token = Session["access_token"].ToString();
            var response = g.ApiOrderAccepted(Token, Id);          
            
            rm = JsonConvert.DeserializeObject<ResponseMeals>(response.Content);
            if (!rm.error)
            {
                ViewBag.SuccessMessage = rm.message;
                return RedirectToAction("Orders", "Manage");                
            }
            else
            {
                AddErrors(rm.message);
                return View();
            }

            
        }

        [AllowAnonymous]
        public ActionResult OrdersRejected(int Id)
        {
            ResponseMeals rm = new ResponseMeals();
            string Token = Session["access_token"].ToString();
            var response = g.ApiOrderRejected(Token, Id);
           
            rm = JsonConvert.DeserializeObject<ResponseMeals>(response.Content);
            if (!rm.error)
            {
                ViewBag.SuccessMessage = rm.message;
                return RedirectToAction("Orders", "Manage");                
            }
            else
            {
                AddErrors(rm.message);
                return View();
            }

        }

        #endregion

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

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        #endregion

        public IRestResponse ApiAddMeal(MealViewModel viewModel)
        {
            string Token = Session["access_token"].ToString();
            List<dietspecialitieswithoutId> dcw = new List<dietspecialitieswithoutId>();
            List<cuisinespecialitieswithoutId> csw = new List<cuisinespecialitieswithoutId>();
            dcw = viewModel.diet_specialities
            .Select(m => new dietspecialitieswithoutId
            {
                name = m.name
            }).ToList();
            if (!string.IsNullOrEmpty(viewModel.Otherdiet))
            {
                //dcw.Where(w => w.name == "Other").Select(w => w.name== viewModel.Otherdiet).ToList();
                dcw.Where(c => c.name == "Other").Select(c => { c.name = viewModel.Otherdiet; return c; }).ToList();
            }
            csw = viewModel.cuisine_specialities
            .Select(m => new cuisinespecialitieswithoutId
            {
                name = m.name
            }).ToList();



            if (!string.IsNullOrEmpty(viewModel.Othercuisine))
            {
                csw.Where(c => c.name == "Other").Select(c => { c.name = viewModel.Othercuisine; return c; }).ToList();
                //csw.Where(w => w.name == "Other").Select(w => w.name == viewModel.Othercuisine).ToList();
            }
            var diets = JsonConvert.SerializeObject(dcw);
            var cuisines = JsonConvert.SerializeObject(csw);
            List<string> lstingredient = new List<string>();
            lstingredient = g.ConvertStringList(viewModel.AddIngredient);
            var AddON = JsonConvert.SerializeObject(viewModel.AddON);
            var AddIngredient = JsonConvert.SerializeObject(lstingredient.ToArray());
            var imgresponse = UploadImageToServer(viewModel);
            var apiimgresponse = JsonConvert.DeserializeObject<ResponseImageUpload>(imgresponse.Content);
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/prepper/meals");
            //client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer" + Token + "");
            request.AddHeader("content-type", "application/json");
            // request.AddParameter("application/json", "{\r\n \"name\": \""+ viewModel.meal.MealName+"\",\r\n \"image\": \"https://work.splitreef.com/client/development/local_prep/public/storage/meal/17280342-dd8c-4a1a-b891-6c0ef7f3929b.jpg\",\r\n \"price\": \""+viewModel.meal.MealPrice+ "\",\r\n \"ingredients\": [\r\n \"asperiores\",\r\n \"et\"\r\n ],\r\n \"add_on\": [\r\n {\r\n \"add_on_name\": \"consequuntur\",\r\n \"add_on_price\": \"10.00\"\r\n },\r\n {\r\n \"add_on_name\": \"repellendus\",\r\n \"add_on_price\": \"10.00\"\r\n }\r\n ],\r\n \"nutritional_info\": [\r\n {\r\n \"nutritional_name\": \"Servings\",\r\n\"nutritional_value\": \"" + viewModel.meal.Servings + "\"\r\n },\r\n {\r\n \"nutritional_name\": \"Calories/Serving\",\r\n \"nutritional_value\": \"" + viewModel.meal.CaloriesServing + "\"\r\n },\r\n {\r\n \"nutritional_name\": \"Calories\",\r\n \"nutritional_value\": \"" + viewModel.meal.Calories + "\"\r\n},\r\n \r\n \"nutritional_name\": \"Fat\",\r\n \"nutritional_value\": \"" + viewModel.meal.Fat + "\"\r\n },\r\n {\r\n\"nutritional_name\": \"Protein\",\r\n\"nutritional_value\": \"" + viewModel.meal.Protein + "\"\r\n },\r\n {\r\n \"nutritional_name\": \"Sugar\",\r\n \"nutritional_value\": \"" + viewModel.meal.Sugar + "\"\r\n },\r\n{\r\n \"nutritional_name\": \"Sodium\",\r\n\"nutritional_value\": \"" + viewModel.meal.Sodium + "\"\r\n },\r\n {\r\n \"nutritional_name\": \"Cholesterol\",\r\n \"nutritional_value\": \"" + viewModel.meal.Cholesterol + "\"\r\n },\r\n {\r\n\"nutritional_name\": \"Total Carbohydrates\",\r\n \"nutritional_value\": \"" + viewModel.meal.TotalCarb + "\"\r\n }\r\n],\r\n \"nutritional_image\": \"https://work.splitreef.com/client/development/local_prep/public/storage/meal/17280342-dd8c-4a1a-b891-6c0ef7f3929b.jpg\",\r\n \"heating_instructions\": \"" + viewModel.meal.HeatingInstructions + "\",\r\n \"cuisine_specialities\": "+cuisines+",\r\n \"diet_specialities\": "+diets+",\r\n \"status\": \"" + viewModel.meal.IsActive+"\"\r\n}", ParameterType.RequestBody);
            request.AddParameter("application/json", "{ \r\n \"name\": \"" + viewModel.meal.MealName + "\",\r\n \"image\": \"" + apiimgresponse.response.url + "\",\r\n \"price\": " + viewModel.meal.MealPrice + ",\r\n \"description\": \"" + viewModel.meal.MealDescription + "\",\r\n \"ingredients\": " + AddIngredient + ",\r\n \"add_on\": " + AddON + ",\r\n \"nutritional_info\": [\r\n {\r\n \"nutritional_name\": \"Servings\",\r\n \"nutritional_value\": \"" + viewModel.meal.Servings + "\"\r\n },\r\n {\r\n \"nutritional_name\": \"Calories/Serving\",\r\n \"nutritional_value\": \"" + viewModel.meal.CaloriesServing + "\"\r\n },\r\n {\r\n \"nutritional_name\": \"Calories\",\r\n \"nutritional_value\": \"" + viewModel.meal.Calories + "\"\r\n },\r\n {\r\n \"nutritional_name\": \"Fat\",\r\n \"nutritional_value\": \"" + viewModel.meal.Fat + "\"\r\n },\r\n {\r\n \"nutritional_name\": \"Protein\",\r\n \"nutritional_value\":\"" + viewModel.meal.Protein + "\"\r\n },\r\n {\r\n \"nutritional_name\": \"Sugar\",\r\n \"nutritional_value\": \"" + viewModel.meal.Sugar + "\"\r\n },\r\n {\r\n \"nutritional_name\": \"Sodium\",\r\n \"nutritional_value\": \"" + viewModel.meal.Sodium + "\"\r\n },\r\n {\r\n \"nutritional_name\": \"Cholesterol\",\r\n \"nutritional_value\": \"" + viewModel.meal.Cholesterol + "\"\r\n },\r\n {\r\n \"nutritional_name\": \"Total Carbohydrates\",\r\n \"nutritional_value\": \"" + viewModel.meal.TotalCarb + "\"\r\n }\r\n ],\r\n \"nutritional_image\": \"https://work.splitreef.com/client/development/local_prep/public/storage/meal/17280342-dd8c-4a1a-b891-6c0ef7f3929b.jpg\",\r\n \"heating_instructions\": \"" + viewModel.meal.HeatingInstructions + "\",\r\n \"cuisine_specialities\": " + cuisines + ",\r\n \"diet_specialities\": " + diets + ",\r\n \"status\": \"" + viewModel.Active + "\"\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            return (response);

        }



        [AllowAnonymous]
        public ActionResult AddonMeal(List<AddOn> lstaddon)
        {
            //General g = new General();
            MealViewModel model = new MealViewModel();
            List<AddOn> objlstaddon = new List<AddOn>();
            objlstaddon = lstaddon;            
            objlstaddon = g.AddDefaultAddON(lstaddon);
            //ViewData["Student"] = objlstaddon;
            //ViewBag.MealAddOns = objlstaddon;
            Session.Add("AddonList", objlstaddon);

            return PartialView("_AddOnMeal", objlstaddon);
        }

     
        [AllowAnonymous]
        public ActionResult AddIngredients(List<AddIngredient> lstaddIngredient)
        {
            List<AddIngredient> objlstAddIngredient = new List<AddIngredient>();
            objlstAddIngredient = lstaddIngredient;
            objlstAddIngredient = g.AddDefaultIngredient(lstaddIngredient);
            Session.Add("IngredientsList", objlstAddIngredient);
            return PartialView("_Ingredient", objlstAddIngredient);
        }


        public IRestResponse ApiEditMeal(responsedata viewModel)
        {
            string Token = Session["access_token"].ToString();

            //var diets = JsonConvert.SerializeObject(viewModel.diet_specialities_respo);
            //var cuisines = JsonConvert.SerializeObject(viewModel.cuisine_specialities_respo);

            List<dietspecialitieswithoutId> dcw = new List<dietspecialitieswithoutId>();
            List<cuisinespecialitieswithoutId> csw = new List<cuisinespecialitieswithoutId>();
            dcw = viewModel.diet_specialities_respo
            .Select(m => new dietspecialitieswithoutId
            {
                name = m.name
            }).ToList();
            if (!string.IsNullOrEmpty(viewModel.Otherdiet))
            {
                //dcw.Where(w => w.name == "Other").Select(w =>  w.name== viewModel.Otherdiet).ToList();
                dcw.Where(c => c.name == "Other").Select(c => { c.name = viewModel.Otherdiet; return c; }).ToList();
            }
            csw = viewModel.cuisine_specialities_respo
            .Select(m => new cuisinespecialitieswithoutId
            {
                name = m.name
            }).ToList();

            if (!string.IsNullOrEmpty(viewModel.Othercuisine))
            {
                csw.Where(c => c.name == "Other").Select(c => { c.name = viewModel.Othercuisine; return c; }).ToList();
                //csw.Where(w => w.name == "Other").Select(w => w.name == viewModel.Othercuisine).ToList();
            }
            var diets = JsonConvert.SerializeObject(dcw);
            var cuisines = JsonConvert.SerializeObject(csw);






            var AddON = JsonConvert.SerializeObject(viewModel.add_on);
            var AddIngredient = JsonConvert.SerializeObject(viewModel.ingredients);
            string Url = string.Empty;
            if (viewModel.ImageFileE!=null)
            {
                var imgresponse = UploadImageToServer(viewModel);
                var apiimgresponse = JsonConvert.DeserializeObject<ResponseImageUpload>(imgresponse.Content);
                Url = apiimgresponse.response.url;

            }
            else
            {
                Url = viewModel.image;
            }
            
            
            
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/prepper/meals");
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Authorization", "Bearer" + Token + "");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{ \r\n    \"meal_id\":  \"" + viewModel.id + "\",   \r\n    \"name\": \"" + viewModel.name + "\",\r\n    \"image\": \"" + Url + "\",\r\n    \"price\": " + viewModel.price + ",\r\n \"description\": \"" + viewModel.description + "\",\r\n    \"ingredients\": " + AddIngredient + ",\r\n    \"add_on\": " + AddON + ",\r\n    \"nutritional_info\": [\r\n        {\r\n            \"nutritional_name\": \"Servings\",\r\n            \"nutritional_value\": " + viewModel.nutritional_info[0].nutritional_value + "\r\n        },\r\n        {\r\n            \"nutritional_name\": \"Calories/Serving\",\r\n            \"nutritional_value\": " + viewModel.nutritional_info[1].nutritional_value + "\r\n        },\r\n        {\r\n            \"nutritional_name\": \"Calories\",\r\n            \"nutritional_value\": " + viewModel.nutritional_info[2].nutritional_value + "\r\n        },\r\n        {\r\n            \"nutritional_name\": \"Fat\",\r\n            \"nutritional_value\": " + viewModel.nutritional_info[3].nutritional_value + "\r\n        },\r\n        {\r\n            \"nutritional_name\": \"Protein\",\r\n            \"nutritional_value\": " + viewModel.nutritional_info[4].nutritional_value + "\r\n        },\r\n        {\r\n            \"nutritional_name\": \"Sugar\",\r\n            \"nutritional_value\": " + viewModel.nutritional_info[5].nutritional_value + "\r\n        },\r\n        {\r\n            \"nutritional_name\": \"Sodium\",\r\n            \"nutritional_value\": " + viewModel.nutritional_info[6].nutritional_value + "\r\n        },\r\n        {\r\n            \"nutritional_name\": \"Cholesterol\",\r\n            \"nutritional_value\": " + viewModel.nutritional_info[7].nutritional_value + "\r\n        },\r\n        {\r\n            \"nutritional_name\": \"Total Carbohydrates\",\r\n            \"nutritional_value\": " + viewModel.nutritional_info[8].nutritional_value + "\r\n        }\r\n    ],\r\n    \"nutritional_image\": \"https://work.splitreef.com/client/development/local_prep/public/storage/meal/17280342-dd8c-4a1a-b891-6c0ef7f3929b.jpg\",\r\n    \"heating_instructions\": \"" + viewModel.heating_instructions + "\",\r\n        \"diet_specialities\": " + diets + ",\r\n        \"cuisine_specialities\":" + cuisines + ",\r\n    \"status\": \"" + viewModel.status + "\"\r\n}", ParameterType.RequestBody);
            //request.AddParameter("application/json", "{\r\n    \"meal_id\": \"27\",\r\n    \"name\": \"Jules 1 update 2\",\r\n    \"image\": \"https://work.splitreef.com/client/development/local_prep/public/storage/meal/17280342-dd8c-4a1a-b891-6c0ef7f3929b.jpg\",\r\n    \"price\": \"952.00\",\r\n    \"ingredients\": [\r\n        \"asperiores\",\r\n        \"et\"\r\n    ],\r\n    \"add_on\": [\r\n        {\r\n            \"add_on_name\": \"consequuntur\",\r\n            \"add_on_price\": \"10.00\"\r\n        }\r\n        \r\n    ],\r\n    \"nutritional_info\": [\r\n        {\r\n            \"nutritional_name\": \"Servings\",\r\n            \"nutritional_value\": \"10\"\r\n        },\r\n        {\r\n            \"nutritional_name\": \"Calories/Serving\",\r\n            \"nutritional_value\": \"10\"\r\n        },\r\n        {\r\n            \"nutritional_name\": \"Calories\",\r\n            \"nutritional_value\": \"10\"\r\n        },\r\n        {\r\n            \"nutritional_name\": \"Fat\",\r\n            \"nutritional_value\": \"10\"\r\n        },\r\n        {\r\n            \"nutritional_name\": \"Protein\",\r\n            \"nutritional_value\": \"10\"\r\n        },\r\n        {\r\n            \"nutritional_name\": \"Sugar\",\r\n            \"nutritional_value\": \"10\"\r\n        },\r\n        {\r\n            \"nutritional_name\": \"Sodium\",\r\n            \"nutritional_value\": \"10\"\r\n        },\r\n        {\r\n            \"nutritional_name\": \"Cholesterol\",\r\n            \"nutritional_value\": \"10\"\r\n        },\r\n        {\r\n            \"nutritional_name\": \"Total Carbohydrates\",\r\n            \"nutritional_value\": \"10\"\r\n        }\r\n    ],\r\n    \"nutritional_image\": \"https://work.splitreef.com/client/development/local_prep/public/storage/meal/17280342-dd8c-4a1a-b891-6c0ef7f3929b.jpg\",\r\n    \"heating_instructions\": \"Iste iusto magni rerum non rem distinctio assumenda cum. Non quod et sunt sint ipsa vitae velit sunt.\",\r\n    \"cuisine_specialities\": [\r\n        {\r\n            \"id\": 1,\r\n            \"name\": \"Ketogenic\"\r\n        },\r\n        {\r\n            \"id\": 2,\r\n            \"name\": \"Paleo\"\r\n        }\r\n    ],\r\n    \"diet_specialities\": [\r\n  {\r\n            \"id\": 1,\r\n  \"name\": \"American\"\r\n     },\r\n        {\r\n            \"id\": 2,\r\n            \"name\": \"Mexican\"\r\n        }\r\n    ],\r\n    \"status\": \"IN-ACTIVE\"\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (response);

        }

        public IRestResponse ApiGetMeals()
        {
            string Token = Session["access_token"].ToString();
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/prepper/meals");            
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + Token + "");
            var body = @"";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);           
            return (response);

        }

        public IRestResponse ApiGetMealbyId(int mealid)
        {
            string Token = Session["access_token"].ToString();
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/prepper/meals/"+ mealid + "");          
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer" + Token + "");
            var body = @"";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);            
            return (response);

        }

        public IRestResponse ApiDeleteMeal(int meal_id)
        {
            string Token = Session["access_token"].ToString();
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/prepper/meals");            
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Authorization", "Bearer" + Token + "");
            request.AddHeader("Content-Type", "application/json");          
            request.AddParameter("application/json", "{    \r\n    \"meal_id\": \""+ meal_id + "\"    \r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);           
            return (response);

        }

        #region API upload image
        public IRestResponse UploadImageToServer(MealViewModel model)
        {
            
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
            request.AddFile("image", @"" + path + "");
            IRestResponse response = client.Execute(request);
            return response;

        }
        #endregion
        public IRestResponse UploadImageToServer(responsedata model)
        {

            string fileName = string.Empty;
            string imgUrl = string.Empty;
            if (model.ImageFileE != null)
            {
                var validImageTypes = new string[]
                {
                        "image/gif",
                        "image/jpeg",
                        "image/pjpeg",
                        "image/png"
                };

                if (!validImageTypes.Contains(model.ImageFileE.ContentType))
                {
                    ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
                }

                //imgUrl = ImageRepo.UploadImage(viewModel.ImageFile, "Meals");

                fileName = Path.GetFileNameWithoutExtension(model.ImageFileE.FileName);
                string extension = Path.GetExtension(model.ImageFileE.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;




                string fileNamePath = Path.Combine(Server.MapPath("~/img/"), fileName);
                model.ImageFileE.SaveAs(fileNamePath);

            }

            string path = Server.MapPath("~/img/") + fileName;
            var client = new RestClient("https://work.splitreef.com/client/development/local_prep/api/file/upload");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddFile("image", @"" + path + "");
            IRestResponse response = client.Execute(request);
            return response;

        }
        public void AddErrors(string result)
        {

            ModelState.AddModelError("", result);

        }
    }
}
