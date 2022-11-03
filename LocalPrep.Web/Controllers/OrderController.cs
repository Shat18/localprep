using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Controllers.Bases;
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
    public class OrderController : Controller
    {
        General g = new General();
        // GET: Order
        public ActionResult Meal(int? mealId)
        {
            ResponseMeals rm = new ResponseMeals();
            if (Session["access_token"] != null)
            {
                if (mealId == 0)
                {
                    return RedirectToAction("Index", "Home");
                    //RedirectToAction("Index", "Home");
                }
                string accesstoken = Session["access_token"].ToString();                
                var response = g.ApiCustomerMealId(accesstoken, mealId);
                rm = JsonConvert.DeserializeObject<ResponseMeals>(response.Content);
                return View(rm.response);
            }
            else
            {
                //return RedirectToAction("Index", "Home");
                return RedirectToAction("Login", "Account");
            }
           

        }

        

        [HttpPost]
        public JsonResult Pay(string cardNumber, string cardYear, string cardCVV, int Id, string customerpaymentprofileid, string pickupDelivery, string pickupDeliveryDt,bool paymentFlag)
        {
            string CustomercardYearNew = "2025-12";
            paymentFlag = true;
            string cardnumberNew = cardNumber;
            cardnumberNew = cardnumberNew.Replace(" ", "");
            ResponseSearch RS = new ResponseSearch();
            ResponseAddress RA = new ResponseAddress();
            string Token = Session["access_token"].ToString();
            if (customerpaymentprofileid == null)
                paymentFlag = false;
                var Response = g. Apiorderscheckout(Token, cardnumberNew, CustomercardYearNew, cardCVV, Id, customerpaymentprofileid, pickupDelivery, pickupDeliveryDt, paymentFlag);

            RS = JsonConvert.DeserializeObject<ResponseSearch>(Response.Content);           

            return Json(RS.message);
        }

        
    }
}