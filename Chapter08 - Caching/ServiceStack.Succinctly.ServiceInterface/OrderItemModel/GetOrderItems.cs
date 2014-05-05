using System.Runtime.Serialization;

namespace ServiceStack.Succinctly.ServiceInterface.OrderItemModel
{
    [DataContract]
    public class GetOrderItems
    {
        [DataMember]
        public int OrderId { get; set; }
    }
}