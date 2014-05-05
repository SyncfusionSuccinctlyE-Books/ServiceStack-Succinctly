using System.Runtime.Serialization;
using ServiceStack.Succinctly.ServiceInterface.OrderModel;
using System;
using System.Collections.Generic;

namespace ServiceStack.Succinctly.ServiceInterface.OrderItemModel
{
    [DataContract]
    public class OrderItemsResponse
    {
        [DataMember]
        public List<OrderItemResponse> Items { get; set; }
    }
}