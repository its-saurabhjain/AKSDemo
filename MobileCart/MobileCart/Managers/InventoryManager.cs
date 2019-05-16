using Microsoft.Extensions.Configuration;
using MobileCart.Interfaces;
using MobileCart.Models;
using MobileCart.Saga;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MobileCart.Managers
{
    public class InventoryManager
    {
        private IConfiguration _configuration;
        private int _orderRequestId;
        private IProcessOrderPlacedManager _processOrderManager;

        public InventoryManager(IProcessOrderPlacedManager processOrderManager)
        {
            _processOrderManager = processOrderManager;
            _configuration = processOrderManager.configuration;            
            processOrderManager.SubscribeToOrderPlaced(CheckInventory);
        }

        public void CheckInventory(Order order)
        {
            _orderRequestId = _processOrderManager.orderRequestId;
            var message = string.Empty;
            var url = _configuration.GetSection("RestAPI").GetSection("InventoryRestAPI").Value;
            try
            {
                var product = order.Cart[0];
                //foreach (var product in order.Cart)
                //{
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(100);                    
                    Product productResponse = null;
                    HttpResponseMessage response;
                    message = url + product.Id.ToString();
                    try
                    {
                        
                        response = client.GetAsync(url + product.Id.ToString()).Result;
                        message.Append('1');
                        response.EnsureSuccessStatusCode();
                        message.Append('2');
                    }
                    catch(AggregateException e)
                    {                       
                        response = null;
                        foreach(var except in e.InnerExceptions)
                        {
                            message += except.Message + except.StackTrace;
                            if(except.InnerException!= null)
                            {
                                message += except.InnerException.Message + except.InnerException.StackTrace;
                            }
                        }
                       
                    }
                    if (response.IsSuccessStatusCode)
                    {
                        message.Append('4');
                        productResponse = response.Content.ReadAsAsync<Product>().Result;
                        if (productResponse != null)
                        {
                            message.Append('5');
                            var prod = order.Cart.FirstOrDefault(x => x.Id == product.Id);
                            prod.Title = productResponse.Title;
                            prod.Description = productResponse.Description;
                            prod.Price = productResponse.Price;
                            StatusManager.OrderLst.AddOrUpdate(_orderRequestId, "OrderPlaced", (id, val) => { return val; });
                        }
                    }
                }
                // }
            }
            catch(Exception ex)
            {
                throw new Exception(url + message);
                //throw ex;
            }
        }


    }
}
