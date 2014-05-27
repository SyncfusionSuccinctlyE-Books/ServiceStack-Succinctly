using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ServiceStack.Succinctly.ServiceInterface.ProductModel
{
    [DataContract]
    public class ProductsResponse
    {
        public ProductsResponse()
        {
            Links = new List<Link>();
        }

        [DataMember]
        public List<ProductResponse> Products { get; set; }

        [DataMember]
        public List<Link> Links { get; set; }
    }
}