using Corporation.Data;
using Corporation.Infrastructure.Helper.Converter;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Corporation.Models.Admin.Notification
{
    [Serializable]
    public class NotificationModel : BaseModel
    {
        [Display(Name = "Khách hàng")]
        [Required(ErrorMessage = "Chưa chọn Khách hàng")]
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId ClientId { get; set; }

        [Display(Name = "Loại thông báo")]
        [Required(ErrorMessage = "Chưa chọn Loại thông báo")]
        public NotificationType Type { get; set; }

        [Display(Name = "Nội dung")]
        [Required(ErrorMessage = "Chưa chọn Nội dung")]
        [MaxLength(500, ErrorMessage = "Chiều dài Nội dung tối đa 500 ký tự")]
        public string Message { get; set; }

        public bool IsNew { get; set; } = true;

        public string GetCssClass()
        {
            var cssClass = string.Empty;
            switch (Type)
            {
                case NotificationType.Danger:
                    cssClass = "alert alert-danger";
                    break;
                case NotificationType.Warning:
                    cssClass = "alert alert-warning";
                    break;
                default:
                    cssClass = "alert alert-info";
                    break;
            }
            return IsNew ? $"{cssClass} alert-bordered" : cssClass;
        }
    }
}
