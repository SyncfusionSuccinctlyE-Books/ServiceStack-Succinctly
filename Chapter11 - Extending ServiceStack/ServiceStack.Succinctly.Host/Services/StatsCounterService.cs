using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack.CacheAccess;
using ServiceStack.Succinctly.ServiceInterface.StatsModel;

namespace ServiceStack.Succinctly.Host.Services
{
    public class StatsCounterService : ServiceStack.ServiceInterface.Service
    {
        public ICacheClient Cache { get; set; }

        public object Get(GetCacheContent request)
        {
            var data = Cache.GetAll<long>(new[]
                {
                    request.ServiceName + "_GET",
                    request.ServiceName + "_SUCCESSFUL",
                });

            List<StatsItem> items = new List<StatsItem>();
            data.Keys.ToList().ForEach(x => items.Add(new StatsItem()
                {
                    ServiceName = x,
                    Value = data[x]
                }));
            return items;
        }
    }
}