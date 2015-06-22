using System.ServiceModel;

namespace ActiveCommerce.Training.OrderUpdateService.Services
{
    [ServiceContract]
    public interface IOrderUpdate
    {
        [OperationContract]
        void UpdateOrderShipped(string orderId, string trackingUrl);
    }
}
