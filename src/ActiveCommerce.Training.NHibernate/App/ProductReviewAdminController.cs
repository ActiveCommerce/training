using System.Linq.Dynamic;
using System.Linq;
using System.Web.Mvc;
using ActiveCommerce.Orders;
using ActiveCommerce.Training.SimpleReviews.Controllers;
using Microsoft.Practices.Unity;

namespace ActiveCommerce.Training.SimpleReviews.App
{
    [ShopContextFilter]
    public class ProductReviewAdminController : Controller
    {
        public ActionResult Search(string where, string orderBy, int? pageSize, int? pageIndex)
        {
            var repository = Sitecore.Ecommerce.Context.Entity.Resolve<IProductReviewRepository>();
            var reviews = repository.GetAll();
            if (!string.IsNullOrEmpty(where))
            {
                reviews = reviews.Where(where);
            }
            if (!string.IsNullOrEmpty(orderBy))
            {
                reviews = reviews.OrderBy(orderBy);
            }
            var count = reviews.Count();
            if (pageSize.HasValue)
            {
                if (pageIndex.HasValue)
                {
                    reviews = reviews.Skip(pageSize.Value*pageIndex.Value);
                }
                reviews = reviews.Take(pageSize.Value);
            }
            return Json(
                new {
                    Items = reviews,
                    TotalCount = count
                },
                JsonRequestBehavior.AllowGet);
        }
    }
}