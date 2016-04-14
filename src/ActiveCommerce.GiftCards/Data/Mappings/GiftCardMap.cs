using ActiveCommerce.Data.Mappings;
using FluentNHibernate.Mapping;

namespace ActiveCommerce.GiftCards.Data.Mappings
{
    public class GiftCardMap : ClassMap<GiftCard>
    {
        public GiftCardMap()
        {
            Id(x => x.Id);
            Map(x => x.Number).Length(Constants.BigCodeFieldLength).Not.Nullable().Unique();
            Map(x => x.Pin);
            Map(x => x.Balance).Not.Nullable();
        }
    }
}