using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.IoC;
using Microsoft.Practices.Unity;
using Sitecore.Ecommerce.DomainModel.CheckOuts;
using Sitecore.Ecommerce.DomainModel.Payments;

namespace ActiveCommerce.Training.Payment.Loader
{
    public class RegisterTypes : ITypeRegistration
    {
        public void Process(Microsoft.Practices.Unity.IUnityContainer container)
        {
            //checkout data holder (session-based)
            container.RegisterType<ICheckOut, ActiveCommerce.Training.Payment.CheckOut.CheckOut>();

            //payment provider
            container.RegisterType(typeof(Sitecore.Ecommerce.DomainModel.Payments.PaymentProvider),
                                   typeof(ActiveCommerce.Training.Payment.InvoicePaymentOption), "InvoicePayment");
        }

        public int SortOrder
        {
            get { return 0; }
        }
    }
}