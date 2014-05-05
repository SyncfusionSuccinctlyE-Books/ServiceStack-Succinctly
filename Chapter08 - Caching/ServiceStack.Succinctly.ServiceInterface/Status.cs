using System.Runtime.Serialization;

namespace ServiceStack.Succinctly.ServiceInterface
{
    [DataContract]
    public class Status
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}