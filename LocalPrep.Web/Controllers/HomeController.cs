using LocalPrep.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocalPrep.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HomeViewModel viewModel = new HomeViewModel();           

            return View(viewModel);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return View(contact);
            }

            using (var model = new localprepdbEntities())
            {
                contact.SubmitDt = DateTime.Now;
                model.Contacts.Add(contact);
                model.SaveChanges();
            }

            ViewBag.Success = "Yes";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Search(int id)
        {
            if (!ModelState.IsValid)
            {
                return View(id);
            }

            
            return View();
        }


        public ActionResult Faq()
        {
            return View();
        }

        public ActionResult Diets()
        {
            using (var model = new localprepdbEntities())
            {
                List<Diet> diets = model.Diets.Where(x => x.IsActive == true).ToList();
                return View(diets);
            }
            
        }

        public ActionResult Cuisines()
        {
            using (var model = new localprepdbEntities())
            {
                List<Cuisine> cuisines = model.Cuisines.Where(x => x.IsActive == true).ToList();
                return View(cuisines);
            }
        }

        public ActionResult OurMealPreppers()
        {
            return View();
        }

        public ActionResult HowItWorks()
        {
            return View();
        }
    }
}