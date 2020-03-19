using Demo.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.Model.Admin.Widget
{
    [Serializable]
    public class WidgetModel : BaseModel
    {
        [Display(Name = "Tiêu đề")]
        [Required(ErrorMessage = "Chưa nhập Tiêu đề")]
        [MaxLength(150, ErrorMessage = "Chiều dài Tiêu đề tối đa 150 ký tự")]
        public string Title { get; set; }
        
        [Display(Name = "Vị trí")]
        public WidgetPosition Position { get; set; }

        [Display(Name = "Nội dung")]
        [Required(ErrorMessage = "Chưa nhập Nội dung")]
        [MaxLength(5000, ErrorMessage = "Chiều dài Nội dung tối đa 5000 ký tự")]
        public string Content { get; set; }

        [Display(Name = "Kích hoạt")]
        public bool IsActive { get; set; }
    }
}
