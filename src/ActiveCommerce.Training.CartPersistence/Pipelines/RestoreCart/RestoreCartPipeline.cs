using Sitecore.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.RestoreCart
{
    public static class RestoreCartPipeline
    {
        public const string Pipeline = "acRestoreCart";

        public static void Run(RestoreCartArgs args)
        {
            CorePipeline.Run(Pipeline, args);
        }
    }
}