using System;
using System.Threading.Tasks;

namespace Demo.Util
{
    public partial class CacheHelper : ICacheHelper
	{
        private readonly ICache _cache;

        public CacheHelper(ICache cache)
        {
            _cache = cache;
        }

        public T GetOrSet<T>(CacheKey key, Func<T> execSet = null, TimeSpan? timeExpired = null)
        {
            return GetOrSet(FormatKey(key), execSet, timeExpired);
        }

        public T GetOrSet<T>(string key, Func<T> execSet = null, TimeSpan? timeExpired = null)
        {
            return _cache.GetOrSet<T>(key, execSet, timeExpired);
        }

        public async Task<T> GetOrSetAsync<T>(CacheKey key, Func<Task<T>> execSet = null, TimeSpan? timeExpired = null)
        {
            return await GetOrSetAsync(FormatKey(key), execSet, timeExpired);
        }

        public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> execSet = null, TimeSpan? timeExpired = null)
        {
            return await _cache.GetOrSetAsync<T>(key, execSet, timeExpired);
        }

        public void Clear(CacheKey key, params object[] extendKeys)
        {
            _cache.Remove(FormatKey(key, extendKeys));
        }

        public string FormatKey(CacheKey key, params object[] extendKeys)
        {
            return _cache.FormatKey(key, extendKeys);
        }
    }
}
