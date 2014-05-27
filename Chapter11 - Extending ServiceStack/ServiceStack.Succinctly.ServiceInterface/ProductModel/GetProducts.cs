using System.Runtime.Serialization;

namespace ServiceStack.Succinctly.ServiceInterface.ProductModel
{
    [DataContract]
    public class GetProducts
    {
        [DataMember]
        public int? Page { get; set; }

        [DataMember]
        public int? Size { get; set; }

        public int GetPage()
        {
            return !Page.HasValue ? 1 : Page.Value;
        }

        public int GetSize()
        {
            return !Size.HasValue ? 10 : Size.Value;
        }
    }
}