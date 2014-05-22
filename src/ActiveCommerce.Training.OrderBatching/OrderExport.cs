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

namespace ActiveCommerce.Training.OrderBatching
{
    public class OrderExportCommand
    {
        public void ExportOrders(Item[] itemArray, CommandItem commandItem, ScheduleItem scheduleItem)
        {
            Sitecore.Diagnostics.Log.Info("Starting export...", this);
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

                        DoExport(startStatus, endStatus);
                    }
                }
            }
        }

        public void DoExport(string startStatus, OrderStatus endStatus)
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
                    var id = ExportOrder(order);
                    (order as ActiveCommerce.Training.OrderExtension.Order).ExternalOrderId = id;
                    order.Status = endStatus;
                    orderManager.SaveOrder(order);
                }
                catch (Exception e)
                {
                    Sitecore.Diagnostics.Log.Error("Error exporting order {0}".FormatWith(order.OrderNumber), e, this);
                }
            }
        }

        public Guid ExportOrder(Order order)
        {
            var serviceOrder = new Services.Order();
            serviceOrder.CustomerEmail = order.CustomerInfo.Email;
            serviceOrder.CustomerName = order.CustomerInfo.BillingAddress.Name + " " +
                                        order.CustomerInfo.BillingAddress.Name2;
            serviceOrder.Billing =
                ConvertAddress((ActiveCommerce.Addresses.AddressInfo) order.CustomerInfo.BillingAddress);
            serviceOrder.Shipping =
                ConvertAddress((ActiveCommerce.Addresses.AddressInfo) order.CustomerInfo.ShippingAddress);
            var lines = new List<Services.OrderLine>();
            foreach (var line in order.OrderLines)
            {
                var newLine = new Services.OrderLine();
                newLine.ProductId = line.Product.Code;
                newLine.Price = line.Totals.PriceExVat;
                newLine.Quantity = line.Quantity;
                lines.Add(newLine);
            }
            serviceOrder.OrderLines = lines.ToArray();
            serviceOrder.Total = order.Totals.TotalPriceIncVat;
            serviceOrder.TaxTotal = order.Totals.TotalVat;
            serviceOrder.ShippingCost = order.ShippingPrice;

            var client = new Services.OrderServiceClient();
            var id = client.CreateOrder(serviceOrder);
            Sitecore.Diagnostics.Log.Warn("Successfully exported order {0}".FormatWith(id), this);
            return id;
        }

        protected Services.Address ConvertAddress(AddressInfo address)
        {
            var newAddress = new Services.Address();
            newAddress.AddressLine = address.Address;
            newAddress.City = address.City;
            newAddress.State = address.State;
            newAddress.Country = address.Country.Code;
            newAddress.Zip = address.Zip;
            return newAddress;
        }
    }
}