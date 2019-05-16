using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MobileCart.Interfaces;
using MobileCart.Models;
using MobileCart.Requests;
using MobileCart.Saga;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileCart.Managers
{
    
    public class ProcessOrderManager : IProcessOrderPlacedManager, IProcessOrderShippedManager, IProcessItemDeliveredManager, IProcessOrderApprovedManager
    {
        private ProcessOrderSaga _processOrderSaga;
        private InventoryManager _inventoryManager;
        private CustomerDetailsManager _customerDetailsManager;
        private OrderApproveManager _orderApproveManager;
        private ShippingManager _shippingManager;
        private NotificationManager _notificationManager;
        public Order _order;
        public IConfiguration configuration { get; set; }
        public ILogger log { get; set; }
        public int orderRequestId { get; set; }
        public ProcessOrderManager(IConfiguration iconfiguration,ILogger ilog)
        {
            configuration = iconfiguration;
            log = ilog;
            intialize();
        }

        //public static ProcessOrderManager Instance
        //{            
        //    get { lock (lockObject) { if (_instance == null) { _instance = new ProcessOrderManager(); } return _instance; } }
        //}

        private void intialize()
        {
            _processOrderSaga = new ProcessOrderSaga();
            _inventoryManager = new InventoryManager(this);
            _customerDetailsManager = new CustomerDetailsManager(this);
            _orderApproveManager = new OrderApproveManager(this);
            _shippingManager = new ShippingManager(this);
            _notificationManager = new NotificationManager(this);
        }

        public void ProcessOrder(OrderRequest orderRequest)
        {
            orderRequestId = orderRequest.Id;
            _order = new Order();
            _order.CustomerDetails = new Customer() { Id = orderRequest.CustomerId };
            _order.Cart = new List<Product>();
            foreach (var productId in orderRequest.Products)
            {
                _order.Cart.Add(new Product() { Id = productId });
            }

            _processOrderSaga.ProcessOrder(_order);
        }

        public void SubscribeToOrderPlaced(ProcessOrderHandler orderHandler)
        {
            _processOrderSaga.OrderPlaced += orderHandler;
        }
        public void SubscribeToOrderApproved(ProcessOrderHandler orderHandler)
        {
            _processOrderSaga.OrderApproved += orderHandler;
        }
        public void SubscribeToOrderShipped(ProcessOrderHandler orderHandler)
        {
            _processOrderSaga.OrderShipped += orderHandler;
        }
        public void SubscribeToItemsDelivered(ProcessOrderHandler orderHandler)
        {
            _processOrderSaga.ItemsDelivered += orderHandler;
        }

        
    }
}
