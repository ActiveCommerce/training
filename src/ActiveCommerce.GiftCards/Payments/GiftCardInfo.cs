using System;
using ActiveCommerce.Payments;
using ActiveCommerce.SitecoreX.Globalization;

namespace ActiveCommerce.GiftCards.Payments
{
    [Serializable]
    public class GiftCardInfo : PaymentDetails
    {
        private string _cardNumber;
        public virtual string CardNumber
        {
            get
            {
                return _cardNumber;
            }
            set
            {
                _cardNumber = value != null ? value.Replace(" ", string.Empty) : null;
            }
        }

        private string _pin;
        public virtual string Pin
        {
            get
            {
                return _pin;
            }
            set
            {
                _pin = value != null ? value.Replace(" ", string.Empty) : null;
            }
        }

        public virtual decimal Balance { get; set; }

        public override string Description
        {
            get
            {
                var lastFour = !string.IsNullOrEmpty(CardNumber) && CardNumber.Length > 4
                                 ? CardNumber.Substring(CardNumber.Length - 4)
                                 : CardNumber;
                return Translator.Format("Gift-Card-Description", lastFour);
            }
        }
    }
}
