using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliveryAppBackend.Features.Chat.Models;
using DeliveryAppBackend.Features.Chat.Services;
using DeliveryAppBackend.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DeliveryAppBackend.Features.Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        public ChatController(IChatService chatService)
        {
             _chatService = chatService;

        }
        public async Task<IActionResult> Post(IncommingChatMessageViewModel chatMessageViewModel) {

          var sentMessage=  await this._chatService.SendMessage(chatMessageViewModel);
            return new OkObjectResult(sentMessage);
        }
    }
}