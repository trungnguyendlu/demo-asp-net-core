using Demo.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.Model.Admin.Media
{
    [Serializable]
    public class MediaModel : BaseModel
    {
        [Display(Name = "Tên file")]
        public string FileName { get; set; }
        [Display(Name = "Kiểu file")]
        public string FileType { get; set; }
        [Display(Name = "Dung lượng")]
        public string FileSize { get; set; }

        [Display(Name = "Tên hình ảnh")]
        [Required(ErrorMessage = "Chưa nhập tên hình ảnh")]
        [MaxLength(300, ErrorMessage = "Chiều dài tên hình ảnh tối đa 300 ký tự")]
        public string Title { get; set; }

        [Display(Name = "Dài")]
        public int Width { get; set; }

        [Display(Name = "Rộng")]
        public int Height { get; set; }

        [Display(Name = "Phân loại")]
        public MediaType? Type { get; set; }

        [Display(Name = "Đường dẫn")]
        public string Url => $"/images/upload/{FileName}";
    }
}
