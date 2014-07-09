using Sitecore.Ecommerce.DomainModel.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.RestoreCartProduct
{
    public class CheckProductAvailable : IRestoreCartProductProcessor
    {
        public void Process(RestoreCartProductArgs args)
        {
            //TODO: Use a query instead? Ensure product is not hidden?
            var product = args.ProductRepository.Get<ProductBaseData>(args.ProductCode);
            if (product == null)
            {
                args.Result.Success = false;
                args.Result.FailedProducts.Add(args.ProductCode);
                args.AbortPipeline();
            }
        }
    }
}