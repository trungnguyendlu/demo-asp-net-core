using Demo.BusinessLogic;
using Demo.Infrastructure.Helper;
using Demo.Repository;
using Demo.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Infrastructure.DependencyResolution
{
    public static class DIContainer
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddTransient<IBlogRepository, BlogRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IMediaRepository, MediaRepository>();
            services.AddTransient<ISettingsRepository, SettingsRepository>();
            services.AddTransient<IWidgetRepository, WidgetRepository>();
            services.AddTransient<IBlogService, BlogService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IMediaService, MediaService>();
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<ISessionHelper, SessionHelper>();
            services.AddTransient<IWidgetService, WidgetService>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<DatabaseContext, DatabaseContext>();
            services.AddTransient<ICacheHelper, CacheHelper>();
            services.AddSingleton<ICache, RedisCache>();
        }
    }
}