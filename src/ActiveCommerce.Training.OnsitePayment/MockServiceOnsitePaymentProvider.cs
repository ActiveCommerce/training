using System;
using ActiveCommerce.Carts;
using ActiveCommerce.Extensions;
using ActiveCommerce.Payments;
using ActiveCommerce.Training.OnsitePayment.MockService;
using Sitecore.Diagnostics;
using Sitecore.Ecommerce.DomainModel.Payments;

namespace ActiveCommerce.Training.OnsitePayment
{
    /// <summary>
    /// Utilizes a mock payment service to demonstrate how to implement a new payment gateway
    /// integration for onsite payment. Implement <code>IReservable</code> to allow authorize-only
    /// transactions. Implement <code>ICreditable</code> to allow credits, in particular if there is an order
    /// processing failure.
    /// </summary>
    public class MockServiceOnsitePaymentProvider : IntegratedPaymentProviderBase, IReservable, ICreditable
    {
        /// <summary>
        /// You must implement this method for any integrated/onsite payment provider.
        /// </summary>
        public override void Invoke(Sitecore.Ecommerce.DomainModel.Payments.PaymentSystem paymentSystem, Sitecore.Ecommerce.DomainModel.Payments.PaymentArgs paymentArgs)
        {
            //read credit card and cart from PaymentArgs
            var args = Assert.ResultNotNull(paymentArgs as ActiveCommerce.Payments.PaymentArgs, "PaymentArgs must be of type ActiveCommerce.Payments.PaymentArgs");
            var creditCard = Assert.ResultNotNull(args.PaymentDetails as ActiveCommerce.Payments.CreditCardInfo, "PaymentDetails must be of type ActiveCommerce.Payments.CreditCardInfo");
            var cart = Assert.ResultNotNull(args.ShoppingCart as ShoppingCart, "Cart must be of type ActiveCommerce.Carts.ShoppingCart");
            
            //PaymentSystem contains values configured on payment item
            var paymentService = new MockPaymentService
            {
                EndpointUrl = paymentSystem.PaymentUrl,
                Username = paymentSystem.Username,
                Password = paymentSystem.Password
            };

            //XML settings can be read from PaymentSystem using PaymentSettingsReader
            var settingsReader = new Sitecore.Ecommerce.Payments.PaymentSettingsReader(paymentSystem);
            var authorizeSetting = settingsReader.GetSetting("authorizeOnly");
            var authorizeOnly = authorizeSetting != null &&
                                Boolean.TrueString.ToLower().Equals(authorizeSetting.ToLower());

            //read values from cart as needed to construct payment gateway request
            var request = new Request
            {
                RequestType = authorizeOnly ? RequestType.Authorize : RequestType.AuthorizeAndCapture,
                Amount = cart.Totals.TotalPriceIncVat,
                Currency = cart.Currency.Code,
                MerchantOrderNumber = cart.OrderNumber,
                BillToAddress = new Address
                {
                    Address1 = cart.CustomerInfo.BillingAddress.Address,
                    Address2 = cart.CustomerInfo.BillingAddress.Address2,
                    City = cart.CustomerInfo.BillingAddress.City,
                    State = cart.CustomerInfo.BillingAddress.State,
                    Country = cart.CustomerInfo.BillingAddress.Country.Code,
                    PostalCode = cart.CustomerInfo.BillingAddress.Zip,
                    Email = cart.CustomerInfo.Email,
                    Phone = cart.CustomerInfo.BillingAddress.GetPhoneNumber()
                },
                CreditCard = new CreditCard
                {
                    CardNumber = creditCard.CardNumber,
                    CardType = creditCard.CardType,
                    Expiration = creditCard.ExpirationDate.ToString("MM/yy"),
                    SecurityCode = creditCard.SecurityCode
                }
            };
            var response = paymentService.ExecuteRequest(request);

            //IMPORTANT: Set the PaymentStatus based on response from the payment gateway
            if (response.ResponseStatus == ResponseStatus.Success)
            {
                this.PaymentStatus = authorizeOnly ? PaymentStatus.Reserved : PaymentStatus.Captured;

                //If authorize/capture doesn't apply, just set as success:
                //this.PaymentStatus = PaymentStatus.Succeeded;
            }
            else
            {
                this.PaymentStatus = PaymentStatus.Failure;
            }

            //IMPORTANT: Save payment details
            TransactionDetails.TransactionNumber = response.TransactionId;
            TransactionDetails.AuthorizationCode = response.AuthorizationCode;
            TransactionDetails.ProviderStatus = response.ResponseStatus.ToString();
            TransactionDetails.ProviderMessage = response.Message;
            TransactionDetails.ProviderErrorCode = response.ResponseCode;
            
            //IMPORTANT: If a reservation / auth only, save the reservation ticket
            if (this.PaymentStatus == PaymentStatus.Reserved)
            {
                TransactionDetails.ReservationTicket = new ReservationTicket
                {
                    InvoiceNumber = cart.OrderNumber,
                    Amount = cart.Totals.TotalPriceIncVat,
                    AuthorizationCode = response.AuthorizationCode,
                    TransactionNumber = response.TransactionId
                };
            }
        }


        #region IReservable Members

        /// <summary>
        /// Allows for canceling of a reservation, in particular if there is a failure during
        /// order processing after an authorization has been completed.
        /// </summary>
        public virtual void CancelReservation(Sitecore.Ecommerce.DomainModel.Payments.PaymentSystem paymentSystem, Sitecore.Ecommerce.DomainModel.Payments.PaymentArgs paymentArgs, ReservationTicket reservationTicket)
        {
            var paymentService = new MockPaymentService
            {
                EndpointUrl = paymentSystem.PaymentUrl,
                Username = paymentSystem.Username,
                Password = paymentSystem.Password
            };
            var request = new Request
            {
                RequestType = RequestType.VoidReservation,
                MerchantOrderNumber = reservationTicket.InvoiceNumber,
                TransactionId = reservationTicket.TransactionNumber
            };
            var response = paymentService.ExecuteRequest(request);

            //IMPORTANT: Set PaymentStatus based on response from gateway
            this.PaymentStatus = response.ResponseStatus == ResponseStatus.Success ?
                PaymentStatus.Succeeded : PaymentStatus.Failure;
        }

        /// <summary>
        /// Allows for capturing of a payment reservation. Not used by Active Commerce
        /// out of the box, but would be useful if you wish to automate capture in your
        /// Active Commerce implementation.
        /// </summary>
        public virtual void Capture(Sitecore.Ecommerce.DomainModel.Payments.PaymentSystem paymentSystem, Sitecore.Ecommerce.DomainModel.Payments.PaymentArgs paymentArgs, ReservationTicket reservationTicket, decimal amount)
        {
            var paymentService = new MockPaymentService
            {
                EndpointUrl = paymentSystem.PaymentUrl,
                Username = paymentSystem.Username,
                Password = paymentSystem.Password
            };

            //Capture usually requires confirmation of amount to capture,
            //which can be found on the reservation ticket
            var request = new Request
            {
                RequestType = RequestType.CaptureReservation,
                MerchantOrderNumber = reservationTicket.InvoiceNumber,
                TransactionId = reservationTicket.TransactionNumber,
                Currency = paymentArgs.ShoppingCart.Currency.Code,
                Amount = reservationTicket.Amount
            };
            var response = paymentService.ExecuteRequest(request);

            //IMPORTANT: Set PaymentStatus based on response from gateway
            this.PaymentStatus = response.ResponseStatus == ResponseStatus.Success ?
                PaymentStatus.Succeeded : PaymentStatus.Failure;
        }

        #endregion

        #region ICreditable Members

        /// <summary>
        /// Allows the issue of a credit to a card after a captured transaction. Used in particular
        /// when order processing fails if a payment gateway is configured for authorize-and-capture.
        /// </summary>
        public virtual void Credit(Sitecore.Ecommerce.DomainModel.Payments.PaymentSystem paymentSystem, Sitecore.Ecommerce.DomainModel.Payments.PaymentArgs paymentArgs, ReservationTicket reservationTicket)
        {
            var paymentService = new MockPaymentService
            {
                EndpointUrl = paymentSystem.PaymentUrl,
                Username = paymentSystem.Username,
                Password = paymentSystem.Password
            };

            //amount to credit can be found on the reservation ticket
            var request = new Request
            {
                RequestType = RequestType.Credit,
                MerchantOrderNumber = reservationTicket.InvoiceNumber,
                TransactionId = reservationTicket.TransactionNumber,
                Amount = reservationTicket.Amount
            };
            var response = paymentService.ExecuteRequest(request);

            //IMPORTANT: Set PaymentStatus based on response from gateway
            this.PaymentStatus = response.ResponseStatus == ResponseStatus.Success ?
                PaymentStatus.Succeeded : PaymentStatus.Failure;
        }

        #endregion

    }
}
