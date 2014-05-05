using System.Runtime.Serialization;

namespace ServiceStack.Succinctly.ServiceInterface.OrderModel
{
    [DataContract]
    public class DeleteOrder
    {
        [DataMember]
        public int Id { get; set; }
    }
}