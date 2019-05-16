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
    public class OrderApproveManager
    {
        public IConfiguration _configuration;
        private int _orderRequestId;
        private IProcessOrderApprovedManager _processOrderManager;

        public OrderApproveManager(IProcessOrderApprovedManager processOrderManager)
        {
            _processOrderManager = processOrderManager;
            _configuration = processOrderManager.configuration;
            _orderRequestId = processOrderManager.orderRequestId;
            processOrderManager.SubscribeToOrderApproved(IsOrderApproved);
        }

        public void IsOrderApproved(Order order)
        {
            _orderRequestId = _processOrderManager.orderRequestId;
            var url = _configuration.GetSection("RestAPI").GetSection("OrderApprovalRestAPI").Value;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(url + order.Id.ToString()).Result;
                if (response.IsSuccessStatusCode)
                {
                    var IsOrderApproved = response.Content.ReadAsAsync<bool>().Result;
                    order.IsOrderApproved = IsOrderApproved;                   
                    StatusManager.OrderLst.AddOrUpdate(_orderRequestId, "OrderApproved", (id, val) => { return "OrderApproved"; });
                }
            }
            
        }
    }
}
