using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalPrep.Web
{
    [MetadataType(typeof(MealTransactionMetaData))]
    public partial class MealTransaction
    {
        public bool successFlag { get; set; }
    }

    public class MealTransactionMetaData
    {
        
    }
}