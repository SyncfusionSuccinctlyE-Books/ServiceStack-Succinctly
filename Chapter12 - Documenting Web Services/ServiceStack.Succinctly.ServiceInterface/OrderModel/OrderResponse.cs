using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ServiceStack.Succinctly.ServiceInterface.OrderModel
{
    [DataContract]
    public class OrderResponse
    {
        public OrderResponse()
        {
            Links = new List<Link>();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public bool IsTakeAway { get; set; }

        [DataMember]
        public DateTime CreationDate { get; set; }

        [DataMember]
        public List<OrderItemResponse> Items { get; set; }

        [DataMember]
        public List<Link> Links { get; set; }

        [DataMember]
        public Status Status { get; set; }
    }
}