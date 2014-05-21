using System;
using System.Collections.Generic;
using System.ServiceModel;
using Microsoft.Practices.Unity;
using ActiveCommerce.Addresses;
using ActiveCommerce.OrderProcessing;
using Sitecore.Ecommerce.DomainModel.Orders;
using Sitecore.StringExtensions;

namespace ActiveCommerce.Training.OrderProcessing
{
    public class OrderIntegration : IOrderPipelineProcessor
    {
        public void Process(OrderPipelineArgs args)
        {
            var order = args.Order;

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

            try
            {
                var client = new Services.OrderServiceClient();
                var id = client.CreateOrder(serviceOrder);
                Sitecore.Diagnostics.Log.Warn("Successfully created order {0}".FormatWith(id), this);
            }
            catch (FaultException e)
            {
                Sitecore.Diagnostics.Log.Error("Error while sending order data. Error from server was: {0}".FormatWith(e.Message), e, this);
                args.AddMessage(e.Message);
                args.Order = null;
            }
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