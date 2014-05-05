using System;
using System.Linq;
using System.Net;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.Succinctly.Host.Filters;
using ServiceStack.Succinctly.ServiceInterface.ProductModel;
namespace ServiceStack.Succinctly.Host.Services
{
    [ServiceUsageStatsRequestFilter(ServiceName = "ProductService", ApplyTo = ApplyTo.Get | ApplyTo.Put, Priority=1)]
    [ServiceUsageStatsResponseFilter(ServiceName = "ProductService", ApplyTo = ApplyTo.All)]
    public class ProductService : ServiceStack.ServiceInterface.Service
    {
        public object Get(GetProduct request)
        {
            return new ProductResponse()
                {
                    Id = 1,
                    Name = "Product"
                };
        }        
    }
}