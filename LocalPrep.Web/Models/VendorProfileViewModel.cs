using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalPrep.Web.Models
{
    public class VendorProfileViewModel
    {
        public Vendor Vendor { get; set; }
        public List<Meal> Meals { get; set; }
        public int TotalMealRatings { get; set; }
        public double MealRating { get; set; }
        public IEnumerable<string> Cuisines { get; set; }

        public List<MealRating> reviews { get; set; }
    }

    //#region Login response
    ///// <summary>
    /////Profile response
    ///// </summary>
    //public class ResponseProfile
    //{
    //    public string message { get; set; }

    //    public bool error { get; set; }

    //    public response response { get; set; }
    //}

    //public class response
    //{
    //    public user user { get; set; }
    //    //public token_info token_info { get; set; }

    //}


    //public class user
    //{
    //    public int id { get; set; }
    //    public string first_name { get; set; }
    //    public string last_name { get; set; }
    //    public string email { get; set; }
    //    public string phone { get; set; }
    //    public string email_verified_at { get; set; }
    //    public string status { get; set; }
    //    public account account { get; set; }
    //    public business business { get; set; }
    //    public List<addressess> addresses { get; set; }

    //    public string created_at { get; set; }
    //    public string updated_at { get; set; }



    //}

    //public class token_info
    //{
    //    public string access_token { get; set; }

    //}

    //public class account
    //{
    //    public int id { get; set; }
    //    public int user_id { get; set; }
    //    public string role { get; set; }
    //    public string photo { get; set; }
    //    public string created_at { get; set; }
    //    public string updated_at { get; set; }

    //}

    //public class business
    //{
    //    public int id { get; set; }
    //    public int user_id { get; set; }
    //    public string vendor_name { get; set; }
    //    public string company_name { get; set; }
    //    public string website { get; set; }
    //    public string about_yourself { get; set; }
    //    public string favorite_cuisine_to_cook { get; set; }
    //    public string describe_cooking_style { get; set; }
    //    public string describe_business { get; set; }
    //    public string delivery_available { get; set; }
    //    public string pickup_available { get; set; }
    //    public List<diet_specialities> dietspecialities { get; set; }
    //    public List<cuisine_specialities> cuisine_specialities { get; set; }
    //}

    //public class addressess
    //{
    //    public int id { get; set; }
    //    public int user_id { get; set; }
    //    public string type { get; set; }
    //    public string email { get; set; }
    //    public string phone { get; set; }
    //    public string address_line_1 { get; set; }
    //    public string address_line_2 { get; set; }
    //    public string state { get; set; }
    //    public string city { get; set; }
    //    public string zip_code { get; set; }
    //    public string created_at { get; set; }
    //    public string updated_at { get; set; }

    //}

    //public class cuisine_specialities
    //{
    //    public int id { get; set; }
    //    public string name { get; set; }

    //}

    ////End Login Response
    //#endregion
}