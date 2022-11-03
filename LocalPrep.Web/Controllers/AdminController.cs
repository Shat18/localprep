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
    public class AdminController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;      

        General g = new General();

        public AdminController()
        {
            
        }

        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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


        




        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            LoginViewModel model = new LoginViewModel();
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }       

      

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Dashboard(string returnUrl)
        {
            Responsedashboard RD = new Responsedashboard();
            if (Session["access_token"] != null)
            {
                string accesstoken = Session["access_token"].ToString();
                var responseToken = g.ApiGetAdminDashboard(accesstoken);
                RD = JsonConvert.DeserializeObject<Responsedashboard>(responseToken.Content);
                return View(RD);
            }
            else
            {
                return RedirectToAction("Login", "Admin");

            }
                       
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
                if ((RL.response.user.subscriptions) != null)
                    Session["subscriptions"] = RL.response.user.subscriptions.Count;
                //role = RL.response.user.account.role;
                Session["role"] = RL.response.user.account.role;
                Session["Fullname"] = RL.response.user.first_name + " " + RL.response.user.last_name;
                Session["Photo"] = RL.response.user.account.photo;
                Session["access_token"] = RL.response.token_info.access_token;
                return RedirectToAction("dashboard", "Admin");
                //return RedirectToAction("VerifyCode", "Account");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }

            //return RedirectToAction("Dashboard", "Admin");
           
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult customers(string returnUrl)
        {
            ResponseActiveOrders RAO = new ResponseActiveOrders();
            if (Session["access_token"] != null)
            {

                string accesstoken = Session["access_token"].ToString();
                var responseToken = g.ApiGetAdminCustomers(accesstoken);
                RAO = JsonConvert.DeserializeObject<ResponseActiveOrders>(responseToken.Content);
                return View(RAO.response);


            }
            else
            {
                return RedirectToAction("Login", "Admin");

            }
            //return View(RAC);
        }

        [HttpPost]
        [AllowAnonymous]      
        public ActionResult customers(Customers model)
        {
            string URL = @"https://work.splitreef.com/client/development/local_prep/api/admin/customers?keyword=ShatCustomer&start_date=2022-04-01&end_date=2022-04-07";
            ResponseActiveOrders RAO = new ResponseActiveOrders();
            if (Session["access_token"] != null)
            {
                if(model.first_name==null && model.start_date!= null)
                {
                    URL = @"https://work.splitreef.com/client/development/local_prep/api/admin/customers?keyword=ShatCustomer&start_date=2022-04-01&end_date=2022-04-07";
                }
                else
                {
                    URL = @"https://work.splitreef.com/client/development/local_prep/api/admin/customers?keyword="+model.first_name+"";
                }
                //if (model.start_date == null)
                //{
                    
                //}

                string accesstoken = Session["access_token"].ToString();
                var responseToken = g.ApiGetAdminSearchCustomers(accesstoken, URL);
                RAO = JsonConvert.DeserializeObject<ResponseActiveOrders>(responseToken.Content);               
                ModelState.Clear();
                return View(RAO.response);


            }
            else
            {
                return RedirectToAction("Login", "Admin");

            }
            //return View(RAC);
        }

        [AllowAnonymous]
        public PartialViewResult CustomerDetails()
        {
            int customer_id = Convert.ToInt32(Session["customer_id"]);             
            ResponseCustomers custs = new ResponseCustomers();
            string accesstoken = Session["access_token"].ToString();
            var responseToken = g.ApiGetAdminCustomersDetails(accesstoken, customer_id);
            custs = JsonConvert.DeserializeObject<ResponseCustomers>(responseToken.Content);
            custs.response.created_at = DateTime.Parse(custs.response.created_at).ToString();
            return PartialView("_customerDetail", custs.response);

        }

        /// <summary>
        /// Customer Active Orders List
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public PartialViewResult CustomerActiveOrdersList()
        {
            int customer_id = Convert.ToInt32(Session["customer_id"]);
            ResponseActiveOrders1 RSO = new ResponseActiveOrders1();
            string accesstoken = Session["access_token"].ToString();
            //var responseToken = null;
            //if (nextPageUrl != null) {
            //    isMoreDataAvailable = true;
            //    pageNo++;
            //    responseToken = g.ApiGetAdminRecentOrders(accesstoken, customer_id, pageNo);
            //    setState(() {
            //    });
            //}



            var responseToken = g.ApiGetAdminRecentOrders(accesstoken, customer_id);

            RSO = JsonConvert.DeserializeObject<ResponseActiveOrders1>(responseToken.Content);
            foreach (var Add in RSO.response.data)
            {
                //DateTime myDate = DateTime.ParseExact(Add.created_at, "yyyy-MM-dd HH:mm:ss,fff",
                //                   System.Globalization.CultureInfo.InvariantCulture);
                //Add.created_at = myDate.ToString();
                //DateTime localDateTime = DateTime.Parse(Add.created_at);
                Add.created_at = DateTime.Parse(Add.created_at).ToString();
            }

            return PartialView("_customerActiveOrders", RSO);

        }

        /// <summary>
        /// Customer Orders History List
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public PartialViewResult CustomerOrdersHistoryList()
        {
            int customer_id = Convert.ToInt32(Session["customer_id"]);
            ResponseActiveOrders1 RSO = new ResponseActiveOrders1();
            string accesstoken = Session["access_token"].ToString();
            var responseToken = g.ApiGetAdminOrdersHistory(accesstoken, customer_id);
            RSO = JsonConvert.DeserializeObject<ResponseActiveOrders1>(responseToken.Content);
            foreach (var Add in RSO.response.data)
            {
                //DateTime myDate = DateTime.ParseExact(Add.created_at, "yyyy-MM-dd HH:mm:ss,fff",
                //                   System.Globalization.CultureInfo.InvariantCulture);
                //Add.created_at = myDate.ToString();
                //DateTime localDateTime = DateTime.Parse(Add.created_at);
                Add.created_at = DateTime.Parse(Add.created_at).ToString();
            }
            return PartialView("_custOrdersHistory", RSO);

        }


        /// <summary>
        /// Customer Active Orders Detail By Order_id
        /// </summary>
        /// <param name="Order_id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult CustomerActiveOrdersDetailbyId(int Order_id)        
        {
            ResponseHistoryOrders1 RHO = new ResponseHistoryOrders1();
            int customer_id = Convert.ToInt32(Session["customer_id"]);
            Session["Order_id"] = Order_id;
            if (Session["access_token"] != null)
            {
                string accesstoken = Session["access_token"].ToString();                
                //ResponseHistoryOrders1 RHO = new ResponseHistoryOrders1();                
                var responseToken = g.ApiGetAdminRecentOrders(accesstoken, customer_id);
                RHO = JsonConvert.DeserializeObject<ResponseHistoryOrders1>(responseToken.Content);
                RHO.response.HiddenId = Order_id;
                RHO.response.id = customer_id;
                //RHO.response.data.mWhere(a => a.id == Order_id);
                //return PartialView("_customerOrdersHistory", RHO);

                return View(RHO);


            }
            else
            {
                return RedirectToAction("Login", "Admin");

            }
            //return View(RAC);

        }

        /// <summary>
        /// Customer Orders History Detail By Order_id
        /// </summary>
        /// <param name="Order_id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult CustomerOrdersHistoryDetailbyId(int Order_id)
        {
            ResponseHistoryOrders1 RHO = new ResponseHistoryOrders1();
            int customer_id = Convert.ToInt32(Session["customer_id"]);
            if (Session["access_token"] != null)
            {
                string accesstoken = Session["access_token"].ToString();
                //ResponseHistoryOrders1 RHO = new ResponseHistoryOrders1();                
                var responseToken = g.ApiGetAdminOrdersHistory(accesstoken, customer_id);
                RHO = JsonConvert.DeserializeObject<ResponseHistoryOrders1>(responseToken.Content);
                RHO.response.id = customer_id;
                RHO.response.HiddenId = Order_id;
                //RHO.response.data.mWhere(a => a.id == Order_id);
                //return PartialView("_customerOrdersHistory", RHO);

                return View(RHO);
            }
            else
            {
                return RedirectToAction("Login", "Admin");

            }
            

        }

        
        [AllowAnonymous]
        public ActionResult CustomerDetail(int customer_id)
        {
            ResponseAdminCustomers RAC = new ResponseAdminCustomers();
            Session["customer_id"] = customer_id;
            if (Session["access_token"] != null)
            {                
                string accesstoken = Session["access_token"].ToString();
                var responseToken = g.ApiGetAdminOrdersHistory(accesstoken, customer_id);
                var responseTokenRC = g.ApiGetAdminRecentOrders(accesstoken, customer_id);               

                //if (!RAC.)
                RAC.ResponseActiveOrders = JsonConvert.DeserializeObject<ResponseActiveOrders>(responseTokenRC.Content);
                RAC.ResponseHistoryOrders = JsonConvert.DeserializeObject<ResponseHistoryOrders>(responseToken.Content);
                // RAC.ResponseActiveOrders.response.data

                foreach (var Add in RAC.ResponseActiveOrders.response.data)
                {
                    //DateTime myDate = DateTime.ParseExact(Add.created_at, "yyyy-MM-dd HH:mm:ss,fff",
                    //                   System.Globalization.CultureInfo.InvariantCulture);
                    //Add.created_at = myDate.ToString();
                    //DateTime localDateTime = DateTime.Parse(Add.created_at);
                    Add.created_at= DateTime.Parse(Add.created_at).ToString();
                }
                return View(RAC);


            }
            else
            {
                return RedirectToAction("Login", "Admin");

            }


        }


        [AllowAnonymous]
        public ActionResult preppers(string returnUrl)
        {
            ResponseActiveOrders RAO = new ResponseActiveOrders();
            if (Session["access_token"] != null)
            {

                string accesstoken = Session["access_token"].ToString();
                var responseToken = g.ApiGetAdminpreppers(accesstoken);
                RAO = JsonConvert.DeserializeObject<ResponseActiveOrders>(responseToken.Content);
                return View(RAO.response);
                //return View();

            }
            else
            {
                return RedirectToAction("Login", "Admin");

            }
            //LoginViewModel model = new LoginViewModel();
            //ViewBag.ReturnUrl = returnUrl;

        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult preppers(Customers model)
        {
            string URL = @"https://work.splitreef.com/client/development/local_prep/api/admin/preppers?keyword=shat&start_date=2022-04-01&end_date=2022-04-07";
            ResponseActiveOrders RAO = new ResponseActiveOrders();
            if (Session["access_token"] != null)
            {
                if (model.first_name == null && model.start_date != null)
                {
                    URL = @"https://work.splitreef.com/client/development/local_prep/api/admin/preppers?keyword=shat&start_date=2022-04-01&end_date=2022-04-07";
                }
                else
                {
                    URL = @"https://work.splitreef.com/client/development/local_prep/api/admin/preppers?keyword=" + model.first_name + "";
                }
                //if (model.start_date == null)
                //{

                //}

                string accesstoken = Session["access_token"].ToString();
                var responseToken = g.ApiGetAdminPrepperSearchCustomers(accesstoken, URL);
                RAO = JsonConvert.DeserializeObject<ResponseActiveOrders>(responseToken.Content);
                ModelState.Clear();
                return View(RAO.response);


            }
            else
            {
                return RedirectToAction("Login", "Admin");

            }
            //return View(RAC);
        }

        [AllowAnonymous]
        public ActionResult Earnings(string returnUrl)
        {
            Earnings Earnings = new Earnings();
            if (Session["access_token"] != null)
            {

                string accesstoken = Session["access_token"].ToString();
                var responseToken = g.ApiGetAdminPreppersEarnings(accesstoken);
                Earnings = JsonConvert.DeserializeObject<Earnings>(responseToken.Content);
                //return View(RAC.response);
                return View(Earnings.Response);


            }
            else
            {
                return RedirectToAction("Login", "Admin");

            }
            //LoginViewModel model = new LoginViewModel();
            //ViewBag.ReturnUrl = returnUrl;

        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Earnings(ResponseData model)
        {
       string URL = @"https://work.splitreef.com/client/development/local_prep/api/admin/preppers/earnings?start_date=2022-04-01&end_date=2022-04-12&keyword=ss";
            Earnings Earnings = new Earnings();
            if (Session["access_token"] != null)
            {
                if (model.start_date == null && model.end_date != null)
                {
                URL = @"https://work.splitreef.com/client/development/local_prep/api/admin/preppers/earnings?start_date=2022-04-01&end_date=2022-04-12&keyword=ss";
                }
                else
                {
                    if (model.sub_amount != null)
                    {
                        URL = @"https://work.splitreef.com/client/development/local_prep/api/admin/preppers/earnings?keyword=" + model.sub_amount + "";
                    }
                    else
                    {
                        URL = @"https://work.splitreef.com/client/development/local_prep/api/admin/preppers/earnings?start_date=2022-04-01&end_date=2022-04-12&keyword=ss";
                    }
                    
                }
                //if (model.start_date == null)
                //{

                //}

                string accesstoken = Session["access_token"].ToString();
                var responseToken = g.ApiGetAdminEarningSearch(accesstoken, URL);
                Earnings = JsonConvert.DeserializeObject<Earnings>(responseToken.Content);
                ModelState.Clear();
                return View(Earnings.Response);


            }
            else
            {
                return RedirectToAction("Login", "Admin");

            }
            //return View(RAC);
        }

        [AllowAnonymous]
        public ActionResult PreppersDetail(int customer_id)
        {
            Session["customer_id"] = customer_id;
            ResponseActiveOrders RAO = new ResponseActiveOrders();
            if (Session["access_token"] != null)
            {
                string accesstoken = Session["access_token"].ToString();
                var responseToken = g.ApiGetAdminpreppersDetails(accesstoken, customer_id);
                if (!RAO.error)
                    RAO = JsonConvert.DeserializeObject<ResponseActiveOrders>(responseToken.Content);
                return View(RAO.response);

            }
            else
            {
                return RedirectToAction("Login", "Admin");

            }
            //LoginViewModel model = new LoginViewModel();
            //ViewBag.ReturnUrl = returnUrl;

        }

        [AllowAnonymous]
        public PartialViewResult _PreppersDetail()
        {
            int customer_id = Convert.ToInt32(Session["customer_id"]);
            ResponseActiveOrders Cust = new ResponseActiveOrders();            
            string accesstoken = Session["access_token"].ToString();
            var responseToken = g.ApiGetAdminpreppersDetails(accesstoken, customer_id);
            Cust = JsonConvert.DeserializeObject<ResponseActiveOrders>(responseToken.Content);
            return PartialView("_PreppersDetail", Cust.response);

        }

        /// <summary>
        /// Preppers Active Orders List
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public PartialViewResult _PreppersActiveOrderList()
        {           

            int customer_id = Convert.ToInt32(Session["customer_id"]);
            ResponseActiveOrders1 RSO = new ResponseActiveOrders1();
            string accesstoken = Session["access_token"].ToString();
            var responseToken = g.ApiGetAdminPreppersRecentOrders(accesstoken, customer_id);
            RSO = JsonConvert.DeserializeObject<ResponseActiveOrders1>(responseToken.Content);
            foreach (var Add in RSO.response.data)
            {
                //DateTime myDate = DateTime.ParseExact(Add.created_at, "yyyy-MM-dd HH:mm:ss,fff",
                //                   System.Globalization.CultureInfo.InvariantCulture);
                //Add.created_at = myDate.ToString();
                //DateTime localDateTime = DateTime.Parse(Add.created_at);
                Add.created_at = DateTime.Parse(Add.created_at).ToString();
            }
            return PartialView("_prepperActiveOrders", RSO);

        }

        /// <summary>
        /// Preppers Order History List
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public PartialViewResult _PreppersOrderHistoryList()
        {

            int customer_id = Convert.ToInt32(Session["customer_id"]);
            ResponseActiveOrders1 RSO = new ResponseActiveOrders1();
            string accesstoken = Session["access_token"].ToString();
            var responseToken = g.ApiGetAdminPreppersOrdersHistory(accesstoken, customer_id);
            RSO = JsonConvert.DeserializeObject<ResponseActiveOrders1>(responseToken.Content);
            foreach (var Add in RSO.response.data)
            {
                //DateTime myDate = DateTime.ParseExact(Add.created_at, "yyyy-MM-dd HH:mm:ss,fff",
                //                   System.Globalization.CultureInfo.InvariantCulture);
                //Add.created_at = myDate.ToString();
                //DateTime localDateTime = DateTime.Parse(Add.created_at);
                Add.created_at = DateTime.Parse(Add.created_at).ToString();
            }
            return PartialView("_prepperOrdersHistory", RSO);

        }

        /// <summary>
        /// Preppers Active Order Details by Id
        /// </summary>
        /// <param name="Order_id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult PreppersActiveOrderDetailsbyId(int Order_id)
        {
            ResponseHistoryOrders1 RHO = new ResponseHistoryOrders1();
            int customer_id = Convert.ToInt32(Session["customer_id"]);
            Session["Order_id"] = Order_id;
            if (Session["access_token"] != null)
            {
                string accesstoken = Session["access_token"].ToString();                            
                var responseToken = g.ApiGetAdminPreppersRecentOrders(accesstoken, customer_id);
                RHO = JsonConvert.DeserializeObject<ResponseHistoryOrders1>(responseToken.Content);
                RHO.response.HiddenId = Order_id;                
                return View(RHO);


            }
            else
            {
                return RedirectToAction("Login", "Admin");

            }
            

        }

        /// <summary>
        /// Prepper Order History Details by Id
        /// </summary>
        /// <param name="Order_id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult PrepperOrderHistoryDetailsbyId(int Order_id)
        {
            ResponseHistoryOrders1 RHO = new ResponseHistoryOrders1();
            int customer_id = Convert.ToInt32(Session["customer_id"]);
            if (Session["access_token"] != null)
            {
                string accesstoken = Session["access_token"].ToString();
                //ResponseHistoryOrders1 RHO = new ResponseHistoryOrders1();                
                var responseToken = g.ApiGetAdminPreppersOrdersHistory(accesstoken, customer_id);
                RHO = JsonConvert.DeserializeObject<ResponseHistoryOrders1>(responseToken.Content);
                RHO.response.HiddenId = Order_id;
                //RHO.response.data.mWhere(a => a.id == Order_id);
                //return PartialView("_customerOrdersHistory", RHO);

                return View(RHO);


            }
            else
            {
                return RedirectToAction("Login", "Admin");

            }


        }


        [AllowAnonymous]
        public ActionResult EarningDetail(int Order_id)
        {
            Earnings Earnings = new Earnings();
            if (Session["access_token"] != null)
            {
                string accesstoken = Session["access_token"].ToString();
                var responseToken = g.ApiGetAdminPreppersEarnings(accesstoken);
                Earnings = JsonConvert.DeserializeObject<Earnings>(responseToken.Content);
                Earnings.Response.Hidden_id = Order_id;
                Session["Order_id"] = Order_id;
                //return View(RAC.response);
                return View(Earnings);
            }
            else
            {
                return RedirectToAction("Login", "Admin");

            }
            //LoginViewModel model = new LoginViewModel();
            //ViewBag.ReturnUrl = returnUrl;

        }


        [AllowAnonymous]
        public PartialViewResult _deliveryaddress()
        {
            Earnings Earnings = new Earnings();           
            string accesstoken = Session["access_token"].ToString();
            var responseToken = g.ApiGetAdminPreppersEarnings(accesstoken);
            Earnings = JsonConvert.DeserializeObject<Earnings>(responseToken.Content);
            Earnings.Response.Hidden_id = Convert.ToInt32(Session["Order_id"]);            
            return PartialView("_deliveryaddress", Earnings);
         
        }

        [AllowAnonymous]
        public ActionResult _customerdeliveryaddress()
        {
            ResponseHistoryOrders1 RHO = new ResponseHistoryOrders1();
            int customer_id = Convert.ToInt32(Session["customer_id"]);
               string accesstoken = Session["access_token"].ToString();
                //ResponseHistoryOrders1 RHO = new ResponseHistoryOrders1();                
                var responseToken = g.ApiGetAdminRecentOrders(accesstoken, customer_id);
                RHO = JsonConvert.DeserializeObject<ResponseHistoryOrders1>(responseToken.Content);
                RHO.response.HiddenId = Convert.ToInt32(Session["Order_id"]);
            foreach (var Add in RHO.response.data)
            {
                //DateTime myDate = DateTime.ParseExact(Add.created_at, "yyyy-MM-dd HH:mm:ss,fff",
                //                   System.Globalization.CultureInfo.InvariantCulture);
                //Add.created_at = myDate.ToString();
                //DateTime localDateTime = DateTime.Parse(Add.created_at);
                Add.created_at = DateTime.Parse(Add.created_at).ToString();
            }
             return PartialView("_customerdeliveryaddress", RHO);

        }

        [AllowAnonymous]
        public ActionResult _prepperdeliveryaddress()
        {
            ResponseActiveOrders1 RHO = new ResponseActiveOrders1();
            int customer_id = Convert.ToInt32(Session["customer_id"]);
            string accesstoken = Session["access_token"].ToString();
            //ResponseHistoryOrders1 RHO = new ResponseHistoryOrders1();                
            var responseToken = g.ApiGetAdminPreppersRecentOrders(accesstoken, customer_id);
            RHO = JsonConvert.DeserializeObject<ResponseActiveOrders1>(responseToken.Content);
            RHO.response.HiddenId = Convert.ToInt32(Session["Order_id"]);
            foreach (var Add in RHO.response.data)
            {
                //DateTime myDate = DateTime.ParseExact(Add.created_at, "yyyy-MM-dd HH:mm:ss,fff",
                //                   System.Globalization.CultureInfo.InvariantCulture);
                //Add.created_at = myDate.ToString();
                //DateTime localDateTime = DateTime.Parse(Add.created_at);
                Add.created_at = DateTime.Parse(Add.created_at).ToString();
            }
            return PartialView("_prepperDeliveryaddress", RHO);

        }




        [HttpGet]
        [AllowAnonymous]
        public ActionResult Profile()
        {
            //NotificationsCount();
            string Token = Session["access_token"].ToString();
            ResponseStates rs = new ResponseStates();
            General g = new General();
            var responseState = g.Apistatelist();
            rs = JsonConvert.DeserializeObject<ResponseStates>(responseState.Content);
            ResponseProfile rp = new ResponseProfile();                                
            var responseProfile = g.ApiGetAdminprofile(Token);
            rp = JsonConvert.DeserializeObject<ResponseProfile>(responseProfile.Content);
            return View(rp);

        }

        [AllowAnonymous]
        public ActionResult Getdashboard()
        {

            //string role = string.Empty;

            //if (Session["role"] != null)
            //    role = Session["role"].ToString();

            Responsedashboard RD = new Responsedashboard();
            if (Session["access_token"] != null)
            {                
                    string accesstoken = Session["access_token"].ToString();
                    var responseToken = g.ApiGetAdminDashboard(accesstoken);
                    RD = JsonConvert.DeserializeObject<Responsedashboard>(responseToken.Content);
                    return View(RD);
             }
            else
            {
                return RedirectToAction("Login", "Admin");

            }






        }


        [AllowAnonymous]
        public ActionResult GetData()
        {
            GraphData objGraphData = new GraphData();
            objGraphData = GettotalSellAcoordingmonth();
            return Json(objGraphData, JsonRequestBehavior.AllowGet);
        }
        public GraphData GettotalSellAcoordingmonth()
        {


            //string Gjson= "[{\"Month\":\"Jan\",\"Sales\":600,\"Perc\":5},{\"Month\":\"Feb\",\"Sales\":1200,\"Perc\":15},{\"Month\":\"Mar\",\"Sales\":1900,\"Perc\":11},{\"Month\":\"Apr\",\"Sales\":800,\"Perc\":17},{\"Month\":\"May\",\"Sales\":260,\"Perc\":5},{\"Month\":\"Jun\",\"Sales\":3100,\"Perc\":2},{\"Month\":\"Jul\",\"Sales\":370,\"Perc\":28},{\"Month\":\"Aug\",\"Sales\":1600,\"Perc\":14},{\"Month\":\"Sep\",\"Sales\":2500,\"Perc\":19},{\"Month\":\"Oct\",\"Sales\":300,\"Perc":8},{\"Month\":"Nov","Sales":3000,"Perc":8},{"Month":"Dec","Sales":700,"Perc":8}]"


            GraphData objGraphData = new GraphData();
            List<TotalSell> objlsttotallSell = new List<TotalSell>();
            List<SellMonth> objlstmonthname = new List<SellMonth>();
            TotalSell objtotalsell = new TotalSell();
            SellMonth objSellMonth = new SellMonth();
            objtotalsell.totalSell = 10;
            objlsttotallSell.Add(objtotalsell);
            TotalSell objtotalsell1 = new TotalSell();
            objtotalsell1.totalSell =10;
            objlsttotallSell.Add(objtotalsell1);
            TotalSell objtotalsell2 = new TotalSell();
            objtotalsell2.totalSell = 10;
            objlsttotallSell.Add(objtotalsell2);
            TotalSell objtotalsell3 = new TotalSell();
            objtotalsell3.totalSell = 12483;
            objlsttotallSell.Add(objtotalsell3);
            //TotalSell objtotalsell4 = new TotalSell();
            //objtotalsell4.totalSell = 800;
            //objlsttotallSell.Add(objtotalsell4);
            //TotalSell objtotalsell5 = new TotalSell();
            //objtotalsell5.totalSell = 900;
            //objlsttotallSell.Add(objtotalsell5);
            //TotalSell objtotalsell6 = new TotalSell();
            //objtotalsell6.totalSell = 1000;
            //objlsttotallSell.Add(objtotalsell6);
            //TotalSell objtotalsell7 = new TotalSell();
            //objtotalsell7.totalSell = 300;
            //objlsttotallSell.Add(objtotalsell7);
            //TotalSell objtotalsell8 = new TotalSell();
            //objtotalsell8.totalSell = 200;
            //objlsttotallSell.Add(objtotalsell8);
            //TotalSell objtotalsell9 = new TotalSell();
            //objtotalsell9.totalSell = 100;
            //objlsttotallSell.Add(objtotalsell9);
            //TotalSell objtotalsell10 = new TotalSell();
            //objtotalsell10.totalSell = 1100;
            //objlsttotallSell.Add(objtotalsell10);
            //TotalSell objtotalsell11 = new TotalSell();
            //objtotalsell11.totalSell = 1200;
            //objlsttotallSell.Add(objtotalsell11);
            objGraphData.totalsell = objlsttotallSell;
            objSellMonth.monthName = "Jan";
            objlstmonthname.Add(objSellMonth);
            SellMonth objSellMonth1 = new SellMonth();
            objSellMonth1.monthName = "Feb";
            objlstmonthname.Add(objSellMonth1);
            SellMonth objSellMonth2 = new SellMonth();
            objSellMonth2.monthName = "Mar";
            objlstmonthname.Add(objSellMonth2);
            SellMonth objSellMonth3 = new SellMonth();
            objSellMonth3.monthName = "Apr";
            objlstmonthname.Add(objSellMonth3);
            SellMonth objSellMonth4 = new SellMonth();
            objSellMonth4.monthName = "May";
            objlstmonthname.Add(objSellMonth4);
            SellMonth objSellMonth5 = new SellMonth();
            objSellMonth5.monthName = "Jun";
            objlstmonthname.Add(objSellMonth5);
            SellMonth objSellMonth6 = new SellMonth();
            objSellMonth6.monthName = "Jul";
            objlstmonthname.Add(objSellMonth6);
            SellMonth objSellMonth7 = new SellMonth();
            objSellMonth7.monthName = "Aug";
            objlstmonthname.Add(objSellMonth7);
            SellMonth objSellMonth8 = new SellMonth();
            objSellMonth8.monthName = "Sep";
            objlstmonthname.Add(objSellMonth8);
            SellMonth objSellMonth9 = new SellMonth();
            objSellMonth9.monthName = "Oct";
            objlstmonthname.Add(objSellMonth9);
            SellMonth objSellMonth10 = new SellMonth();
            objSellMonth10.monthName = "Nov";
            objlstmonthname.Add(objSellMonth10);
            SellMonth objSellMonth11 = new SellMonth();
            objSellMonth11.monthName = "Dec";
            objlstmonthname.Add(objSellMonth11);
            objGraphData.monthname = objlstmonthname;
            return objGraphData;
        }


        [AllowAnonymous]
        public PartialViewResult Notifications()
        {
            int Ncount = NotificationsCount();      
            ResponseNotifications RN = new ResponseNotifications();
            string accesstoken = Session["access_token"].ToString();
            var responseNotifications = g.ApiGetAdminNotifications(accesstoken);
            RN = JsonConvert.DeserializeObject<ResponseNotifications>(responseNotifications.Content);
            if (!RN.error)
            {
                RN.count = Ncount;               

            }
            return PartialView("_Notifications", RN);

        }

        //
        // GET: /Admin/Notifications
        //[AllowAnonymous]
        //public ActionResult Notifications()
        //{
            
        //    if (Session["access_token"] != null)
        //    {
        //        int Ncount = NotificationsCount();
        //        string Token = Session["access_token"].ToString();
        //        ResponseNotifications RN = new ResponseNotifications();                
        //            var responseNotifications = g.ApiGetAdminNotifications(Token);
        //            RN = JsonConvert.DeserializeObject<ResponseNotifications>(responseNotifications.Content);

        //            if (!RN.error)
        //            {
        //                RN.count = Ncount;
        //                return View(RN);

        //            }
        //            AddErrors(RN.message);
        //            return View(RN);
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login", "Admin");

        //    }
        //}

        [AllowAnonymous]
        public int NotificationsCount()
        {
            ResponseNotificationsCount RNC = new ResponseNotificationsCount();
            if (Session["access_token"] != null)
            {               
                string Token = Session["access_token"].ToString();
                              
                    var responseNotifications = g.ApiGetAdminNotificationscount(Token);
                    RNC = JsonConvert.DeserializeObject<ResponseNotificationsCount>(responseNotifications.Content);
                    if (!RNC.error)
                    {
                        return RNC.response.count;
                    }
                    AddErrors(RNC.message);
                    
            }
            else
            {
                return 0;
            }
            return 0;
        }



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
        // POST: /Account/LogOff

        [AllowAnonymous]
        public ActionResult LogOff()
        {
            Session["IsLogin"] = true;
            Session.Abandon();
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Admin");
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