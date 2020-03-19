using Corporation.Data;
using Corporation.Infrastructure.Helper.Converter;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Corporation.Models.Admin.Notification
{
    [Serializable]
    public class NotificationIndexModel : BaseIndexModel<NotificationModel>
    {
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId ClientId { get; set; } = ObjectId.Empty;
        public NotificationType? Type { get; set; }

        //reference only
        public List<Reference> Clients { get; set; }
    }
}