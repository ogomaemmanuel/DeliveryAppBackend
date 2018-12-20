using DeliveryAppBackend.Data;
using DeliveryAppBackend.Features.Accounts.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryAppBackend.Features.Chat.Entities
{
    public class ChatMessage: TrackableEntity
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime? ReadAt { get; set; }
        public int FromId { get; set; }
        [ForeignKey("FromId")]
        public SystemUser From { get; set; }
        public int ToId { get; set; }
        [ForeignKey("ToId")]
        public SystemUser To { get; set; }
        

    }
}
