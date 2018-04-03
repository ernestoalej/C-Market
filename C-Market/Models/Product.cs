using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace C_Market.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [StringLength(80, ErrorMessage = "The field {0} must contain between {2} and {1} characteres", MinimumLength = 3)]
        [Required(ErrorMessage = "You must enter the field {0}")]
        [Display(Name ="Product Description")]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString ="{0:C2}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "You must enter the field {0}")]
        public decimal Price { get; set; }

        
        [Display(Name ="Last buy")]        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime LastBuy { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString ="{0:N2}", ApplyFormatInEditMode = false)]
        public float Stock { get; set; }


        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        public virtual ICollection<SupplierProduct> SupplierProducts { get; set; }
        public virtual ICollection <OrderDetail> OrderDetails { get; set; }

    }

}