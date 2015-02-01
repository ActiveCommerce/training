using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.OrderExtension
{
    public class OrderFactory : ActiveCommerce.Orders.OrderFactory
    {
        public override ActiveCommerce.Orders.Order CreateOrder()
        {
            return new Order();
        }
    }
}