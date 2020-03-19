using System;
using System.Threading.Tasks;

namespace Demo.Util
{
    public interface ICacheHelper
	{
        T GetOrSet<T>(CacheKey key, Func<T> execSet = null, TimeSpan? timeExpired = null);
        T GetOrSet<T>(string key, Func<T> execSet = null, TimeSpan? timeExpired = null);

        Task<T> GetOrSetAsync<T>(CacheKey key, Func<Task<T>> execSet = null, TimeSpan? timeExpired = null);

        Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> execSet = null, TimeSpan? timeExpired = null);

        void Clear(CacheKey key, params object[] extendKeys);

        string FormatKey(CacheKey key, params object[] extendKeys);
    }
}
