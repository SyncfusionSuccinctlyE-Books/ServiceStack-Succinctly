using System;
using System.Collections.Generic;
using System.Web;
using Funq;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.MiniProfiler;
using ServiceStack.MiniProfiler.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceInterface.Validation;
using ServiceStack.Succinctly.Host.Extensions;
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

                Plugins.Add(new ValidationFeature());
            }

            public override void Configure(Container container)
            {
                Plugins.Add(new AuthFeature(() => new AuthUserSession(),
                                            new IAuthProvider[]
                                                {
                                                    new BasicAuthProvider()
                                                }));

                Plugins.Add(new RegistrationFeature());

                container.Register<ICacheClient>(new MemoryCacheClient());

                //configuring OrmLiteAuthRepository that uses SQL Server backend
                var connString = "Data Source=;Initial Catalog=;User ID=;password=";

                var factory = new OrmLiteConnectionFactory(connString, SqlServerOrmLiteDialectProvider.Instance)
                    {
                        ConnectionFilter = x => new ProfiledDbConnection(x, Profiler.Current)
                    };

                var ormLiteRepository = new OrmLiteAuthRepository(factory);

                //registering the repository
                container.Register<IUserAuthRepository>(ormLiteRepository);

                //should be run only once as this creates a necessary tables in the db
                ormLiteRepository.CreateMissingTables();
                //ormLiteRepository.DropAndReCreateTables();

                //shoulnd't run here, but for the sake of the example we 
                //create a new user and store it in the repository.
                if (ormLiteRepository.GetUserAuthByUserName("johnd") == null)
                {
                    ormLiteRepository.CreateUserAuth(new UserAuth
                        {
                            UserName = "johnd",
                            FirstName = "John",
                            LastName = "Doe",
                            Roles = new List<string> {RoleNames.Admin}
                        }, "mypassword");
                }
            }


            private void RoutesDefinition()
            {
                Routes
                    .Add<GetProduct>("/products/{Id}", "GET", "Returns a Product")
                    .Add<GetProducts>("/products", "GET");
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (Request.IsLocal)
            {
                Profiler.Start();
            }
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            Profiler.Stop();
        }
    }
}