using C_Market.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace C_Market
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Cada vez que incie la aplicacion verificar si tiene que modificar la base de datos
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<Models.C_MarketContext,
                    Migrations.Configuration>());

            // Conectar con la Base de datos
            ApplicationDbContext db = new ApplicationDbContext();
            CreateRoles(db);
            CreateSuperUser(db);

            db.Dispose();   // Libearamos el objetos y cerramos la conexion.*/

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);         
        }

        private void CreateSuperUser(ApplicationDbContext db)
        {
            //Crea un usuario por código
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var user = userManager.FindByName("ernestocontreras28@gmail.com");

            if (user == null) 
            {
                user = new ApplicationUser
                {
                   UserName = "ernestocontreras28@gmail.com",
                    Email = "ernestocontreras28@gmail.com",       
                };

                userManager.Create(user, "Ernxls(GM)");

            }

            user = userManager.FindByName("crsoftware@hotmail.com");

            if (user == null)
            {
   
                user = new ApplicationUser
                {
                    UserName = "crsoftware@hotmail.com",
                    Email = "crsoftware@hotmail.com"
                };

               
                userManager.Create(user, "Ernxls(CE)");
            }
        }

        private void CreateRoles(ApplicationDbContext db)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            // Si no existe el rol ver, crearlo
           if (!roleManager.RoleExists("View")){
                roleManager.Create(new IdentityRole("View"));
            }

            if (!roleManager.RoleExists("Edit"))
            {
                roleManager.Create(new IdentityRole("Edit"));
            }

            if (!roleManager.RoleExists("Create"))
            {
                roleManager.Create(new IdentityRole("Create"));
            }

            if (!roleManager.RoleExists("Delete"))
            {
                roleManager.Create(new IdentityRole("Delete"));
            }

        }
    }
}
