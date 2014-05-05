using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using OrderManagement.Core.Domain;
using OrderManagement.DataAccessLayer;
using ServiceStack.Succinctly.ServiceInterface;
using SrvObjType = ServiceStack.Succinctly.ServiceInterface;
using SrvObj = ServiceStack.Succinctly.ServiceInterface.ProductModel;
using DomainObj = OrderManagement.Core.Domain;
using ServiceStack.Text;

namespace ServiceStack.Succinctly.Host.Mappers
{
    public class ProductMapper : IProductMapper
    {
        static ProductMapper()
        {
            Mapper.CreateMap<SrvObjType.Status, DomainObj.Status>();
            Mapper.CreateMap<DomainObj.Status, SrvObjType.Status>();         
            Mapper.CreateMap<Product, SrvObj.ProductResponse>();
        }

        
        public SrvObj.ProductResponse ToProductResponse(Product product)
        {
            var productResponse = Mapper.Map<SrvObj.ProductResponse>(product);

            productResponse.Links = new List<Link>
                {
                    new Link
                        {
                            Title = "self",
                            Rel = "self",
                            Href = "products/{0}".Fmt(product.Id),
                        }
                };
            return productResponse;
        }

        /// <summary>
        /// Transforms a list of products into a list of ProductResponse
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        public List<SrvObj.ProductResponse> ToProductResponseList(List<Product> products)
        {
            var productResponseList = new List<SrvObj.ProductResponse>();
            products.ForEach(x => productResponseList.Add(ToProductResponse(x)));
            return productResponseList;
        }

        public SrvObj.ProductsResponse ToProductsResponse(PagedListResult<Product> products)
        {
            var productList = ToProductResponseList(products.Entities.ToList());

            var productResponse = new SrvObj.ProductsResponse();
            productResponse.Products = productList;

            SelfLink(products, productResponse);

            NextLink(products, productResponse);

            PreviousLink(products, productResponse);

            FirstLink(products, productResponse);

            LastLink(products, productResponse);

            return productResponse;
        }

        private void SelfLink(PagedListResult<Product> products, SrvObj.ProductsResponse productResponse)
        {
            productResponse
                .Links
                .Add(NewLink("self", "products?page={0}&size={1}"
                                            .Fmt(products.Page, products.Size)));
        }

        private void LastLink(PagedListResult<Product> products, SrvObj.ProductsResponse productResponse)
        {
            var lastPage = products.LastPage();
            productResponse
                .Links
                .Add(NewLink("last", "products?page={0}&size={1}".Fmt(lastPage, products.Size)));
        }

        private void FirstLink(PagedListResult<Product> products, SrvObj.ProductsResponse productResponse)
        {
            productResponse
                .Links
                .Add(NewLink("first", "products?page=1&size={0}".Fmt(products.Size)));
        }

        private void PreviousLink(PagedListResult<Product> products, SrvObj.ProductsResponse productResponse)
        {
            if (products.HasPrevious)
            {
                productResponse
                    .Links
                    .Add(NewLink("previous", "products?page={0}&size={1}"
                                                 .Fmt(products.Page - 1, products.Size)));
            }
        }

        private void NextLink(PagedListResult<Product> products, SrvObj.ProductsResponse productResponse)
        {
            if (products.HasNext)
            {
                productResponse
                    .Links
                    .Add(NewLink("next", "products?page={0}&size={1}"
                                                .Fmt(products.Page + 1, products.Size)));
            }
        }

        private Link NewLink(string name, string uri)
        {
            return new Link
                {
                    Title = name,
                    Rel = name,
                    Href = uri
                };
        }
    }
}