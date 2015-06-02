using ActiveCommerce.IoC;
using Microsoft.Practices.Unity;
using Sitecore.Ecommerce.DomainModel.CheckOuts;

namespace ActiveCommerce.Training.InvoicePayment.Loader
{
    public class RegisterTypes : ITypeRegistration
    {
        public void Process(Microsoft.Practices.Unity.IUnityContainer container)
        {
            //checkout data holder (session-based)
            container.RegisterType<ICheckOut, CheckOut.CheckOut>();

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