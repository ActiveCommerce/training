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
            routes.MapRoute(
                "InvoicePayment",
                "ac/invoicepayment/{action}",
                new { controller = "InvoicePayment" },
                new[] { "ActiveCommerce.Training.InvoicePayment.Controllers" }
            );
        }
    }
}