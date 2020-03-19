using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.Model.Admin.Role
{
    [Serializable]
    public class RoleIndexModel : BaseIndexModel<RoleModel>
    {
        [Display(Name = "Tên phân quyền")]
        [MaxLength(100, ErrorMessage = "Chiều dài Tên phân quyền tối đa 100 ký tự")]
        public string Name { get; set; }
    }
}