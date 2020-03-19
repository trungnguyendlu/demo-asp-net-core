using MongoDB.Bson;

namespace Demo.Data
{
    public class PostFindByTagRequest : BaseFindRequest
    {
        public ObjectId TagId { get; set; }
    }
}
