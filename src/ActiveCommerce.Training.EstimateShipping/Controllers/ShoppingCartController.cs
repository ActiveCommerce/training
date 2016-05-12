using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using ActiveCommerce.SitecoreX.Globalization;
using ActiveCommerce.Web.Filters;
using ActiveCommerce.Web.Models;
using ActiveCommerce.Web.Models.Factories;
using Sitecore.Ecommerce;
using Sitecore.Ecommerce.Carts;
using Sitecore.Ecommerce.DomainModel.Users;

namespace ActiveCommerce.Training.EstimateShipping.Controllers
{
    [NoCache]
    [NoSitecoreAnalytics]
    public class ShoppingCartController : ActiveCommerce.Web.Controllers.ShoppingCartController
    {
        public const string US_ZIP_REGEX = @"^\d{5}(-\d{4})?$";

        [HttpPost]
        public virtual ActionResult EstimateShipping(string zipcode)
        {
            if (!IsValidUSZipCode(zipcode))
            {
                return JsonError("Cart-Estimate-Shipping-Not-Valid");
            }

            // Set the zip on the current user
            var cart = Sitecore.Ecommerce.Context.Entity.GetInstance<ShoppingCart>() as ActiveCommerce.Carts.ShoppingCart;
            var customerManager = Sitecore.Ecommerce.Context.Entity.Resolve<ICustomerManager<CustomerInfo>>();
            var customerInfo = customerManager.CurrentUser;

            cart.CustomerInfo = customerInfo;
            var shippingAddress = customerInfo.ShippingAddress as ActiveCommerce.Addresses.AddressInfo;

            using (shippingAddress.StartEditing())
            {
                shippingAddress.Zip = zipcode;
            }

            // Get shipping options
            var shippingOptions = cart.GetShippingOptions().ToList();

            // Select the cheapest one by default
            var cheapestOption = shippingOptions.OrderBy(x => x.DiscountedPrice).FirstOrDefault();
            if (cheapestOption != null)
            {
                cart.ShippingProvider = cheapestOption;
            }

            var viewFactory = Context.Entity.Resolve<IViewModelFactory<Sitecore.Ecommerce.DomainModel.Shippings.ShippingProvider, ShippingOptionViewModel>>();

            return Json(shippingOptions.Select(viewFactory.GetViewModel));
        }

        protected virtual bool IsValidUSZipCode(string zipcode)
        {
            return !string.IsNullOrWhiteSpace(zipcode) && (new Regex(US_ZIP_REGEX)).IsMatch(zipcode);
        }

        protected virtual JsonResult JsonError(string message = null, string[] errors = null, HttpStatusCode status = HttpStatusCode.BadRequest, JsonRequestBehavior behavior = JsonRequestBehavior.AllowGet)
        {
            message = !string.IsNullOrEmpty(message) ? Translator.ContainsEntry(message) ? Translator.Text(message) : message : message;

            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = (int)status;
            if (!string.IsNullOrEmpty(message))
            {
                Response.StatusDescription = message;
            }
            return Json(new { Status = (int)status, Message = message, Errors = errors }, behavior);
        }
    }
}