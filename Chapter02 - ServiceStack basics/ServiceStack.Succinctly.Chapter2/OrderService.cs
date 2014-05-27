using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace ServiceStack.Succinctly.Chapter2
{
    public class OrderService : ServiceStack.ServiceInterface.Service
    {
        public IOrderRepository OrderRepository { get; set; }
        private IProductRepository _productRepository { get; set; }

        public OrderService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [AddHeader(ContentType = ContentType.Json)]
        public object Get(GetOrdersRequest request)
        {
            //value = json
            var pageQueryStringValue = this.Request.QueryString["page"];

            //value = application/json
            var acceptHeaderValue = this.Request.Headers["Accept"];

            //value = GET
            var httpMethod = this.Request.HttpMethod;

            //Setting up the response.
            this.Response.ContentType = ContentType.Json;
            this.Response.AddHeader("Location", "http://<servername>/orders");
            return new HttpResult{StatusCode = HttpStatusCode.OK};

        }
    }

    [Route("/orders", "GET")]
    [Route("/orders/{Id}", "GET")]
    public class GetOrdersRequest
    {
        public int Id { get; set; }
    }

    public interface IOrderRepository
    {
        
    }
    public class OrderRepository : IOrderRepository
    {
        
    }

    public interface IProductRepository
    {

    }

    public class ProductRepository : IProductRepository
    {

    }
}