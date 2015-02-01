using ActiveCommerce.IoC;
using Microsoft.Practices.Unity;

namespace ActiveCommerce.Training.OrderExtension
{
    public class RegisterTypes : ITypeRegistration
    {
        public void Process(Microsoft.Practices.Unity.IUnityContainer container)
        {
            container.RegisterType<ActiveCommerce.Orders.IOrderFactory, OrderFactory>(new HierarchicalLifetimeManager());
        }

        public int SortOrder
        {
            get { return 0; }
        }
    }
}