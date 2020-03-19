using System;
using MongoDB.Bson;

namespace Demo.Entity
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }
        public string Introduction { get; set; }
        public string AvatarUrl { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public ObjectId RoleId { get; set; }
        public bool IsAdmin { get; set; }
        public int Status { get; set; }
        public DateTime? LastLoginDate { get; set; }
        
        public string GetUserName() => !string.IsNullOrEmpty(Email) ? Email.Substring(0, Email.IndexOf('@')) : string.Empty;
    }
}
