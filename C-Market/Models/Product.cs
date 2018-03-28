﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace C_Market.Models
{
    public class Product
    {
        [Key]
        public int ProductoID { get; set; }

        [Required]
        [StringLength(80)]
        public string Description { get; set; }

        public decimal Price { get; set; }

        [DisplayFormat(DataFormatString ="{0:C2}", ApplyFormatInEditMode = false)]
        [Display(Name ="Last buy")]
        public DateTime LastBuy { get; set; }
        
        public float Stock { get; set; }

        public string Remarks { get; set; }

        public virtual ICollection<SupplierProduct> SupplierProducts { get; set; }

    }

}