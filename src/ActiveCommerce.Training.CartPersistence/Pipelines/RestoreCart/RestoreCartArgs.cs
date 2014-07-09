using Sitecore.Ecommerce.DomainModel.Carts;
using Sitecore.Ecommerce.DomainModel.Products;
using Sitecore.Ecommerce.DomainModel.Users;
using Sitecore.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.RestoreCart
{
    public class RestoreCartArgs : PipelineArgs
    {
        public RestoreCartResult Result { get; set; }
        public IProductRepository ProductRepository { get; set; }
        public IProductStockManager StockManager { get; set; }
        public IShoppingCartManager CartManager { get; set; }
        public ActiveCommerce.Carts.ShoppingCart ShoppingCart { get; set; }
        public ICustomerManager<CustomerInfo> CustomerManager { get; set; }
        public IDictionary<string, uint> CartItems { get; set; }
        public string CouponCode { get; set; }
    }
}