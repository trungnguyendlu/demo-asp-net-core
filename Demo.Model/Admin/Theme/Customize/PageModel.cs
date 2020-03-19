using LiteDB;

namespace Corporation.Models.Admin.Theme.Customize
{
    public class PageModel
    {
        public ObjectId Id { get; set; }
        public ObjectId ThemeId { get; set; }
        public string Html { get; set; }
    }
}
