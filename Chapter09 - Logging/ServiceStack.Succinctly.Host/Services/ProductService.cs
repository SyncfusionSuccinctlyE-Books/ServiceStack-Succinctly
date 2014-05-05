using ServiceStack.Logging;
using ServiceStack.Succinctly.ServiceInterface.ProductModel;

namespace ServiceStack.Succinctly.Host.Services
{
    public class ProductService : ServiceStack.ServiceInterface.Service
    {
        public ILog Logger { get; set; }

        public ProductResponse Get(GetProduct request)
        {
            Logger.Debug("Getting the product");

            return new ProductResponse();
        }
    }
}