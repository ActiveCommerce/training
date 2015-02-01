using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using ActiveCommerce.Orders;
using ActiveCommerce.Orders.Management;
using ActiveCommerce.ShopContext;
using ActiveCommerce.SitecoreX;
using Microsoft.Practices.Unity;
using ActiveCommerce.Addresses;
using Sitecore.Data;
using Sitecore.Ecommerce.DomainModel.Data;
using Sitecore.Ecommerce.Search;
using Sitecore.SecurityModel;
using Sitecore.Sites;
using Sitecore.StringExtensions;
using Sitecore.Tasks;
using Item = Sitecore.Data.Items.Item;
using Order = ActiveCommerce.Training.OrderExtension.Order;

namespace ActiveCommerce.Training.OrderBatching
{
    public class OrderExportCommand
    {
        public void ExportOrders(Item[] itemArray, CommandItem commandItem, ScheduleItem scheduleItem)
        {
            Sitecore.Diagnostics.Log.Info("Starting export...", this);
            var schedule = new ActiveCommerce.SitecoreX.ScheduledTasks.ExtendedScheduleItem(scheduleItem);
            using (new ShopContextSwitcher(schedule.SiteContext, schedule.Database))
            {
                var startStatus = schedule.Arguments["startStatus"];
                var endStatus = schedule.Arguments["endStatus"];
                DoExport(startStatus, endStatus);
            }
        }

        public void DoExport(string startStatus, string endStatus)
        {
            var statesRepository = Sitecore.Ecommerce.Context.Entity.Resolve<IOrderStatesRepository>();
            var endState = statesRepository.GetStates().Single(state => state.Code == endStatus);

            var orderManager = Sitecore.Ecommerce.Context.Entity.Resolve<IOrderManager<ActiveCommerce.Orders.Order>>();
            var ordersToProcess = orderManager.GetOrders().Where(order => order.State.Code == startStatus);

            foreach (var order in ordersToProcess)
            {
                try
                {
                    var id = ExportOrder(order);
                    (order as ActiveCommerce.Training.OrderExtension.Order).ExternalOrderId = id;
                    order.State = endState;
                    orderManager.Save(order);
                }
                catch (Exception e)
                {
                    Sitecore.Diagnostics.Log.Error("Error exporting order {0}".FormatWith(order.OrderId), e, this);
                }
            }
        }

        public Guid ExportOrder(ActiveCommerce.Orders.Order order)
        {
            var serviceOrder = new Services.Order
            {
                CustomerEmail = order.BuyerCustomerParty.Party.Contact.ElectronicMail,
                CustomerName = order.BuyerCustomerParty.Party.PartyName,
                Billing = ConvertAddress((Address) order.BuyerCustomerParty.Party.PostalAddress),
                Shipping = ConvertAddress((Address) order.DefaultDelivery.DeliveryParty.PostalAddress)
            };
            var lines = new List<Services.OrderLine>();
            foreach (var line in order.OrderLines)
            {
                var newLine = new Services.OrderLine
                {
                    ProductId = line.LineItem.Item.Code,
                    Price = line.LineItem.Price.PriceAmount.Value,
                    Quantity = (uint) line.LineItem.Quantity
                };
                lines.Add(newLine);
            }
            serviceOrder.OrderLines = lines.ToArray();
            serviceOrder.Total = order.AnticipatedMonetaryTotal.TaxInclusiveAmount.Value;
            serviceOrder.TaxTotal = order.TaxTotal.TaxAmount.Value;
            var shippingCharges = order.AllowanceCharge.Cast<AllowanceCharge>().Where(x => x.ChargeIndicator && x.ShippingIndicator);
            serviceOrder.ShippingCost = shippingCharges.Sum(x => x.Amount.Value);

            var client = new Services.OrderServiceClient();
            var id = client.CreateOrder(serviceOrder);
            Sitecore.Diagnostics.Log.Warn("Successfully exported order {0}".FormatWith(id), this);
            return id;
        }

        protected Services.Address ConvertAddress(ActiveCommerce.Orders.Address address)
        {
            var newAddress = new Services.Address
            {
                AddressLine = address.AddressLine,
                City = address.CityName,
                State = address.CountrySubentity,
                Country = address.CountryCode,
                Zip = address.PostalZone
            };
            return newAddress;
        }
    }
}