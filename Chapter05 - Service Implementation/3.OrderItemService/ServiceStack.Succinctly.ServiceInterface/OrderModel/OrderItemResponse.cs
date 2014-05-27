using System.Collections.Generic;

namespace ServiceStack.Succinctly.ServiceInterface.OrderModel
{
    public class OrderItemResponse 
    {
        public int Id { get; set; }
        public ProductResponse Product { get; set; }
        public int Quantity { get; set; }
        public List<Link> Links { get; set; }
    }
}