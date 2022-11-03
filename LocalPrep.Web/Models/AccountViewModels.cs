using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace LocalPrep.Web.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        //[Required]
        //public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        //public string ReturnUrl { get; set; }

        //[Display(Name = "Remember this browser?")]
        //public bool RememberBrowser { get; set; }

        //public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        //[Required]
        //[Display(Name = "User Name")]
        //public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        //[Display(Name = "Phone Number")]
        //public string Phone { get; set; }
        [Required]  
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address1 { get; set; }

        [Display(Name = "Address 2")]
        public string Address2 { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

       
        [Display(Name = "State ID/ Drivers License")]
        public string DriversLicense { get; set; }

        
        [Display(Name = "Social Security Number")]
        public string SocialSecurityNumber { get; set; }

        //[Required]
        //[Display(Name = "State")]
        //public int StateId { get; set; }

        [Required]
        [Display(Name = "State")]
        public string short_code { get; set; }

        [Required]
        [Display(Name = "Zip")]
        public string Zip { get; set; }

        public List<StateList> States { get; set; }

        public List<Diet> Diets { get; set; }

        public List<Cuisine> Cuisines { get; set; }

        [Display(Name = "Website")]
        public string Website { get; set; }

        [Display(Name = "Cuisine Specialties")]
        public List<string> CuisinesSelected { get; set; }

        [Display(Name = "Diet Specialties")]
        public List<string> DietsSelected { get; set; }

        [Display(Name = "How would you best describe your cooking style? This will be used for your profile bio.")]
        public string CookingStyle { get; set; }

        //[Display(Name = "Profile Pic")]
        //public byte[] ProfilePic { get; set; }
        //[Required]
        [Display(Name = "Profile Pic")]        
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageFile { get; set; }

        public string filepath { get; set; }

   
        [Display(Name = "Drivers License Pic")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageFileDriversLicense { get; set; }

        public string filepathDL { get; set; }




    }

    public class ResetPasswordViewModel
    {
        //[Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    #region States response
    /// <summary>
    /// List of States
    /// </summary>

    public class ResponseStates
    {
        public string message { get; set; }

        public bool error { get; set; }

        public List<StateList> response { get; set; }
               
    }

    public class StateList
    {
        public int id { get; set; }

        public string name { get; set; }

        public string short_code { get; set; }
              
    }
    #endregion

    #region cuisine_specialities response
    /// <summary>
    /// List of States
    /// </summary>

    public class Responsecuisinespecialities 
    {
        public string message { get; set; }

        public bool error { get; set; }

        public List<cuisinespecialitiesList> response { get; set; }

    }

    public class cuisinespecialitiesList
    {
        public int id { get; set; }

        public string name { get; set; }

        public string short_code { get; set; }

        public bool IsSelected { get; set; }

    }
    #endregion

    #region diet_specialities response
    /// <summary>
    /// List of States
    /// </summary>

    public class Responsedietspecialities
    {
        public string message { get; set; }

        public bool error { get; set; }

        public List<diet_specialities> response { get; set; }

    }

    public class diet_specialities
    {
        public int id { get; set; }

        public string name { get; set; }

        public string short_code { get; set; }
        public bool IsSelected { get; set; }

    }

    public class CheckBoxListItem
    {
        public int ID { get; set; }

        public string Display { get; set; }

        public bool IsChecked { get; set; }       

        public string Istextbox
        {
            get; set;
        }

    }
    #endregion

    #region Login response
    /// <summary>
    /// Login response
    /// </summary>
    public class ResponseLogin
    {       
        public string message { get; set; }

        public bool error { get; set; }

        public response response { get; set; }
    }

    public class ResponseProfile
    {
        public string message { get; set; }

        public bool error { get; set; }

        public response response { get; set; }

        public string role { get; set; }
    }

    public class response
    {
        
        public user user { get; set; }
        public token_info token_info { get; set; }

    }


    public class user
    {
        public int id { get; set; }

        [Display(Name = "First Name")]
        public string first_name { get; set; }
        [Display(Name = "Last Name")]
        public string last_name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string email_verified_at { get; set; }
        public string status { get; set; }
        public string last_login_at { get; set; }
        public account account { get; set; }
        public business business { get; set; }
        public List<addressess> addresses { get; set; }
        public List<subscriptions> subscriptions { get; set; }
        

        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string rating { get; set; }


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
        public string photopath { get; set; }
        
        public string created_at { get; set; }
        public string updated_at { get; set; }
        
    }

    public class business
    {
        public int id { get; set; }
        public int user_id { get; set; }

        [Display(Name = "Vendor Name")]
        public string vendor_name { get; set; }
        [Display(Name = "Company Name")]

        public string company_name { get; set; }
        public string website { get; set; }
        [Display(Name = "About Yourself")]
        public string about_yourself { get; set; }
        [Display(Name = "Favorite Cuisine to cook")]
        public string favorite_cuisine_to_cook { get; set; }
        public string describe_cooking_style { get; set; }
        public string describe_business { get; set; }
        public string delivery_available { get; set; }
        public string pickup_available { get; set; }
        public string licence_number { get; set; }        
        public List<diet_specialities> dietspecialities { get; set; }
        public List<cuisine_specialities> cuisine_specialities { get; set; }
    }
    public class upcoming_orders_date
    {
       public string upcomingordersdate { get; set; }
        
    }

    

    public class addressess
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string type { get; set; }
        [Display(Name = "Email")]
        public string email { get; set; }
        [Display(Name = "Phone")]
        public string phone { get; set; }
        [Display(Name = "Address Line 1")]
        public string address_line_1 { get; set; }
        [Display(Name = "Address Line 2")]
        public string address_line_2 { get; set; }
        public string state { get; set; }
        [Display(Name = "City")]
        public string city { get; set; }
        [Display(Name = "Zip code")]
        public string zip_code { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }

    }  

    public class subscriptions
    {
        public int id { get; set; }
        public string user_id { get; set; }
        public string plan_id { get; set; }
        public string started_at { get; set; }
        public string end_at { get; set; }
        public string status { get; set; }
        public string type { get; set; }
        public string plan_type { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
                          
    }

    public class cuisine_specialities
    {
        public int id { get; set; }
        public string name { get; set; }

    }

    //End Login Response
    #endregion

    #region Image upload response
    /// <summary>
    /// Start to Image response
    /// </summary>

    public class ResponseImageUpload
    {
        public string message { get; set; }

        public bool error { get; set; }

        public responseImg response { get; set; }
    }

    public class responseImg
    {
        public string file_name { get; set; }
        public string url { get; set; }       
    }

    #endregion

    #region Registration/check
    /// <summary>
    /// Registration check
    /// </summary>

    public class ResponseRegistCheck
    {
        public string message { get; set; }

        public bool error { get; set; }

        public RegistCheck response { get; set; }

    }

    public class RegistCheck
    {
        public string registration_data { get; set; }
    }


    #endregion

    #region Verifycode
    /// <summary>
    /// Response Verifycode
    /// </summary>
    public class ResponseVerifycode
    {
        public bool error { get; set; }
        public string message { get; set; }
    }
    #endregion

    public class LoginPartial
    {
        
        public int ?CartCount { get; set; }

        public int? NotificationCount { get; set; }

        public bool IsLogin { get; set; }

        public bool IsVerified { get; set; }

        public bool Isregistered { get; set; }

        public string Fullname { get; set; }
        public string Photo { get; set; }
        public string role { get; set; }



    }

    public class cuisinespecialities
    {

        public int id { get; set; }

        public string name { get; set; }

        
        
    }

    public class dietspecialities
    {

        public int id { get; set; }

        public string name { get; set; }



    }

    public class dietspecialitieswithoutId
    {



        public string name { get; set; }



    }
    public class cuisinespecialitieswithoutId
    {
        public string name { get; set; }
    }

    public class HelpRequest
    {
        [Required]
        [Display(Name = "Subject")]
        public string subject { get; set; }

        [Required]
        [Display(Name = "Message")]
        public string message { get; set; }



    }

    public class ResponseNotifications
    {
        public string message { get; set; }

        public bool error { get; set; }

        public List<Notifications> response { get; set; }

        public int count { get; set; }
    }

    public class ResponseNotificationsCount
    {
        public string message { get; set; }

        public bool error { get; set; }

        public responseCount response { get; set; }
        

    }

    public class responseCount
    {
        public int count { get; set; }        

    }

    public class Notifications
    {

       
        public int id { get; set; }
        public string user_id { get; set; }
        public string title { get; set; }
        public content content { get; set; }
        public string type { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
      
    }

    public class content
    {
        public int reference_id { get; set; }        
        public string title { get; set; }
        public string message { get; set; }
        public string type { get; set; }
        public string click_action { get; set; }
    

    }

    public class ResponseLocaldata
    {
        //public string message { get; set; }

        public bool error { get; set; }

        public message message { get; set; }

    }

    public class message
    {
        //public int[] meals { get; set; }

        //public int[] preppers { get; set; }

        //public int[] cities { get; set; }

        public List<meals> meals { get; set; }
        public List<preppers> preppers { get; set; }
        public List<cities> cities { get; set; }


    }

    public class meals
    {
        public string name { get; set; }

    }

    public class preppers
    {
        public string name { get; set; }

    }

    public class cities
    {
        public string name { get; set; }

    }


    public class ResponseSearch
    {
        public bool error { get; set; }
        public string message { get; set; }

        public responseresult response { get; set; }

    }

    public class responseresult
    {
        public string phone { get; set; }
        public int total { get; set; }
        public List<items> items { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public string rating { get; set; }

        public List<cardProfile> cardProfile { get; set; }

        public bestdishes best_dishes { get; set; }
       

        public List<dataresult> data { get; set; }
        public Mealsdata meals { get; set; }

        public companyaddress company_address { get; set; }

        public List<address> address { get; set; }
        //public List<cities> cities { get; set; }


    }

    public class dataresult
    {
        public int id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public string rating { get; set; }
        public string user_id { get; set; }
        public string customer_first_name { get; set; }
        public string customer_last_name { get; set; }
        public string prepper_id { get; set; }
        public string rate { get; set; }
        public string comment { get; set; }

        public string latitude { get; set; }

        public string longitude { get; set; }

        
            
        //"id": 1,
        //"user_id": "7",
        //"customer_first_name": "Prakash",
        //"customer_last_name": "Customer",
        //"prepper_id": "2",
        //"rate": "4",
        //"comment": "Velit ipsum tempora totam quo voluptatem.",

    }

    public class companyaddress
    {
        public int user_id{ get; set; }
        public string address_line_1{ get; set; }
        public string address_line_2{ get; set; }
        public string phone { get; set; }
        public string state{ get; set; }
        public string zip_code { get; set; }
        public int id { get; set; }
        public string city { get; set; }
        public string email { get; set; }
        public string type { get; set; }




        



        //"id": 1,
        //"user_id": "7",
        //"customer_first_name": "Prakash",
        //"customer_last_name": "Customer",
        //"prepper_id": "2",
        //"rate": "4",
        //"comment": "Velit ipsum tempora totam quo voluptatem.",

    }


    public class address
    {
        public int user_id { get; set; }
        public string address_line_1 { get; set; }
        public string address_line_2 { get; set; }
        public string phone { get; set; }
        public string state { get; set; }
        public string zip_code { get; set; }
        public int id { get; set; }
        public string city { get; set; }
        public string email { get; set; }
        public string type { get; set; }




        //"id": 1,
        //"user_id": "7",
        //"customer_first_name": "Prakash",
        //"customer_last_name": "Customer",
        //"prepper_id": "2",
        //"rate": "4",
        //"comment": "Velit ipsum tempora totam quo voluptatem.",

    }

    



    public class bestdishes
    {
        public List<dataresult> data { get; set; }

    }

    public class ResponsePrepperDetails
    {
        public bool error { get; set; }
        public string message { get; set; }

        public responseresult response { get; set; }

    }

    public class Mealsdata
    {
        public List<Meals> data { get; set; }
        
        

    }

    public class items
    {
        public int id { get; set; }
        public string customer_id { get; set; }
        public string prepper_id { get; set; }
        public string meal_id { get; set; }
        public string quantity { get; set; }               
        public Meals meals { get; set; }



    }

    public class ResponseAddress
    {
        public bool error { get; set; }
        public string message { get; set; }
        public List<address> response { get; set; }
       

    }

    public class ResponseCardInfo
    {
        public bool error { get; set; }
        public string message { get; set; }

        public CardInforesult response { get; set; }

    }

    public class CardInforesult
    {
        //public bankProfile bankProfile { get; set; }
        public List<cardProfile> cardProfile { get; set; }


    }

    public class cardProfile
    {
        //public int id { get; set; }
        public string customer_payment_profile_id { get; set; }
        public string card_holder_name { get; set; }
        public string card_number { get; set; }
        public string expiration_date { get; set; }



    }


    /// <summary>
    /// Admin Model
    /// </summary>
    /// <returns></returns>
    /// 

    public class ResponseAdminCustomers
    {
        public ResponseActiveOrders ResponseActiveOrders { get; set; }
        public ResponseHistoryOrders ResponseHistoryOrders { get; set; }
        

    }

    public class ResponseActiveOrders
    {
        public bool error { get; set; }
        public string message { get; set; }
        public Customers response { get; set; }

    }
    public class ResponseActiveOrders1
    {
        public bool error { get; set; }
        public string message { get; set; }
        public Customers response { get; set; }

    }

    public class ResponseHistoryOrders1
    {
        public bool error { get; set; }
        public string message { get; set; }
        public Customers response { get; set; }

    }

    public class ResponseHistoryOrders
    {
        public bool error { get; set; }
        public string message { get; set; }
        public Customers response { get; set; }

    }

    public class Customers
    {

        //public int PageNumber { get; set; }
        //public int PageSize { get; set; }
        //public Uri FirstPage { get; set; }
        //public Uri LastPage { get; set; }
        //public int TotalPages { get; set; }
        //public int TotalRecords { get; set; }
        //public Uri NextPage { get; set; }
        //public Uri PreviousPage { get; set; }


        public int current_page { get; set; }
        public string first_page_url { get; set; }        
        public string from { get; set; }
        public string next_page_url { get; set; }
        public string path { get; set; }        
        public int per_page { get; set; }
        public string prev_page_url { get; set; }     
        public string to { get; set; }
        public List<ListCustomers> data { get; set; }        
        public int id { get; set; }
        public string first_name { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }

        public string last_name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public int HiddenId { get; set; }

        

        

        public business business { get; set; }
        public List<addressess> addresses { get; set; }

        public delivery_address delivery_address { get; set; }

        


    }

    public class ListCustomers
    {
        public Customers Customer { get; set; }

        public List<upcoming_orders_date> upcoming_orders_date { get; set; }
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }        
        public string status { get; set; }
        public string email_verified_at { get; set; }
        public string created_at { get; set; }

        public string last_login_at { get; set; }
        public string total { get; set; }

        

        public List<Meals> meals { get; set; }
        

    }

    public class ResponseCustomers
    {
        public bool error { get; set; }
        public string message { get; set; }
        public Cust response { get; set; }

    }

    public class Cust
    {        
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string created_at { get; set; }       

        public List<addresses> addresses { get; set; }

    }

    public class addresses
    {
        public string address_line_1 { get; set; }
        public string address_line_2 { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string zip_code { get; set; }
        

        

    }

    public class Responsedashboard
    {
        public bool error { get; set; }
        public string message { get; set; }
        public dashboard response { get; set; }

    }
    public class dashboard 
    {        
        public List<ListCustomers> Customers { get; set; }
        public counts counts { get; set; }
        public List<monthWiseSales> monthWiseSales { get; set; }

        

    }

    public class counts
    {
        public int active_order_counts { get; set; }
        public int customers_counts { get; set; }
        public int order_counts { get; set; }
        public int preppers_counts { get; set; }

    }

    public class ResponseEarnings
    {
        public bool error { get; set; }
        public string message { get; set; }
        public Earnings response { get; set; }

    }

    public class Earnings
    {
        public bool error { get; set; }
        public string message { get; set; }
        public ResponseData Response { get; set; }
       

    }

    public class ResponseData
    {
        public List<EarningsData> data { get; set; }
        public int Hidden_id { get; set; }
        public string sub_amount { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }

    }

    public class EarningsData
    {
        public int id { get; set; }
        public string sub_amount { get; set; }
        public string status { get; set; }
        public string transaction_id { get; set; }
        public string amount { get; set; }
        public string charges { get; set; }
        public ordersdata orders { get; set; }
        public Preppersdata Preppers { get; set; }

    }

    public class ordersdata
    {
        public List<Meals> meals { get; set; }

        public Customers Customer { get; set; }
        //public orders Oreders { get; set; }
        //public Preppers Preppers { get; set; }

    }

    public class Preppersdata
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
       
    }

    public class GraphData
    {
        public List<TotalSell> totalsell { get; set; }
        public List<SellMonth> monthname { get; set; }
    }

    public class TotalSell
    {
        public int totalSell { get; set; }
    }
    public class SellMonth
    {
        public string monthName { get; set; }
    }
    public class monthWiseSales
    {
        public string month { get; set; }
        public string total_sales { get; set; }
    }
    



}
