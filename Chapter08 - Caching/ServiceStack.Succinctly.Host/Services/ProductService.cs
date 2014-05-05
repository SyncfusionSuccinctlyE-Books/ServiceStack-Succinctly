using System;
using System.Linq;
using System.Net;
using OrderManagement.Core.Domain;
using OrderManagement.DataAccessLayer;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.Succinctly.Host.Mappers;
using ServiceStack.Succinctly.ServiceInterface.ProductModel;

namespace ServiceStack.Succinctly.Host.Services
{
    public class ProductService : ServiceStack.ServiceInterface.Service
    {
        public IProductMapper ProductMapper { get; set; }
        public IProductRepository ProductRepository { get; set; }

        public object Get(GetProduct request)
        {
            var key = UrnId.Create<GetProduct>(request.Id.ToString());

            var cachedProduct = RequestContext.ToOptimizedResultUsingCache(this.Cache, key, new TimeSpan(0, 1, 0), () =>
                {
                    var product = ProductRepository.GetById(request.Id);
                    if (product == null)
                    {
                        Response.StatusCode = (int)HttpStatusCode.NotFound;
                        return default(ProductResponse);
                    }
                    //transform to ProductsResponse and return.
                    return ProductMapper.ToProductResponse(product);
                });
            return cachedProduct;
        }

        //returns all the Products
        public ProductsResponse Get(GetProducts request)
        {
            ProductsResponse response;
            int? page = request.Page;
            int? size = request.Size;

            if (page.HasValue && size.HasValue)
            {
                //get data from the database
                PagedListResult<Product> products = ProductRepository.GetPaged(page.Value, size.Value);

                response = ProductMapper.ToProductsResponse(products);
            }
            else
            {
                var products = ProductRepository.GetAll();
                response = new ProductsResponse()
                    {
                        Products = ProductMapper.ToProductResponseList(products)
                    };
            }
            return response;
        }

        //creates a new product.
        public ProductResponse Post(CreateProduct request)
        {
            //transform the request to Domain.Product
            var domainProduct = ProductMapper.ToProduct(request);

            //storing data to databasd.
            var newProduct = ProductRepository.Add(domainProduct);

            //transform to ProductResponse
            var response = ProductMapper.ToProductResponse(newProduct);

            //manipulate the header and StatusCode.
            Response.AddHeader("Location", Request.AbsoluteUri + "/" + newProduct.Id);
            Response.StatusCode = (int)HttpStatusCode.Created;

            return response;
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

        //updates a product
        public ProductResponse Put(UpdateProduct request)
        {
            var key = UrnId.Create<GetProduct>(request.Id.ToString());
            base.RequestContext.RemoveFromCache(base.Cache, key);

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
    }
}