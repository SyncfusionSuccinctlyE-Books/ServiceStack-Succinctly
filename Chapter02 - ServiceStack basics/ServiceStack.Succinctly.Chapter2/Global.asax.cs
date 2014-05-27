using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Funq;
using ServiceStack.Common.Web;
using ServiceStack.FluentValidation;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Validation;
using ServiceStack.WebHost.Endpoints;

namespace ServiceStack.Succinctly.Chapter2
{
    public class Global : HttpApplication
    {
        public class ServiceAppHost : AppHostBase
        {
            public ServiceAppHost()
                : base("Order Management", typeof (ServiceAppHost).Assembly)
            {
                Config.AllowRouteContentTypeExtensions = false;
                Config.DefaultContentType = ContentType.Json;

                Plugins.Add(new ValidationFeature());
                Container.RegisterValidator(typeof(GetOrderValidator)); 
            }

            public override void Configure(Container container)
            {
                container.Register<IOrderRepository>(new OrderRepository());
                container.Register<IProductRepository>(new ProductRepository());
            }
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            new ServiceAppHost().Init();
        }
    }

    public class GetOrderValidator : AbstractValidator<GetOrdersRequest>
    {
        public GetOrderValidator()
    {
        //Validation rules for GET request
        RuleSet(ApplyTo.Get |  ApplyTo.Post, () =>
            {
                RuleFor(x => x.Id)
                   .GreaterThan(2)   
                   .WithMessage("OrderID has to be greater than 2");
            });
    }
    }


}