using ActiveCommerce.Carts;
using ActiveCommerce.IoC;
using ActiveCommerce.Training.Discounts.ViewModel;
using ActiveCommerce.Web.Models.Factories;
using Microsoft.Practices.Unity;

namespace ActiveCommerce.Training.Discounts.Loader
{
    public class RegisterTypes : ITypeRegistration
    {
        public void Process(Microsoft.Practices.Unity.IUnityContainer container)
        {
            container.RegisterType<ActiveCommerce.Web.Models.ShoppingCartViewModel, ShoppingCartViewModel>(new HierarchicalLifetimeManager());
            container.RegisterType(
                typeof(ActiveCommerce.Web.Models.Factories.IViewModelFactory<ShoppingCart, ActiveCommerce.Web.Models.ShoppingCartViewModel>),
                typeof(ActiveCommerce.Training.Discounts.ViewModel.ShoppingCartViewModelFactory),
                new HierarchicalLifetimeManager(),
                new InjectionMember[] {
                    new InjectionProperty("EstimatedCosts", true),
                    new InjectionProperty("ShopContext"),
                    new InjectionProperty("PriceFormatter"),
                    new InjectionProperty("CartProductViewModelFactory"),
                    new InjectionProperty("RelatedProductViewModelFactory")
                }
            );
        }

        public int SortOrder
        {
            get { return 0; }
        }
    }
}