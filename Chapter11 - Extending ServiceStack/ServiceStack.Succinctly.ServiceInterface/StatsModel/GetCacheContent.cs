using ServiceStack.ServiceHost;

namespace ServiceStack.Succinctly.ServiceInterface.StatsModel
{
    [Route("/Cache/{ServiceName}", "GET")]    
    public class GetCacheContent
    {
        public string ServiceName { get; set; }
    }
}
