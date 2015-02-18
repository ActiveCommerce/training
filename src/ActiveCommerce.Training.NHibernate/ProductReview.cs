using System;

namespace ActiveCommerce.Training.SimpleReviews
{
    public class ProductReview
    {
        //IMPORTANT: All properties must be virtual!
        public virtual int ReviewId { get; set; }
        public virtual string ShopContext { get; set; }
        public virtual bool Approved { get; set; }
        public virtual bool Hidden { get; set; }
        public virtual string ProductCode { get; set; }
        public virtual string ReviewTitle { get; set; }
        public virtual string ReviewerName { get; set; }
        public virtual string ReviewerEmail { get; set; }
        public virtual DateTime ReviewedOn { get; set; }
        public virtual int Rating { get; set; }
        public virtual string Review { get; set; }
    }
}