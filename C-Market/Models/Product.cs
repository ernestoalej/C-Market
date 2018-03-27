using System;
using System.ComponentModel.DataAnnotations;

namespace C_Market.Models
{
    public class Product
    {
        [Key]
        public int ProductoID { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime LastBuy { get; set; }
        public float Stock { get; set; }
        public string Remarks { get; set; }
       /* public string Deleteme { get; set; }*/
    }

}