using ActiveCommerce.Prices.Pipelines.GetProductTotals;

namespace ActiveCommerce.Training.RealTimePricing
{
    public class GetProductPriceFromService : IGetProductTotalsProcessor
    {
        public void Process(GetProductTotalsArgs args)
        {
            var product = args.Product;
            var service = new Services.ProductServiceClient();
            var price = service.GetPrice(product.Code);
            args.Totals.PriceExVat = price;
            args.HasPrice = true;
        }
    }
}