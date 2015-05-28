using System;
using Sitecore.Analytics.Model.Framework;

namespace ActiveCommerce.Training.CartPersistence.Analytics.Model
{
    [Serializable]
    public class ShoppingCartLine : Element, IShoppingCartLine
    {
        private const string PRODUCTCODE = "ProductCode";
        private const string QUANTITY = "Quantity";

        public ShoppingCartLine()
        {
            base.EnsureAttribute<string>(PRODUCTCODE);
            base.EnsureAttribute<uint>(QUANTITY);
        }

        public string ProductCode
        {
            get
            {
                return base.GetAttribute<string>(PRODUCTCODE);
            }
            set
            {
                base.SetAttribute<string>(PRODUCTCODE, value);
            }
        }

        public uint Quantity
        {
            get
            {
                return base.GetAttribute<uint>(QUANTITY);
            }
            set
            {
                base.SetAttribute<uint>(QUANTITY, value);
            }
        }
    }
}
