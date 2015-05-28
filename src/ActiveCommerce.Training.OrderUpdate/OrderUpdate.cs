using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using ActiveCommerce.Orders;
using ActiveCommerce.Orders.Management;
using ActiveCommerce.Orders.States;
using ActiveCommerce.ShopContext;
using ActiveCommerce.SitecoreX;
using Microsoft.Practices.Unity;
using Sitecore.Data;
using Sitecore.Ecommerce.DomainModel.Data;
using Sitecore.Ecommerce.DomainModel.Orders;
using Sitecore.Ecommerce.Search;
using Sitecore.SecurityModel;
using Sitecore.Sites;
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
                            orderManager.Save(order);
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