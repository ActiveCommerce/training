using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.OnsitePayment.MockService
{
    public class CreditCard
    {
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string SecurityCode { get; set; }
    }
}