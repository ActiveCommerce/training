using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.CartPersistence
{
    /// <summary>
    /// TODO: Distinguish unavailable products, no stock products, less stock than requested?
    /// </summary>
    public class RestoreCartResult
    {
        public const string SessionKey = "AC_RESTORE_CART_RESULT";

        public bool AttemptedRestore { get; set; }
        public bool Success { get; set; }
        public IList<string> FailedProducts { get; set; }

        public RestoreCartResult()
        {
            AttemptedRestore = false;
            Success = true;
            FailedProducts = new List<string>();
        }
    }
}