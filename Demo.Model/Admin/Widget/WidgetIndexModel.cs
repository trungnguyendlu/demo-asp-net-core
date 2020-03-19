using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.Model.Admin.Widget
{
    [Serializable]
    public class WidgetIndexModel : BaseIndexModel<WidgetModel>
    {
        [Display(Name = "Tiêu đề")]
        [MaxLength(150, ErrorMessage = "Chiều dài Tiêu đề tối đa 150 ký tự")]
        public string Title { get; set; }
    }
}