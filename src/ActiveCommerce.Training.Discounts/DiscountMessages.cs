using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.Extensions;
using ActiveCommerce.SitecoreX.Globalization;
using Sitecore.Diagnostics;

namespace ActiveCommerce.Training.Discounts
{
    public static class DiscountMessages
    {
        private static readonly string DiscountMessageKey = "AC_CART_DISCOUNT_MESSAGE";

        private static IList<string> InnerMessages
        {
            get
            {
                var messages = HttpContext.Current.Session[DiscountMessageKey] as IList<string>;
                if (messages == null)
                {
                    messages = new List<string>();
                    HttpContext.Current.Session[DiscountMessageKey] = messages;
                }
                return messages;
            }
        }

        public static IEnumerable<string> Messages
        {
            get
            {
                return InnerMessages.AsEnumerable();
            }
        }

        public static void Add(string message)
        {
            Add(message, null);
        }

        public static void Add(string message, params object[] values)
        {
            Assert.IsNotNullOrEmpty(message, "message");
            if (Translator.ContainsEntry(message))
            {
                if (values != null && values.Any())
                {
                    InnerMessages.Add(Translator.Format(message, values));
                }
                else
                {
                    InnerMessages.Add(Translator.Text(message));
                }
            }
            else
            {
                if (values != null && values.Any())
                {
                    InnerMessages.Add(string.Format(message, values));
                }
                else
                {
                    InnerMessages.Add(message);
                }
            }
        }

        public static void Clear()
        {
            InnerMessages.Clear();
        }

        public static bool Any()
        {
            return InnerMessages.Any();
        }


    }
}