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

        public override bool WillHandle()
        {
            return true;
        }
    }
}