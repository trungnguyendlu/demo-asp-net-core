using Demo.Data;
using Demo.Entity;
using MongoDB.Bson;

namespace Demo.Infrastructure.Helper
{
    public static class SessionKey
	{
		public const string CurrentUser = "CurrentUser";
        public const string CurrentUserId = "CurrentUserId";

        public const string CurrentRole = "CurrentRole";
	}

	public interface ISessionHelper
	{
        User CurrentUser { get; }

        ObjectId CurrentUserId { get; }

		Role CurrentRole { get; }

        bool HasPermission(FunctionType permission);
            
        T Get<T>(string key);

        void Set<T>(string key, T source);

        void SetString(string key, string value);

        void Remove(string key);
	}
}
