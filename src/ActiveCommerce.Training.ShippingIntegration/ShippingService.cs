namespace ActiveCommerce.Training.ShippingIntegration
{
    public class ShippingService : ActiveCommerce.Shipping.BaseShippingService
    {
        public override bool Display
        {
            get
            {
                return true;
            }
        }

        public override decimal GetPrice()
        {
            //var user = base.Configuration.SelectSingleNode("/training/api/user").Value;
            var client = new Services.ShippingServiceClient();
            return client.GetShippingCost(this.ShippingWeight);
        }
    }
}