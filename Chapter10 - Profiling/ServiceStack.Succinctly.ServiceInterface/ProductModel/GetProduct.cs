using System.Runtime.Serialization;

namespace ServiceStack.Succinctly.ServiceInterface.ProductModel
{
    [DataContract]
    public class GetProduct
    {
        [DataMember]
        public int Id { get; set; }
    }

    [DataContract]
    public class GetProducts
    {
       
    }
}