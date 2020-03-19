using System;
using System.ComponentModel.DataAnnotations;
using Demo.Util;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Demo.Model
{
    public abstract class BaseModel
    {
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId CreatedUserId { get; set; }
        [Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; }
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId UpdatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }

        //reference only
        public string CreatedUser { get; set; }
        public string UpdatedUser { get; set; }
    }
}