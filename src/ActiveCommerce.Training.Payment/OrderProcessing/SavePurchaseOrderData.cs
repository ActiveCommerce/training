using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.OrderProcessing;
using ActiveCommerce.Training.Payment.CheckOut;
using Sitecore.Ecommerce.DomainModel.CheckOuts;
using Sitecore.Ecommerce.DomainModel.Orders;
using Microsoft.Practices.Unity;

namespace ActiveCommerce.Training.Payment.OrderProcessing
{
    public class SavePurchaseOrderData : IOrderPipelineProcessor
    {
        public void Process(OrderPipelineArgs args)
        {
            if (!(args.PaymentProvider is InvoicePaymentOption))
            {
                return;
            }

            var checkout = Sitecore.Ecommerce.Context.Entity.GetInstance<ICheckOut>() as IInvoicePayment;
            if (checkout == null)
            {
                Sitecore.Diagnostics.Log.Warn(string.Format("Could not find {0} checkout data when saving purchase order data", typeof(IInvoicePayment)), this);
                return;
            }
            
            if (string.IsNullOrWhiteSpace(checkout.PurchaseOrderNumber))
            {
                Sitecore.Diagnostics.Log.Warn("Empty purchase order number when saving purchase order data", this);
                return;
            }

            var order = args.Order as ActiveCommerce.Training.OrderExtension.Order;
            if (order == null)
            {
                Sitecore.Diagnostics.Log.Warn(
                    string.Format(
                        "Order was not of expected type when saving purchase order data. Was: {0}, Expected: {1}",
                        args.Order.GetType(), typeof (ActiveCommerce.Training.OrderExtension.Order)), this);
                return;
            }

            var orderManager = Sitecore.Ecommerce.Context.Entity.Resolve<IOrderManager<Order>>();
            order.PurchaseOrderNumber = checkout.PurchaseOrderNumber;
            orderManager.SaveOrder(order);
        }
    }
}