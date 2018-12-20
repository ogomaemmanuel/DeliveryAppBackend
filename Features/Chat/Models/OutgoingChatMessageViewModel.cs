using DeliveryAppBackend.Features.Accounts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryAppBackend.Features.Chat.Models
{
    public class OutgoingChatMessageViewModel
    {
        public string Message { get; set; }
        public int FromId { get; set; }
        public String FromUserName { get; set; }
        public int ToId { get; set; }
        public String ToUserName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<String> Attachments { get; set; }
        public int Id { get; set; }
    }
}
