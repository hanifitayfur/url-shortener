using System;
using System.Collections.Generic;

namespace ltunes.Core.Cache.Abstraction
{
    public interface ICacheManager : IDisposable
    {
        T Get<T>(string key);
        List<T> GetList<T>(string key);
        bool IsSet(string key);
        bool Set<T>(string key, T data, int cacheTimeMinute);
        bool Remove(string key);
        void Clear();
    }
}