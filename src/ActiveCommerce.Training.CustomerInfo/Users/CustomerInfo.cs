namespace ActiveCommerce.Training.CustomerInfo.Users
{
    public class CustomerInfo : Sitecore.Ecommerce.Users.CustomerInfo
    {
        public const string BirthdayKey = "birthday";

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