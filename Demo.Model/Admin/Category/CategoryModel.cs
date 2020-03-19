using Demo.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.Model.Admin.Category
{
    [Serializable]
    public class CategoryModel : BaseModel
	{
        [Display(Name = "Tên chuyên mục")]
        [Required(ErrorMessage = "Chưa nhập tên chuyên mục")]
        [MaxLength(150, ErrorMessage = "Chiều dài tên chuyên mục tối đa 150 ký tự")]
        public string Name { get; set; }

        [Display(Name = "Đường dẫn")]
        [Required(ErrorMessage = "Chưa nhập đường dẫn")]
        [MaxLength(150, ErrorMessage = "Chiều dài đường dẫn tối đa 150 ký tự")]
        public string Slug { get; set; }
        
        [Display(Name = "Từ khóa")]
        [MaxLength(1000, ErrorMessage = "Chiều dài Từ khóa tối đa 1000 ký tự")]
        public string Keyword { get; set; }

        [Display(Name = "Mô tả")]
        [MaxLength(1000, ErrorMessage = "Chiều dài mô tả tối đa 1000 ký tự")]
        public string Description { get; set; }

        [Display(Name = "Kích hoạt")]
        public bool IsActive { get; set; }
    }
}
