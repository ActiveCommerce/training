using System.Linq;
using System.Web;
using ActiveCommerce.SitecoreX.Globalization;
using ActiveCommerce.Web.Models;

namespace ActiveCommerce.Training.Discounts.ViewModel
{
    public class ShoppingCartViewModelFactory : ActiveCommerce.Web.Models.Factories.ShoppingCartViewModelFactory
    {
        public ShoppingCartViewModelFactory(ShoppingCartViewModel viewModel) : base(viewModel)
        {
        }

        public override Web.Models.ShoppingCartViewModel GetViewModel(ActiveCommerce.Carts.ShoppingCart source)
        {
            var model = base.GetViewModel(source);
            if (HttpContext.Current == null)
            {
                return model;
            }
            if (!DiscountMessages.Any())
            {
                return model;
            }
            var discountMessages = string.Join("<br />", DiscountMessages.Messages);
            model.Message = discountMessages;
            DiscountMessages.Clear();
            return model;
        }
    }
}