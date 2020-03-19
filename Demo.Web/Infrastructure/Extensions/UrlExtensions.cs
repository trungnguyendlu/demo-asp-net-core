using Demo.Infrastructure.Utils;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;

namespace Demo.Infrastructure.Extensions
{
    public static class UrlExtensions
    {
        public static IHtmlContent RenderScripts(this IUrlHelper urlHelper, string rootPath, params string[] paths)
        {
            return RenderInSiteHelper(urlHelper, "<script src=\"{0}\"></script>", rootPath, paths);
        }

        public static IHtmlContent RenderStyles(this IUrlHelper urlHelper, string rootPath, params string[] paths)
        {
            return RenderInSiteHelper(urlHelper, "<link rel=\"stylesheet\" href=\"{0}\" />", rootPath, paths);
        }

        private static IHtmlContent RenderInSiteHelper(this IUrlHelper urlHelper, string tag, string rootPath, params string[] paths)
        {
            var tags = string.Join(Environment.NewLine, paths.Select(path => string.Format(tag, urlHelper.Content(rootPath + GetSlash(path) + GetPath(path, !Common.IsDevelopment) + "?v=" + Common.Settings.Version ?? "1.1.5"))));

            return new HtmlString(tags);
        }

        private static string GetSlash(string path)
        {
            return path.StartsWith("/") || path.StartsWith("\\") ? string.Empty : "/";
        }

        private static string GetPath(string path, bool minify)
        {
            return minify && !(path.EndsWith(".min.js") || path.EndsWith(".min.css")) ? Path.ChangeExtension(path, ".min" + Path.GetExtension(path)) : path;
        }
    }
}
