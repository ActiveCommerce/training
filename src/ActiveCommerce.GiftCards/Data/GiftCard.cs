namespace ActiveCommerce.GiftCards.Data
{
    public class GiftCard
    {
        public virtual int Id { get; set; }
        public virtual string Number { get; set; }
        public virtual string Pin { get; set; }
        public virtual decimal Balance { get; set; }
    }
}