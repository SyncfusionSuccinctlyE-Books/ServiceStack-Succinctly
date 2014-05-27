using ServiceStack.WebHost.Endpoints;

namespace ServiceStack.Succinctly.Host.Plugins
{
    public class RegisteredPluginsFeature : IPlugin
    {
        public void Register(IAppHost appHost)
        {
            appHost.Routes.Add(typeof (RegisteredPluginsDTO), "/ListPlugins", "GET");
            appHost.RegisterService(typeof (RegisteredPluginsService), "/ListPlugins");
        }
    }
}