using System.Collections.Generic;

namespace ServiceStack.Succinctly.ServiceInterface.ProductModel
{
    public class ProductsResponse
    {
        public List<ProductResponse> Products { get; set; }
    }
}