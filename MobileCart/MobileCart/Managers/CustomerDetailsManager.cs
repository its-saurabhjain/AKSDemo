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
    public class CustomerDetailsManager
    {
        public IConfiguration _configuration;
        private int _orderRequestId;
        private IProcessOrderPlacedManager _processOrderManager;

        public CustomerDetailsManager(IProcessOrderPlacedManager processOrderManager)
        {
            _processOrderManager = processOrderManager;
            _configuration = processOrderManager.configuration;
            processOrderManager.SubscribeToOrderPlaced(GetCustomerDetails);
        }

        public void GetCustomerDetails(Order order)
        {
            _orderRequestId = _processOrderManager.orderRequestId;
            var url = _configuration.GetSection("RestAPI").GetSection("CustomerRestAPI").Value;

            using (HttpClient client = new HttpClient())
            {
                Customer customer = null;
                HttpResponseMessage response = client.GetAsync(url + order.CustomerDetails.Id.ToString()).Result;
                if (response.IsSuccessStatusCode)
                {
                    customer = response.Content.ReadAsAsync<Customer>().Result;
                    if (customer != null)
                    {
                        order.CustomerDetails = customer;
                        StatusManager.OrderLst.AddOrUpdate(_orderRequestId, "OrderPlaced", (id, val) => { return val; });                        
                    }
                }
            }
        }
    }
}
