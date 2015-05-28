using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.IoC;
using Microsoft.Practices.Unity;

namespace ActiveCommerce.Training.ProductStockExtension.IoC
{
    public class RegisterTypes : ITypeRegistration
    {
        public void Process(Microsoft.Practices.Unity.IUnityContainer container)
        {
            container.RegisterType<ActiveCommerce.Products.Stock.ProductStock, ActiveCommerce.Training.ProductStockExtension.ProductStock>();
            container.RegisterType<ActiveCommerce.Products.Stock.IProductStockManager, ActiveCommerce.Training.ProductStockExtension.ProductStockManager>();
        }

        public int SortOrder
        {
            get { return 0; }
        }
    }
}