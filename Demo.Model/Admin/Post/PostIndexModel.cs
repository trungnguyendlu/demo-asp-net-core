using Demo.Data;
using Demo.Util;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Demo.Model.Admin.Post
{
    [Serializable]
    public class PostIndexModel : BaseIndexModel<PostModel>
    {
        [Display(Name = "Chuyên mục")]
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId CategoryId { get; set; } = ObjectId.Empty;

        [Display(Name = "Tiêu đề")]
        [MaxLength(300, ErrorMessage = "Chiều dài Tiêu đề tối đa 300 ký tự")]
        public string Title { get; set; }

        //reference only
        public List<Reference> Categories { get; set; }
    }
}