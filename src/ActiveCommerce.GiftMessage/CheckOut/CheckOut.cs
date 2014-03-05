using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.GiftMessage.CheckOut
{
    [Serializable] //stored in session
    public class CheckOut : ActiveCommerce.CheckOuts.CheckOut, IGiftMessage
    {
        public string GiftMessage
        {
            get;
            set;
        }
    }
}