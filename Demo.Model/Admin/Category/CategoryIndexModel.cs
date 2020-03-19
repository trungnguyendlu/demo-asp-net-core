using Demo.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.Model.Admin.Category
{
    [Serializable]
    public class CategoryIndexModel : BaseIndexModel<CategoryModel>
    {
        [Display(Name = "Tên chuyên mục")]
        [MaxLength(150, ErrorMessage = "Chiều dài tên chuyên mục tối đa 150 ký tự")]
        public string Name { get; set; }
    }
}