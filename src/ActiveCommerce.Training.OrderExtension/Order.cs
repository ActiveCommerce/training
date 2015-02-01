using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Ecommerce.DomainModel.Orders;

namespace ActiveCommerce.Training.OrderExtension
{
    public class Order : ActiveCommerce.Orders.Order
    {
        public virtual Guid ExternalOrderId { get; set; }

        public virtual string GiftMessage { get; set; }

        public virtual string PurchaseOrderNumber { get; set; }
    }
}