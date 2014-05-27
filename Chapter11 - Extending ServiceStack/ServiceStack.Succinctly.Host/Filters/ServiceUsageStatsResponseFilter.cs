using ServiceStack.CacheAccess;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace ServiceStack.Succinctly.Host.Filters
{
    public class ServiceUsageStatsResponseFilter : ResponseFilterAttribute
    {
        public ICacheClient Cache { get; set; }

        public string ServiceName { get; set; }
        
        public override void Execute(IHttpRequest req, IHttpResponse res, object responseDto)
        {
            string successKey = ServiceName + "_SUCCESSFUL";
            if (res.StatusCode >= 200 && res.StatusCode < 300)
            {
                var item = Cache.Get<long>(successKey);
                Cache.Set(successKey, item + 1);
            }
        }
    }
}