using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.CartPersistence.Common
{
    public static class CartPersistenceContext
    {
        private const string RequestCartUpdatedKey = "AC_PERSISTENCE_CART_UPDATED";
        private const string SessionCartInitializedKey = "AC_PERSISTENCE_CART_INITIALIZED";
        private const string SessionCartUpdatedEventInitializedKey = "AC_PERSISTENCE_CART_UPDATE_INITIALIZED";

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

                if (!HttpContext.Current.Items.Contains(RequestCartUpdatedKey))
                {
                    return false;
                }

                var value = HttpContext.Current.Items[RequestCartUpdatedKey];

                if (value == null)
                {
                    return false;
                }

                return (bool) value;
            }
            set
            {
                HttpContext.Current.Items[RequestCartUpdatedKey] = value;
            }
        }

        /// <summary>
        /// Property to determine if the current cart has been initialized in the session.
        /// Meaning if the persistent restore pipelines have run.
        /// This is so we only run the restore the first time.
        /// </summary>
        public static bool PersistenceInitialized {
            get
            {
                if (HttpContext.Current == null || HttpContext.Current.Items.Count == 0)
                {
                    return false;
                }

                if (HttpContext.Current.Session[SessionCartInitializedKey] == null)
                {
                    return false;
                }

                var value = HttpContext.Current.Session[SessionCartInitializedKey];

                if (value == null)
                {
                    return false;
                }

                return (bool)value;
            }
            set
            {
                HttpContext.Current.Session[SessionCartInitializedKey] = value;
            }
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

                if (HttpContext.Current.Session[SessionCartUpdatedEventInitializedKey] == null)
                {
                    return false;
                }

                var value = HttpContext.Current.Session[SessionCartUpdatedEventInitializedKey];

                if (value == null)
                {
                    return false;
                }

                return (bool)value;
            }
            set
            {
                HttpContext.Current.Session[SessionCartUpdatedEventInitializedKey] = value;
            }
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
        public static CustomerRestoreStrategy CustomerRestoreStrategy
        {
            get
            {
                var settingValue = Sitecore.Configuration.Settings.GetSetting("ActiveCommerce.Cart.Persistence.CustomerRestoreStrategy");
                CustomerRestoreStrategy enumValue;
                if (Enum.TryParse(settingValue, true, out enumValue))
                {
                    return enumValue;
                }
                return CustomerRestoreStrategy.None;
            }
        }
    }
}