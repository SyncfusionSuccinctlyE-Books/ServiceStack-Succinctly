using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.Succinctly.ServiceInterface.ProductModel;

namespace ServiceStack.Succinctly.Host.Plugins
{
    public class RegisteredPluginsService : ServiceStack.ServiceInterface.Service
    {
        public object Get(RegisteredPluginsDTO request)
        {
            var plugins = base.GetAppHost().Plugins.ToList();
            var list = plugins.Select(x => new RegisteredPlugin()
            {
                Name = x.GetType().Name

            });

            return list.ToList();
        }
    }
}