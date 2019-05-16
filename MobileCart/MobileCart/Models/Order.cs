using System.Collections.Generic;

namespace MobileCart.Models
{
    public class Order
    {
        public Order()
        {
           
        }

        public int Id { get; set; }
        public Customer CustomerDetails { get; set; }

        public List<Product> Cart { get; set; }

        public bool IsOrderApproved { get; set; }

        public bool IsOrderShipped { get; set; }

        public bool IsItemsDelivered { get; set; }
    }
}