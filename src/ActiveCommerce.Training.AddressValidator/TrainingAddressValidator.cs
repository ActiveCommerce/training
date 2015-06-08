using System;
using ActiveCommerce.Validation;
using Sitecore.Diagnostics;

namespace ActiveCommerce.Training.AddressValidator
{
    public class TrainingAddressValidator : IAddressValidator
    {
        public Sitecore.Ecommerce.DomainModel.Addresses.AddressInfo Validate(Sitecore.Ecommerce.DomainModel.Addresses.AddressInfo address)
        {
            ValidateRequired(address.Address, "Address Line 1");
            ValidateRequired(address.City, "City");
            ValidateRequired(address.Country.Code, "Country");

            /**
             * After doing basic validations, you could call an address validation service or API here.
             * Instead, we're going to do some custom validation. The builtin default address validator is
             * definitely more thorough -- this is for example purposes only.
             */

            if (address.Country.Code.Equals("US", StringComparison.InvariantCultureIgnoreCase))
            {
                ValidateRequired(address.State, "State");
                ValidateRequired(address.Zip, "Postal Code");

                //zip or zip plus 4
                if (address.Zip.Length != 5 && address.Zip.Length != 10)
                {
                    throw new AddressValidationException("Not a valid U.S. zip code");
                }
            }

            /**
             * To emphasize that this is for example only :)
             * 
             */
            if (address.Address.Contains("hubert"))
            {
                throw new AddressValidationException("You taste like soot and...");
            }

            /**
             * We can make simple corrections to the address as well. Interactive address
             * corrections (e.g. with user acceptance of corrected address) would require
             * customization of the checkout UX.
             */
            address.Address = address.Address.ToUpper();
            address.Address2 = address.Address2 != null ? address.Address2.ToUpper() : null;
            address.City = address.City.ToUpper();
            address.State = address.State.ToUpper();
            address.Zip = address.Zip.ToUpper();

            return address;
        }

        protected void ValidateRequired(string required, string name)
        {
            Assert.ArgumentNotNullOrEmpty(name, "name");
            if (string.IsNullOrWhiteSpace(required))
            {
                throw new AddressValidationException(string.Format("{0} is required", name));
            }
        }
    }
}