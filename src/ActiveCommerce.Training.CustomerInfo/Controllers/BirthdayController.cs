using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Ecommerce.DomainModel.Users;
using Microsoft.Practices.Unity;

namespace ActiveCommerce.Training.CustomerInfo.Controllers
{
    public class BirthdayController : Controller
    {
        public ActionResult SaveBirthday(string birthday)
        {
            if (string.IsNullOrEmpty(birthday))
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            var manager = Sitecore.Ecommerce.Context.Entity.Resolve<ICustomerManager<Sitecore.Ecommerce.DomainModel.Users.CustomerInfo>>();
            var customer = manager.CurrentUser as ActiveCommerce.Training.CustomerInfo.Users.CustomerInfo;
            if (customer == null)
            {
                Sitecore.Diagnostics.Log.Warn("CustomerInfo is not of the expected type", this);
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            //set the value on our added field, and save the customer profile
            customer.Birthday = birthday;
            manager.UpdateCustomerProfile(customer);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}