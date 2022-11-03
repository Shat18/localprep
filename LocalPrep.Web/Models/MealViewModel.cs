using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalPrep.Web.Models
{
    public class MealViewModel
    {
        public Meal meal { get; set; }

        [Display(Name = "Cuisine Specialties")]
        public List<string> CuisinesSelected { get; set; }

        [Display(Name = "Diet Specialties")]
        public List<string> DietsSelected { get; set; }

        public List<CheckBoxListItem> Diets { get; set; }
        public List<CheckBoxListItem> Cuisines { get; set; }

        public List<MealIngredient> MealIngredients { get; set; }

        [Display(Name = "Ingredient")]
        public List<MealAddOn> MealAddOns { get; set; }

        [Required]
        public HttpPostedFileBase ImageFile { get; set; }

        public List<dietspecialities> diet_specialities { get; set; }


        public List<cuisinespecialities> cuisine_specialities { get; set; }

        //[Required]
        public List<AddOn> AddON { get; set; }

        //[Required]
        public List<AddIngredient> AddIngredient { get; set; }

        public string Active { get; set; }
        public string Otherdiet { get; set; }
        public string Othercuisine { get; set; }

        /// <summary>
        /// Registration check
        /// </summary>


    }

    public class ResponseMeals
    {
        public string message { get; set; }

        public bool error { get; set; }

        public responsedata response { get; set; }

    }

    public class responsedata
    {
        public int prepper_id { get; set; }

        public int customer_id { get; set; }
        //public customer[] customer { get; set; }
        public customer customer { get; set; }       
        public string sub_total { get; set; }
        public string tax { get; set; }
        public string tax_amount { get; set; }
        public string total { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }

        public string roleResponse { get; set; }

        public List<payments> payments { get; set; }

        public List<Meals> Meals { get; set; }

        //----------------------------------------------------
        public List<data> data { get; set; }
        public int id { get; set; }

        [Display(Name = "Meal Name")]
        public string name { get; set; }
        [Display(Name = "Meal Price")]
        public string price { get; set; }
        [Display(Name = "Meal Description ")]
        public string description { get; set; }

        public string user_id { get; set; }

        public HttpPostedFileBase ImageFileE { get; set; }
        public string image { get; set; }

        public string nutritional_image { get; set; }
        [Display(Name = "Heating Instructions")]

        public string heating_instructions { get; set; }
        [Display(Name = "Active")]
        public string status { get; set; }

        public bool active { get; set; }

        public List<nutritional_info> nutritional_info { get; set; }
        public List<AddOn> add_on { get; set; }
        public string[] ingredients { get; set; }
        public List<AddIngredient> ingredientsl { get; set; }

        public List<dietspecialities> diet_specialities_respo { get; set; }


        public List<cuisinespecialities> cuisine_specialities_respo { get; set; }

        public List<cuisine_specialities> cuisine_specialities { get; set; }

        public List<diet_specialities> diet_specialities { get; set; }

        public List<CheckBoxListItem> lstcuisinespecialities { get; set; }
        public List<CheckBoxListItem> lstdietspecialities { get; set; }

        [Display(Name = "Cuisine Specialties")]
        public List<string> CuisinesSelected { get; set; }

        [Display(Name = "Diet Specialties")]
        public List<string> DietsSelected { get; set; }
        [Required]
        public HttpPostedFileBase ImageFile { get; set; }
        public string Otherdiet { get; set; }
        public string Othercuisine { get; set; }

    }

    public class data
    {
        public int id { get; set; }
        public string name { get; set; }
        public string price { get; set; }
        public string user_id { get; set; }

        public List<Meals> Meals { get; set; }




    }

    public class nutritional_info
    {
        public string nutritional_name { get; set; }
        public string nutritional_value { get; set; }

    }

    public class AddOn
    {
        public string add_on_name { get; set; }
        public string add_on_price { get; set; }
    }

    public class AddIngredient
    {
        public string IngredientName { get; set; }

    }

    public class customer
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string email_verified_at { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public delivery_address delivery_address { get; set; }


    }

    public class delivery_address
    {
        public int user_id { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address_line_1 { get; set; }
        public string address_line_2 { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string zip_code { get; set; }

    }

    public class payments
    {
        public int id { get; set; }
        public int customer_id { get; set; }
        public int order_id { get; set; }
        public string transaction_id { get; set; }
        public int customer_profile_id { get; set; }
        public string amount { get; set; }
        public string status { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }

    }
    public class Meals
    {
        public int id { get; set; }

        public int user_id { get; set; }

        public string name { get; set; }
        public string image { get; set; }
        public string description { get; set; }
        public string price { get; set; }
        public HttpPostedFileBase ImageFileE { get; set; }

        public string quantity { get; set; }
        public string nutritional_image { get; set; }
        [Display(Name = "Heating Instructions")]

        public string heating_instructions { get; set; }
        [Display(Name = "Active")]
        public string status { get; set; }

        public bool active { get; set; }

        public List<nutritional_info> nutritional_info { get; set; }
        public List<AddOn> add_on { get; set; }
        public string[] ingredients { get; set; }
        public List<AddIngredient> ingredientsl { get; set; }

        public List<dietspecialities> diet_specialities_respo { get; set; }


        public List<cuisinespecialities> cuisine_specialities_respo { get; set; }

        public List<cuisine_specialities> cuisine_specialities { get; set; }

        public List<diet_specialities> diet_specialities { get; set; }

        public List<CheckBoxListItem> lstcuisinespecialities { get; set; }
        public List<CheckBoxListItem> lstdietspecialities { get; set; }

        [Display(Name = "Cuisine Specialties")]
        public List<string> CuisinesSelected { get; set; }

        [Display(Name = "Diet Specialties")]
        public List<string> DietsSelected { get; set; }
        [Required]
        public HttpPostedFileBase ImageFile { get; set; }


    }


}








