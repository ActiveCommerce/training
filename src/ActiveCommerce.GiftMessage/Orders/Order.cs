using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Ecommerce.DomainModel.Orders;

namespace ActiveCommerce.GiftMessage.Orders
{
    public class Order : ActiveCommerce.Orders.Order
    {
        public Order(OrderStatus status) : base(status) { }

        public Order() : base() { }

        public string GiftMessage { get; set; }
    }
}