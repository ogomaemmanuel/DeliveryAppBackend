using DeliveryAppBackend.Features.Accounts.Models;
using DeliveryAppBackend.Features.Chat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryAppBackend.Hubs
{
    [Authorize]
    public class NotificationHub : Hub<INotificationHubClient>
    {
        private static readonly Dictionary<String, SystemUserViewModel> Users = new Dictionary<String, SystemUserViewModel>();
        public NotificationHub()
        {

        }
        public void RegisterUser(SystemUserViewModel OnlineUser)
        {

            NotificationHub.Users.Add(Context.ConnectionId, OnlineUser);
            UpdateUserList();
        }

    
        public override Task OnConnectedAsync()
        {
            UpdateUserList();
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            if (Users.ContainsKey(Context.ConnectionId))
            {
                Users.Remove(Context.ConnectionId);
                UpdateUserList();
            }
            return base.OnDisconnectedAsync(exception);
        }

        private Task UpdateUserList()
        {
            
            var usersList = Users.Select(x => new
            {
                conectionId = x.Key,
                User = new SystemUserViewModel {
                FirstName=    x.Value.FirstName,
                LastName = x.Value.LastName,
                Email= x.Value.Email,
                Id =x.Value.Id,
                PhoneNumber=x.Value.PhoneNumber,
                UserName=x.Value.UserName,
                },
                IsOnline = true,
            }).ToList();

            return Clients.All.UpdatedUserList(usersList);
        }
    }
}