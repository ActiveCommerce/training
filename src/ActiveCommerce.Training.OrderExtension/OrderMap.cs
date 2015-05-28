using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace ActiveCommerce.Training.OrderExtension
{
    public class OrderMap : SubclassMap<Order>
    {
        public OrderMap()
        {
            Table("OrderExtensions");
            Map(x => x.ExternalOrderId);
            Map(x => x.GiftMessage);
        }
    }
}