using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Demo.Model.Admin.User
{
    [Serializable]
    public class ChangePasswordModel
    {
        [Display(Name = "Mật khẩu hiện tại")]
        [Required(ErrorMessage = "Chưa nhập Mật khẩu hiện tại")]
        [MaxLength(100, ErrorMessage = "Chiều dài Mật khẩu hiện tại tối đa 100 ký tự")]
        public string OldPassword { get; set; }

        [Display(Name = "Mật khẩu mới")]
        [Required(ErrorMessage = "Chưa nhập Mật khẩu mới")]
        [MinLength(6, ErrorMessage = "Chiều dài Mật khẩu mới tối thiểu 6 ký tự")]
        [MaxLength(100, ErrorMessage = "Chiều dài Mật khẩu mới tối đa 100 ký tự")]
        public string NewPassword { get; set; }

        [Display(Name = "Xác nhận mật khẩu mới")]
        [Required(ErrorMessage = "Chưa nhập Xác nhận mật khẩu mới")]
        [MaxLength(100, ErrorMessage = "Chiều dài Xác nhận mật khẩu mới tối đa 100 ký tự")]
        public string ConfirmNewPassword { get; set; }
    }
}