using System.Web.Mvc;
using System.Web.Routing;
using Sitecore.Pipelines;

namespace ActiveCommerce.Training.EstimateShipping.Mvc
{
    public class RegisterRoutesInitializeProcessor
    {
        public void Process(PipelineArgs args)
        {
            RegisterRoutes(RouteTable.Routes);
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            //remove existing cart controller
            routes.Remove(routes["ShoppingCart"]);

            //map our extended controller
            routes.MapRoute(
                "ShoppingCart",
                "ac/cart/{action}/{code}/{quantity}",
                new { controller = "ShoppingCart", code = UrlParameter.Optional, quantity = UrlParameter.Optional },
                new[] { "ActiveCommerce.Training.EstimateShipping.Controllers" }
            );
        }
    }
}