using System.Collections.Generic;

namespace ServiceStack.Succinctly.ServiceInterface.OrderModel
{
    public class OrdersResponse
    {
        public List<OrderResponse> Orders { get; set; }
    }
}