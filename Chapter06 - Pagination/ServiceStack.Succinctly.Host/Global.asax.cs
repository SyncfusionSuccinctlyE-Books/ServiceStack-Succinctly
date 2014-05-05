using System;
using System.Web;
using Funq;
using OrderManagement.DataAccessLayer;
using ServiceStack.ServiceHost;
using ServiceStack.Succinctly.Host.Extensions;
using ServiceStack.Succinctly.Host.Mappers;
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
                : base("Order Management", typeof (ServiceAppHost).Assembly)
            {
                Routes
                    //Products
                    .Add<GetProducts>("/products", "GET", "Returns Products");
            }

            public override void Configure(Container container)
            {
                //Products
                container.Register<IProductRepository>(new ProductRepository());
                container.Register<IProductMapper>(new ProductMapper());
                container.Register<IStatusRepository>(new StatusRepository());
            }
        }
    }
}

