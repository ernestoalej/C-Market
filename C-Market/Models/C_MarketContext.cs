using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace C_Market.Models
{
    public class C_MarketContext : DbContext
    {
        public C_MarketContext() : base("name=C_MarketContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Quitar la  creacion de eliminar en cascada.
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public DbSet<Product> Products { get; set; }

        public System.Data.Entity.DbSet<C_Market.Models.DocumentType> DocumentTypes { get; set; }

        public System.Data.Entity.DbSet<C_Market.Models.Employee> Employees { get; set; }

        public System.Data.Entity.DbSet<C_Market.Models.Supplier> Suppliers { get; set; }

        public System.Data.Entity.DbSet<C_Market.Models.Customer> Customers { get; set; }
    }
}
