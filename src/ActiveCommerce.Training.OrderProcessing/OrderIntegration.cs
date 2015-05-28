using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using ActiveCommerce.Orders;
using ActiveCommerce.Orders.Pipelines;
using Microsoft.Practices.Unity;
using ActiveCommerce.Addresses;
using Sitecore.Ecommerce.DomainModel.Orders;
using Sitecore.StringExtensions;

namespace ActiveCommerce.Training.OrderProcessing
{
    public class OrderIntegration : ActiveCommerce.Orders.Pipelines.OrderPipelineProcessor
    {
        /// <summary>
        /// This tells Active Commerce whether an exception or error in this processor
        /// should be considered fatal.
        /// </summary>
        protected override bool ContinueOnFailure
        {
            get { return false; }
        }

        protected override void DoProcess(OrderPipelineArgs args)
        {
            var order = args.Order;

            var serviceOrder = new Services.Order
            {
                CustomerEmail = order.BuyerCustomerParty.Party.Contact.ElectronicMail,
                CustomerName = order.BuyerCustomerParty.Party.PartyName,
                Billing = ConvertAddress((Address)order.BuyerCustomerParty.Party.PostalAddress),
                Shipping = ConvertAddress((Address)order.DefaultDelivery.DeliveryParty.PostalAddress)
            };
            var lines = new List<Services.OrderLine>();
            foreach (var line in order.OrderLines)
            {
                var newLine = new Services.OrderLine
                {
                    ProductId = line.LineItem.Item.Code,
                    Price = line.LineItem.Price.PriceAmount.Value,
                    Quantity = (uint)line.LineItem.Quantity
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