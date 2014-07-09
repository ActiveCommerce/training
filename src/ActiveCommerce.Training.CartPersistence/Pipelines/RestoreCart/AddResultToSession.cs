using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.RestoreCart
{
    public class AddResultToSession : IRestoreCartProcessor
    {
        public void Process(RestoreCartArgs args)
        {
            if (HttpContext.Current == null)
            {
                return;
            }
            HttpContext.Current.Session[RestoreCartResult.SessionKey] = args.Result;
        }
    }
}