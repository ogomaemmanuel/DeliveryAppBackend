using DeliveryAppBackend.Features.Accounts.Models;
using DeliveryAppBackend.Features.Chat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryAppBackend.Hubs
{
    public interface INotificationHubClient
    {
        Task MessageToUser(OutgoingChatMessageViewModel outgoingChatMessageViewModel);
        Task UpdatedUserList(Object onlineUsers);
    }
}
