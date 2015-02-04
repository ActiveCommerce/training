using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Sitecore.Pipelines;

namespace ActiveCommerce.Training.CheckoutViaApi.Mvc
{
    public class RegisterRoutesProcessor
    {
        public void Process(PipelineArgs args)
        {
            RegisterRoutes(RouteTable.Routes);
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            //map our new controller
            routes.MapRoute(
                "CustomCheckout",
                "customCheckout/{action}",
                new { controller = "CustomCheckout" },
                new[] { "ActiveCommerce.Training.CheckoutViaApi.Controllers" }
            );
        }
    }
}