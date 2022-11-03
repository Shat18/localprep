using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalPrep.Web
{
    [MetadataType(typeof(MealMetaData))]
    public partial class Meal
    {
    }

    public class MealMetaData
    {
        [Required]
        [Display(Name = "Meal Name")]
        public string MealName { get; set; }

        [Required]
        [Display(Name = "Meal Description")]
        public string MealDescription { get; set; }

        [Required]
        [Display(Name = "Meal Price")]
        public decimal MealPrice { get; set; }

        [Display(Name = "Heating Instructions")]
        public string HeatingInstructions { get; set; }

        [Display(Name = "Diet")]
        public int DietId { get; set; }

        [Display(Name = "Cuisine")]
        public int CuisineId { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
}