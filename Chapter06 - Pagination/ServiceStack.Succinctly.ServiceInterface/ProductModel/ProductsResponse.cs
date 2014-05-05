using System.Collections.Generic;

namespace ServiceStack.Succinctly.ServiceInterface.ProductModel
{
    public class ProductsResponse
    {
        public ProductsResponse()
        {
            Links = new List<Link>();
        }

        public List<ProductResponse> Products { get; set; }
        public List<Link> Links { get; set; }
    }
}