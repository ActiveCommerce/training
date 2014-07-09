using Sitecore.Ecommerce.DomainModel.Users;
using Sitecore.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.PersistCart
{
    public class PersistCartArgs : PipelineArgs
    {
        public ActiveCommerce.Carts.ShoppingCart ShoppingCart { get; set; }
        public ICustomerManager<CustomerInfo> CustomerManager { get; set; }
        public IDictionary<string, uint> CartItems { get; set; }
        public string CouponCode { get; set; }
    }
}