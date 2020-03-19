namespace Demo.Model.Category
{
    public class CategoryModel : BaseModel
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Keyword { get; set; }
        public string Description { get; set; }

        public string GetUrl()
        {
            return $"/blog/{Slug}";
        }
    }
}
