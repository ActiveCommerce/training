using System;
using System.Linq;
using System.ServiceModel.Activation;
using ActiveCommerce.Orders;
using ActiveCommerce.Orders.States;
using ActiveCommerce.ShopContext;
using Microsoft.Practices.Unity;
using Sitecore.Data;
using Sitecore.Sites;

namespace ActiveCommerce.Training.OrderUpdateService.Services
{
    [AspNetCompatibilityRequirements(
        RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class OrderUpdate : IOrderUpdate
    {
        //you should put these into a config and access via Sitecore.Configuration.Settings
        protected readonly string EndStatus = "Complete";
        protected readonly string ShopContext = "sherpa_winter_outfitters";
        protected readonly string DatabaseContext = "master";

        public void UpdateOrderShipped(string orderNumber, string trackingUrl)
        {
            try
            {
                var siteContext = SiteContextFactory.GetSiteContext(ShopContext);
                var databaseContext = Database.GetDatabase(DatabaseContext);
                using (new ShopContextSwitcher(siteContext, databaseContext))
                {
                    var statesRepository = Sitecore.Ecommerce.Context.Entity.Resolve<IOrderStatesRepository>();
                    var endState = statesRepository.GetStates().Single(state => state.Code == EndStatus);

                    var orderManager = Sitecore.Ecommerce.Context.Entity.Resolve<Orders.Management.IOrderManager<Order>>();
                    var order = orderManager.GetOrder(orderNumber);
                    if (order == null)
                    {
                        throw new Exception(string.Format("Order {0} not found", orderNumber));
                    }
                    order.State = endState;
                    order.DefaultDelivery.TrackingID = trackingUrl;
                    orderManager.Save(order);
                }
            }
            catch (Exception e)
            {
                Sitecore.Diagnostics.Log.Error("Error during order update service call", e, this);
                throw;
            }
        }
    }
}
