using System.Web.Mvc;
using System.Web.Routing;
using Sitecore.Pipelines;

namespace ActiveCommerce.Training.InvoicePayment.Mvc
{
    public class RegisterRoutesInitializeProcessor
    {
        public void Process(PipelineArgs args)
        {
            RegisterRoutes(RouteTable.Routes);
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            //remove existing checkout controller
            routes.Remove(routes["Checkout"]);

            //map our extended controller
            routes.MapRoute(
                "Checkout",
                "ac/checkout/{action}",
                new { controller = "Checkout", action = "Index" },
                new[] { "ActiveCommerce.Training.Payment.Controllers" }
            );
        }
    }
}