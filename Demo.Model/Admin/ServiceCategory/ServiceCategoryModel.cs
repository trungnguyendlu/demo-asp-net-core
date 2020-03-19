using System;
using System.ComponentModel.DataAnnotations;

namespace Corporation.Models.Admin.ServiceCategory
{
    [Serializable]
    public class ServiceCategoryModel : BaseModel
    {
        [Display(Name = "Thứ tự")]
        public int Order { get; set; }

        [Display(Name = "Tên loại dịch vụ")]
        [Required(ErrorMessage = "Chưa nhập tên loại dịch vụ")]
        [MaxLength(150, ErrorMessage = "Chiều dài tên loại dịch vụ tối đa 150 ký tự")]
        public string Name { get; set; }

        [Display(Name = "Đường dẫn")]
        [Required(ErrorMessage = "Chưa nhập đường dẫn")]
        [MaxLength(150, ErrorMessage = "Chiều dài đường dẫn tối đa 150 ký tự")]
        public string Slug { get; set; }

        [Display(Name = "Hình ảnh")]
        [Required(ErrorMessage = "Chưa nhập Hình ảnh")]
        public string PhotoUrl { get; set; }

        [Display(Name = "Từ khóa")]
        [MaxLength(1000, ErrorMessage = "Chiều dài Từ khóa tối đa 1000 ký tự")]
        public string Keyword { get; set; }

        [Display(Name = "Mô tả")]
        [MaxLength(1000, ErrorMessage = "Chiều dài mô tả tối đa 1000 ký tự")]
        public string Description { get; set; }

        [Display(Name = "Kích hoạt")]
        public bool IsActive { get; set; }

        [Display(Name = "Dịch vụ tiêu biểu")]
        public bool IsFeature { get; set; }
    }
}
