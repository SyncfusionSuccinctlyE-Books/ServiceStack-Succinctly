using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack.Common.Web;
using ServiceStack.ServiceClient.Web;
using ServiceStack.Succinctly.ServiceInterface;
using ServiceStack.Succinctly.ServiceInterface.ProductModel;

namespace ServiceStack.Succinctly.Host.IntegrationTest
{
    [TestClass]
    public class ProductServiceIntegrationTest
    {
        [TestMethod]
        public void GetProductByProductId_ReturnsValidProduct()
        {
            //ARRANGE --- 
            int PRODUCT_ID = 1;
            var client = new JsonServiceClient("http://localhost:50712/");

            //ACT  ------ 
            var product = client.Get<ProductResponse>("/products/" + PRODUCT_ID);

            //ASSERT ---- 
            Assert.IsTrue(product != null);
            Assert.IsTrue(product.Id == PRODUCT_ID);
        }

        [TestMethod]
        public void GetAllProducts_ReturnsValidProductList()
        {
            //ARRANGE --- 
            var client = new JsonServiceClient("http://localhost:50712/");

            //ACT  ------ 
            var products = client.Get<ProductsResponse>("/products");

            //ASSERT ---- 
            Assert.IsTrue(products != null);
            Assert.IsTrue(products.Products.Count > 0);
        }

        [TestMethod]
        public void CreateNewProduct_ReturnsObjectAnd201CreatedStatus()
        {
            //ARRANGE --- 
            WebHeaderCollection headers = null;
            HttpStatusCode statusCode = 0;
            const string PRODUCT_NAME = "Cappuccino";
            const string SITE = "http://localhost:50712";
            const string PRODUCTS = "/products";
            const string URI = SITE + PRODUCTS;

            var client = new JsonServiceClient(SITE)
            {
                //grabbing the header once the call is ended.
                LocalHttpWebResponseFilter =
                    httpRes =>
                    {
                        headers = httpRes.Headers;
                        statusCode = httpRes.StatusCode;
                    }
            };

            var newProduct = new CreateProduct
            {
                Name = PRODUCT_NAME,
                Status = new Status { Id = 1 }
            };

            //ACT  ------ 
            var product = client.Post<ProductResponse>(PRODUCTS, newProduct);

            //ASSERT ---- 
            Assert.IsTrue(headers["Location"] == URI + "/" + product.Id);
            Assert.IsTrue(statusCode == HttpStatusCode.Created);
            Assert.IsTrue(product.Name == PRODUCT_NAME);
        }

        [TestMethod]
        public void UpdateProduct_ReturnsUpdatedObject()
        {
            //ARRANGE --- 
            HttpStatusCode statusCode = 0;
            const string PRODUCT_NAME = "White Wine";
            const string SITE = "http://localhost:50712";
            const string PRODUCT_LINK = "/products/2";

            var client = new JsonServiceClient(SITE)
            {
                //grabbing the header once the call is ended.
                LocalHttpWebResponseFilter =
                    httpRes =>
                    {
                        statusCode = httpRes.StatusCode;
                    }
            };

            var updateProduct = new UpdateProduct
            {
                Name = PRODUCT_NAME,
                Status = new Status { Id = 2 } // Id = 2 means inactive.
            };

            //ACT  ------ 
            var product = client.Put<ProductResponse>(PRODUCT_LINK, updateProduct);

            //ASSERT ---- 
            Assert.IsTrue(statusCode == HttpStatusCode.OK);
            Assert.IsTrue(product.Name == PRODUCT_NAME);
            Assert.IsTrue(product.Status.Id == 2);
        }

        [TestMethod]
        public void DeleteProduct_ReturnsNoContent()
        {
            //ARRANGE --- 
            HttpStatusCode statusCode = 0;
            const string SITE = "http://localhost:50712";

            var client = new JsonServiceClient(SITE)
            {
                //grabbing the header once the call is ended.
                LocalHttpWebResponseFilter =
                    httpRes =>
                    {
                        statusCode = httpRes.StatusCode;
                    }
            };

            //ACT  ------ 
            client.Delete<HttpResult>("/products/5");

            //ASSERT ---- 
            Assert.IsTrue(statusCode == HttpStatusCode.NoContent);
        }
    }
}
