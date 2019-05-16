using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MobileCart.Saga;

namespace MobileCart.Interfaces
{
    public interface IProcessOrderApprovedManager
    {
        IConfiguration configuration { get; set; }
        ILogger log { get; set; }
        int orderRequestId { get; set; }

        void SubscribeToOrderApproved(ProcessOrderHandler orderHandler);
    }
}