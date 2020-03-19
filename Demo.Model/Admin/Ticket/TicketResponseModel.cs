using Corporation.Infrastructure.Helper.Converter;
using MongoDB.Bson;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Corporation.Models.Admin.Ticket
{
    public class TicketResponseModel : BaseModel
    {
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId TicketId { get; set; }
        public bool IsClient { get; set; }

        [Display(Name = "Nội dung")]
        [Required(ErrorMessage = "Chưa nhập Nội dung")]
        [MaxLength(1000, ErrorMessage = "Chiều dài Nội dung tối đa 1000 ký tự")]
        public string Content { get; set; }
    }
}
