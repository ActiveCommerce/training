using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.Training.CartPersistence.Common;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.PersistCart
{
    public class CheckCartUpdated : IPersistCartProcessor
    {
        public void Process(PersistCartArgs args)
        {
            if (!CartPersistenceContext.CartUpdated)
            {
                args.AbortPipeline();
                return;
            }
        }
    }
}