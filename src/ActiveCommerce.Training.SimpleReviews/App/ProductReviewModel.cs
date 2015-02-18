using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.SimpleReviews.App
{
    public class ProductReviewModel
    {
        public int ReviewId { get; set; }
        public string ProductCode { get; set; }
        public string ReviewTitle { get; set; }
        public string ReviewerName { get; set; }
        public string ReviewerEmail { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; }
        public bool Approved { get; set; }
    }
}