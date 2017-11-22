using System;
using System.Linq;
using System.Runtime.Caching;

namespace Dbgo.Core.Caching
{
    public partial class MemoryCacheManager : ICacheManager
    {

        protected ObjectCache Cache
        {
            get
            {
                return MemoryCache.Default;
            }
        }
        public void Clear()
        {
            foreach (var item in Cache)
            {
                Remove(item.Key);
            }
        }

        public void Dispose()
        {
        }

        public T Get<T>(string key)
        {
            return (T)Cache[key];
        }

        public bool IsSet(string key)
        {
            return (Cache.Contains(key));
        }

        public void Remove(string key)
        {
            Cache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            //this.RemoveByPattern(pattern, Cache.Select(p => p.Key));
        }

        public void Set(string key, object data, int cacheTime)
        {
            if (data == null)
                return;

            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime);
            Cache.Add(new CacheItem(key, data), policy);
        }
    }
}
