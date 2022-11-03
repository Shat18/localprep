using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalPrep.Web.Models
{
    public class HomeViewModel
    {
        public List<Diet> diets { get; set; }
        public List<Cuisine> cuisines { get; set; }
    }

    public class PrepperHomeViewModel
    {
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string PhoneNumber { get; set; }
        public string FormattedAddress { get; set; }
        public bool DeliveryAvailable { get; set; }
        public bool PickupAvailable { get; set; }
        public string ImgSrc { get; set; }
        public List<Meal> meals { get; set; }
    }
}