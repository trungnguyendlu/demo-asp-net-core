using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Demo.Data;
using Demo.Util;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Demo.Model.Admin.User
{
    [Serializable]
    public class UserIndexModel : BaseIndexModel<UserModel>
    {
        [Display(Name = "Quyền")]
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId RoleId { get; set; } = ObjectId.Empty;
        
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [MaxLength(100, ErrorMessage = "Chiều dài Email tối đa 100 ký tự")]
        public string Email { get; set; }

        public List<Reference> Roles { get; set; }
    }
}