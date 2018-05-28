using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace C_Market.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [StringLength(30, ErrorMessage = "The field {0} must contain between {2} and {1} characteres", MinimumLength = 5)]
        [Display(Name = "Document")]
        public string Description { get; set; }
    }
}