using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ActiveCommerce.Carts;
using ActiveCommerce.CheckOuts;
using ActiveCommerce.Orders.Processing;
using ActiveCommerce.Payment;
using ActiveCommerce.Products;
using Microsoft.Practices.Unity;
using Sitecore.Ecommerce.DomainModel.Addresses;
using Sitecore.Ecommerce.DomainModel.Data;
using Sitecore.Ecommerce.DomainModel.Prices;
using Sitecore.Ecommerce.DomainModel.Shippings;
using Sitecore.Ecommerce.Products;
using PaymentSystem = Sitecore.Ecommerce.DomainModel.Payments.PaymentSystem;
using ShoppingCart = Sitecore.Ecommerce.DomainModel.Carts.ShoppingCart;

namespace ActiveCommerce.Training.CheckoutViaApi.Controllers
{
    public class CustomCheckoutController : ActiveCommerce.Web.Controllers.AppControllerBase
    {

        public virtual ActionResult ProcessOrder(string productCode)
        {
            //find a product by code to add to the cart
            var repository = Sitecore.Ecommerce.Context.Entity.Resolve<IAdvancedProductRepository>();
            var product = repository.Get<ProductBaseData>(productCode);
            if (product == null)
            {
                return JsonError(string.Format("Could not find product {0}", productCode));
            }

            //GetInstance will get the shopping cart in the user's session -- do this if appropriate
            //var cart = Sitecore.Ecommerce.Context.Entity.GetInstance<ShoppingCart>();
            //  as ActiveCommerce.Carts.ShoppingCart;

            //Resolve will get us an empty/disconnected cart to work with
            var cart = Sitecore.Ecommerce.Context.Entity.Resolve<ShoppingCart>()
                as ActiveCommerce.Carts.ShoppingCart;
            cart.ShoppingCartLines.Add(new ShoppingCartLine
            {
                Product = product,
                Quantity = 1
            });

            //construct a "virtual" cart line not based on a repository product
            cart.ShoppingCartLines.Add(new ShoppingCartLine
            {
                Product = new ActiveCommerce.Products.Product
                {
                    Code = "VIRTUAL-UPCHARGE",
                    SKU = "VIRTUAL-UPCHARGE",
                    Title = "Premium Upcharge",
                    ShortDescription = "Upcharge for premium option"
                },
                Quantity = 1,
                Totals = new Totals
                {
                    PriceExVat = 4.99m,
                    TotalPriceExVat = 4.99m
                }
            });

            var countryProvider = Sitecore.Ecommerce.Context.Entity.Resolve<IEntityProvider<Country>>();

            //when editing shipping or billing address, use StartEditing() to ensure event system is notified of changes
            var shipping = cart.CustomerInfo.ShippingAddress as ActiveCommerce.Addresses.AddressInfo;
            using (shipping.StartEditing())
            {
                shipping.Name = "Test";
                shipping.Name2 = "Shipping";
                shipping.Address = "123 Test St";
                shipping.Address2 = "Apt 1";
                shipping.City = "Milwaukee";
                shipping.State = "WI";
                shipping.Zip = "53202";
                shipping.Country = countryProvider.Get("US");
                shipping.Phone = "414 555 5555";
            }

            var shipMethodProvider = Sitecore.Ecommerce.Context.Entity.Resolve<IEntityProvider<ShippingProvider>>();
            var shipMethod = shipMethodProvider.Get("Flat Rate");
            cart.ShippingProvider = shipMethod;

            var billing = cart.CustomerInfo.BillingAddress as ActiveCommerce.Addresses.AddressInfo;
            using (billing.StartEditing())
            {
                billing.Name = "Test";
                billing.Name2 = "Billing";
                billing.Address = "123 Billing St";
                billing.City = "Milwaukee";
                billing.State = "WI";
                billing.Zip = "53207";
                billing.Country = countryProvider.Get("US");
                billing.Phone = "414 555 1111";
            }

            cart.CustomerInfo.Email = "test@tester.com";
            cart.CustomerInfo.Phone = billing.Phone;

            var paymentSystemProvider = Sitecore.Ecommerce.Context.Entity.Resolve<IEntityProvider<PaymentSystem>>();
            var paymentSystem = paymentSystemProvider.Get("MockIntegratedPayment");
            cart.PaymentSystem = paymentSystem;

            cart.CreditCardInfo = new CreditCardInfo
            {
                CardType = "Visa",
                CardNumber = "4111111111111111",
                ExpirationDate = new DateTime(2020, 11, 1),
                SecurityCode = "111"
            };

            var orderProcessingArgs = new OrderProcessingArgs
            {
                ShoppingCart = cart,
                CheckOut = new CheckOut() //can be used to store / pass additional data to order processing
            };

            var orderProcessor = Sitecore.Ecommerce.Context.Entity.Resolve<IOrderProcessor>();
            var result = orderProcessor.ProcessOrder(orderProcessingArgs);

            switch (result.Status)
            {
                //you'll need to configure a return page on the root Payment Options node under Business Settings
                case OrderProcessingStatus.RedirectingToPayment:
                    Response.End();
                    return new EmptyResult();

                case OrderProcessingStatus.Succeeded:
                    cart.CreditCardInfo = null;
                    return Json("Success!", JsonRequestBehavior.AllowGet);

                case OrderProcessingStatus.PaymentFailed:
                    return JsonError(String.IsNullOrWhiteSpace(result.Message) ? "Checkout-Error-Payment-Failed" : result.Message, new[] { result.Status.ToString() }, HttpStatusCode.PaymentRequired);

                case OrderProcessingStatus.OrderCreationFailed:
                case OrderProcessingStatus.OtherFatalError:
                    return JsonError(String.IsNullOrWhiteSpace(result.Message) ? "Checkout-Error-General" : result.Message, new[] { result.Status.ToString() }, HttpStatusCode.InternalServerError);

                default:
                    return Json("Unknown??", JsonRequestBehavior.AllowGet);
            }
        }
    }
}