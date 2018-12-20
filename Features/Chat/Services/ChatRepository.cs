using DeliveryAppBackend.Data;
using DeliveryAppBackend.Features.Chat.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryAppBackend.Features.Chat.Services
{
    public class ChatRepository:Repository<ChatMessage,int>,IChatRepository
    {
        public ChatRepository(DeliveryAppDbContext deliveryAppDbContext):base(deliveryAppDbContext)
        {

        }

    }
}
