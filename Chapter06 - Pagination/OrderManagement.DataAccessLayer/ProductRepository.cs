using System;
using System.Collections.Generic;
using System.Linq;
using OrderManagement.Core.Domain;

namespace OrderManagement.DataAccessLayer
{
    public interface IProductRepository
    {
        PagedListResult<Product> GetPaged(int page, int size);
        Product GetById(int id);
        List<Product> GetAll();
        Product Add(Product order);
        void Delete(List<Product> products);
        void Delete(int productId);
        void Delete();
        Product Update(Product order);
    }

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
                Statuses.Add(new Status { Id = 1, Name = "Active" });
                Statuses.Add(new Status { Id = 2, Name = "Inactive" });

                for (int i = 1; i < 1000; i++)
                {
                    Products.Add(new Product()
                        {
                            Id = i,
                            Version = 1,
                            Status = Statuses[0],
                            Name = "Product " + i
                        });
                }
            }
        }

        public PagedListResult<Product> GetPaged(int page, int size)
        {
            var skip = (page == 0 || page == 1) ? 0 : (page - 1) * size;
            var take = size;

            IQueryable<Product> sequence = Products.AsQueryable();

            var resultCount = sequence.Count();

            //getting the result
            var result = (take > 0)
                                ? (sequence.Skip(skip).Take(take).ToList())
                                : (sequence.ToList());

            // setting up the return object.
            bool hasNext = (skip > 0 || take > 0)
                            && (skip + take < resultCount);

            return new PagedListResult<Product>()
            {
                Page = page,
                Size = size,
                Entities = result,
                HasNext = hasNext,
                HasPrevious = (skip > 0),
                Count = resultCount
            };
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