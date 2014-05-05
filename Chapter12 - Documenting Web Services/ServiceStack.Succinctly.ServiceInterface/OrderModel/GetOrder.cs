using System.Runtime.Serialization;

namespace ServiceStack.Succinctly.ServiceInterface.OrderModel
{
    [DataContract]
    public class GetOrder
    {
        [DataMember]
        public int Id { get; set; }
    }
}