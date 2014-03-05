using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.GiftMessage.CheckOut;
using ActiveCommerce.OrderProcessing;
using Sitecore.Ecommerce.DomainModel.Orders;
using Sitecore.Ecommerce;

namespace ActiveCommerce.GiftMessage.OrderProcessing
{
    public class SaveGiftMessage : IOrderPipelineProcessor
    {
        public void Process(OrderPipelineArgs args)
        {
            var order = args.Order as ActiveCommerce.GiftMessage.Orders.Order;
            var checkout = args.CheckOut as IGiftMessage;
            if (order != null && checkout != null)
            {
                order.GiftMessage = checkout.GiftMessage;

                var orderManager = Sitecore.Ecommerce.Context.Entity.Resolve<IOrderManager<Order>>();
                orderManager.SaveOrder(order);
            }
        }
    }
}