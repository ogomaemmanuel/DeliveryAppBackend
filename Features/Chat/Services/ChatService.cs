using DeliveryAppBackend.Data;
using DeliveryAppBackend.Features.Chat.Entities;
using DeliveryAppBackend.Features.Chat.Models;
using DeliveryAppBackend.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace DeliveryAppBackend.Features.Chat.Services
{
    public class ChatService : IChatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IHubContext<NotificationHub, INotificationHubClient> _hubContext;
        public ChatService(IUnitOfWork unitOfWork, IHubContext<NotificationHub, INotificationHubClient>
            hubContext)
        {
            _hubContext = hubContext;
            _unitOfWork = unitOfWork;
        }

        public async Task<OutgoingChatMessageViewModel> SendMessage(IncommingChatMessageViewModel chatMessageViewModel)
        {

            var chatMessage = new ChatMessage()
            {
                Message = chatMessageViewModel.Message,
                ToId = chatMessageViewModel.To.Id,
                FromId = chatMessageViewModel.From.Id,
            };
            await this._unitOfWork.Chats.AddAsync(chatMessage);
            await this._unitOfWork.CompleteAsync();
            var outGoingMessage = new OutgoingChatMessageViewModel
            {
                Id= chatMessage.Id,
                FromId = chatMessageViewModel.From.Id,
                ToId = chatMessageViewModel.To.Id,
                Message = chatMessageViewModel.Message,
                CreatedAt = chatMessage.CreatedAt,
                UpdatedAt = chatMessage.UpdatedAt,
                FromUserName = chatMessageViewModel.From.UserName,
                ToUserName = chatMessageViewModel.To.UserName,
            };
            await this._hubContext.Clients
                .User(chatMessageViewModel.To.Id.ToString())
                .MessageToUser(outGoingMessage);

            return outGoingMessage;
        }
    }
}
