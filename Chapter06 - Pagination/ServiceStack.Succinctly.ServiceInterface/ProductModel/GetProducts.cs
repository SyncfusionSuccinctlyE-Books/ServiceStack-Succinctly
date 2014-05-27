using System;
using System.Linq;
using System.Text;

namespace ServiceStack.Succinctly.ServiceInterface.ProductModel
{
    public class GetProducts
    {
        public int? Page { get; set; }
        public int? Size { get; set; }
    }
}
