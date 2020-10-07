using System;
using System.Threading.Tasks;
using MediatR;
using Messenger.Application.EntitiesContext.MessageContext.Commands;
using Messenger.Application.Extensions;
using Messenger.Application.Interfaces.Services;
using Messenger.Domain.Entities;
using Messenger.Domain.Structs;
using Messenger.Persistence.Repositories.Interfaces.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Messenger.Application.Hubs
{
    [Authorize]
    public class WebsocketHub : Hub
    {
        
        private readonly IWebSocketManager _webSocketManager;
        private readonly IMediator _mediator;

            public WebsocketHub(IWebSocketManager webSocketManager, IMediator mediator)
        {
            _webSocketManager = webSocketManager;
            _mediator = mediator;
        }
            

        public override Task OnConnectedAsync()
        {
            _webSocketManager.AddClient(new WebsocketUser
            {
                ConnectionId = Context.ConnectionId,
                UserId = Context.User.Identity.GetUserId() ?? Guid.Empty,
                UserIdentifier = Context.UserIdentifier,
            });
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _webSocketManager.RemoveClient(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }


        public async Task SendMessage(CreateMessage request)
        {
            request.CurrentConnectionId = Context.ConnectionId;
            await _mediator.Send(request);
        }
    }
}