using Sitecore.Ecommerce.DomainModel.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.SimpleReviews.Extensions.Helpers
{
    public class ProductHelper
    {
        protected virtual IProductReviewRepository ProductReviewRepository { get; private set; }

        public ProductHelper(IProductReviewRepository productReviewRepository)
        {
            ProductReviewRepository = productReviewRepository;
        }

        public virtual double? GetAverageRating(ProductBaseData product)
        {
            var reviews = ProductReviewRepository.GetByProduct(product.Code).Where(x => x.Approved && !x.Hidden);
            if (reviews.Any())
            {
                return reviews.Average(x => x.Rating);
            }
            return null;
        }
    }
}