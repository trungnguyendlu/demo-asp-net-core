using System;
using System.ComponentModel.DataAnnotations;

namespace Corporation.Models.Admin.ServiceCategory
{
    [Serializable]
    public class ServiceCategoryIndexModel : BaseIndexModel<ServiceCategoryModel>
    {
        [Display(Name = "Tên loại dịch vụ")]
        [MaxLength(150, ErrorMessage = "Chiều dài tên loại dịch vụ tối đa 150 ký tự")]
        public string Name { get; set; }
    }
}