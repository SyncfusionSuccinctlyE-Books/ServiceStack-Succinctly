using System.Collections.Generic;
using System.Runtime.Serialization;
using ServiceStack.Succinctly.ServiceInterface.ProductModel;

namespace ServiceStack.Succinctly.ServiceInterface.OrderModel
{
    [DataContract]
    public class OrderItemResponse
    {
        public OrderItemResponse()
        {
            Links = new List<Link>();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public ProductResponse Product { get; set; }

        [DataMember]
        public int Quantity { get; set; }

        [DataMember]
        public List<Link> Links { get; set; }
    }
}