using System.Web.Mvc;
using System.Web.Routing;
using Sitecore.Pipelines;

namespace ActiveCommerce.Training.SimpleReviews.Mvc
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
                new[] { "ActiveCommerce.Training.SimpleReviews.Controllers" }
            );
        }
    }
}