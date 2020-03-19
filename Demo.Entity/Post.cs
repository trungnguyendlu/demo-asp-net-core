using MongoDB.Bson;
using System;

namespace Demo.Entity
{
    public class Post : BaseEntity
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string PhotoUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Keyword { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public bool IsPublished { get; set; }
        public DateTime? PublishedDate { get; set; }
        public ObjectId CategoryId { get; set; }
        public bool IsFeaturePost { get; set; }
    }
}
