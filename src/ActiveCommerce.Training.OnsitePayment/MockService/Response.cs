using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.OnsitePayment.MockService
{
    public class Response
    {
        public ResponseStatus ResponseStatus { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public string ResponseCode { get; set; }
        public string Message { get; set; }
        public string AuthorizationCode { get; set; }
        public string TransactionId { get; set; }
        public string MerchantOrderNumber { get; set; }
    }

    public enum ResponseStatus
    {
        Success,
        Failure
    }

    public enum TransactionStatus
    {
        Authorized,
        Captured,
        Voided,
        Credited
    }
}