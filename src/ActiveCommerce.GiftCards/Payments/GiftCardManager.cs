using System;
using System.Linq;
using ActiveCommerce.GiftCards.Data;
using ActiveCommerce.SitecoreX.Globalization;
using Sitecore.Diagnostics;

namespace ActiveCommerce.GiftCards.Payments
{
    public class GiftCardManager : IGiftCardManager
    {
        protected virtual string InvalidCardMessage { get { return Translator.Render("Gift-Card-Not-Valid"); } }
        protected virtual string InsufficientBalanceMessage { get { return Translator.Render("Gift-Card-Insufficient-Balance"); } }
        protected readonly IGiftCardRepository Repository;

        public GiftCardManager(IGiftCardRepository repository)
        {
            Repository = repository;
        }

        public virtual bool Validate(GiftCardInfo card)
        {
            Assert.ArgumentNotNull(card, "card");
            return FindCard(card) != null;
        }

        public virtual decimal GetBalance(GiftCardInfo card)
        {
            Assert.ArgumentNotNull(card, "card");
            var match = Assert.ResultNotNull(FindCard(card), InvalidCardMessage);
            return match != null ? match.Balance : 0;
        }

        public virtual void Debit(GiftCardInfo card, decimal amount)
        {
            Assert.ArgumentNotNull(card, "card");
            Assert.ArgumentNotNull(amount, "amount");
            var match = Assert.ResultNotNull(FindCard(card), InvalidCardMessage);
            Assert.IsTrue(match.Balance >= amount, InsufficientBalanceMessage);
            match.Balance -= amount;
            Repository.Flush();
        }

        public virtual void Credit(GiftCardInfo card, decimal amount)
        {
            Assert.ArgumentNotNull(card, "card");
            Assert.ArgumentNotNull(amount, "amount");
            var match = Assert.ResultNotNull(FindCard(card), InvalidCardMessage);
            match.Balance += amount;
            Repository.Flush();
        }

        protected virtual GiftCard FindCard(GiftCardInfo card)
        {
            Assert.ArgumentNotNull(card, "card");
            var match = Repository.GetAll().FirstOrDefault(x => x.Number == card.CardNumber);
            // if pin exists, also match on that
            if (match != null && !string.IsNullOrWhiteSpace(match.Pin) && card.Pin != match.Pin)
            {
                return null;
            }
            return match;
        }
    }
}