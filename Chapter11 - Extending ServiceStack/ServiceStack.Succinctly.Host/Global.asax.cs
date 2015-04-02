using System;
using System.Web;
using Funq;
using ServiceStack.Succinctly.Host.Extensions;
using ServiceStack.Succinctly.Host.Plugins;
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

        public class ServiceAppHost : AppHostBase
        {
            public ServiceAppHost()
                : base("Order Management", typeof (ServiceAppHost).Assembly)
            {
                RoutesDefinition();
            }

            public override void Configure(Container container)
            {
				Plugins.Add(new RegisteredPluginsFeature());
            }

            private void RoutesDefinition()
            {
                Routes
                    //Products
                    .Add<GetProducts>("/products", "GET", "Returns Products")
                    .Add<GetProduct>("/products/{Id}", "GET", "Returns a Product");
            }
        }
    }
}

