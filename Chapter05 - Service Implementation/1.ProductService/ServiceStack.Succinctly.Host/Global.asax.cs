using System;
using System.Web;
using Funq;
using OrderManagement.DataAccessLayer;
using ServiceStack.ServiceInterface.Validation;
using ServiceStack.Succinctly.Host.Extensions;
using ServiceStack.Succinctly.Host.Mappers;
using ServiceStack.Succinctly.Host.Validation;
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
                Routes
                    .Add<GetProducts>("/products", "GET", "Returns a collection of Products")
                    .Add<GetProduct>("/products/{Id}", "GET", "Returns a single Product")
                    .Add<CreateProduct>("/products", "POST", "Create a product")
                    .Add<UpdateProduct>("/products/{Id}", "PUT", "Update a product")
                    .Add<DeleteProduct>("/products/{Id}", "DELETE", "Deletes a product")
                    .Add<DeleteProduct>("/products", "DELETE", "Deletes all products");

                Plugins.Add(new ValidationFeature());
            }

            public override void Configure(Container container)
            {
                container.Register<IProductRepository>(new ProductRepository());
                container.Register<IProductMapper>(new ProductMapper());

                container.RegisterValidator(typeof (CreateProductValidator));
                container.RegisterValidator(typeof (UpdateProductValidator));
            }
        }
    }
}

