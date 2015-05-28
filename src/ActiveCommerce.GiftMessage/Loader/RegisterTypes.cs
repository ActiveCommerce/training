using System;
using ActiveCommerce.Carts;
using ActiveCommerce.IoC;
using ActiveCommerce.Web.Models;
using Sitecore.Ecommerce.DomainModel.CheckOuts;
using Microsoft.Practices.Unity;

namespace ActiveCommerce.GiftMessage.Loader
{
    public class RegisterTypes : ITypeRegistration
    {
        public void Process(Microsoft.Practices.Unity.IUnityContainer container)
        {
            //checkout data holder (session-based)
            container.RegisterType<ICheckOut, ActiveCommerce.GiftMessage.CheckOut.CheckOut>();

            //checkout view model -- used for JSON model during checkout
            container.RegisterType<CheckoutViewModel, ActiveCommerce.GiftMessage.Model.CheckoutViewModel>();
            container.RegisterType(
                typeof(ActiveCommerce.Web.Models.Factories.IViewModelFactory<ShoppingCart, CheckoutViewModel>),
                typeof(ActiveCommerce.GiftMessage.Model.Factories.CheckoutViewModelFactory),
                new TransientLifetimeManager(),
                new InjectionMember[] {
                    new InjectionProperty("ShopContext"),
                    new InjectionProperty("PriceFormatter"),
                    new InjectionProperty("CustomerManager"), 
                    new InjectionProperty("ShippingOptionViewModelFactory"),
                    new InjectionProperty("PaymentViewModelFactory"), 
                    new InjectionProperty("CartProductViewModelFactory")
                }
            );
        }

        public int SortOrder
        {
            get { return 0; }
        }
    }
}