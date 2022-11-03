using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalPrep.Entity
{
    public class VendorsEntity
    {
        public int? Step { get; set; }
        public int VendorId { get; set; }
        public string UserId { get; set; }
        public string VendorName { get; set; }
        public string CompanyName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public string Zip { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string VendorDescription { get; set; }
        public bool DeliveryAvailable { get; set; }
        public bool PickupAvailable { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string PlaceId { get; set; }
        public string FormattedAddress { get; set; }
        public string ImgSrc { get; set; }
        public bool IsApproved { get; set; }
        public string ApprovedById { get; set; }
        public System.DateTime SubmitDt { get; set; }
        public bool LicensedInState { get; set; }
        public string LicenseNo { get; set; }
        public string AboutYourself { get; set; }
        public string FavoriteThingsToCook { get; set; }


    }

    public class Vendormodel
    {
        public string plan { get; set; }
        public int VendorId { get; set; }
        public string UserId { get; set; }
        public string VendorName { get; set; }
        public string CompanyName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public string Zip { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string VendorDescription { get; set; }
        public bool DeliveryAvailable { get; set; }
        public bool PickupAvailable { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string PlaceId { get; set; }
        public string FormattedAddress { get; set; }
        public string ImgSrc { get; set; }
        public bool IsApproved { get; set; }
        public string ApprovedById { get; set; }
        public System.DateTime SubmitDt { get; set; }
        public bool LicensedInState { get; set; }
        public string LicenseNo { get; set; }
        public string AboutYourself { get; set; }
        public string FavoriteThingsToCook { get; set; }


    }
}
