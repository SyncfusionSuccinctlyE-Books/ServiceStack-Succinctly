using System;
using System.Web;
using Funq;
using ServiceStack.Logging;
using ServiceStack.Logging.Log4Net;
using ServiceStack.ServiceInterface.Admin;
using ServiceStack.ServiceInterface.Providers;
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
                LogManager.LogFactory = new Log4NetFactory(true);
                container.Register<ILog>(LogManager.GetLogger(""));

                //Built-in request/response logging.
                Plugins.Add(new RequestLogsFeature()
                {
                    RequiredRoles = new string[] { },
                    EnableRequestBodyTracking = true,
                    EnableResponseTracking = true,
                    EnableErrorTracking = true,
                    RequestLogger = new InMemoryRollingRequestLogger(),                    
                    EnableSessionTracking = true,
                });
            }


            private void RoutesDefinition()
            {
                Routes
                    .Add<GetProduct>("/products/{Id}", "GET")
                    .Add<GetProducts>("/products", "GET");
            }
        }
    }
}