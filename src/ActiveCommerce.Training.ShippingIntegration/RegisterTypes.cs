using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;

namespace ActiveCommerce.Training.ShippingIntegration
{
    public class RegisterTypes : ActiveCommerce.IoC.ITypeRegistration
    {
        public void Process(Microsoft.Practices.Unity.IUnityContainer container)
        {
            container.RegisterType<ActiveCommerce.Shipping.IShippingService,
                ActiveCommerce.Training.ShippingIntegration.ShippingService>("training");
        }

        public int SortOrder
        {
            get { return 0; }
        }
    }
}