namespace Demo.Data
{
    public class MediaFindRequest : BaseFindRequest
    {
        public string Name { get; set; }
        public int? Type { get; set; }
    }
}
