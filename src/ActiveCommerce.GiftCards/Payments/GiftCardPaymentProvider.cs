using System;
using ActiveCommerce.Extensions;
using ActiveCommerce.Payments;
using Sitecore.Diagnostics;
using Sitecore.Ecommerce.DomainModel.Payments;

namespace ActiveCommerce.GiftCards.Payments
{
    public class GiftCardPaymentProvider : PaymentProvider, ICreditable, IHasTransactionDetails, ICreatesPaymentDetails
    {
        private readonly TransactionDetails _transactionDetails = new TransactionDetails();
        public virtual TransactionDetails TransactionDetails
        {
            get { return _transactionDetails; }
        }

        public virtual PaymentDetails CreatePaymentDetails()
        {
            return new GiftCardInfo { Balance = 0 };
        }

        protected virtual IGiftCardManager GiftCardManager { get; private set; }

        public GiftCardPaymentProvider(IGiftCardManager giftCardManager)
        {
            GiftCardManager = giftCardManager;
        }

        public override void Invoke(Sitecore.Ecommerce.DomainModel.Payments.PaymentSystem paymentSystem, Sitecore.Ecommerce.DomainModel.Payments.PaymentArgs paymentArgs)
        {
            var args = Assert.ResultNotNull(paymentArgs as ActiveCommerce.Payments.PaymentArgs, "PaymentArgs must be of type ActiveCommerce.Payments.PaymentArgs");
            var card = Assert.ResultNotNull(args.PaymentDetails as GiftCardInfo, "PaymentDetails must be of type GiftCardInfo");

            try
            {
                GiftCardManager.Debit(card, args.Amount);
            }
            catch (Exception e)
            {
                TransactionDetails.ProviderMessage = e.Message;
                PaymentStatus = PaymentStatus.Failure;
                return;
            }
            TransactionDetails.TransactionNumber = Guid.NewGuid().ToString();
            PaymentStatus = PaymentStatus.Succeeded;
        }

        #region ICreditable Members

        public virtual void Credit(Sitecore.Ecommerce.DomainModel.Payments.PaymentSystem paymentSystem, Sitecore.Ecommerce.DomainModel.Payments.PaymentArgs paymentArgs, ReservationTicket reservationTicket)
        {
            var args = Assert.ResultNotNull(paymentArgs as ActiveCommerce.Payments.PaymentArgs, "PaymentArgs must be of type ActiveCommerce.Payments.PaymentArgs");
            var card = Assert.ResultNotNull(args.PaymentDetails as GiftCardInfo, "PaymentDetails must be of type GiftCardInfo");

            try
            {
                GiftCardManager.Credit(card, args.Amount);
            }
            catch (Exception e)
            {
                PaymentStatus = PaymentStatus.Failure;
                Log.Error(string.Format("Unable to credit gift card: TranactionID={0}; Customer={1}; OrderNumber={2}", reservationTicket.TransactionNumber, paymentArgs.ShoppingCart.GetCustomerNameForLog(), paymentArgs.ShoppingCart.OrderNumber), e, this);
                return;
            }
            PaymentStatus = PaymentStatus.Succeeded;
        }

        #endregion

        #region Irrelevant to gift card payment
        public override void ProcessCallback(Sitecore.Ecommerce.DomainModel.Payments.PaymentSystem paymentSystem, Sitecore.Ecommerce.DomainModel.Payments.PaymentArgs paymentArgs)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
