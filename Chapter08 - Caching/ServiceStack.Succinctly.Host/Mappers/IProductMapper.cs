using System.Collections.Generic;
using OrderManagement.Core.Domain;
using OrderManagement.DataAccessLayer;
using ServiceStack.Succinctly.ServiceInterface.ProductModel;

namespace ServiceStack.Succinctly.Host.Mappers
{
    public interface IProductMapper
    {
        Product ToProduct(CreateProduct request);
        Product ToProduct(UpdateProduct request);
        ProductResponse ToProductResponse(Product product);
        List<ProductResponse> ToProductResponseList(List<Product> products);
        ProductsResponse ToProductsResponse(PagedListResult<Product> products);
    }
}