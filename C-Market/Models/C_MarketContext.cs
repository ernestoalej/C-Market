using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace C_Market.Models
{
    public class C_MarketContext : DbContext
    {
        public C_MarketContext() : base("name=C_MarketContext")
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
