 
using System.Collections.Generic;
using System.Net;
using OrderManagement.Core.Domain;
using OrderManagement.DataAccessLayer;
using ServiceStack.Common.Web;
using ServiceStack.Succinctly.Host.Mappers;
using ServiceStack.Succinctly.ServiceInterface.ProductModel;

namespace ServiceStack.Succinctly.Host.Services
{
    public class ProductService : ServiceStack.ServiceInterface.Service
    {
        public IProductMapper ProductMapper { get; set; }
        public IProductRepository ProductRepository { get; set; }

        public ProductResponse Get(GetProduct request)
        {
            var product = ProductRepository.GetById(request.Id);
            if (product == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return default(ProductResponse);
            }

            //transform to ProductsResponse and return.
            return ProductMapper.ToProductResponse(product);
        }

        //returns all the Products
        public ProductsResponse Get(GetProducts request)
        {
            //get data from the database
            List<Product> products = ProductRepository.GetAll();

            //transform to ProductsResponse and return.
            return new ProductsResponse()
            {
                Products = ProductMapper.ToProductResponseList(products)
            };
        }

        //returns all the orders
        public ProductResponse Post(CreateProduct request)
        {
            //transform the request to Domain.Product
            var domainProduct = ProductMapper.ToProduct(request);

            //storing data to database.
            var newProduct = ProductRepository.Add(domainProduct);

            //transform to ProductResponse
            var response = ProductMapper.ToProductResponse(newProduct);

            //manipulate the header and StatusCode.
            Response.AddHeader("Location", Request.AbsoluteUri + "/" + newProduct.Id);
            Response.StatusCode = (int)HttpStatusCode.Created;

            return response;
        }

        public ProductResponse Put(UpdateProduct request)
        {
            var domainObject = ProductRepository.GetById(request.Id);
            if (domainObject == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return null;
            }

            //transform to Domain.Product
            var domainProduct = ProductMapper.ToProduct(request);

            //store data to database
            var updatedProduct = ProductRepository.Update(domainProduct);

            //transform to ProductResponse and return 
            return ProductMapper.ToProductResponse(updatedProduct);
        }

        //deletes a product
        public HttpResult Delete(DeleteProduct request)
        {
            var domainObject = ProductRepository.GetById(request.Id);
            if (domainObject == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            else
            {
                ProductRepository.Delete(request.Id);
                Response.StatusCode = (int)HttpStatusCode.NoContent;
            }
            return null;
        }
    }
}