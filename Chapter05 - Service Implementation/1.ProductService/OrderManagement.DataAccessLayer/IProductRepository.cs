using System.Collections.Generic;
using OrderManagement.Core.Domain;

namespace OrderManagement.DataAccessLayer
{
    public interface IProductRepository
    {
        Product GetById(int id);
        List<Product> GetAll();
        Product Add(Product order);
        void Delete(List<Product> products);
        void Delete(int productId);
        void Delete();
        Product Update(Product order);
    }
}