namespace ActiveCommerce.GiftCards.Payments
{
    public interface IGiftCardManager
    {
        bool Validate(GiftCardInfo card);
        decimal GetBalance(GiftCardInfo card);
        void Debit(GiftCardInfo card, decimal amount);
        void Credit(GiftCardInfo card, decimal amount);
    }
}