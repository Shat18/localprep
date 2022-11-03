using LocalPrep.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalPrep.Web
{
    [MetadataType(typeof(VendorMetaData))]
    public partial class Vendor
    {
        public List<Models.StateList> States { get; set; }

        [Display(Name = "Profile Pic")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageFile { get; set; }

        [Display(Name = "Cuisine Specialties")]
        public List<string> CuisinesSelected { get; set; }

        [Display(Name = "Diet Specialties")]
        public List<string> DietsSelected { get; set; }

        public List<CheckBoxListItem> Diets { get; set; }
        public List<CheckBoxListItem> Cuisines { get; set; }

        
        public List<dietspecialities> diet_specialities { get; set; }
      

        public List<cuisinespecialities> cuisine_specialities { get; set; }

        [Required]
        [Display(Name = "State")]
        public string short_code { get; set; }

        [Display(Name = "Website")]
        public string Website { get; set; }

        public string Otherdiet { get; set; }

        public string Othercuisine { get; set; }


    }

    public class VendorMetaData
    {
        [Required]
        [Display(Name = "Vendor Name")]
        public string VendorName { get; set; }

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Display(Name = "Are You Licensed?")]
        public bool LicensedInState { get; set; }

        [Display(Name = "License Number")]
        public string LicenseNo { get; set; }

        [Required]
        [Display(Name = "Address 1")]
        public string Address1 { get; set; }

        [Display(Name = "Address 2")]
        public string Address2 { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        //[Required]
        //[Display(Name = "State")]
        //public int StateId { get; set; }

        [Required]
        [Display(Name = "State")]
        public int short_code { get; set; }

        [Required]
        [Display(Name = "Zip")]
        public string Zip { get; set; }

        [Required]
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

        [Display(Name = "Describe Your Business")]
        public string VendorDescription { get; set; }

        [Display(Name = "About Yourself")]
        public string AboutYourself { get; set; }

        [Display(Name = "Favorite Things to Cook")]
        public string FavoriteThingsToCook { get; set; }

        [Display(Name = "Delivery Available")]
        public bool DeliveryAvailable { get; set; }

        [Display(Name = "Pickup Available")]
        public bool PickupAvailable { get; set; }

        public string ImgSrc { get; set; }

       





    }


    #region Login response
    /// <summary>
    /// Login response
    /// </summary>
    public class ResRegFirst
    {
        public string message { get; set; }

        public bool error { get; set; }

        public response response { get; set; }
    }

    public class response
    {
        public user user { get; set; }
        public token_info token_info { get; set; }

    }


    public class user
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string email_verified_at { get; set; }
        public string status { get; set; }
        public account account { get; set; }
        public business business { get; set; }
        public List<addressess> addresses { get; set; }

        public string created_at { get; set; }
        public string updated_at { get; set; }



    }

    public class token_info
    {
        public string access_token { get; set; }

    }

    public class account
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string role { get; set; }
        public string photo { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }

    }

    public class business
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string vendor_name { get; set; }
        public string company_name { get; set; }
        public string website { get; set; }
        public string about_yourself { get; set; }
        public string favorite_cuisine_to_cook { get; set; }
        public string describe_cooking_style { get; set; }
        public string describe_business { get; set; }
        public string delivery_available { get; set; }
        public string pickup_available { get; set; }
        public List<Models.diet_specialities> diet_specialities { get; set; }
        public List<Models.cuisine_specialities> cuisine_specialities { get; set; }
    }

    public class addressess
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string type { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address_line_1 { get; set; }
        public string address_line_2 { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string zip_code { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }

    }

    public class PurchasePlan
    {
        [Required]
        [Display(Name = "Card Number")]
        public string card_number { get; set; }

        
        [Required]
        [Display(Name = "Expiration Date")]
        public string expiration_date { get; set; }

        [Required]
        [DataType(DataType.Password)]        
        [Display(Name = "CVV")]
        public string card_code { get; set; }
        public string subscription_type { get; set; }


        //    "card_number": "4111111111111111",
        //"expiration_date": "2025-12",
        //"card_code": "122",
        //"subscription_type": "ONE-TIME"
    }
    public class ResponsePurchasePlan
    {
        
        public bool error { get; set; }
       
        public string message { get; set; }
       
    }



    //End Login Response
    #endregion

    public class ResponseBankAccount
    {
        public bool error { get; set; }
        public string message { get; set; }

        public responseBanks response { get; set; }

    }

    public class responseBanks
    {
        public List<BankAccount> bankProfile { get; set; }

        public List<cardProfile>  cardProfile { get; set; }

    }

    public class BankAccount
    {
        
        public string customer_payment_profile_id { get; set; }

        public bool default_payment_profile { get; set; }



        


        [Required]
        [Display(Name = "Account Holder Name")]
        public string name_on_account { get; set; }

        [Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 16)]        
        [MaxLength(16)]       
        [RegularExpression("^[0-9]*$", ErrorMessage = "Bank Account Number must be numeric")]
        [Display(Name = "Bank Account Number")]
        
        public string bank_account_number { get; set; }

        public string account_number { get; set; }

        [Required]
        [MaxLength(16)]        
        [RegularExpression("^[0-9]*$", ErrorMessage = "Bank Account Number must be numeric")]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 16)]
        [Compare("bank_account_number", ErrorMessage = "The Bank Account Number and Re-type Bank Account Number do not match.")]
        [Display(Name = "Re-type Bank Account Number")]
        public string re_type_bank_account_number { get; set; }

        [Required]
        [MaxLength(15)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Routing Number must be numeric")]
        [Display(Name = "Routing Number")]
        public string routing_number { get; set; }       

               
        [Display(Name = "Account Type")]
        public string account_type { get; set; }
        
        [Required]
        [Display(Name = "Bank Name")]
        public string bank_name { get; set; }

       
    }

    public class cardProfile
    {
        //[Required]
        //[Display(Name = "Account Holder Name")]
        public string customer_payment_profile_id { get; set; }

        //[Required]
        //[Display(Name = "Bank Account Number")]
        public string card_holder_name { get; set; }

        //[Required]
        //[Display(Name = "Re-type Bank Account Number")]
        public string card_number { get; set; }

        //[Required]
        //[Display(Name = "Routing Number")]
        public string expiration_date { get; set; }


        //[Display(Name = "Account Type")]
        public string card_type { get; set; }

        //[Required]
        //[Display(Name = "Bank Name")]
        public string issuer_number { get; set; }


    }
}