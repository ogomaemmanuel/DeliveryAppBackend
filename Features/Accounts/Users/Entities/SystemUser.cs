using DeliveryAppBackend.Features.Chat.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryAppBackend.Features.Accounts.Models
{
    public class SystemUser:IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [InverseProperty("From")]
        public ICollection<ChatMessage> SentMessages { get; set; }
        [InverseProperty("To")]
        public ICollection<ChatMessage> ReceivedMessages { get; set; }
    }
}