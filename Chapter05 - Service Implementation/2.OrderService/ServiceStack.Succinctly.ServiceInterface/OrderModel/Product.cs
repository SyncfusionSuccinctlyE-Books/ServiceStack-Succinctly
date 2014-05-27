using System.Linq;
using System.Text;

namespace ServiceStack.Succinctly.ServiceInterface.OrderModel
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
    }
}