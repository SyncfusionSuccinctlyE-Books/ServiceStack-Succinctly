using System;
using System.Web;
using Funq;
using OrderManagement.DataAccessLayer;
using ServiceStack.Api.Swagger;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceInterface.Validation;
using ServiceStack.Succinctly.Host.Extensions;
using ServiceStack.Succinctly.Host.Mappers;
using ServiceStack.Succinctly.Host.Validation;
using ServiceStack.Succinctly.ServiceInterface.OrderItemModel;
using ServiceStack.Succinctly.ServiceInterface.OrderModel;
using ServiceStack.Succinctly.ServiceInterface.ProductModel;
using ServiceStack.WebHost.Endpoints;

namespace ServiceStack.Succinctly.Host
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            new ServiceAppHost().Init();
        }

        public static class RouteDefinition
        {
            public static void Define(IServiceRoutes routes)
            {
            }
        }

        public class ServiceAppHost : AppHostBase
        {
            public ServiceAppHost()
                : base("Order Management", typeof(ServiceAppHost).Assembly)
            {
                Routes
                    //Products
                    .Add<GetProducts>("/products", "GET", "Returns Products")
                    //.Add<GetProduct>("/products/{Id}", "GET", "Returns a Product")
                    .Add<CreateProduct>("/products", "POST", "Creates a Product")
                    .Add<UpdateProduct>("/products/{Id}", "PUT", "Updates a Product")
                    .Add<DeleteProduct>("/products/{Id}", "DELETE", "Deletes a Product")
                    //Orders
                    .Add<GetOrder>("/orders/{Id}", "GET", "Returns an Order")
                    .Add<GetOrders>("/orders", "GET", "Returns Orders")
                    .Add<Order>("/orders", "POST", "Creates an Order")
                    .Add<Order>("/orders/{Id}", "PUT", "Updates an Order")
                    .Add<DeleteOrder>("/orders/{Id}", "DELETE", "Deletes an Order")
                    //Order Items
                    .Add<GetOrderItem>("/orders/{OrderId}/items/{ItemId}", "GET", "Return single OrderItem")
                    .Add<GetOrderItems>("/orders/{OrderId}/items", "GET", "Return a list of OrderItem");

                Plugins.Add(new ValidationFeature());
                Plugins.Add(new SwaggerFeature());
            }

            public override void Configure(Container container)
            {
                //Products
                container.Register<IProductRepository>(new ProductRepository());
                container.Register<IProductMapper>(new ProductMapper());

                //Validators
                container.RegisterValidator(typeof(CreateProductValidator));
                container.RegisterValidator(typeof(UpdateProductValidator));
                container.RegisterValidator(typeof(OrderValidator));

                //Orders
                container.Register<IOrderRepository>(new OrderRepository());
                container.Register<IOrderMapper>(new OrderMapper());

                container.Register<IStatusRepository>(new StatusRepository());
            }
        }
    }

    public class X509CredentialsAuthProvider : CredentialsAuthProvider
    {
        public override object Authenticate(IServiceBase authService, IAuthSession session, Auth request)
        {
            var httpRequest = authService.RequestContext.Get<IHttpRequest>();
            HttpClientCertificate certificate = ((HttpRequest)httpRequest.OriginalRequest).ClientCertificate;

            if (certificate == null)
            {
                throw HttpError.Unauthorized("Certificate not supplied.");
            }

            string subject = certificate.Subject;
            string issuer = certificate.Issuer;
            return Authenticate(authService, session, subject, issuer, request.Continue);
        }
    }

}

