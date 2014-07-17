using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.Training.CartPersistence.Common;

namespace ActiveCommerce.Training.CartPersistence.Carts
{
    public static class CartsUpdatedHandler
    {
        /// <summary>
        /// Method that handles set the CartUpdated context property.
        /// </summary>
        public static void CartUpdated(object sender, ActiveCommerce.Carts.CartChangedEventArgs e)
        {
            CartPersistenceContext.CartUpdated = true;
        }
    }
}