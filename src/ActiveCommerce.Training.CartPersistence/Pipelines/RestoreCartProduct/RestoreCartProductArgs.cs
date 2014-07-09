using Sitecore.Ecommerce.DomainModel.Carts;
using Sitecore.Ecommerce.DomainModel.Products;
using Sitecore.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.RestoreCartProduct
{
    public class RestoreCartProductArgs : PipelineArgs
    {
        public IProductRepository ProductRepository { get; set; }
        public IProductStockManager StockManager { get; set; }
        public IShoppingCartManager CartManager { get; set; }
        public ActiveCommerce.Carts.ShoppingCart ShoppingCart { get; set; }
        public string ProductCode { get; set; }
        public uint Quantity { get; set; }
        public RestoreCartResult Result { get; set; }
    }
}