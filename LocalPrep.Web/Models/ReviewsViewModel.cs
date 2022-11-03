using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalPrep.Web.Models
{
    public class ReviewsViewModel
    {
        public Vendor vendor { get; set; }
        public MealRating review { get; set; }
        public List<Meal> meals { get; set; }
    }
}