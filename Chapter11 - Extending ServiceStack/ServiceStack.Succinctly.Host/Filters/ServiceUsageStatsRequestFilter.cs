using ServiceStack.CacheAccess;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace ServiceStack.Succinctly.Host.Filters
{
    public class ServiceUsageStatsRequestFilter : RequestFilterAttribute
    {
        public string ServiceName { get; set; }
        //This property will be resolved by the IoC container
        public ICacheClient Cache { get; set; }

        public override void Execute(IHttpRequest req, IHttpResponse res, object requestDto)
        {
            string getKey = ServiceName + "_GET";

            if (req.HttpMethod == "GET")
            {
                var item = Cache.Get<long>(getKey);
                Cache.Set(getKey, (item + 1));
            }
        }
    }
}