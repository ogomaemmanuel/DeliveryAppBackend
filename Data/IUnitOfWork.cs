using DeliveryAppBackend.Features.Chat.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryAppBackend.Data
{
   public interface IUnitOfWork
    {
         IChatRepository Chats { get; }
        int Complete();
        Task<int> CompleteAsync();

    }
}
