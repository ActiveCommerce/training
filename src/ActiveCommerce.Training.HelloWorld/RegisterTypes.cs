using ActiveCommerce.IoC;
using Microsoft.Practices.Unity;

namespace ActiveCommerce.Training.HelloWorld
{
    public class RegisterTypes : ITypeRegistration
    {
        public void Process(Microsoft.Practices.Unity.IUnityContainer container)
        {
            container.RegisterType<Sitecore.Ecommerce.DomainModel.Carts.IShoppingCartManager, ActiveCommerce.Training.HelloWorld.ShoppingCartManager>(
                new InjectionMember[] {
                    new InjectionProperty("ProductRepository"),
                    new InjectionProperty("ProductPriceManager")
                }
            );
        }

        public int SortOrder
        {
            get { return 0; }
        }
    }
}