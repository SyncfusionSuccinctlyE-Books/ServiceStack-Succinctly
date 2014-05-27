using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ServiceStack.Succinctly.ServiceInterface.ProductModel
{
    [DataContract]
    public class ProductResponse
    {
        public ProductResponse()
        {
            Links = new List<Link>();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public Status Status { get; set; }

        [DataMember]
        public List<Link> Links { get; set; }
    }
}