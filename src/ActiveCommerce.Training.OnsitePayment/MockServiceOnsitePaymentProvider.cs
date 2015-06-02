using System;
using ActiveCommerce.Carts;
using ActiveCommerce.Extensions;
using ActiveCommerce.Payment;
using ActiveCommerce.Training.OnsitePayment.MockService;
using Sitecore.Ecommerce;
using Sitecore.Ecommerce.DomainModel.Payments;
using PaymentSystem = Sitecore.Ecommerce.DomainModel.Payments.PaymentSystem;

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
        public override void Invoke(PaymentSystem paymentSystem, PaymentArgs paymentArgs)
        {
            //read cart from PaymentArgs
            var cart = paymentArgs.ShoppingCart as ShoppingCart;

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
                    CardNumber = cart.CreditCardInfo.CardNumber,
                    CardType = cart.CreditCardInfo.CardType,
                    Expiration = cart.CreditCardInfo.ExpirationDate.ToString("MM/yy"),
                    SecurityCode = cart.CreditCardInfo.SecurityCode
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

            //IMPORTANT: Save payment details using SaveCallBackValues
            var transactionData = Context.Entity.Resolve<ITransactionData>();
            transactionData.SaveCallBackValues(
                paymentArgs.ShoppingCart.OrderNumber, //order number
                this.PaymentStatus.ToString(), //payment status
                response.TransactionId, //transaction number
                cart.Totals.TotalPriceIncVat.ToString(), //final amount
                cart.Currency.Code, //currency code
                this.PaymentStatus.ToString(), //repeat payment status (due to SES bug)
                response.ResponseCode, //gateway response code
                response.Message, //gateway message
                cart.CreditCardInfo.CardType //card type
            );

            //IMPORTANT: Save authorization number
            transactionData.SavePersistentValue(cart.OrderNumber, TransactionConstants.AuthorizationNumber,
                response.AuthorizationCode);

            //IMPORTANT: If a reservation / auth only, save the reservation ticket
            if (this.PaymentStatus == PaymentStatus.Reserved)
            {
                var reservationTicket = new ReservationTicket
                {
                    InvoiceNumber = cart.OrderNumber,
                    Amount = cart.Totals.TotalPriceIncVat,
                    AuthorizationCode = response.AuthorizationCode,
                    TransactionNumber = response.TransactionId
                };
                transactionData.SavePersistentValue(cart.OrderNumber, PaymentConstants.ReservationTicket,
                    reservationTicket);
            }
        }


        #region IReservable Members

        /// <summary>
        /// Allows for canceling of a reservation, in particular if there is a failure during
        /// order processing after an authorization has been completed.
        /// </summary>
        public virtual void CancelReservation(PaymentSystem paymentSystem, PaymentArgs paymentArgs, ReservationTicket reservationTicket)
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
        public virtual void Capture(PaymentSystem paymentSystem, PaymentArgs paymentArgs, ReservationTicket reservationTicket, decimal amount)
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
        public virtual void Credit(PaymentSystem paymentSystem, PaymentArgs paymentArgs, ReservationTicket reservationTicket)
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
