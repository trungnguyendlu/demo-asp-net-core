using MongoDB.Bson;

namespace Demo.Data
{
    public class RelatedPostFindRequest
    {
        public ObjectId PostId { get; set; }
        public ObjectId CreatedUserId { get; set; }
        public ObjectId CategoryId { get; set; }
        public int Top { get; set; }
    }
}
