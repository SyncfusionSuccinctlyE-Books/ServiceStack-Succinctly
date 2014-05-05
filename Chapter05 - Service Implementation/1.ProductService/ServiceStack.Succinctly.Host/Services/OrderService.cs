using OrderManagement.DataAccessLayer;

namespace ServiceStack.Succinctly.Host.Services
{
    public class OrderService : ServiceStack.ServiceInterface.Service
    {
        public IOrderRepository OrderRepository { get; set; }

        public OrderService()
        {
            
        }
    }
}