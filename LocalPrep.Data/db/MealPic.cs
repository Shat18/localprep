//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LocalPrep.Data.db
{
    using System;
    using System.Collections.Generic;
    
    public partial class MealPic
    {
        public int MealPicId { get; set; }
        public int MealId { get; set; }
        public string ImgSrc { get; set; }
        public string BriefDescription { get; set; }
        public System.DateTime UploadDt { get; set; }
    
        public virtual Meal Meal { get; set; }
    }
}
