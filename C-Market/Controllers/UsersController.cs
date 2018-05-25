using C_Market.Models;
using C_Market.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace C_Market.Controllers
{
    public class UsersController : Controller
    {
        // Conectar con la Base de datos
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Users
        public ActionResult Index()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var users = userManager.Users.ToList();

            var usersView = new List<UserView>();

            foreach (var user in users)
            {
                var userView = new UserView
                {
                    Email = user.Email,
                    Name = user.UserName,
                    UserID = user.Id
                };

                usersView.Add(userView);
            }

            return View(usersView);
        }

        public ActionResult Roles(string userID)
        {
            if (string.IsNullOrEmpty(userID))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            var roles = roleManager.Roles.ToList();
            var users = userManager.Users.ToList();
            var user = users.Find(u => u.Id == (userID));

            if (user== null)
            {
                return HttpNotFound();
            }

            var rolesView = new List<RoleView>();

            foreach (var item in user.Roles) {
                var role = roles.Find(r => r.Id == item.RoleId);

                var roleView = new RoleView {
                    RoleID = role.Id,
                    Name = role.Name
                };

                rolesView.Add(roleView);
            }

            var userView = new UserView
            {
                Email = user.Email,
                Name = user.UserName,
                UserID = user.Id,
                Roles = rolesView
            };

            return View(userView);
        }

        [HttpPost]
        public ActionResult  AddRole(string userID, FormCollection form)
        {
            var RoleID = Request["RoleID"];

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var users = userManager.Users.ToList();
            var user = users.Find(u => u.Id == (userID));
            var userView = new UserView
            {
                Email = user.Email,
                Name = user.UserName,
                UserID = user.Id
            };

            if (string.IsNullOrEmpty(RoleID))
            {
                ViewBag.Error = "You must select a role";

                var list = roleManager.Roles.ToList();

                list.Add(new IdentityRole { Id = "", Name = "[Select a role]" });

                list = list.OrderBy(r => r.Name).ToList();
                ViewBag.RoleID = new SelectList(list, "Id", "Name");

                return View(userView);
            }

         //   userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var roles = roleManager.Roles.ToList();
            var role = roles.Find(r => r.Id == RoleID);

            if (!userManager.IsInRole(user.Id, role.Name))
            {
                userManager.AddToRole(userID, role.Name);
            }

            /*************************************************************/
            var rolesView = new List<RoleView>();

            foreach (var item in user.Roles)
            {
                role = roles.Find(r => r.Id == item.RoleId);

                var roleView = new RoleView
                {
                    RoleID = role.Id,
                    Name = role.Name
                };

                rolesView.Add(roleView);
            }

            userView = new UserView

            {
                Email = user.Email,
                Name = user.UserName,
                UserID = user.Id,
                Roles = rolesView
            };

            return View("Roles", userView);

        }

        [HttpGet]
        public ActionResult AddRole(string userID)
        {
            if (string.IsNullOrEmpty(userID))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));        
            var users = userManager.Users.ToList();
            var user = users.Find(u => u.Id == (userID));
            
            if (user == null)
            {
                return HttpNotFound();
            }

            var userView = new UserView
            {
                Email = user.Email,
                Name = user.UserName,
                UserID = user.Id
            };

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            var list = roleManager.Roles.ToList();

            list.Add(new IdentityRole { Id = "", Name = "[Select a role]" });            
            list = list.OrderBy(r => r.Name).ToList();
            ViewBag.RoleID = new SelectList(list, "Id", "Name");

            return View(userView);


        }

        public ActionResult Delete(string userID, string roleID)
        {
            if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(roleID)) 
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var user = userManager.Users.ToList().Find(u=> u.Id == userID);
            var role = roleManager.Roles.ToList().Find(u=> u.Id == roleID);

            if (userManager.IsInRole(userID, role.Name))
            { 
                userManager.RemoveFromRole(userID, role.Name);
            }


            // Prepare the view to return
            var users = userManager.Users.ToList();
            var roles = roleManager.Roles.ToList();           
            var rolesView = new List<RoleView>();

            foreach (var item in user.Roles)
            {
                 role = roles.Find(r => r.Id == item.RoleId);

                var roleView = new RoleView
                {
                    RoleID = role.Id,
                    Name = role.Name
                };

                rolesView.Add(roleView);
            }

            var userView = new UserView
            {
                Email = user.Email,
                Name = user.UserName,
                UserID = user.Id,   
                Roles = rolesView
            };

            return View("Roles", userView);
          
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