using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LocalPrep.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Contact",
                url: "contact-us",
                defaults: new { controller = "Home", action = "Contact", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Manage",
                url: "my-account",
                defaults: new { controller = "Manage", action = "Orders", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Register",
                url: "user-registration",
                defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "BecomePrepper",
               url: "become-a-prepper",
               defaults: new { controller = "Account", action = "BecomePrepper", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "HowItWorks",
                url: "how-it-works",
                defaults: new { controller = "Home", action = "HowItWorks", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "OurMealPreppers",
                url: "our-meal-preppers",
                defaults: new { controller = "Home", action = "OurMealPreppers", id = UrlParameter.Optional }
            );  

            routes.MapRoute(
                name: "About",
                url: "about",
                defaults: new { controller = "Home", action = "About", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
