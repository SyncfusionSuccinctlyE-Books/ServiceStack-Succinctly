using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using ServiceStack.ServiceHost;

namespace ServiceStack.Succinctly.Host.Extensions
{
    /// <summary>
    /// Definition of shortcut methods 
    /// </summary>
public static class RoutesExtensions
{
    public static IServiceRoutes Add<T>(this IServiceRoutes routes, string restPath, string verbs, string summary)
    {
        return routes.Add(typeof (T), restPath, verbs, summary, "");
    }

    public static IServiceRoutes Add<T>(this IServiceRoutes routes, string restPath, string verbs, string summary, string notes)
    {
        return routes.Add(typeof(T), restPath, verbs, summary, notes);
    }
}
}