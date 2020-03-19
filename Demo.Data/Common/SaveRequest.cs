using Demo.Entity;
using MongoDB.Bson;

namespace Demo.Data
{
    public class SaveRequest<T> where T : BaseEntity
    {
        public T Entity { get; set; }
        public bool IsEdit { get; set; }
        public ObjectId UserId { get; set; }
    }
}
