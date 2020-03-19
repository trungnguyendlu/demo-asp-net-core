using MongoDB.Bson;

namespace Demo.Data
{
    public class PostSearchRequest : BaseFindRequest
    {
        public ObjectId CategoryId { get; set; } = ObjectId.Empty;
        public string Keyword { get; set; }
    }
}
