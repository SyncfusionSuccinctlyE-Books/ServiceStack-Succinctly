using System;
using System.Web;
using Funq;
using OrderManagement.DataAccessLayer;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.AwsDynamoDb;
using ServiceStack.CacheAccess.Azure;
using ServiceStack.CacheAccess.Memcached;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.Redis;
using ServiceStack.ServiceHost;
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

        public class ServiceAppHost : AppHostBase
        {
            public ServiceAppHost()
                : base("Order Management", typeof(ServiceAppHost).Assembly)
            {
                RoutesDefinition();

                Plugins.Add(new ValidationFeature());
            }

            public override void Configure(Container container)
            {
                //MemoryCacheClient
                container.Register<ICacheClient>(new MemoryCacheClient());

                ////Redis
                //container.Register<IRedisClientsManager>(c => new PooledRedisClientManager("<redis_address_here>"));
                //container.Register(c => c.Resolve<IRedisClientsManager>().GetCacheClient()).ReusedWithin(ReuseScope.None);

                ////MemCached
                //container.Register<ICacheClient>(new MemcachedClientCache(new[] {"<memcached_host>"}));

                ////Azure
                //container.Register<ICacheClient>(new AzureCacheClient("CacheName")); 

                ////AWS Dynamo Db
                //container.Register<ICacheClient>(new DynamoDbCacheClient(...))

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

            private void RoutesDefinition()
            {
                Routes
                    //Products
                    .Add<GetProducts>("/products", "GET", "Returns Products")
                    .Add<GetProduct>("/products/{Id}", "GET", "Returns a Product")
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
            }
        }
    }
}

