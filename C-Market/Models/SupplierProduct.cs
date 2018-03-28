using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C_Market.Models
{
    public class SupplierProduct
    {
        public int SupplierProductID { get; set; }
        public int SupplierID { get; set; }
        public int ProductoID { get; set; }

        public virtual Supplier Supplier { get; set; }
        public virtual Product Product { get; set; }

    }
}