using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.OrderProcessing;

namespace ActiveCommerce.Training.OrderProcessing
{
    public class OrderTrace : IOrderPipelineProcessor
    {
        public string Message { get; set; }

        public void Process(OrderPipelineArgs args)
        {
            Sitecore.Diagnostics.Log.Info(string.Format("{0} for Customer {1}", Message, args.Cart.CustomerInfo.CustomerId), this);
        }
    }
}