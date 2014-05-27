using System.Runtime.Serialization;

namespace ServiceStack.Succinctly.ServiceInterface.ProductModel
{
    [DataContract]
    public class UpdateProduct
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public Status Status { get; set; }
    }
}