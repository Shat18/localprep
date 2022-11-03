using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalPrep.Web.Models.Annotated
{
    [MetadataType(typeof(MealIngredientMetaData))]
    public partial class MealIngredient
    {
    }

    public class MealIngredientMetaData
    {
        [Display(Name = "Ingredient")]
        public int MealIngredientId { get; set; }
    }
}