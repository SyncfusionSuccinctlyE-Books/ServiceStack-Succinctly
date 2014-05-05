using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ServiceStack.Succinctly.ServiceInterface.OrderModel
{
    [DataContract]
    public class OrdersResponse
    {
        [DataMember]
        public List<OrderResponse> Orders { get; set; }
    }
}