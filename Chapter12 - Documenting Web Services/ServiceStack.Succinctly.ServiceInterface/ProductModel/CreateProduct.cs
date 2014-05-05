using System.Runtime.Serialization;

namespace ServiceStack.Succinctly.ServiceInterface.ProductModel
{
    [DataContract]    
    public class CreateProduct
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public Status Status { get; set; }
    }
}