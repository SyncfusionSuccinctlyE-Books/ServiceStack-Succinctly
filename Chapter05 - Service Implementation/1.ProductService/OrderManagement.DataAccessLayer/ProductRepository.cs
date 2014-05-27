using System.Collections.Generic;
using System.Linq;
using OrderManagement.Core.Domain;

namespace OrderManagement.DataAccessLayer
{
    /// <summary>
    /// In-memory orders repository, that fakes the database
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private static readonly List<Product> Products = new List<Product>();
        private static readonly List<Status> Statuses = new List<Status>();

        public ProductRepository()
        {
            if (!Products.Any())
            {
                Statuses.Add(new Status {Id = 1, Name = "Active"});
                Statuses.Add(new Status { Id = 2, Name = "Inactive" });

                //Add a list of products.
                Products.Add(new Product() { Id = 1, Version = 1, Status = Statuses[0], Name = "Pizza" });
                Products.Add(new Product() { Id = 2, Version = 1, Status = Statuses[0], Name = "White Wine" });
                Products.Add(new Product() { Id = 3, Version = 1, Status = Statuses[0], Name = "Coffee" });
                Products.Add(new Product() { Id = 4, Version = 1, Status = Statuses[0], Name = "Cake" });
                Products.Add(new Product() { Id = 5, Version = 1, Status = Statuses[0], Name = "Beer" });
                Products.Add(new Product() { Id = 6, Version = 1, Status = Statuses[0], Name = "Pasta" });
            }
        }

        public Product Add(Product product)
        {
            var nextOrderId = GetNextId();
            product.Id = nextOrderId;
            product.Status = Statuses.FirstOrDefault(x => x.Id == product.Status.Id);
            Products.Add(product);
            return product;
        }

        private int GetNextId()
        {
            int max = Products.Max(x => x.Id);
            return max + 1;
        }

        public void Delete(List<Product> products)
        {
            for (int i = 0; i < products.Count; i++)
            {
                Products.Remove(products[i]);
            }
        }

        public void Delete(int productId)
        {
            Product order = Products.FirstOrDefault(x => x.Id == productId);
            Products.Remove(order);
        }

        public Product GetById(int id)
        {
            return Products.FirstOrDefault(x => x.Id == id);
        }

        public List<Product> GetAll()
        {
            return Products.Select(x => x).ToList();
        }

        public Product Update(Product product)
        {
            Product domainProduct = GetById(product.Id);
            domainProduct.Name = product.Name;
            domainProduct.Status = Statuses.FirstOrDefault(x => x.Id == product.Status.Id);
            return domainProduct;
        }

        public void Delete()
        {
            Products.Clear();
        }
    }
}