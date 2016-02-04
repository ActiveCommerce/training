using ActiveCommerce.Training.SimpleReviews.Extensions.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using Sitecore.Ecommerce.DomainModel.Products;

namespace ActiveCommerce.Training.SimpleReviews.Extensions
{
    public static class ProductExtensions
    {
        private static volatile ProductHelper _instance;
        private static readonly object _lock = new object();

        private static ProductHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = Sitecore.Ecommerce.Context.Entity.Resolve<ProductHelper>();
                        }
                    }
                }
                return _instance;
            }
        }

        public static double? GetAverageRating(this ProductBaseData product)
        {
            return Instance.GetAverageRating(product);
        }
    }
}