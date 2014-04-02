using ActiveCommerce.IoC;
using Sitecore.Ecommerce.DomainModel.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using Sitecore.Ecommerce;

namespace ActiveCommerce.Training.CustomProduct
{
    public class RegisterTypes : ITypeRegistration
    {
        public void Process(Microsoft.Practices.Unity.IUnityContainer container)
        {
            //register our product type for a specific product template
            container.RegisterType<ProductBaseData, BookProduct>("{7CAA3164-BB54-40B5-923F-C90AB2BCC34A}");
        }

        public int SortOrder
        {
            get { return 0; }
        }
    }
}