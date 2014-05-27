using System.Collections.Generic;

namespace ServiceStack.Succinctly.ServiceInterface.OrderModel
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }    public List<Link> Links { get; set; }
    }
}