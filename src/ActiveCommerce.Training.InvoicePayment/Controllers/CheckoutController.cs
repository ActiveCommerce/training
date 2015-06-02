using System.Web.Mvc;
using ActiveCommerce.Training.InvoicePayment.CheckOut;
using Sitecore.Ecommerce.DomainModel.CheckOuts;

namespace ActiveCommerce.Training.InvoicePayment.Controllers
{
    public class CheckoutController : ActiveCommerce.Web.Controllers.CheckoutController
    {
        public virtual ActionResult UpdatePurchaseOrderNumber(string purchaseOrderNumber)
        {
            //ICheckOut is utilized for storing values in session during checkout
            //Our registered implementation extends the base, and implements IInvoicePayment for purchase order data

            var checkout = Sitecore.Ecommerce.Context.Entity.GetInstance<ICheckOut>() as IInvoicePayment;
            if (checkout == null)
            {
                var message = string.Format("ICheckOut does not implement {0}", typeof (IInvoicePayment));
                Sitecore.Diagnostics.Log.Warn(message, this);
                return JsonError(message);
            }
            checkout.PurchaseOrderNumber = purchaseOrderNumber;
            return Json(true);
        }
    }
}