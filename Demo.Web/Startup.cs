using System;
using System.Globalization;
using System.Net;
using Demo.Data;
using Demo.Infrastructure;
using Demo.Infrastructure.Authorize;
using Demo.Infrastructure.Binder;
using Demo.Infrastructure.DependencyResolution;
using Demo.Infrastructure.Utils;
using Demo.BusinessLogic;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using WebMarkupMin.AspNetCore2;
using WebMarkupMin.Core;
//using WilderMinds.MetaWeblog;

using IWmmLogger = WebMarkupMin.Core.Loggers.ILogger;
//using MetaWeblogService = Miniblog.Core.Services.MetaWeblogService;
using WmmNullLogger = WebMarkupMin.Core.Loggers.NullLogger;
using Microsoft.AspNetCore.Mvc;

namespace Corporation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfiguration());
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            services.AddMemoryCache();
            services.AddMvc(options =>
            {
                // add custom binder to beginning of collection
                options.ModelBinderProviders.Insert(0, new ObjectIdBinderProvider());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //redis cache
            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = "127.0.0.1:6379,password=abcd@6789";
                option.InstanceName = "demo";//prefix key: ex: key = key2 => full key = masterkey2
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(45);
                options.Cookie.HttpOnly = true;
                options.Cookie.Name = "demo";
            });

            // Cookie authentication.
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/login";
                    options.LogoutPath = "/logout";
                    options.AccessDeniedPath = "/error/403";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(45);
                    //todo
                    //options.EventsType = typeof(CustomCookieAuthenticationEvents);
                });

            // authorize
            services.AddAuthorization(options =>
            {
                foreach(FunctionType permission in Enum.GetValues(typeof(FunctionType)))
                {
                    options.AddPolicy(permission.ToString(),
                        policy => policy.Requirements.Add(new PermissionRequirement(permission)));
                }
            });

            services.AddScoped<IAuthorizationHandler, PermissionHandler>();

            // HTML minification (https://github.com/Taritsyn/WebMarkupMin)
            services.AddWebMarkupMin(options =>
                {
                    options.AllowMinificationInDevelopmentEnvironment = true;
                    options.DisablePoweredByHttpHeaders = true;
                })
                .AddHtmlMinification(options =>
                {
                    options.MinificationSettings.RemoveOptionalEndTags = false;
                    options.MinificationSettings.WhitespaceMinificationMode = WhitespaceMinificationMode.Safe;
                });

            // Used by HTML minifier
            services.AddSingleton<IWmmLogger, WmmNullLogger>();

            // Bundling, minification and Sass transpilation (https://github.com/ligershark/WebOptimizer)
            services.AddWebOptimizer(pipeline =>
            {
                pipeline.MinifyJsFiles();
                pipeline.MinifyCssFiles();
            });

            //db setting
            services.Configure<DbSetting>(options =>
            {
                options.User = Configuration.GetSection("dbsetting:user").Value;
                options.Pass = Configuration.GetSection("dbsetting:pass").Value;
                options.Name = Configuration.GetSection("dbsetting:name").Value;
            });

            // DI config
            DIContainer.Initialize(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Common.WebSetting = Configuration.GetSection("setting").Get<WebSetting>();
            Common.Init(Configuration, app.ApplicationServices.GetService<ISettingsService>(), env.IsDevelopment());
            
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error/500");
            }

            if (!env.IsDevelopment())
            {
                app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });

                app.UseStatusCodePagesWithReExecute("/error/{0}");
                app.UseWebOptimizer();
                app.UseWebMarkupMin();
            }

            app.UseAuthentication();


            //culture
            var cultureInfo = new CultureInfo("vi-VN");
            cultureInfo.NumberFormat.CurrencySymbol = "đ";
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] = "public, max-age=31536000, stale-while-revalidate=2592000";
                }
            });
            app.UseCookiePolicy(new CookiePolicyOptions {  Secure = env.IsDevelopment() ? CookieSecurePolicy.SameAsRequest : CookieSecurePolicy.Always });
            app.UseSession();
            app.UseMvc(routes =>
            {
                //blog
                routes.MapRoute(
                    name: "blog",
                    template: "blog",
                    defaults: new { controller = "Blog", action = "Index" });

                routes.MapRoute(
                    name: "category",
                    template: "blog/{slug:required}",
                    defaults: new { controller = "Blog", action = "Category" });

                routes.MapRoute(
                    name: "post",
                    template: "blog/{cat:required}/{slug:required}",
                    defaults: new { controller = "Blog", action = "Detail" });

                //admin
                routes.MapRoute(
                        name: "admin",
                        template: "{controller=Admin}/{action=Index}/{id}");

                //default
                routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}");
            });
        }
    }
}
