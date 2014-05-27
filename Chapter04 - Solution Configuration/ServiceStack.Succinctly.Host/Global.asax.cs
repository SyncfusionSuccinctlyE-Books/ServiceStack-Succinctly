using System;
using System.Web;
using Funq;
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
            public ServiceAppHost() : base("Order Management", typeof(ServiceAppHost).Assembly)
            {                
            }

            public override void Configure(Container container)
            {
 
            }
        }
    }
}

