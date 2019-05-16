using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MobileCart.Saga;

namespace MobileCart.Interfaces
{
    public interface IProcessItemDeliveredManager
    {
        IConfiguration configuration { get; set; }
        ILogger log { get; set; }
        int orderRequestId { get; set; }

        void SubscribeToItemsDelivered(ProcessOrderHandler orderHandler);
    }
}