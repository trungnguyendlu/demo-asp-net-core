using MongoDB.Bson;
using System;

namespace Demo.Model.Post
{
    public class PostForSitemapModel
    {
        public ObjectId CategoryId { get; set; }
        public string Slug { get; set; }
        public string CategorySlug { get; set; }
        public DateTime? PublishedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public string GetUrl()
        {
            return $"/blog/{CategorySlug}/{Slug}";
        }
    }
}
