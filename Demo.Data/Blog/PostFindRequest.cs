using MongoDB.Bson;

namespace Demo.Data
{
    public class PostFindRequest : BaseFindRequest
    {
        public ObjectId CategoryId { get; set; }
        public string Title { get; set; }
    }
}
