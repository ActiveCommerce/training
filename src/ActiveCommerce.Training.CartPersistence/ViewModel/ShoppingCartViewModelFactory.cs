using ActiveCommerce.SitecoreX.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.CartPersistence.ViewModel
{
    public class ShoppingCartViewModelFactory : ActiveCommerce.Web.Models.Factories.ShoppingCartViewModelFactory
    {
        public override Web.Models.ShoppingCartViewModel GetViewModel(ActiveCommerce.Carts.ShoppingCart source)
        {
            var model = base.GetViewModel(source);
            if (HttpContext.Current == null)
            {
                return model;
            }
            var restoreResult = HttpContext.Current.Session[RestoreCartResult.SessionKey] as RestoreCartResult;
            if (restoreResult == null || !restoreResult.AttemptedRestore)
            {
                return model;
            }
            model.Message = restoreResult.Success ? Translator.Text("Cart-Restore-Success") : Translator.Text("Cart-Restore-Failure");
            HttpContext.Current.Session[RestoreCartResult.SessionKey] = null;
            return model;
        }
    }
}