using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.Discounts.ViewModel
{
    /// <summary>
    /// Override the bultin cart view model as a quick means of ensuring that the cart controller doesn't override any
    /// discount messages we add when a coupon code is applied.
    /// </summary>
    public class ShoppingCartViewModel : ActiveCommerce.Web.Models.ShoppingCartViewModel
    {
        private string _message;

        public override string Message
        {
            get
            {
                return _message;
            }
            set
            {
                if (string.IsNullOrEmpty(_message))
                {
                    _message = value;
                }
            }
        }
    }
}