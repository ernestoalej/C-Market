using C_Market.Models;
using C_Market.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace C_Market.Controllers
{
    public class OrdersController : Controller
    {
        C_MarketContext db = new C_MarketContext();

        // GET: Orders
        public ActionResult NewOrder()
        {
            var orderView = new OrderView();
            orderView.Customer = new Customer();
            orderView.Products = new List<ProductOrder>();

            var list = db.Customers.ToList();

            list.Add(new Customer { CustomerID = 0, FullName = "[Seleccione un cliente]" });
            list = list.OrderBy(c => c.FullName).ToList();
            ViewBag.CustomerID = new SelectList(list, "CustomerID", "FullName");
            
            return View(orderView);
        }

        [HttpPost]
        public ActionResult NewOrder(OrderView orderView)
        {                 
            var list = db.Customers.ToList();

            list.Add(new Customer { CustomerID = 0, FullName = "[Seleccione un cliente]" });
            list = list.OrderBy(c => c.FullName).ToList();
            ViewBag.CustomerID = new SelectList(list, "CustomerID", "FullName");

            return View(orderView);
        }

        public ActionResult AddProduct(ProductOrder productOrder)
        {
            var list = db.Products.ToList();
            list = list.OrderBy(c => c.Description).ToList();            
            ViewBag.ProductID = new SelectList(list, "ProductID", "Description");

            return View(productOrder);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}