using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.GiftMessage.Model
{
    public class CheckoutViewModel : ActiveCommerce.Web.Models.CheckoutViewModel
    {
        public CheckoutViewModel()
        {
            GiftMessage = new GiftMessage();
        }

        public GiftMessage GiftMessage { get; set; }
    }

    public class GiftMessage
    {
        public string Text
        {
            get;
            set;
        }
    }
}