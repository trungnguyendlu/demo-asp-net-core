using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Util
{
    public class RedisCache : ICache
    {
        private readonly IDistributedCache _cache;

        public RedisCache(IDistributedCache cache)
        {
            _cache = cache;
        }

        public TimeSpan CacheExpired => TimeSpan.FromDays(90);

        public string FormatKey(CacheKey key, params object[] extendKeys)
        {
            var extend = extendKeys.Where(k => k != null).Select(k => k.ToString().ToLower().Replace("-", ""));
            return $"{key.ToString()}_{string.Join("_", extend)}";
        }

        public T GetOrSet<T>(string key, T value, TimeSpan? timeExpired = null)
        {
            var json = _cache.GetString(key);
            if (string.IsNullOrEmpty(json))
            {
                json = JsonConvert.SerializeObject(value);
                _cache.SetString(key, json, new DistributedCacheEntryOptions
                {
                    SlidingExpiration = timeExpired ?? CacheExpired
                });
                return value;
            }
            return JsonConvert.DeserializeObject<T>(json);
        }

        public T GetOrSet<T>(string key, Func<T> execSet = null, TimeSpan? timeExpired = null)
        {
            var json = _cache.GetString(key);
            if (string.IsNullOrEmpty(json))
            {
                var data = execSet();
                if (data != null)
                {
                    json = JsonConvert.SerializeObject(data);
                    _cache.SetString(key, json, new DistributedCacheEntryOptions
                    {
                        SlidingExpiration = timeExpired ?? CacheExpired
                    });
                }
                return data;
            }
            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> execSet = null, TimeSpan? timeExpired = null)
        {
            var json = _cache.GetString(key);
            if (string.IsNullOrEmpty(json))
            {
                var data = await execSet();
                if (data != null)
                {
                    json = JsonConvert.SerializeObject(data);
                    _cache.SetString(key, json, new DistributedCacheEntryOptions
                    {
                        SlidingExpiration = timeExpired ?? CacheExpired
                    });
                }
                return data;
            }
            return JsonConvert.DeserializeObject<T>(json);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}
