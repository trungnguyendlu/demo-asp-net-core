using System;
using System.ComponentModel.DataAnnotations;
using Demo.Data;
using Demo.Util;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Demo.Model.Admin.User
{
    public class UserModel : BaseModel
    {
        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "Chưa nhập họ tên")]
        [MaxLength(100, ErrorMessage = "Chiều dài họ tên tối đa 100 ký tự")]
        public string FullName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Chưa nhập email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [MaxLength(100, ErrorMessage = "Chiều dài email tối đa 100 ký tự")]
        public string Email { get; set; }

        [Display(Name = "Mật khẩu")]
        [MaxLength(100, ErrorMessage = "Chiều dài mật khẩu tối đa 100 ký tự")]
        public string Password { get; set; }

        public string AvatarUrl { get; set; }

        [Display(Name = "Phân quyền")]
        [Required(ErrorMessage = "Chưa nhập phân quyền")]
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId RoleId { get; set; } = ObjectId.Empty;

        public bool IsAdmin { get; set; }

        [Display(Name = "Tình trạng")]
        [Required(ErrorMessage = "Chưa nhập tình trạng")]
        public UserStatus Status { get; set; }

        [Display(Name = "Lần cuối đăng nhập")]
        public DateTime? LastLoginDate { get; set; }

        //reference
        public string RoleName { get; set; }
    }
}