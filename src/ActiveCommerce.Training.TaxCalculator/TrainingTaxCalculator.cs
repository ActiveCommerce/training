using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.TaxCalculator
{
    public class TrainingTaxCalculator : ActiveCommerce.Taxes.TaxCalculatorBase<Taxes.TaxConfiguration>
    {
        public readonly decimal RATE = .055m;
        public readonly string TYPE = "DEFAULT";
        public readonly string SHIPPING = "SHIPPING";
        public readonly string HANDLING = "HANDLING";

        public override Taxes.TaxTotals GetTaxes(IEnumerable<Taxes.TaxInquiry> order)
        {
            /**
             * Address and other information is available in properties of the base class.
             */

            //Shipping address is usually the relevant one for tax calculation
            var shippingAddress = this.Shipping;
            var city = shippingAddress.City;
            var state = shippingAddress.State;
            var countryCode = shippingAddress.Country.Code;
            var postalCode = shippingAddress.Zip;

            //Billing address is not usually relevant for taxes, but available
            var billingAddress = this.Billing;

            //Config values from the Tax Calculator item are also available.
            //You can extend the TaxConfiguration class and map to additional template fields if needed.
            var config = this.TaxConfiguration;

            /**
             * You should return tax amount for each order line, including
             * shipping and handling.
             */ 
            var taxTotals = new Taxes.TaxTotals();
            var productTaxes = new List<ActiveCommerce.Taxes.TaxLine>();
            taxTotals.ProductTax = productTaxes;
            foreach (var taxLine in order)
            {
                if (taxLine.IsShipping)
                {
                    taxTotals.ShippingTax = GetTaxLine(SHIPPING, taxLine.Total);
                }
                else if (taxLine.IsHandling)
                {
                    taxTotals.HandlingTax = GetTaxLine(HANDLING, taxLine.Total);
                }
                else
                {
                    var productLine = GetTaxLine(taxLine.ProductCode, taxLine.Total);
                    productTaxes.Add(productLine);
                }
            }
            return taxTotals;
        }

        public Taxes.TaxLine GetTaxLine(string productCode, decimal total)
        {
            return new Taxes.TaxLine
                    {
                        TaxedAmount = total,
                        ProductCode = productCode,
                        Jurisdictions = new List<Taxes.TaxJurisdiction>
                        {
                            /* just return a single jurisdiction for this example.
                             * if you need AC to track tax breakdown by jurisdiction,
                             * you would need to add a line for each jurisdiction according to its
                             * tax rate. */
                            new Taxes.TaxJurisdiction {
                                 Name = TYPE,
                                 Type = TYPE,
                                 Rate = RATE,
                                 Tax = total * RATE
                            }
                        }
                    };
        }

        /// <summary>
        /// Your tax calculator can decide whether it can handle taxes for the order,
        /// based on the shipping address and tax configuration.
        /// </summary>
        /// <returns></returns>
        public override bool WillHandle()
        {
            return true;
        }
    }
}