using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using ActiveCommerce.ShopContext;
using ActiveCommerce.SitecoreX;
using Microsoft.Practices.Unity;
using ActiveCommerce.Addresses;
using ActiveCommerce.OrderProcessing;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Ecommerce.DomainModel.Data;
using Sitecore.Ecommerce.DomainModel.Orders;
using Sitecore.Ecommerce.Search;
using Sitecore.SecurityModel;
using Sitecore.Sites;
using Sitecore.StringExtensions;
using Sitecore.Tasks;

namespace ActiveCommerce.Training.OrderUpdate
{
    public class OrderUpdateCommand
    {
        public void UpdateOrders(Item[] itemArray, CommandItem commandItem, ScheduleItem scheduleItem)
        {
            Sitecore.Diagnostics.Log.Info("Starting update...", this);
            var schedule = new ActiveCommerce.SitecoreX.ScheduledTasks.ExtendedScheduleItem(scheduleItem);
            using (new SecurityDisabler())
            {
                using (new ShopContextSwitcher(schedule.SiteContext, schedule.Database))
                {
                    //otherwise, if we have a preview cookie in place, we can't get to the orders root item
                    using (new ItemFilteringDisabler())
                    {
                        var startStatus = schedule.Arguments["startStatus"];

                        //to get an appropriately typed OrderStatus, to set on an order
                        var endStatus = Sitecore.Ecommerce.Context.Entity.Resolve<OrderStatus>(schedule.Arguments["endStatus"]);

                        DoUpdate(startStatus, endStatus);
                    }
                }
            }
        }

        public void DoUpdate(string startStatus, OrderStatus endStatus)
        {
            var orderManager = Sitecore.Ecommerce.Context.Entity.Resolve<IOrderManager<Order>>();

            // find orders which have not yet been processed
            var queryOrders = new Query();
            queryOrders.AppendField("Status", startStatus, MatchVariant.Exactly);
            var ordersToProcess = orderManager.GetOrders(queryOrders).ToList();

            foreach (var order in ordersToProcess)
            {
                try
                {
                    var myOrder = order as OrderExtension.Order;
                    if (myOrder != null && myOrder.ExternalOrderId != Guid.Empty)
                    {
                        CheckOrder(myOrder, endStatus);
                    }
                }
                catch (Exception e)
                {
                    Sitecore.Diagnostics.Log.Error("Error exporting order {0}".FormatWith(order.OrderNumber), e, this);
                }
            }
        }

        public void CheckOrder(ActiveCommerce.Training.OrderExtension.Order order, OrderStatus endStatus)
        {
            var orderManager = Sitecore.Ecommerce.Context.Entity.Resolve<IOrderManager<Order>>();
            var client = new Services.OrderServiceClient();
            var serviceOrder = client.Get(order.ExternalOrderId);
            if (serviceOrder != null && serviceOrder.Shipped)
            {
                order.TrackingNumber = serviceOrder.TrackingUrl;
                order.Status = endStatus;
                orderManager.SaveOrder(order);
            }
        }

    }
}