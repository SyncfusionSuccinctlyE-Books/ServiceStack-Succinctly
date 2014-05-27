using System.Runtime.Serialization;

namespace ServiceStack.Succinctly.ServiceInterface.OrderItemModel
{
    [DataContract]
    public class GetOrderItem
    {
        [DataMember]
        public int OrderId { get; set; }

        [DataMember]
        public int ItemId { get; set; }
    }
}