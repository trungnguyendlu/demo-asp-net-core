using Demo.Util;
using System;
using System.Threading.Tasks;

namespace Demo.Util
{
    public interface ICache
    {
        TimeSpan CacheExpired { get; }

        T GetOrSet<T>(string key, T value, TimeSpan? timeExpired = null);

        T GetOrSet<T>(string key, Func<T> execSet = null, TimeSpan? timeExpired = null);

        Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> execSet = null, TimeSpan? timeExpired = null);

        void Remove(string key);

        string FormatKey(CacheKey key, params object[] extendKeys);
    }
}
