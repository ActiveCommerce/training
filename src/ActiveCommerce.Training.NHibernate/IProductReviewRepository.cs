using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.NHibernate
{
    public interface IProductReviewRepository
    {
        ProductReview GetById(int reviewId);
        IQueryable<ProductReview> GetByProduct(string productCode);
        IQueryable<ProductReview> GetAll(); 
        void Add(ProductReview review);
        void Delete(ProductReview review);
        void Flush();
    }
}