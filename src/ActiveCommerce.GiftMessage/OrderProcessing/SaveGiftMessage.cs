using System;
using ActiveCommerce.GiftMessage.CheckOut;
using ActiveCommerce.Orders.Pipelines;
using Sitecore.Ecommerce;
using ActiveCommerce.Orders.Management;
using ActiveCommerce.Training.OrderExtension;

namespace ActiveCommerce.GiftMessage.OrderProcessing
{
    public class SaveGiftMessage : OrderPipelineProcessor
    {
        protected override bool ContinueOnFailure
        {
            get { return true; }
        }

        protected override void DoProcess(OrderPipelineArgs args)
        {
            var order = args.Order as ActiveCommerce.Training.OrderExtension.Order;
            var checkout = args.CheckOut as IGiftMessage;
            if (order != null && checkout != null)
            {
                order.GiftMessage = checkout.GiftMessage;
            }
        }
    }
}