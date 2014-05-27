using System.Collections.Generic;
using ServiceStack.ServiceInterface;
using ServiceStack.Succinctly.ServiceInterface;
using ServiceStack.Succinctly.ServiceInterface.ProductModel;

namespace ServiceStack.Succinctly.Host.Services
{
    [Authenticate]
    public class ProductService : ServiceStack.ServiceInterface.Service
    {
        [RequiredRole(RoleNames.Admin)]
        public ProductResponse Get(GetProduct request)
        {
            return new ProductResponse
                {
                    Id = 1,
                    Name = "Product",
                    Status = new Status { Id = 1, Name = "Active" }
                };
        }

        //only authenticated users are allowed to execute this method
        [RequiredPermission("Some_permission")]
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