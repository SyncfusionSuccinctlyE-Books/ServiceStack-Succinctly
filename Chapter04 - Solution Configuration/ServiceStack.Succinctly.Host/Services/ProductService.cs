 
using OrderManagement.DataAccessLayer;

namespace ServiceStack.Succinctly.Host.Services
{
    public class ProductService : ServiceStack.ServiceInterface.Service
    {
        public IProductRepository ProductRepository { get; set; }

        public ProductService()
        {
            
        }
    }
}