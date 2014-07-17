using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.CartPersistence.Common
{
    public static class CartPersistenceContext
    {
        private const string _requestCartUpdatedKey = "AC_PERSISTENCE_CART_UPDATED";
        private const string _sessionCartInitializedKey = "AC_PERSISTENCE_CART_INITIALIZED";
        private const string _sessionCartUpdatedEventInitializedKey = "AC_PERSISTENCE_CART_UPDATE_INITIALIZED";

        /// <summary>
        /// Property to determine if the cart has been updated during the current Request.
        /// </summary>
        public static bool CartUpdated
        {
            get
            {
                if (HttpContext.Current == null || HttpContext.Current.Items.Count == 0)
                {
                    return false;
                }

                if (!HttpContext.Current.Items.Contains(_requestCartUpdatedKey))
                {
                    return false;
                }

                var value = HttpContext.Current.Items[_requestCartUpdatedKey];

                if (value == null)
                {
                    return false;
                }

                return (bool) value;
            }
            set { HttpContext.Current.Items[_requestCartUpdatedKey] = value; }
        }

        /// <summary>
        /// Property to determine if the current cart has been initialized in the session.
        /// Meaning if the persistent restore pipelines have run.
        /// This is so we only run the restore the first time.
        /// </summary>
        public static bool CartSessionInitialized {
            get
            {
                if (HttpContext.Current == null || HttpContext.Current.Items.Count == 0)
                {
                    return false;
                }

                if (HttpContext.Current.Session[_sessionCartInitializedKey] == null)
                {
                    return false;
                }

                var value = HttpContext.Current.Session[_sessionCartInitializedKey];

                if (value == null)
                {
                    return false;
                }

                return (bool)value;
            }
            set { HttpContext.Current.Session[_sessionCartInitializedKey] = value; }
        }

        /// <summary>
        /// Property to determine if the carts updated event has been initialized.
        /// </summary>
        public static bool CartUpdatedEventInitialized
        {
            get
            {
                if (HttpContext.Current == null || HttpContext.Current.Items.Count == 0)
                {
                    return false;
                }

                if (HttpContext.Current.Session[_sessionCartUpdatedEventInitializedKey] == null)
                {
                    return false;
                }

                var value = HttpContext.Current.Session[_sessionCartUpdatedEventInitializedKey];

                if (value == null)
                {
                    return false;
                }

                return (bool)value;
            }
            set { HttpContext.Current.Session[_sessionCartUpdatedEventInitializedKey] = value; }
        }


        /// <summary>
        /// Property to determine if Card Pesistence is active.
        /// can be deactivated in the settings config file.
        /// </summary>
        public static bool IsActive {
            get
            {
                var settingValue = Sitecore.Configuration.Settings.GetSetting("ActiveCommerce.Cart.Persistence.Active");

                var active = false;
                if (bool.TryParse(settingValue, out active))
                {
                    return active;
                }

                return false;
            }
        }

        /// <summary>
        /// Property to get the globally defined Customer Restore Strategy.
        /// </summary>
        public static CustomerRestoreStrategy CustomerRestoreStrategyGlobalSetting
        {
            get
            {
                var settingValue = Sitecore.Configuration.Settings.GetSetting("ActiveCommerce.Cart.Persistence.CustomerRestoreStrategy");
                
                var enumValue = CustomerRestoreStrategy.None;
                if (Enum.TryParse(settingValue, true, out enumValue))
                {
                    return enumValue;
                }

                return CustomerRestoreStrategy.None;
            }
        }
    }
}