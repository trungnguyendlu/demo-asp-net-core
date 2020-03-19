using LiteDB;
using System.Collections.Generic;

namespace Corporation.Models.Admin.Theme
{
    public class PageModel
    {
        public ObjectId Id { get; set; }
        public Data.PageType PageType { get; set; }
        public bool IsHomePage { get; set; }
        public SeoModel Seo { get; set; }
        public List<string> Css { get; set; } = new List<string>();
        public List<string> Js { get; set; } = new List<string>();
        public List<WidgetModel> Widgets { get; set; }
        public string Html { get; set; }
    }
}
