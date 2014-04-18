using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.IoC;
using Microsoft.Practices.Unity;

namespace ActiveCommerce.Training.ComparePrice
{
    public class RegisterTypes : ITypeRegistration
    {
        public void Process(Microsoft.Practices.Unity.IUnityContainer container)
        {
            container.RegisterType(typeof(ActiveCommerce.Products.Comparison.IProductComparer<>),
                                   typeof(ActiveCommerce.Training.ComparePrice.ProductComparer<>), new InjectionMember[]
                                                                                                      {
                                                                                                          new InjectionProperty("ProductRepository"),
                                                                                                          new InjectionProperty("LookupResolver")
                                                                                                      });
        }

        public int SortOrder
        {
            get { return 0; }
        }
    }
}