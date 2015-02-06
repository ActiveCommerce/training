using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Sitecore.Pipelines;

namespace ActiveCommerce.Training.NHibernate.Mvc
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
                "ProductReviews",
                "productReview/{action}",
                new { controller = "ProductReview" },
                new[] { "ActiveCommerce.Training.NHibernate.Controllers" }
            );
        }
    }
}