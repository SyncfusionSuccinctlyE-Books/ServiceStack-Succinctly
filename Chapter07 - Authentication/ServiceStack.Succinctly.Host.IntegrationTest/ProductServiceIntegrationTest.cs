using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using ServiceStack.ServiceClient.Web;
using ServiceStack.Succinctly.ServiceInterface.ProductModel;


namespace ServiceStack.Succinctly.Host.IntegrationTest
{
    [TestClass]
    public class ProductServiceIntegrationTest
    {

        [TestMethod]
        public void GetProductAndAuthenticateByUsingServiceStackClient()
        {
            //ARRANGE ---
            var client = new JsonServiceClient("http://localhost:50712/");
            client.UserName = "johnd";
            client.Password = "mypassword";

            //ACT  ------ 
            var product = client.Get<ProductResponse>("/products/1");

            //ASSERT ---- 
            Assert.IsTrue(product != null);
        }

        [TestMethod]
        public void GetProductAndAuthenticateByUsingRestClient()
        {
            var client = new RestClient("http://localhost:50712");
            client.Authenticator = new HttpBasicAuthenticator("johnd", "mypassword");

            var request = new RestRequest("products/1", Method.GET);

            var productResponse = client.Get<ProductResponse>(request);

            Assert.IsTrue(productResponse != null);
            Assert.IsTrue(productResponse.Data.Id == 1);
        }

        [TestMethod]
        public void RegisterNewUser()
        {
            var client = new RestClient("http://localhost:50712");
            client.Authenticator = new HttpBasicAuthenticator("johnd", "mypassword");

            var request = new RestRequest("register", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new
                {
                    UserName = "JaneR",
                    FirstName = "Jane",
                    LastName = "Roe",
                    DisplayName = "Jane Roe",
                    Email = "jane.roe@email.com",
                    Password = "somepassword",
                    AutoLogin = true
                });
            var response = client.Post<UserResponse>(request);

            Assert.IsTrue(response != null);
            Assert.IsTrue(response.Data.UserId != null);
        }

        [TestMethod]
        public void AssignRoles()
        {
            var client = new RestClient("http://localhost:50712");
            client.Authenticator = new HttpBasicAuthenticator("johnd", "mypassword");

            var request = new RestRequest("assignroles", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new
                {
                    UserName = "JaneR",
                    Permissions = "some_permissions",
                    Roles = "Admin, Reader"
                });

            var response = client.Post<AssignRoleResponse>(request);

            Assert.IsTrue(response != null);
        }
    }

    public class UserResponse
    {
        public string UserId { get; set; }
        public string SessionId { get; set; }
        public string UserName { get; set; }
        public object ResponseStatus { get; set; }
    }

    public class AssignRoleResponse
    {
        public string AllRoles { get; set; }
        public string AllPermissions { get; set; }
        public object ResponseStatus { get; set; }
    }
}