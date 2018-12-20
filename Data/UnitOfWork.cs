using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliveryAppBackend.Features.Accounts.Roles.Services;
using DeliveryAppBackend.Features.Chat.Services;

namespace DeliveryAppBackend.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DeliveryAppDbContext _deliveryAppDbContext;
        public UnitOfWork(DeliveryAppDbContext deliveryAppDbContext)
        {
            _deliveryAppDbContext = deliveryAppDbContext;
            Chats = new ChatRepository(_deliveryAppDbContext);
            Roles = new RolesRepository(_deliveryAppDbContext);
        }
        public IChatRepository Chats { get; private set; }
        public IRolesRepository Roles { get; private set; }
        public int Complete()
        {
          return  this._deliveryAppDbContext.SaveChanges();
        }

        public Task<int> CompleteAsync()
        {
            return this._deliveryAppDbContext.SaveChangesAsync();
        }

       
    }
}
