namespace Demo.Entity
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Keyword { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
