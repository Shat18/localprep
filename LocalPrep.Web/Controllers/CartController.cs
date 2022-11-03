using LocalPrep.Web.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocalPrep.Web.Controllers
{
    public class CartController : Controller
    {
        General g = new General();
        // GET: Cart
        public ActionResult Index()
        {
            if (Session["access_token"] != null)
            {
                ResponseSearch RS = new ResponseSearch();
                ResponseAddress RA = new ResponseAddress();
                ResponseCardInfo RCI = new ResponseCardInfo();
                string Token = Session["access_token"].ToString();
                var Response = g.ApiGetCart(Token);

                RS = JsonConvert.DeserializeObject<ResponseSearch>(Response.Content);
                if (!RS.error)
                {
                    var Address = g.ApiCustomeraddresses(Token);
                    RA = JsonConvert.DeserializeObject<ResponseAddress>(Address.Content);
                    var responseCard = g.ApiGetcustomercards(Token);
                    RS.response.address = RA.response;
                    RCI = JsonConvert.DeserializeObject<ResponseCardInfo>(responseCard.Content);
                    if (!RCI.error)
                    {
                        RS.response.cardProfile = RCI.response.cardProfile;
                    }
                    return View(RS);
                }
                return View(RS);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

            
        }


        //public ActionResult AddToCartFromAPI(int mealId,int mealQty)
        //{
        //    ResponseSearch RS = new ResponseSearch();           
        //    string Token = Session["access_token"].ToString();
        //    var Response = g.ApiGetCart(Token);

        //    RS = JsonConvert.DeserializeObject<ResponseSearch>(Response.Content);
        //    if (!RS.error)
        //    {
        //        var Address = g.ApiCustomeraddresses(Token);
        //        RA = JsonConvert.DeserializeObject<ResponseAddress>(Address.Content);
        //        var responseCard = g.ApiGetcustomercards(Token);
        //        RS.response.address = RA.response;
        //        RCI = JsonConvert.DeserializeObject<ResponseCardInfo>(responseCard.Content);
        //        if (!RCI.error)
        //        {
        //            RS.response.cardProfile = RCI.response.cardProfile;
        //        }

        //        return View(RS);
        //    }
        //    return View(RS);


        //}

        // POST: AddToCart
        [HttpPost]
        public JsonResult AddToCart(int mealId, string pickupDelivery, string pickupDeliveryDt, string[] removeIngredients, string[] addOns, int mealQty)
        {

            string Token = Session["access_token"].ToString();
            LoginPartial loginPartial = new LoginPartial();
            ResponseSearch RS = new ResponseSearch();

           

            var Response = g.ApiAddCart(Token, mealId, mealQty);           
            RS = JsonConvert.DeserializeObject<ResponseSearch>(Response.Content);
            if (RS != null)
            {
                if (!RS.error)
                {
                    var ResponseCart = g.ApiGetCart(Token);
                    RS = JsonConvert.DeserializeObject<ResponseSearch>(ResponseCart.Content);
                    if (!RS.error)
                    {
                        loginPartial.CartCount = RS.response.items.Count;
                        //RS.message = "";

                         //RedirectToAction("Meal", "Order");
                        //loginPartial.NotificationCount = Ncount;
                    }
                    return Json(RS.response.items.Count);
                }
                
                return Json(RS.response.items.Count);
            }
            else
            {
                return Json(RS.response.items.Count);
            }

        }


        /// <summary>
        /// Remove Item from add to cart list 
        /// created by piyush on 06082021
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult DeleteConfirmed(int Id)
        {
            string Token = Session["access_token"].ToString();
            var Response = g.ApiDeleteCart(Token, Id);            
            ResponseSearch RS = new ResponseSearch();
            RS = JsonConvert.DeserializeObject<ResponseSearch>(Response.Content);
            if (!RS.error)
            {
                return RedirectToAction("Index");
            }
            return View();
           
        }

        [HttpPost]
        public ActionResult SaveCustomerAddress(string address_line_1, string address_line_2, string state, string city, string zip_code)
        {

            ResponseSearch RS = new ResponseSearch();
            ResponseAddress RA = new ResponseAddress();
            string Token = Session["access_token"].ToString();

            var Response = g.ApiSaveCustomeraddresses(Token, address_line_1, address_line_2, state, city, zip_code);

            RA = JsonConvert.DeserializeObject<ResponseAddress>(Response.Content);





            return Json("");
        }

    }
}