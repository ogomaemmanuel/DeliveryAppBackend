using DeliveryAppBackend.Features.Chat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryAppBackend.Features.Chat.Services
{
    public interface IChatService
    {
        Task<OutgoingChatMessageViewModel> SendMessage(IncommingChatMessageViewModel chatMessageViewModel);
    }
}
