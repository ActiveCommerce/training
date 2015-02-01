using System;
using ActiveCommerce.Orders.Pipelines;

namespace ActiveCommerce.Training.OrderProcessing
{
    public class OrderTrace : OrderPipelineProcessor
    {
        public string Message { get; set; }

        protected override bool ContinueOnFailure
        {
            get { return true; }
        }

        protected override void DoProcess(OrderPipelineArgs args)
        {
            Sitecore.Diagnostics.Log.Info(string.Format("{0} for Customer {1}", Message, args.ShoppingCart.CustomerInfo.CustomerId), this);
        }
    }
}