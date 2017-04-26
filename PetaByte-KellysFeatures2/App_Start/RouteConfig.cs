using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using PetaByte_KellysFeatures2.Models;

namespace PetaByte_KellysFeatures2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //custom pages route for when a user makes a new page
            routes.MapRoute(
                name: "Pages",
                url: "{controller}/{action}/{pageUrl}",
                defaults: new { controller = "Pages", action = "Details", pageUrl = "" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

    }
}
