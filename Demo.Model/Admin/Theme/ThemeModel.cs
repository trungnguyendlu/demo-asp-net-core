using Corporation.Data;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Corporation.Models.Admin.Theme
{
    public class ThemeModel
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public string Description { get; set; }
        public ResourceModel Resource { get; set; }
        public List<PageModel> Pages { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
