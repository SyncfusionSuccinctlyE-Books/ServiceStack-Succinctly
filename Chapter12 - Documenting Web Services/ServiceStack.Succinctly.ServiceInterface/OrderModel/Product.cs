using System.Runtime.Serialization;

namespace ServiceStack.Succinctly.ServiceInterface.OrderModel
{
    [DataContract]
    public class Product
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public Status Status { get; set; }
    }
}