using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MobileCart.Saga;

namespace MobileCart.Interfaces
{
    public interface IProcessOrderPlacedManager
    {
        IConfiguration configuration { get; set; }
        ILogger log {get;set;}
        int orderRequestId { get; set; }
        void SubscribeToOrderPlaced(ProcessOrderHandler orderHandler);
    }
}