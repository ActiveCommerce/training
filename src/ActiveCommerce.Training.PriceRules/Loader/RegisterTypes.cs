using ActiveCommerce.IoC;
using ActiveCommerce.Training.PriceRules.Configuration;
using Microsoft.Practices.Unity;

namespace ActiveCommerce.Training.PriceRules.Loader
{
    public class RegisterTypes : ITypeRegistration
    {

        public void Process(Microsoft.Practices.Unity.IUnityContainer container)
        {
            container.RegisterType(typeof(Sitecore.Ecommerce.DomainModel.Configurations.GeneralSettings), typeof(GeneralSettings), new InjectionMember[] {
                new InjectionProperty("Alias", "General")
            });
        }

        public int SortOrder
        {
            get { return 0; }
        }
    }
}