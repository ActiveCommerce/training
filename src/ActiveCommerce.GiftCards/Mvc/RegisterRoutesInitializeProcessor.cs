using System.Web.Mvc;
using Sitecore.Pipelines;
using System.Web.Routing;

namespace ActiveCommerce.GiftCards.Mvc
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
                "GiftCard",
                "ac/giftcard/{action}/{id}",
                new { controller = "GiftCard", id = UrlParameter.Optional },
                new[] { "ActiveCommerce.GiftCards.Controllers" }
            );
        }
    }
}