using System;
using System.ComponentModel.DataAnnotations;
using Demo.Data;

namespace Demo.Model.Admin.Role
{
	[Serializable]
	public class PermissionModel
    {
        [Display(Name = "Chức năng")]
        public FunctionType Function { get; set; }

        [Display(Name = "Cho phép")]
        public bool Enable { get; set; }
	}
}
