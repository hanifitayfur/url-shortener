using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using ltunes.Core.Cache.Abstraction;

namespace lTunes.Core.Cache.Memory
{
    public sealed class MemoryCacheManager : ICacheManager
    {
        public MemoryCacheManager(string prefix = "")
        {
            Prefix = prefix;
        }

        private static ObjectCache Cache => MemoryCache.Default;
        private static readonly List<string> Keys = new();
        public string Prefix { get; set; }


        public bool IsSet(string key)
        {
            return (Cache.Contains(key));
        }

        public bool Remove(string key)
        {
            Cache.Remove(key);
            Keys.Remove(key);
            return true;
        }

        public void Clear()
        {
            foreach (var item in Cache)
                Remove(item.Key);
        }

        public void Dispose()
        {
            var memoryCache = (MemoryCache)Cache;
            memoryCache.Dispose();
        }

        public T Get<T>(string key)
        {
            return (T)Cache[key];
        }

        public List<T> GetList<T>(string key)
        {
            return (List<T>)Cache[key];
        }

        public bool Set<T>(string key, T data, int cacheTimeMinute)
        {
            if (data == null)
                return false;
            Keys.Add(key);
            var cacheItemPolicy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTimeMinute)
            };
            return Cache.Add(new CacheItem(key, data), cacheItemPolicy);
        }
    }
}