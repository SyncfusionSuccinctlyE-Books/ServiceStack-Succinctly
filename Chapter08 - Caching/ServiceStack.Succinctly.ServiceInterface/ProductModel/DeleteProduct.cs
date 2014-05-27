using System.Runtime.Serialization;

namespace ServiceStack.Succinctly.ServiceInterface.ProductModel
{
    [DataContract]
    public class DeleteProduct
    {
        [DataMember]
        public int Id { get; set; }
    }
}