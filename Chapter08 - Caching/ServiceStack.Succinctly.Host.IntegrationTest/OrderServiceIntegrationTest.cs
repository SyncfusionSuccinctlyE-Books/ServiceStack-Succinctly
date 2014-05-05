using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using ServiceStack.Common.Web;
using ServiceStack.ServiceClient.Web;
using ServiceStack.Succinctly.ServiceInterface;
using ServiceStack.Succinctly.ServiceInterface.OrderModel;
using ServiceStack.Succinctly.ServiceInterface.ProductModel;

namespace ServiceStack.Succinctly.Host.IntegrationTest
{
    [TestClass]
    public class OrderServiceIntegrationTest
    {
        [TestMethod]
        public void GetOrdersByOrderId()
        {
            //ARRANGE --- 
            const int ORDER_ID = 1;
            var client = new XmlServiceClient("http://localhost:50712/");

            //ACT  ------ 
            var order = client.Get<OrderResponse>("/orders/" + ORDER_ID);

            //ASSERT ---- 
            Assert.IsTrue(order != null);
            Assert.IsTrue(order.Id == ORDER_ID);
        }

        [TestMethod]
        public void GetAllOrders()
        {
            //ARRANGE --- 
            var client = new XmlServiceClient("http://localhost:50712/");

            //ACT  ------ 
            var orders = client.Get<OrdersResponse>("/orders");

            //ASSERT ---- 
            Assert.IsTrue(orders != null);
            Assert.IsTrue(orders.Orders.Count > 0);
        }

        [TestMethod]
        public void CreateNewOrder()
        {
            //ARRANGE --- 
            WebHeaderCollection headers = null;
            HttpStatusCode statusCode = 0;
            const string SITE = "http://localhost:50712";
            const string ORDERS = "/orders";
            const string URI = SITE + ORDERS;

            var client = new XmlServiceClient(SITE)
                {
                    //grabbing the header once the call is ended.
                    LocalHttpWebResponseFilter =
                        httpRes =>
                        {
                            headers = httpRes.Headers;
                            statusCode = httpRes.StatusCode;
                        }
                };
            var newOrder = new Order
                {
                    CreationDate = DateTime.Now,
                    IsTakeAway = true,
                    Status = new Status { Id = 1 }, //active
                    Items = new List<OrderItem>
                {
                    new OrderItem {Product = new Product {Id = 1}, Quantity = 10},
                    new OrderItem {Product = new Product {Id = 2}, Quantity = 10}
                }
                };

            //ACT  ------ 
            var order = client.Post<OrderResponse>(ORDERS, newOrder);

            //ASSERT ---- 

            Assert.IsTrue(headers["Location"] == URI + "/" + order.Id);
            Assert.IsTrue(statusCode == HttpStatusCode.Created);
            Assert.IsTrue(order.Items.Count == 2);
            Assert.IsTrue(order.Status.Id == 1); //status is active.
        }

        [TestMethod]
        public void UpdateOrder()
        {
            //ARRANGE --- 
            HttpStatusCode statusCode = 0;
            const string SITE = "http://localhost:50712";
            const string ORDERS_LINK = "/orders/1";
            const string URI = SITE + ORDERS_LINK;
            const int NEW_PRODUCT_ID = 5;
            DateTime NEW_CREATION_DATE = new DateTime(2013, 08, 08);

            var client = new XmlServiceClient(SITE)
            {
                //grabbing the header once the call is ended.
                LocalHttpWebResponseFilter =
                    httpRes =>
                    {
                        statusCode = httpRes.StatusCode;
                    }
            };

            var updateOrder = new Order
                {
                    CreationDate = NEW_CREATION_DATE,
                    IsTakeAway = false,
                    Items = new List<OrderItem>()
                {
                    new OrderItem
                        {
                            Id = 6,
                            //setting a different product!
                            Product = new Product {Id = NEW_PRODUCT_ID}, 
                            //setting a different quantity.
                            Quantity = 100
                        }
                },
                    Status = new Status { Id = 1 }
                };

            //ACT  ------ 
            var orderResponse = client.Put<OrderResponse>(ORDERS_LINK, updateOrder);

            //ASSERT ---- 
            Assert.IsTrue(statusCode == HttpStatusCode.OK);
            Assert.IsTrue(orderResponse.CreationDate == NEW_CREATION_DATE);
            Assert.IsTrue(orderResponse.IsTakeAway == false);
            //only one item
            Assert.IsTrue(orderResponse.Items.Count == 1);
            Assert.IsTrue(orderResponse.Items[0].Product.Id == NEW_PRODUCT_ID);
        }

        [TestMethod]
        public void DeleteOrder()
        {
            //ARRANGE --- 
            HttpStatusCode statusCode = 0;
            const string SITE = "http://localhost:50712";

            var client = new XmlServiceClient(SITE)
            {
                //grabbing the header once the call is ended.
                LocalHttpWebResponseFilter =
                    httpRes =>
                    {
                        statusCode = httpRes.StatusCode;
                    }
            };

            //ACT  ------ 
            client.Delete<HttpResult>("/orders/2");

            //ASSERT ---- 
            Assert.IsTrue(statusCode == HttpStatusCode.NoContent);
        }
    }
}
