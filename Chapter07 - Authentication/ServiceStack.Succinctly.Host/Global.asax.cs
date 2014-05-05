using System;
using System.Collections.Generic;
using System.Web;
using Funq;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
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
                //1. Registering the Authorization Provider
                Plugins.Add(new AuthFeature(() => new AuthUserSession(),
                                            new IAuthProvider[]
                                                {
                                                    new BasicAuthProvider()
                                                }));

                //2. Enabling the /register service
                Plugins.Add(new RegistrationFeature());

                //3. configuring the Repository that uses SQL Server backend                 
                var connString = "Data Source=;Initial Catalog=;User ID=;password=";

                var factory = new OrmLiteConnectionFactory(
                    connString,
                    SqlServerOrmLiteDialectProvider.Instance);

                var ormLiteRepository = new OrmLiteAuthRepository(factory);

                //registering the repository
                container.Register<IUserAuthRepository>(ormLiteRepository);

                //should be run only once as this creates a necessary tables in the db
                //there is a possibility to DropAndRecreate the entire table.
                ormLiteRepository.CreateMissingTables();
                //ormLiteRepository.DropAndReCreateTables();

                //just for this example, we create in code a new user
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

                //4. Registering the Session Cache
                container.Register<ICacheClient>(new MemoryCacheClient());
            }

            private void RoutesDefinition()
            {
                Routes
                    .Add<GetProduct>("/products/{Id}", "GET", "Returns a Product")
                    .Add<GetProducts>("/products", "GET");
            }
        }
    }
}