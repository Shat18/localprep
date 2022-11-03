using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalPrep.Web
{
    [MetadataType(typeof(ContactMetaData))]
    public partial class Contact
    {
    }

    public class ContactMetaData
    {
        [Required]
        [Display(Name = "Name")]
        public string ContactName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

        [Required]
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Message")]
        public string ContactText { get; set; }
    }
}