using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ActiveCommerce.Training.OnsitePayment.MockService
{
    /// <summary>
    /// Placeholder for a payment gateway API. Doesn't actually communicate externally.
    /// You should not need to reference this code at all, unless you're looking to simulate a
    /// payment processor yourself.
    /// </summary>
    public class MockPaymentService
    {
        protected const string GoodCardNumber = "4111111111111111";
        protected const string AnotherCardNumber = "4007000000027";
        protected const int MinSecurityCode = 100;

        private static IDictionary<string, Response> _transactionStatus = new ConcurrentDictionary<string, Response>();

        public string EndpointUrl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public Response ExecuteRequest(Request request)
        {
            if (string.IsNullOrEmpty(EndpointUrl))
            {
                throw new ArgumentException("Endpoint not configured");
            }

            if (!"hubert".Equals(Username) || !"comberdale".Equals(Password))
            {
                return new Response
                {
                    ResponseStatus = ResponseStatus.Failure,
                    ResponseCode = "401",
                    Message = "Authentication failed"
                };
            }

            Response response;
            switch(request.RequestType)
            {
                case RequestType.Authorize:
                    response = Authorize(request);
                    break;
                case RequestType.AuthorizeAndCapture:
                    response = AuthorizeAndCapture(request);
                    break;
                case RequestType.CaptureReservation:
                    response = Capture(request);
                    break;
                case RequestType.VoidReservation:
                    response = Void(request);
                    break;
                case RequestType.Credit:
                    response = Credit(request);
                    break;
                default:
                    throw new ArgumentException();
            }
            if (!string.IsNullOrEmpty(response.TransactionId))
            {
                _transactionStatus.Add(response.TransactionId, response);
            }
            return response;
        }

        protected Response Authorize(Request request)
        {
            var transactionId = Guid.NewGuid().ToString();
            var card = request.CreditCard;
            int securityCode = -1;
            int.TryParse(card.SecurityCode, out securityCode);
            if (card.CardNumber != GoodCardNumber && card.CardNumber != AnotherCardNumber)
            {
                return new Response
                {
                    ResponseStatus = ResponseStatus.Failure,
                    ResponseCode = "200",
                    Message = "Bad card number",
                    TransactionId = transactionId,
                    MerchantOrderNumber = request.MerchantOrderNumber
                };
            }
            else if (securityCode < MinSecurityCode)
            {
                return new Response
                {
                    ResponseStatus = ResponseStatus.Failure,
                    ResponseCode = "201",
                    Message = "Bad security code",
                    TransactionId = transactionId,
                    MerchantOrderNumber = request.MerchantOrderNumber
                };
            }
            return new Response
            {
                ResponseStatus = ResponseStatus.Success,
                ResponseCode = "100",
                Message = "OK",
                TransactionId = transactionId,
                AuthorizationCode = Guid.NewGuid().ToString(),
                MerchantOrderNumber = request.MerchantOrderNumber,
                TransactionStatus = TransactionStatus.Authorized
            };
        }

        protected Response AuthorizeAndCapture(Request request)
        {
            var response = Authorize(request);
            CapturePrevious(response);
            return response;
        }

        protected Response Capture(Request request)
        {
            var previous = FindPrevious(request);
            var response = CapturePrevious(previous);
            return response;
        }

        protected Response FindPrevious(Request request)
        {
            if (string.IsNullOrEmpty(request.TransactionId))
            {
                throw new ArgumentException("Requires TransactionId");
            }
            if (!_transactionStatus.ContainsKey(request.TransactionId))
            {
                return new Response
                {
                    ResponseStatus = ResponseStatus.Failure,
                    ResponseCode = "300",
                    Message = "Unknown transaction id"
                };
            }
            var previous = _transactionStatus[request.TransactionId];
            if (previous.TransactionStatus != TransactionStatus.Authorized)
            {
                return new Response
                {
                    ResponseStatus = ResponseStatus.Failure,
                    ResponseCode = "301",
                    Message = "Transaction already captured or voided"
                };
            }
            return previous;
        }

        protected Response CapturePrevious(Response response)
        {
            response.TransactionStatus = TransactionStatus.Captured;
            return response;
        }

        protected Response Void(Request request)
        {
            var previous = FindPrevious(request);
            previous.TransactionStatus = TransactionStatus.Voided;
            return previous;
        }

        protected Response Credit(Request request)
        {
            var previous = FindPrevious(request);
            previous.TransactionStatus = TransactionStatus.Credited;
            return previous;
        }
    }
}