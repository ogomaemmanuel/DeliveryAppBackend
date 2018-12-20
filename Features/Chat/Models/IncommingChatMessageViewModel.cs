using DeliveryAppBackend.Features.Accounts.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryAppBackend.Features.Chat.Models
{
    public class IncommingChatMessageViewModel
    {
        public string Message { get; set; }
        public SystemUserViewModel From { get; set; }
        public SystemUserViewModel To { get; set; }
        public List<IFormFile> Attachments { get; set; }
    }
}
