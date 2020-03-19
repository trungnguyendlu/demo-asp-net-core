using Demo.Model.Category;
using System.Collections.Generic;

namespace Demo.Model.Post
{
    public class RightBarModel
    {
        public string Keyword { get; set; }
        public CategoryModel CurrentCategory { get; set; }
        public List<CategoryModel> Categories { get; set; }
        public List<PostModel> Popular { get; set; }
    }
}
