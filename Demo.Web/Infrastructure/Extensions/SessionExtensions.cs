using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Demo.Infrastructure.Extensions
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            var json = JsonConvert.SerializeObject(value);
            session.SetString(key, json);
            var test = session.GetString(key);
        }

        public static T Get<T>(this ISession session, string key)
        {
            var json = session.GetString(key);

            return json == null ? default(T) :
                JsonConvert.DeserializeObject<T>(json);
        }
    }
}
