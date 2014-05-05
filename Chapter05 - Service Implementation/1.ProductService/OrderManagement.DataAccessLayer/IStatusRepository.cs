using OrderManagement.Core.Domain;

namespace OrderManagement.DataAccessLayer
{
    public interface IStatusRepository
    {
        Status GetById(int id); //returns an order given the order id        
    }
}