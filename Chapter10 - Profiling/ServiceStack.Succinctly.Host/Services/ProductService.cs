using System.Collections.Generic;
using ServiceStack.MiniProfiler;
using ServiceStack.ServiceInterface;
using ServiceStack.Succinctly.ServiceInterface;
using ServiceStack.Succinctly.ServiceInterface.ProductModel;

namespace ServiceStack.Succinctly.Host.Services
{
    public class ProductService : ServiceStack.ServiceInterface.Service
    {
       [Authenticate]
        public ProductResponse Get(GetProduct request)
        {
            using (Profiler.Current.Step("Returning the Product Response"))
            {
                return default(ProductResponse);
            }
        }
             
        public List<ProductResponse> Get(GetProducts request)
        {
            return new List<ProductResponse>
                {
                    new ProductResponse
                        {
                            Id = 1,
                            Name = "Product",
                            Status = new Status {Id = 1, Name = "Active"}
                        }
                };
        }
    }
}