using Demo.Entity;
using Microsoft.AspNetCore.Http;
using Demo.Infrastructure.Extensions;
using Demo.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using MongoDB.Driver;
using MongoDB.Bson;
using Demo.Util;

namespace Demo.Infrastructure.Helper
{
    public class SessionHelper : ISessionHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public SessionHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        public ObjectId CurrentUserId
        {
            get
            {
                var json = _session.GetString(SessionKey.CurrentUserId);
                if (string.IsNullOrEmpty(json))
                {
                    _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                }
                return json.ToObjectId();
            }
        }

        public User CurrentUser => Get<User>(SessionKey.CurrentUser);


	    public Role CurrentRole => Get<Role>(SessionKey.CurrentRole);
        
        public bool HasPermission(FunctionType permission)
        {
            return (CurrentUser != null && CurrentUser.IsAdmin) || 
                (CurrentRole != null && CurrentRole.Permissions.Exists(s => s.Enable && s.Function == (int)permission));
        }



        public T Get<T>(string key)
        {
            return _session.Get<T>(key);
        }

        public void Set<T>(string key, T source)
        {
            _session.Set(key, source);
        }

        public void SetString(string key, string value)
        {
            _session.SetString(key, value);
        }

        public void Remove(string key)
        {
            _session.Remove(key);
        }

        public void Abandon()
        {
            _session.Clear();
        }
    }
}
