using System;
using System.Collections.Generic;
using AutoMapper;
using ServiceStack.Text;
using OrderManagement.Core.Domain;
using SrvObjType = ServiceStack.Succinctly.ServiceInterface;
using SrvObj = ServiceStack.Succinctly.ServiceInterface.ProductModel;

namespace ServiceStack.Succinctly.Host.Mappers
{
    public class ProductMapper : IProductMapper
    {
        static ProductMapper()
        {
            Mapper.CreateMap<SrvObjType.Status, Status>();
            Mapper.CreateMap<Status, SrvObjType.Status>();
            Mapper.CreateMap<SrvObj.CreateProduct, Product>();
            Mapper.CreateMap<SrvObj.UpdateProduct, Product>();
            Mapper.CreateMap<Product, SrvObj.ProductResponse>();
        }

        public Product ToProduct(SrvObj.CreateProduct request)
        {
            return Mapper.Map<Product>(request);
        }

        public Product ToProduct(SrvObj.UpdateProduct request)
        {
            return Mapper.Map<Product>(request);
        }

        public SrvObj.ProductResponse ToProductResponse(Product product)
        {
            var productResponse = Mapper.Map<SrvObj.ProductResponse>(product);

            productResponse.Links = new List<SrvObjType.Link>
                {
                    new SrvObjType.Link
                        {
                            Title = "self",
                            Rel = "self",
                            Href = "products/{0}".Fmt(product.Id),
                        }
                };
            return productResponse;
        }

        //Transforms a list of products into a list of ProductResponse
        public List<SrvObj.ProductResponse> ToProductResponseList(List<Product> products)
        {
            var productResponseList = new List<SrvObj.ProductResponse>();
            products.ForEach(x => productResponseList.Add(ToProductResponse(x)));
            return productResponseList;
        }
    }
}