using System.ComponentModel.DataAnnotations;

namespace Demo.Model.Account
{
    public class PasswordRecoverModel
    {
        [Required(ErrorMessage = "Chưa nhập Email")]
        [EmailAddress(ErrorMessage = "Địa chỉ Email không hợp lệ")]
        [MaxLength(150, ErrorMessage = "Chiều dài Email tối đa 150 ký tự")]
        public string Email { get; set; }
    }
}
