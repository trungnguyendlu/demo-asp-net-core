using Corporation.Data;
using Corporation.Infrastructure.Helper.Converter;
using MongoDB.Bson;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Corporation.Models.Admin.Ticket
{
    public class TicketModel : BaseModel
    {
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId ClientId { get; set; }

        [Display(Name = "Mã")]
        [Required(ErrorMessage = "Chưa nhập Mã")]
        [MaxLength(10, ErrorMessage = "Chiều dài Mã tối đa 10 ký tự")]
        public string TicketNumber { get; set; }

        [Display(Name = "Tiêu đề")]
        [Required(ErrorMessage = "Chưa nhập Tiêu đề")]
        [MaxLength(200, ErrorMessage = "Chiều dài Tiêu đề tối đa 200 ký tự")]
        public string Title { get; set; }

        [Display(Name = "Nội dung")]
        [Required(ErrorMessage = "Chưa nhập Nội dung")]
        [MaxLength(1000, ErrorMessage = "Chiều dài Nội dung tối đa 1000 ký tự")]
        public string Content { get; set; }

        [Display(Name = "Trạng thái")]
        [Required(ErrorMessage = "Chưa nhập Trạng thái")]
        public TicketStatus Status { get; set; } = TicketStatus.New;

        public List<TicketResponseModel> Responses { get; set; } = new List<TicketResponseModel>();


        public string GetCssClass()
        {
            switch(Status)
            {
                case TicketStatus.Assigned:
                    return "border-left-info";
                case TicketStatus.Resolved:
                    return "border-left-success";
                case TicketStatus.Closed:
                    return "border-left-warning";
                default:
                    return "border-left-grey";
            }
        }
    }
}
