﻿using C_Market.Models;
using C_Market.ViewModels;
using System;
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
            orderView = Session["orderView"] as OrderView;

            var customerID =  int.Parse(Request["customerID"]);

            if (customerID == 0)
            {
                var list = db.Customers.ToList();

                list.Add(new Customer { CustomerID = 0, FirstName = "[Select a customer]" });
                list = list.OrderBy(c => c.FullName).ToList();
                ViewBag.CustomerID = new SelectList(list, "CustomerID", "FullName");
                ViewBag.Error = "You must select a customer ";

                return View(orderView);
            }

            Customer customer = db.Customers.Find(customerID);

            if (customer == null)
            {
                var list = db.Customers.ToList();

                list.Add(new Customer { CustomerID = 0, FirstName = "[Select a customer]" });
                list = list.OrderBy(c => c.FullName).ToList();
                ViewBag.CustomerID = new SelectList(list, "CustomerID", "FullName");
                ViewBag.Error = "Client not exists ";

                return View(orderView);
            }

            if (orderView.Products.Count == 0)
            {
                var list = db.Customers.ToList();

                list.Add(new Customer { CustomerID = 0, FirstName = "[Select a customer]" });
                list = list.OrderBy(c => c.FullName).ToList();
                ViewBag.CustomerID = new SelectList(list, "CustomerID", "FullName");
                ViewBag.Error = "You must enter a detail";

                return View(orderView);
            }

            int orderID = 0;

           // int i = 0;
             
            using (var  transaction  =  db.Database.BeginTransaction())
            {
                try
                {
                    var order = new Order
                    {
                        CustomerID = customerID,
                        DateOrder = DateTime.Now,
                        OrderStatus = OrderStatus.Created

                    };

                    db.Orders.Add(order);
                    db.SaveChanges();

                    orderID = db.Orders.ToList().Select(o => o.OrderID).Max();

                    foreach (var item in orderView.Products)
                    {
                        var orderDetail = new OrderDetail
                        {
                            ProductID = item.ProductID,
                            Description = item.Description,
                            Quantity = item.Quantity,
                            Price = item.Price,
                            OrderID = orderID
                        };

                        db.OrderDetails.Add(orderDetail);

                        /* Generar un errror para poder probar la transacción
                        i++;

                        if (i >1)
                        {
                            int a = 0;
                            i /= a;
                        }*/


                    }

                    db.SaveChanges();

               

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    ViewBag.Error = "An error has ocurred creating a new order : " + e.Message;

                    var listCus = db.Customers.ToList();
                    listCus.Add(new Customer { CustomerID = 0, FirstName = "[Select a customer]" });
                    listCus = listCus.OrderBy(c => c.FullName).ToList();
                    ViewBag.CustomerID = new SelectList(listCus, "CustomerID", "FullName");

                    return View(orderView);
                }
           
            }

            ViewBag.Message = string.Format("Order {0}, saved successfully!", orderID);

            // RedirectToAction("NewOrder");

            //return View(orderView);


            var listC = db.Customers.ToList();
            listC.Add(new Customer { CustomerID = 0, FirstName = "[Select a customer]" });
            listC = listC.OrderBy(c => c.FullName).ToList();
            ViewBag.CustomerID = new SelectList(listC, "CustomerID", "FullName");
        
            orderView = new OrderView();
            orderView.Customer = new Customer();
            orderView.Products = new List<ProductOrder>();

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

                ViewBag.Error = "You must select a product";

                return View(productOrder);
            }

            var product = db.Products.Find(productID);

            if (product == null)
            {

                var list = db.Products.ToList();
                list.Add(new ProductOrder { ProductID = 0, Description = "[You must select a product]" });
                list = list.OrderBy(p => p.Description).ToList();
                ViewBag.ProductID = new SelectList(list, "ProductID", "Description");

                ViewBag.Error = "Product not found";

            }
           

            productOrder = orderView.Products.Find( p=> p.ProductID == productID);  
            
            if (productOrder== null)
            {
                productOrder = new ProductOrder()
                {
                Description = product.Description,
                Price = product.Price,
                ProductID = product.ProductID,
                Quantity = float.Parse(Request["Quantity"])
                };

                orderView.Products.Add(productOrder);
            } else
            {
                productOrder.Quantity += float.Parse(Request["Quantity"]);
            }
           
            var listC = db.Customers.ToList();
            listC = listC.OrderBy(c => c.FullName).ToList();

            listC.Add(new Customer { CustomerID = 0, FirstName = "[You must select a customer]" });
            
            ViewBag.CustomerID = new SelectList(listC, "CustomerID", "FullName");

            return View("NewOrder", orderView);
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
 