using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActiveCommerce.GiftMessage.CheckOut;
using Sitecore.Ecommerce.DomainModel.CheckOuts;

namespace ActiveCommerce.GiftMessage.Controllers
{
    public class CheckoutController : ActiveCommerce.Web.Controllers.CheckoutController
    {
        public virtual ActionResult UpdateGiftMessage(string giftMessage)
        {
            //ICheckOut is utilized for storing values in session during checkout
            //Our registered implementation extends the base, and implements IGiftMessage for gift message data

            var checkout = Sitecore.Ecommerce.Context.Entity.GetInstance<ICheckOut>() as IGiftMessage;
            if (checkout != null)
            {
                checkout.GiftMessage = giftMessage;
            }
            return Json(true);
        }
    }
}