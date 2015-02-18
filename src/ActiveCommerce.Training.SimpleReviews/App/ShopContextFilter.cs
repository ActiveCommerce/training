using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActiveCommerce.ShopContext;
using Sitecore.Sites;

namespace ActiveCommerce.Training.SimpleReviews.App
{
    public class ShopContextFilter : ActionFilterAttribute
    {
        private const string ShopContextParam = "shopContext";
        private const string SwitcherKey = "ShopContextFilter.Switcher";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var shopContext = filterContext.HttpContext.Request.Params[ShopContextParam];
            if (string.IsNullOrEmpty(shopContext))
            {
                return;
            }
            var siteContext = Sitecore.Sites.SiteContextFactory.GetSiteContext(shopContext);
            var database = Sitecore.Data.Database.GetDatabase("master");
            var switcher = new ShopContextSwitcher(siteContext, database);
            filterContext.RequestContext.HttpContext.Items[SwitcherKey] = switcher;
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var switcher = filterContext.RequestContext.HttpContext.Items[SwitcherKey] as ShopContextSwitcher;
            if (switcher != null)
            {
                switcher.Dispose();
            }
        }
    }
}