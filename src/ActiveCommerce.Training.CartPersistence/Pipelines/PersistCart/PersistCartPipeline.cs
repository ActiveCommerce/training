using Sitecore.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.PersistCart
{
    public static class PersistCartPipeline
    {
        public const string Pipeline = "acPersistCart";

        public static void Run(PersistCartArgs args)
        {
            CorePipeline.Run(Pipeline, args);
        }
    }
}