using ActiveCommerce.Training.CartPersistence.Pipelines.RestoreCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using Sitecore.Ecommerce.DomainModel.Carts;
using Sitecore.Ecommerce.DomainModel.Products;
using Sitecore.Ecommerce.DomainModel.Users;

namespace ActiveCommerce.Training.CartPersistence.Users
{
    public class CustomerManager<T> : ActiveCommerce.Users.CustomerManager<T> where T : CustomerInfo
    {
        public override bool LogInCustomer(string nickName, string password)
        {
            var success = base.LogInCustomer(nickName, password);
            if (success)
            {
                var restoreProductArgs = new RestoreCartArgs
                {
                    CartManager = Sitecore.Ecommerce.Context.Entity.Resolve<IShoppingCartManager>(),
                    ShoppingCart = Sitecore.Ecommerce.Context.Entity.GetInstance<ShoppingCart>() as ActiveCommerce.Carts.ShoppingCart,
                    StockManager = Sitecore.Ecommerce.Context.Entity.Resolve<IProductStockManager>(),
                    ProductRepository = Sitecore.Ecommerce.Context.Entity.Resolve<IProductRepository>(),
                    CustomerManager = Sitecore.Ecommerce.Context.Entity.Resolve<ICustomerManager<CustomerInfo>>(),
                    Result = new RestoreCartResult()
                };
                RestoreCartPipeline.Run(restoreProductArgs);
            }
            return success;
        }
    }
}