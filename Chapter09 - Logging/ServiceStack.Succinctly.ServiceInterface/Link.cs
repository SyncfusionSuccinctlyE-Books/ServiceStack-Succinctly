using System.Runtime.Serialization;

namespace ServiceStack.Succinctly.ServiceInterface
{
    [DataContract]
    public class Link
    {
        [DataMember]
        public string Rel { get; set; }

        [DataMember]
        public string Href { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Type { get; set; }
    }
}