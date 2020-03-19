using MongoDB.Bson;

namespace Demo.Data
{
    public class PostFindByCategoryRequest : BaseFindRequest
    {
        public ObjectId CategoryId { get; set; }
    }
}
