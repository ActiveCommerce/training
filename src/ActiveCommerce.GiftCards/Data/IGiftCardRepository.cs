using System.Linq;

namespace ActiveCommerce.GiftCards.Data
{
    public interface IGiftCardRepository
    {
        GiftCard Get(int id);
        IQueryable<GiftCard> GetAll();
        void Add(GiftCard giftCard);
        void Delete(GiftCard giftCard);
        void Flush();
    }
}