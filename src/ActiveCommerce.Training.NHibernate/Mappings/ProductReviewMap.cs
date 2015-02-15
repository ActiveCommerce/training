using ActiveCommerce.Data.Mappings;
using FluentNHibernate.Mapping;

namespace ActiveCommerce.Training.SimpleReviews.Mappings
{
    public class ProductReviewMap : ClassMap<ProductReview>
    {
        public ProductReviewMap()
        {
            //our configuration will default to HiLo identity generation
            Id(x => x.ReviewId);
            Map(x => x.ProductCode).Length(Constants.BigCodeFieldLength)
                                   .Not.Nullable()
                                   .Index("IDX_Product_ShopContext");
            Map(x => x.ShopContext).Length(Constants.NameFieldLength)
                                   .Not.Nullable()
                                   .Index("IDX_Product_ShopContext");
            Map(x => x.ReviewerName).Length(Constants.NameFieldLength)
                                    .Not.Nullable();
            Map(x => x.ReviewerEmail).Length(Constants.NameFieldLength)
                                     .Not.Nullable();
            Map(x => x.ReviewedOn).Not.Nullable();
            Map(x => x.Rating).Not.Nullable();
            Map(x => x.Review).Not.Nullable();
            Map(x => x.Approved).Not.Nullable();
        }
    }
}