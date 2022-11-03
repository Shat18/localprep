using LocalPrep.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocalPrep.Web.Controllers
{
    public class LayoutController : Controller
    {
        General g = new General();
        [ChildActionOnly]
        [ActionName("_PrepperMenu")]
        public ActionResult PrepperMenu()
        {
            if (Session["access_token"] != null)
            {
                LoginPartial loginPartial = new LoginPartial();
                loginPartial.Fullname = Session["Fullname"].ToString();
                //string accesstoken = Session["access_token"].ToString();
                if (Session["role"] != null)
                    loginPartial.role = Session["role"].ToString();

                if (Session["Photo"] != null)
                {
                    loginPartial.Photo = Session["Photo"].ToString();
                }
                else
                {
                    loginPartial.Photo = null;
                }

                return PartialView("_PrepperMenu", loginPartial);
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }

        }



        //public ActionResult NotificationsCount()
        //{

        //    if (Session["access_token"] != null)
        //    {
        //        string Role = string.Empty;
        //        if (Session["role"] != null)
        //            Role = Session["role"].ToString();
        //        string Token = Session["access_token"].ToString();

        //        ResponseNotificationsCount RNC = new ResponseNotificationsCount();
        //        if (Role != "CUSTOMER")
        //        {
        //            var responseNotifications = g.ApiGetNotificationsPreppercount(Token);
        //            RNC = JsonConvert.DeserializeObject<ResponseNotificationsCount>(responseNotifications.Content);

        //            if (!RNC.error)
        //            {
        //                return View(RNC.response.count);

        //            }

        //            AddErrors(RNC.message);
        //            return View(RNC.response.count);

        //        }
        //        else
        //        {
        //            var responseNotifications = g.ApiGetNotificationscustomer(Token);
        //            RNC = JsonConvert.DeserializeObject<ResponseNotificationsCount>(responseNotifications.Content);

        //            if (!RNC.error)
        //            {
        //                return View(RNC.response.count);

        //            }

        //            AddErrors(RNC.message);
        //            return View(RNC.response.count);
        //        }

        //    }
        //    else
        //    {
        //        return RedirectToAction("Login", "Admin");

        //    }




        //}




        [ChildActionOnly]
        public PartialViewResult CartCount()
        {
            LoginPartial loginPartial = new LoginPartial();
            string role = string.Empty;

            if (Session["role"] != null)
            {
                role = Session["role"].ToString();
                loginPartial.role = role;
            }
            int Ncount = 0;
            string userId = User.Identity.GetUserId();
            ResponseNotificationsCount RNC = new ResponseNotificationsCount();
            ResponseSearch RS = new ResponseSearch();
            if (Session["access_token"] != null)
            {
                if (role == "CUSTOMER")
                {
                    string Token = Session["access_token"].ToString();
                    var responseNotifications = g.ApiGetNotificationsCustomercount(Token);
                    RNC = JsonConvert.DeserializeObject<ResponseNotificationsCount>(responseNotifications.Content);
                    if (!RNC.error)
                    {
                        Ncount = RNC.response.count;
                    }
                    var Response = g.ApiGetCart(Token);
                    RS = JsonConvert.DeserializeObject<ResponseSearch>(Response.Content);
                    if (!RS.error)
                    {
                        loginPartial.CartCount = RS.response.items.Count;
                        loginPartial.role = role;
                        loginPartial.NotificationCount = Ncount;
                    }
                    loginPartial.role = role;
                    loginPartial.NotificationCount = Ncount;

                }
                else
                {
                    string Token = Session["access_token"].ToString();
                    var responseNotifications = g.ApiGetNotificationsPreppercount(Token);
                    RNC = JsonConvert.DeserializeObject<ResponseNotificationsCount>(responseNotifications.Content);
                    if (!RNC.error)
                    {
                        Ncount = RNC.response.count;
                        loginPartial.NotificationCount = Ncount;
                    }
                    loginPartial.CartCount = 0;

                }


            }
            else
            {
                loginPartial.CartCount = 0;
                loginPartial.role = role;
                loginPartial.NotificationCount = Ncount;

            }


            //loginPartial.CartCount = RS.response.items.Count;
            if (Session["IsLogin"] != null)
            {
                loginPartial.IsLogin = Convert.ToBoolean(Session["IsLogin"]);
            }
            else
            {
                loginPartial.IsLogin = true;
            }
            if (Session["HideMenu"] != null)
            {
                loginPartial.IsVerified = Convert.ToBoolean(Session["HideMenu"]);
            }
            else
            {
                loginPartial.IsVerified = false;
            }
            if (Session["Isregistered"] != null)
            {
                loginPartial.Isregistered = Convert.ToBoolean(Session["Isregistered"]);
            }
            else
            {
                loginPartial.Isregistered = true;
            }

            return PartialView("_LoginPartial", loginPartial);
            //}
        }
    }
}