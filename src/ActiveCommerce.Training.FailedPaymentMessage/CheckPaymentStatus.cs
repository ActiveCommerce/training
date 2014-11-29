using ActiveCommerce.OrderProcessing;
using Sitecore.Ecommerce.DomainModel.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using Sitecore.Ecommerce.Payments;

namespace ActiveCommerce.Training.FailedPaymentMessage
{
    public class CheckPaymentStatus : ActiveCommerce.OrderProcessing.CheckPaymentStatus
    {
        public new void Process(OrderPipelineArgs args)
        {
            base.Process(args);
            if (args.Status != OrderPipelineStatus.PaymentFailed)
            {
                return;
            }
            var transactionData = Sitecore.Ecommerce.Context.Entity.Resolve<ITransactionData>();
            if (transactionData == null)
            {
                return;
            }
            var message = transactionData.GetPersistentValue(args.Cart.OrderNumber, TransactionConstants.ProviderMessage) as string;
            if (string.IsNullOrEmpty(message))
            {
                return;
            }
            args.AddMessage(message);
        }
    }
}