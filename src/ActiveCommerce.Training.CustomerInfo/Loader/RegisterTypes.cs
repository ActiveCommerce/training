using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.IoC;
using Microsoft.Practices.Unity;

namespace ActiveCommerce.Training.CustomerInfo.Loader
{
    public class RegisterTypes : ITypeRegistration
    {

        public void Process(Microsoft.Practices.Unity.IUnityContainer container)
        {
            //map our new CustomerInfo class. injection properties are important.
            container.RegisterType<Sitecore.Ecommerce.DomainModel.Users.CustomerInfo,
                                   ActiveCommerce.Training.CustomerInfo.Users.CustomerInfo>(new InjectionMember[]
            {
                new InjectionProperty("BillingAddress"),
                new InjectionProperty("ShippingAddress")
            });
        }

        public int SortOrder
        {
            get { return 0; }
        }
    }
}