using System.Collections.Generic;
using System.Linq;
using Demo.Infrastructure.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    public abstract class BaseController : Controller
    {
        protected void SetTitle(string title, string url = null, string photoUrl = null, bool isHomePage = false)
        {
            var host = $"{(Common.IsDevelopment ? "http" : "https")}://{Request.Host}";
            var settings = Common.Settings;
            ViewData["Title"] = title;
            ViewData["FullTitle"] = isHomePage ? settings.SiteName : Common.GetPageTitle(settings.TitleTemplateType, settings.TitleSeparate, title, settings.SiteName);
            ViewData["FullUrl"] = string.IsNullOrEmpty(url) ? host : $"{host}/{url.TrimStart('/')}";
            ViewData["PhotoUrl"] = photoUrl ?? $"{host}/images/cover.jpg";
            ViewData["Host"] = host;
        }

        protected IEnumerable<string> GetModelStateErrors()
        {
            return ModelState.Values.SelectMany(m => m.Errors)
                .Where(e => !string.IsNullOrWhiteSpace(e.ErrorMessage))
                .Select(e => e.ErrorMessage);
        }

        protected string GetModelStateErrorString()
        {
            return string.Join(", ", GetModelStateErrors());
        }
    }
}