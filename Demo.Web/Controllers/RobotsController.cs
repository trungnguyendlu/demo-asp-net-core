using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Demo.Infrastructure.Utils;
using Demo.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    public class RobotsController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;

        public RobotsController(IBlogService blogService,
            ICategoryService categoryService)
        {
            _blogService = blogService;
            _categoryService = categoryService;
        }

        [Route("/robots.txt")]
        [ResponseCache(Duration = 2592000)]
        public string RobotsTxt()
        {
            var sb = new StringBuilder();
            sb.AppendLine("User-agent: *");
            sb.AppendLine("Disallow: /error/");
            sb.AppendLine("Disallow: /blog?q=");
            sb.AppendLine("Disallow: *?q=");
            sb.AppendLine("Disallow: *?p=");
            sb.AppendLine("User-agent: rogerbot");
            sb.AppendLine("Disallow:/");
            sb.AppendLine("User-agent: mj12bot");
            sb.AppendLine("Disallow:/");
            sb.AppendLine("User-agent: dotbot");
            sb.AppendLine("Disallow:/");
            sb.AppendLine("User-agent: exabot");
            sb.AppendLine("Disallow:/");
            sb.AppendLine("User-agent: gigabot");
            sb.AppendLine("Disallow:/");
            sb.AppendLine("User-agent: AhrefsBot");
            sb.AppendLine("Disallow: /");
            sb.AppendLine("User-agent: BacklinkCrawler");
            sb.AppendLine("Disallow: /");
            sb.AppendLine($"Sitemap: https://{Request.Host}/sitemap.xml");

            return sb.ToString();
        }

        [Route("/sitemap.xml")]
        public async Task SitemapXml()
        {
            string host = "https://" + Request.Host;
            Response.ContentType = "application/xml";

            using (var xml = XmlWriter.Create(Response.Body, new XmlWriterSettings { Indent = true }))
            {
                xml.WriteStartDocument();
                xml.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");

                //pages
                var createdDate = Common.Settings.OpeningDate ?? new DateTime(2018, 10, 17, 11, 0, 0);
                WriteSiteMapItem(xml, host, "/", createdDate);
                WriteSiteMapItem(xml, host, "/san-pham", createdDate);
                //WriteSiteMapItem(xml, host, "/dich-vu", createdDate);
                WriteSiteMapItem(xml, host, "/du-an", createdDate);
                //WriteSiteMapItem(xml, host, "/hinh-anh", createdDate);
                WriteSiteMapItem(xml, host, "/lien-he", createdDate);
                WriteSiteMapItem(xml, host, "/blog", createdDate);

                //blog category
                var categories = await _categoryService.GetAllCategoriesAsync();
                foreach (var item in categories)
                {
                    WriteSiteMapItem(xml, host, item.GetUrl(), item.UpdatedDate ?? item.CreatedDate);
                }

                //blog
                foreach (var post in await _blogService.GetAllPostsForSitemapAsync())
                {
                    var category = categories.FirstOrDefault(a => a.Id == post.CategoryId);
                    if (category != null)
                    {
                        post.CategorySlug = category.Slug;
                        var lastMod = new[] { post.PublishedDate.GetValueOrDefault(), post.UpdatedDate.GetValueOrDefault() };
                        WriteSiteMapItem(xml, host, post.GetUrl(), lastMod.Max());
                    }
                }

                xml.WriteEndElement();
            }
        }

        private void WriteSiteMapItem(XmlWriter xmlWriter, string host, string url, DateTime lastModifiedDate)
        {
            xmlWriter.WriteStartElement("url");
            xmlWriter.WriteElementString("loc", $"{host}{url}");
            xmlWriter.WriteElementString("lastmod", lastModifiedDate.ToString("yyyy-MM-ddThh:mmzzz"));
            xmlWriter.WriteEndElement();
        }
    }
}