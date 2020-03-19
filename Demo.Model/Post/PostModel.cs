using Demo.Util;
using Demo.Model.Category;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;

namespace Demo.Model.Post
{
    public class PostModel : BaseModel
    {
        public string Title { get; set; }

        public string Slug { get; set; }

        public string PhotoUrl { get; set; }

        public string ThumbnailUrl { get; set; }

        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId CategoryId { get; set; }

        public string Keyword { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public bool IsPublished { get; set; }

        public DateTime? PublishedDate { get; set; } = DateTime.UtcNow;

        //reference
        public CategoryModel Category { get; set; }


        public string GetUrl()
        {
            return $"/blog/{Category.Slug}/{Slug}";
        }
    }
}
