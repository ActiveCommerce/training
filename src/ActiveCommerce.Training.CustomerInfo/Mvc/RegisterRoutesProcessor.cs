using System.Web.Mvc;
using System.Web.Routing;
using Sitecore.Pipelines;

namespace ActiveCommerce.Training.CustomerInfo.Mvc
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
                "Birthday",
                "training/birthday/{action}",
                new { controller = "Birthday" },
                new[] { "ActiveCommerce.Training.CustomerInfo.Controllers" }
            );
        }
    }
}