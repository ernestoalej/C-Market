using C_Market.Models;
using C_Market.ViewModels;
using System.Collections.Generic;
using System.Linq;
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

            Session["orderView"] = orderView;

            var list = db.Customers.ToList();

            list.Add(new Customer { CustomerID = 0, FirstName = "[You must select a customer]" });
            list = list.OrderBy(c => c.FullName).ToList();
            ViewBag.CustomerID = new SelectList(list, "CustomerID", "FullName");
            
            return View(orderView);
        }

        [HttpPost]
        public ActionResult NewOrder(OrderView orderView)
        {                 
            var list = db.Customers.ToList();
           
            list.Add(new Customer { CustomerID = 0, FirstName = "[You must select a customer]" });
            list = list.OrderBy(c => c.FullName).ToList();
            ViewBag.CustomerID = new SelectList(list, "CustomerID", "FullName");

            return View(orderView);
        }

        public ActionResult AddProduct()
        {
            var list = db.Products.ToList();
            list.Add(new ProductOrder { ProductID = 0, Description = "[You must select a product]" });
            list = list.OrderBy(p => p.Description).ToList();              
            ViewBag.ProductID = new SelectList(list, "ProductID", "Description");
                   
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(ProductOrder productOrder)
        {
            var orderView = Session["orderView"] as OrderView;

            var productID = int.Parse(Request["ProductID"] );

            if (productID == 0)
            {
                var list = db.Products.ToList();
                list.Add(new ProductOrder { ProductID = 0, Description = "[You must select a product]" });
                list = list.OrderBy(p => p.Description).ToList();
                ViewBag.ProductID = new SelectList(list, "ProductID", "Description");

                return View(productOrder);
            }

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