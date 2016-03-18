using ActiveCommerce.IoC;
using ActiveCommerce.Training.InvoicePayment.Payments;
using Microsoft.Practices.Unity;

namespace ActiveCommerce.Training.InvoicePayment.IoC
{
    public class RegisterTypes : ITypeRegistration
    {
        public void Process(Microsoft.Practices.Unity.IUnityContainer container)
        {
            //payment provider
            container.RegisterType(typeof(Sitecore.Ecommerce.DomainModel.Payments.PaymentProvider),
                                   typeof(InvoicePaymentOption), "InvoicePayment");
        }

        public int SortOrder
        {
            get { return 0; }
        }
    }
}