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
            var reviews = repository.GetAll().Where(x => !x.Hidden);
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

        public ActionResult ById(int id)
        {
            var repository = Sitecore.Ecommerce.Context.Entity.Resolve<IProductReviewRepository>();
            var review = repository.GetById(id);
            return Json(review, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Save(ProductReviewModel reviewModel)
        {
            if (reviewModel == null)
            {
                return Json(false);
            }
            var repository = Sitecore.Ecommerce.Context.Entity.Resolve<IProductReviewRepository>();
            var review = repository.GetById(reviewModel.ReviewId);
            if (review == null)
            {
                return Json(false);
            }
            review.ProductCode = reviewModel.ProductCode;
            review.ReviewTitle = reviewModel.ReviewTitle;
            review.ReviewerName = reviewModel.ReviewerName;
            review.ReviewerEmail = reviewModel.ReviewerEmail;
            review.Rating = reviewModel.Rating;
            review.Review = reviewModel.Review;
            review.Approved = reviewModel.Approved;
            repository.Flush();
            return Json(true);
        }

        public ActionResult Delete(int reviewId)
        {
            var repository = Sitecore.Ecommerce.Context.Entity.Resolve<IProductReviewRepository>();
            var review = repository.GetById(reviewId);
            if (review == null)
            {
                return Json(false);
            }
            review.Hidden = true;
            repository.Flush();
            return Json(true);
        }
    }
}