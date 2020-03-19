using Demo.Util;
using MongoDB.Bson;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Model.Post
{
    public class CommentModel : BaseModel
    {
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId ReplyCommentId { get; set; }
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId PostId { get; set; }
        [Required]
        public string Author { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Content { get; set; }
        public bool IsAdmin { get; set; }
        public string AvatarUrl { get; set; }


        public string GetGravatar()
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(Email.Trim().ToLowerInvariant());
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                var sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                return $"https://www.gravatar.com/avatar/{sb.ToString().ToLowerInvariant()}?s=85";
            }
        }
    }
}
