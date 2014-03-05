using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.GiftMessage.CheckOut;
using Sitecore.Diagnostics;
using Sitecore.Ecommerce.DomainModel.CheckOuts;

namespace ActiveCommerce.GiftMessage.Model.Factories
{
    public class CheckoutViewModelFactory : ActiveCommerce.Web.Models.Factories.CheckoutViewModelFactory
    {

        public CheckoutViewModelFactory(ActiveCommerce.Web.Models.CheckoutViewModel viewModel) : base(viewModel) { }

        public override Web.Models.CheckoutViewModel GetViewModel(Carts.ShoppingCart source)
        {
            var giftModel = ViewModel as CheckoutViewModel;
            var checkoutData = Sitecore.Ecommerce.Context.Entity.GetInstance<ICheckOut>() as IGiftMessage;
            if (giftModel != null && checkoutData != null)
            {
                giftModel.GiftMessage.Text = checkoutData.GiftMessage;
            }

            return base.GetViewModel(source);
        }
    }
}