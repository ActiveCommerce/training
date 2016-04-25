using Sitecore.Ecommerce.DomainModel.Addresses;

namespace ActiveCommerce.Training.CustomerInfo.Users
{
    public class CustomerInfo : ActiveCommerce.Users.CustomerInfo
    {
        public const string BirthdayKey = "birthday";
        
        //IMPORTANT: add parameterized constructor so that billing/shippingAddress are constructor injected by Unity
        public CustomerInfo(AddressInfo billingAddress, AddressInfo shippingAddress) : base(billingAddress, shippingAddress)
        {
        }

        /// <summary>
        /// Save birthday value on CustomProperties. Requires that this field has been added in the core
        /// database on the Customer profile template, /sitecore/templates/Ecommerce/Security/Profiles/Customer.
        /// </summary>
        public string Birthday
        {
            get
            {
                return this.CustomProperties[BirthdayKey];
            }
            set
            {
                this.CustomProperties[BirthdayKey] = value;
            }

        }
    }
}