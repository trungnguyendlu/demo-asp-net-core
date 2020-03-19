using Corporation.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace Corporation.Models.Admin.Ticket
{
    [Serializable]
    public class TicketIndexModel : BaseIndexModel<TicketModel>
    {
        [Display(Name = "Trạng thái")]
        public TicketStatus? Status { get; set; }
    }
}