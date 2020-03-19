using Corporation.Data;
using System;
using System.Collections.Generic;

namespace Corporation.Models.Admin.Notification
{
    [Serializable]
    public class NotificationEditModel : BaseEditModel<NotificationModel>
    {
        public List<Reference> Clients { get; set; }
    }
}