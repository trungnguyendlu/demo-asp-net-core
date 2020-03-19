using MongoDB.Bson;
using System;

namespace Demo.Entity
{
    public abstract class BaseEntity
    {
        public ObjectId Id { get; set; }
        public ObjectId CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public ObjectId UpdatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
