using System;
using System.Linq;
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
        public IProductRepository ProductRepository { get; set; }
        public IProductMapper ProductMapper {get;set;}

        public ProductsResponse Get(GetProducts request)
        {
            ProductsResponse response;
            int? page = request.Page;
            int? size = request.Size;

            if (page.HasValue && size.HasValue)
            {
                //get data from the database
                PagedListResult<Product> products =
                                  ProductRepository.GetPaged(page.Value, size.Value);

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

    }
}