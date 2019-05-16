using MobileCart.Managers;
using MobileCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileCart.Saga
{
    public delegate void ProcessOrderHandler(Order order);
    public class ProcessOrderSaga
    {
        public event ProcessOrderHandler OrderPlaced;
        public event ProcessOrderHandler OrderApproved;
        public event ProcessOrderHandler OrderShipped;
        public event ProcessOrderHandler ItemsDelivered;
        public void ProcessOrder(Order order)
        {
            OrderPlaced(order);
            OrderApproved(order);
            if (order.IsOrderApproved)
            {
                OrderShipped(order);
                if (order.IsOrderShipped)
                {
                    ItemsDelivered(order);
                }
            }
        }

        
    }
}
