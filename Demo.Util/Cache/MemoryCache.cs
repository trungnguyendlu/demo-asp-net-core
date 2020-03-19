using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Util
{
    public class MemoryCache : ICache
    {
        private readonly string PrefixKey;
        private readonly IMemoryCache _cache;

        public MemoryCache(IMemoryCache cache, string prefix)
        {
            _cache = cache;
            PrefixKey = prefix;
        }

        public TimeSpan CacheExpired => TimeSpan.FromDays(90);

        public string FormatKey(CacheKey key, params object[] extendKeys)
        {
            var extend = extendKeys.Where(k => k != null).Select(k => k.ToString().ToLower().Replace("-", ""));
            return $"{PrefixKey}_{key.ToString()}_{string.Join("_", extend)}";
        }

        public T GetOrSet<T>(string key, T value, TimeSpan? timeExpired = null)
        {
            return _cache.GetOrCreate(key, entry =>
            {
                entry.SlidingExpiration = timeExpired ?? CacheExpired;
                return value;
            });
        }

        public T GetOrSet<T>(string key, Func<T> execSet = null, TimeSpan? timeExpired = null)
        {
            return _cache.GetOrCreate(key, entry =>
            {
                entry.SlidingExpiration = timeExpired ?? CacheExpired;
                return execSet();
            });
        }

        public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> execSet = null, TimeSpan? timeExpired = null)
        {
            return await _cache.GetOrCreateAsync(key, entry =>
            {
                entry.SlidingExpiration = timeExpired ?? CacheExpired;
                return execSet();
            });
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}
