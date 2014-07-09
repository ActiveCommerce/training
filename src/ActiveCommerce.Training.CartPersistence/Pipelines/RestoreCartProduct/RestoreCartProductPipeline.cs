using Sitecore.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.RestoreCartProduct
{
    public static class RestoreCartProductPipeline
    {
        public const string Pipeline = "acRestoreCartProduct";

        public static void Run(RestoreCartProductArgs args)
        {
            CorePipeline.Run(Pipeline, args);
        }
    }
}