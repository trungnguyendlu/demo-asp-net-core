using LiteDB;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Corporation.Models.Admin.Theme.Customize
{
    public class SeoModel
    {
        [Display(Name = "Tiêu đề trang")]
        [Required(ErrorMessage = "Chưa nhập Tiêu đề trang")]
        [MaxLength(100, ErrorMessage = "Chiều dài Tiêu đề trang tối đa 100 ký tự")]
        public string Title { get; set; }

        [Display(Name = "Từ khóa")]
        [Required(ErrorMessage = "Chưa nhập Từ khóa")]
        [MaxLength(200, ErrorMessage = "Chiều dài Từ khóa tối đa 200 ký tự")]
        public string MetaKeyword { get; set; }

        [Display(Name = "Mô tả trang")]
        [Required(ErrorMessage = "Chưa nhập Mô tả trang")]
        [MaxLength(400, ErrorMessage = "Chiều dài Mô tả trang tối đa 400 ký tự")]
        public string MetaDescription { get; set; }

        //reference only
        //page id
        [JsonIgnore]
        public ObjectId Id { get; set; }

        [JsonIgnore]
        public ObjectId ThemeId { get; set; }
    }
}
