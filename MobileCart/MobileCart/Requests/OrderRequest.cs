using System;
using System.Collections.Generic;

namespace MobileCart.Requests
{
    public class OrderRequest
    {
        public OrderRequest()
        {
        }

        public int Id { get; set; }
        public List<int> Products { get; set; }
        public int CustomerId { get; set; }

    }
}

