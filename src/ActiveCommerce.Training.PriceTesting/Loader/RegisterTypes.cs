using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.IoC;
using Microsoft.Practices.Unity;

namespace ActiveCommerce.Training.PriceTesting.Loader
{
    public class RegisterTypes : ITypeRegistration
    {

        public void Process(Microsoft.Practices.Unity.IUnityContainer container)
        {
            container.RegisterType(typeof(Sitecore.Ecommerce.DomainModel.Configurations.GeneralSettings), typeof(ActiveCommerce.Training.PriceTesting.Configuration.GeneralSettings), new InjectionMember[] {
                new InjectionProperty("Alias", "General")
            });
        }

        public int SortOrder
        {
            get { return 0; }
        }
    }
}