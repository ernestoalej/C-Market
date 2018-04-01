using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace C_Market.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }
        public int MyProperty { get; set; }
    }
}