using System.Linq;

namespace ActiveCommerce.Training.SimpleReviews
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