using MongoDB.Bson;

namespace Demo.Data
{
    public class UserFindRequest : BaseFindRequest
    {
        public ObjectId RoleId { get; set; }
        public string Email { get; set; }
    }
}
