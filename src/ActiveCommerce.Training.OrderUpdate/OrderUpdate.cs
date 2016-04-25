using System;
using System.Collections.Generic;
using System.Linq;
using ActiveCommerce.Orders.States;
using ActiveCommerce.ShopContext;
using Microsoft.Practices.Unity;
using Sitecore.StringExtensions;
using Sitecore.Tasks;
using Item = Sitecore.Data.Items.Item;

namespace ActiveCommerce.Training.OrderUpdate
{
    public class OrderUpdateCommand
    {
        public void UpdateOrders(Item[] itemArray, CommandItem commandItem, ScheduleItem scheduleItem)
        {
            Sitecore.Diagnostics.Log.Info("Starting update...", this);
            var schedule = new ActiveCommerce.SitecoreX.ScheduledTasks.ExtendedScheduleItem(scheduleItem);
            using (new ShopContextSwitcher(schedule.SiteContext, schedule.Database))
            {
                var startStatus = schedule.Arguments["startStatus"];
                var endStatus = schedule.Arguments["endStatus"];

                DoUpdate(startStatus, endStatus);
            }
        }

        public void DoUpdate(string startStatus, string endStatus)
        {
            var statesRepository = Sitecore.Ecommerce.Context.Entity.Resolve<IOrderStatesRepository>();
            var endState = statesRepository.GetStates().Single(state => state.Code == endStatus);

            var orderManager = Sitecore.Ecommerce.Context.Entity.Resolve<Orders.Management.IOrderManager<OrderExtension.Order>>();
            var ordersToProcess = orderManager.GetOrders().Where(order => order.State.Code == startStatus);

            foreach (var order in ordersToProcess)
            {
                try
                {
                    if (order.ExternalOrderId != Guid.Empty)
                    {
                        var client = new Services.OrderServiceClient();
                        var serviceOrder = client.Get(order.ExternalOrderId);
                        if (serviceOrder != null && serviceOrder.Shipped)
                        {
                            order.DefaultDelivery.TrackingID = serviceOrder.TrackingUrl;
                            order.State = endState;
                            orderManager.Save();
                        }
                    }
                }
                catch (Exception e)
                {
                    Sitecore.Diagnostics.Log.Error("Error exporting order {0}".FormatWith(order.OrderId), e, this);
                }
            }
        }
    }
}