using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ZDSL.Webapp
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

          //  routes.MapRoute(
          //      "Default",                                              // Route name
          //      "{controller}/{action}/{id}",                           // URL with parameters
          //      new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
         //   );

            routes.MapRoute(
                "Admin",
                "Admin/{controller}/{action}"
                , new string[] { "ZDSL.Webapp.Controllers.Admin" }
            );

      
            routes.MapRoute(
                "Public",
                "Public/{controller}/{action}"
                , new string[] { "ZDSL.Webapp.Controllers.Public" }
            );

            routes.MapRoute(
                "MemberAdmin",
                "MemberAdmin/{controller}/{action}"
                , new string[] { "ZDSL.Webapp.Controllers.MemberAdmin" }
);

          
        }

        protected void Application_Start()
        {
            //ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new ZdViewEngine("Admin"));
            ViewEngines.Engines.Add(new ZdViewEngine("Public"));
            ViewEngines.Engines.Add(new ZdViewEngine("MemberAdmin"));
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
           
        }
    }

    public class ZdViewEngine : VirtualPathProviderViewEngine {

        public ZdViewEngine(string prefix) {

            MasterLocationFormats = new string[] { };

            AreaMasterLocationFormats = new string[] { };

            ViewLocationFormats = new string[]
              {
                "~/Views/"+prefix+"/{1}/{0}.aspx",
                "~/Views/"+prefix+"/{1}/{0}.ascx",
                "~/Views/"+prefix+"/Shared/{0}.aspx",
                "~/Views/"+prefix+"/Shared/{0}.ascx"
              };

            AreaViewLocationFormats = new string[] { };


            PartialViewLocationFormats = ViewLocationFormats;
            AreaPartialViewLocationFormats = AreaViewLocationFormats;

        }


        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {

            return new WebFormView(partialPath, null);

        }



        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {

            return new WebFormView(viewPath, masterPath);

        }  

    
    }
}