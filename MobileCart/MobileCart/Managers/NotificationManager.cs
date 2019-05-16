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
    public class NotificationManager
    {
        public IConfiguration _configuration;
        private int _orderRequestId;
        private IProcessItemDeliveredManager _processOrderManager;

        public NotificationManager(IProcessItemDeliveredManager processOrderManager)
        {
            _processOrderManager = processOrderManager;
            _configuration = processOrderManager.configuration;
            processOrderManager.SubscribeToItemsDelivered(SendNotofications);
        }

        public void SendNotofications(Order order)
        {
            _orderRequestId = _processOrderManager.orderRequestId;
            var url = _configuration.GetSection("RestAPI").GetSection("NotificationRestAPI").Value;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(url + order.Id.ToString()).Result;
                if (response.IsSuccessStatusCode)
                {
                    var IsItemsDelivered = response.Content.ReadAsAsync<bool>().Result;
                    order.IsItemsDelivered = IsItemsDelivered;
                    StatusManager.OrderLst.AddOrUpdate(_orderRequestId, "ItemsDelivered", (id, val) => { return "ItemsDelivered"; });
                }
            }            
        }
    }
}
