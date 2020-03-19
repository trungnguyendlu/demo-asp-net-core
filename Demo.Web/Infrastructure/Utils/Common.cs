using Demo.Data;
using Demo.Entity;
using Demo.Infrastructure.Helper.Email;
using Demo.BusinessLogic;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using Demo.Util;

namespace Demo.Infrastructure.Utils
{
    public static class Common
    {
        public static void Init(IConfiguration config, ISettingsService settingsService, bool isDevelopment)
        {
            //Client.License = Crypto.EncryptString("quangcaoanviet.com", Client.Id);
            Settings = settingsService.GetById(Constants.DefaultSettingsId);
            DefaultConfig = config.GetSection("smtp").Get<EmailConfiguration>();
            IsDevelopment = isDevelopment;
        }
        
        public static WebSetting WebSetting { get; set; }
        
        public static EmailConfiguration DefaultConfig { get; set; }

        public static Settings Settings { get; set; }

        public static bool IsDevelopment { get; set; }


        public static string GetPageTitle(int type, string separate, string pageName, string siteName)
        {
            switch (type)
            {
                case (int)TitleTemplateType.PageName:
                    return pageName;
                case (int)TitleTemplateType.PageNameBeforeSiteName:
                    return $"{pageName} {separate} {siteName}";
                case (int)TitleTemplateType.SiteNameBeforePageName:
                    return $"{siteName} {separate} {pageName}";
                default:
                    return siteName;
            }
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
