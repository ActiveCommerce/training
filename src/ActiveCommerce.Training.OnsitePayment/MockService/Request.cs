namespace ActiveCommerce.Training.OnsitePayment.MockService
{
    public class Request
    {
        public RequestType RequestType { get; set; }
        public string MerchantOrderNumber { get; set; }
        public Address BillToAddress { get; set; }
        public CreditCard CreditCard { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string TransactionId { get; set; }
    }

    public enum RequestType
    {
        Authorize,
        AuthorizeAndCapture,
        VoidReservation,
        CaptureReservation,
        Credit
    }
}