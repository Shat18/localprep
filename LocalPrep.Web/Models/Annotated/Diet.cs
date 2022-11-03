using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalPrep.Web
{
    [MetadataType(typeof(DietMetaData))]
    public partial class Diet
    {
        [Display(Name = "Upload File")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageFile { get; set; }
    }

    public class DietMetaData
    {
        [Required]
        [Display(Name = "Diet Short Name")]
        public string DietShortName { get; set; }

        [Display(Name = "Diet Long Name")]
        public string DietLongName { get; set; }

        [Display(Name = "Diet Description")]
        public string DietDescription { get; set; }

        [Display(Name = "Image Path")]
        public string ImgSrc { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
}