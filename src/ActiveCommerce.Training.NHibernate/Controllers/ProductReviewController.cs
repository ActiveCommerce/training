using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace ActiveCommerce.Training.SimpleReviews.Controllers
{
    public class ProductReviewController : System.Web.Mvc.Controller
    {
        private const string ProductCode = "ABC";

        public ActionResult Create()
        {
            var review = new ProductReview()
            {
                ProductCode = ProductCode,
                ReviewTitle = "Lorem ipsum review dolor sit",
                ReviewerName = "Testy Tester",
                ReviewerEmail = "test@activecommerce.com",
                ReviewedOn = DateTime.Now,
                Rating = 4,
                Review = "Lorem ipsum dolor sit amet nunc bacon kitty sitecore rocks"
            };
            var repository = Sitecore.Ecommerce.Context.Entity.Resolve<IProductReviewRepository>();
            repository.Add(review);
            repository.Flush();
            return Content("Success!");
        }

        public ActionResult Update()
        {
            var repository = Sitecore.Ecommerce.Context.Entity.Resolve<IProductReviewRepository>();
            var review = repository.GetByProduct(ProductCode).FirstOrDefault();
            if (review != null)
            {
                review.Rating = 5;
                repository.Flush();
            }
            return Content("Success!");
        }

        public ActionResult Query()
        {
            var repository = Sitecore.Ecommerce.Context.Entity.Resolve<IProductReviewRepository>();
            var reviews = repository.GetByProduct(ProductCode);
            return Json(reviews, JsonRequestBehavior.AllowGet);
        }

    }
}