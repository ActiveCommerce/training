using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.Training.CartPersistence.Common;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.RestoreCart
{
    public class CheckPersistenceInitialized : IRestoreCartProcessor
    {
        public void Process(RestoreCartArgs args)
        {
            if (CartPersistenceContext.PersistenceInitialized && !args.ForceRestore)
            {
                args.AbortPipeline();
                return;
            }
        }
    }
}