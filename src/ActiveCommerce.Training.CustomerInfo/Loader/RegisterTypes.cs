using ActiveCommerce.IoC;
using Microsoft.Practices.Unity;

namespace ActiveCommerce.Training.CustomerInfo.Loader
{
    public class RegisterTypes : ITypeRegistration
    {

        public void Process(Microsoft.Practices.Unity.IUnityContainer container)
        {
            //map our new CustomerInfo class.
            container.RegisterType<Sitecore.Ecommerce.DomainModel.Users.CustomerInfo, ActiveCommerce.Training.CustomerInfo.Users.CustomerInfo>();
        }

        public int SortOrder
        {
            get { return 0; }
        }
    }
}