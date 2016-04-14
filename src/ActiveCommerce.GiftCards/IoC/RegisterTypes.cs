using ActiveCommerce.GiftCards.Data;
using ActiveCommerce.GiftCards.Payments;
using ActiveCommerce.IoC;
using Microsoft.Practices.Unity;
using Sitecore.Ecommerce.DomainModel.Payments;

namespace ActiveCommerce.GiftCards.IoC
{
    public class RegisterTypes : ITypeRegistration
    {
        public void Process(IUnityContainer container)
        {
            container.RegisterType(typeof(PaymentProvider), typeof(GiftCardPaymentProvider), "GiftCard");
            container.RegisterType(typeof(IGiftCardManager), typeof(GiftCardManager));
            /*
             * Resolving our repository through unity allows injection of the Active Commerce
             * ISessionBuilder, which will manage your NHibernate sessions and ensure they are
             * properly Disposed.
             */
            container.RegisterType(typeof(IGiftCardRepository), typeof(GiftCardRepository));
        }

        public int SortOrder { get { return 0; } }
    }
}