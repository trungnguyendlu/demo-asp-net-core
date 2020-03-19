using Demo.Util;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.Model.Admin.Post
{
    [Serializable]
    public class PostModel : BaseModel
    {
        [Display(Name = "Tiêu đề")]
        [Required(ErrorMessage = "Chưa nhập Tiêu đề")]
        [MaxLength(300, ErrorMessage = "Chiều dài Tiêu đề tối đa 300 ký tự")]
        public string Title { get; set; }

        [Display(Name = "Đường dẫn")]
        [Required(ErrorMessage = "Chưa nhập Đường dẫn")]
        [MaxLength(300, ErrorMessage = "Chiều dài Đường dẫn tối đa 300 ký tự")]
        public string Slug { get; set; }

        [Display(Name = "Ảnh đại diện")]
        [Required(ErrorMessage = "Chưa nhập Ảnh đại diện")]
        [MaxLength(1000, ErrorMessage = "Chiều dài Ảnh đại diện tối đa 1000 ký tự")]
        public string PhotoUrl { get; set; }
        public string ThumbnailUrl { get; set; }

        [Display(Name = "Từ khóa")]
        [Required(ErrorMessage = "Chưa nhập Từ khóa")]
        [MaxLength(500, ErrorMessage = "Chiều dài Từ khóa tối đa 500 ký tự")]
        public string Keyword { get; set; }

        [Display(Name = "Giới thiệu")]
        [Required(ErrorMessage = "Chưa nhập Giới thiệu")]
        [MaxLength(1000, ErrorMessage = "Chiều dài Giới thiệu tối đa 1000 ký tự")]
        public string Description { get; set; }

        [Display(Name = "Nội dung")]
        [Required(ErrorMessage = "Chưa nhập Nội dung")]
        public string Content { get; set; }

        [Display(Name = "Xuất bản")]
        public bool IsPublished { get; set; }

        [Display(Name = "Ngày xuất bản")]
        public DateTime? PublishedDate { get; set; }

        [Display(Name = "Chuyên mục")]
        [Required(ErrorMessage = "Chưa nhập Chuyên mục")]
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId CategoryId { get; set; }

        [Display(Name = "Bài viết tiêu biểu")]
        public bool IsFeaturePost { get; set; }

        //reference only
        public string CategoryName { get; set; }
        public string PostUrl { get; set; }
    }
}
