using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Demo.Model.Admin.Role
{
    [Serializable]
    public class RoleModel : BaseModel
	{
        [Display(Name = "Tên phân quyền")]
        [Required(ErrorMessage = "Chưa nhập Tên phân quyền")]
        [MaxLength(100, ErrorMessage = "Chiều dài Tên phân quyền tối đa 100 ký tự")]
        public string Name { get; set; }

        [Display(Name = "Mô tả")]
        [MaxLength(1000, ErrorMessage = "Chiều dài Mô tả tối đa 1000 ký tự")]
        public string Description { get; set; }

	    public List<PermissionModel> Permissions { get; set; } = new List<PermissionModel>();
	}
}
