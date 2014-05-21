using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Ecommerce.DomainModel.Orders;

namespace ActiveCommerce.Training.OrderExtension
{
    public class Order : ActiveCommerce.Orders.Order
    {
        public Order(OrderStatus status) : base(status)
        {

        }

        public Order() : base()
        {
            
        }

        public Guid ExternalOrderId { get; set; }
    }
}