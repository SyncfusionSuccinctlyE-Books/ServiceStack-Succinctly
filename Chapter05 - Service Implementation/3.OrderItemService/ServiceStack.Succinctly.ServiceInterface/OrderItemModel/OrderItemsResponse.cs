using System.Collections.Generic;
using ServiceStack.Succinctly.ServiceInterface.OrderModel;

namespace ServiceStack.Succinctly.ServiceInterface.OrderItemModel
{
    public class OrderItemsResponse
    {
        public List<OrderItemResponse> Items { get; set; }
    }
}