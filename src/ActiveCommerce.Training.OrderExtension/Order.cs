using System;

namespace ActiveCommerce.Training.OrderExtension
{
    public class Order : ActiveCommerce.Orders.Order
    {
        public virtual Guid ExternalOrderId { get; set; }

        public virtual string GiftMessage { get; set; }

    }
}