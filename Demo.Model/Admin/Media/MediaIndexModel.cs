using Demo.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.Model.Admin.Media
{
    [Serializable]
    public class MediaIndexModel : BaseIndexModel<MediaModel>
    {
        [Display(Name = "Tên hình ảnh")]
        [MaxLength(300, ErrorMessage = "Chiều dài tên hình ảnh tối đa 300 ký tự")]
        public string Name { get; set; }

        public MediaType? Type { get; set; }
    }
}