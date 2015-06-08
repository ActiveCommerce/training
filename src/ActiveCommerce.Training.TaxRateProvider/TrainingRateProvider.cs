using System;
using System.Collections.Generic;
using ActiveCommerce.Taxes.Rates;

namespace ActiveCommerce.Training.TaxRateProvider
{
    public class TrainingRateProvider : ISalesTaxRateProvider
    {
        private const string RateSetting = "ActiveCommerce.Training.TaxRate.Rate.{0}";
        private const string NameSetting = "ActiveCommerce.Training.TaxRate.Name.{0}";

        public IEnumerable<TaxRate> GetRates(Sitecore.Ecommerce.DomainModel.Addresses.AddressInfo address)
        {
            /**
             * See README, this sort of rate provider is usually a bad idea -- you should utilize a service that can look up
             * tax rates for the specific street address!
             */

            //read the state code from the provided address
            var state = address.State != null ? address.State.ToUpper() : null;
            if (state == null)
            {
                return EmptyRates;
            }

            //look for a configured rate for the given state
            var rateStr = Sitecore.Configuration.Settings.GetSetting(string.Format(RateSetting, state), null);
            if (string.IsNullOrEmpty(rateStr))
            {
                return EmptyRates;
            }

            //attempt to parse into decimal
            decimal rate = 0m;
            var success = decimal.TryParse(rateStr, out rate);
            if (!success)
            {
                return EmptyRates;
            }

            //look for a configured name for the state, defaulting to "State"
            var name = Sitecore.Configuration.Settings.GetSetting(string.Format(NameSetting, state), ActiveCommerce.Taxes.TaxJurisdiction.Types.STATE);

            //return a list of rates -- in a more complex example, could include additional jurisdictions (city, county rates)
            return new TaxRate[]
            {
                new TaxRate
                {
                    Compounds = false, //usually only applies to Canadian GST/PST
                    Name = name, //name of the jurisdiction
                    Rate = rate, //the rate as a decimal (e.g. 0.05), not a percentage (5%)
                    Type = ActiveCommerce.Taxes.TaxJurisdiction.Types.STATE //use provided jurisdiction constants here if possible
                }
            };
        }

        /// <summary>
        /// Known issue, can't return null or empty tax rate list. Need to return 0-rate enumeration
        /// if there's no match. This would only happen if the calculator was configured to charge tax for a state which
        /// we don't have rates for, or for other configuration errors.
        /// </summary>
        private IEnumerable<TaxRate> EmptyRates
        {
            get
            {
                return new TaxRate[]
                {
                    new TaxRate
                    {
                        Compounds = false,
                        Name = "none",
                        Rate = 0,
                        Type = ActiveCommerce.Taxes.TaxJurisdiction.Types.STATE
                    }
                };
            }
        }
    }
}