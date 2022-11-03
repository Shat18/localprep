using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalPrep.Web
{
    [MetadataType(typeof(CuisineMetaData))]
    public partial class Cuisine
    {
    }

    public class CuisineMetaData
    {
        [Required]
        [Display(Name = "Cuisine Name")]
        public string CuisineName { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
}