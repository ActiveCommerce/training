using ActiveCommerce.IoC;
using Microsoft.Practices.Unity;
using Sitecore.Ecommerce.DomainModel.CheckOuts;

namespace ActiveCommerce.Training.OnsitePayment.Loader
{
    public class RegisterTypes : ITypeRegistration
    {
        public void Process(Microsoft.Practices.Unity.IUnityContainer container)
        {
            //payment provider
            container.RegisterType(typeof(Sitecore.Ecommerce.DomainModel.Payments.PaymentProvider),
                                   typeof(MockServiceOnsitePaymentProvider), "MockService");
        }

        public int SortOrder
        {
            get { return 0; }
        }
    }
}