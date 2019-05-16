using Microsoft.Extensions.Configuration;
using MobileCart.Interfaces;
using MobileCart.Models;
using MobileCart.Saga;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MobileCart.Managers
{
    public class ShippingManager
    {
        private IConfiguration _configuration;
        private int _orderRequestId;
        private IProcessOrderShippedManager _processOrderManager;
        public ShippingManager(IProcessOrderShippedManager processOrderManager)
        {
            _processOrderManager = processOrderManager;
            _configuration = processOrderManager.configuration;
            _orderRequestId = processOrderManager.orderRequestId;
            processOrderManager.SubscribeToOrderShipped(ShipItems);
        }

        public void ShipItems(Order order)
        {
            _orderRequestId = _processOrderManager.orderRequestId;
            var url = _configuration.GetSection("RestAPI").GetSection("ShippingRestAPI").Value;

            using (HttpClient client = new HttpClient())
            {

                HttpResponseMessage response = client.GetAsync(url + order.Id.ToString()).Result;                
                if (response.IsSuccessStatusCode)
                {
                    var IsOrderShipped = response.Content.ReadAsAsync<bool>().Result;
                    order.IsOrderShipped = IsOrderShipped;
                    StatusManager.OrderLst.AddOrUpdate(_orderRequestId, "OrderShipped", (id, val) => { return "OrderShipped"; });
                }
            }
        }
    }
}
