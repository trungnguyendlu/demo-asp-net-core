namespace Demo.Entity
{
    public class Media : BaseEntity
    {
        public int? Type { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public string Title { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
