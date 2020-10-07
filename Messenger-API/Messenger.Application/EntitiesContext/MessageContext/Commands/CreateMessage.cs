using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Messenger.Application.Extensions;
using Messenger.Application.Hubs;
using Messenger.Application.Interfaces.Services;
using Messenger.Domain.Entities;
using Messenger.Domain.Enums;
using Messenger.Domain.Structs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace Messenger.Application.EntitiesContext.MessageContext.Commands
{
    public class CreateMessage : IRequest
    {
        public string CurrentConnectionId { get; set; }
        public Guid ToUserId { get; set; }
        public string Content { get; set; }
        public string ContentType { get; set; }
        public DateTime DateSent { get; set; }

        public class CreateMessageHandler : IRequestHandler<CreateMessage>
        {
            private readonly HttpContext _httpContext;
            private readonly IHubContext<WebsocketHub> _webSocketHub;
            private readonly IWebSocketManager _webSocketManager;
            private readonly IMapper _mapper;

            public CreateMessageHandler(IHubContext<WebsocketHub> webSocketHub,
                IHttpContextAccessor httpContextAccessor, IWebSocketManager webSocketManager, IMapper mapper)
            {
                _httpContext = httpContextAccessor.HttpContext;
                _webSocketHub = webSocketHub;
                _webSocketManager = webSocketManager;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(CreateMessage request, CancellationToken cancellationToken)
            {
                Guid guid =_httpContext.User.Identity.GetUserId() ?? Guid.Empty;
                if (guid == Guid.Empty)
                {
                    await _webSocketHub.Clients.Client(request.CurrentConnectionId)
                        .SendCoreAsync(WebSocketEvents.ForceDisconnect, new object[]
                        {
                            WebsocketMessageType.Error,
                            new WebSocketError() { Message = "User not authenticated"}, 
                        }, cancellationToken);
                    
                    return Unit.Value;
                }

                IEnumerable<WebsocketUser> websocketUser = _webSocketManager.GetClients(request.ToUserId);

                Message message = _mapper.Map<Message>(request);
                message.UserId = guid;

                await _webSocketHub.Clients.Clients(websocketUser
                        .Select(x => x.ConnectionId)
                        .ToList())
                    .SendCoreAsync(WebSocketEvents.NewMessage, new object[]
                    {
                        WebsocketMessageType.Message,
                        message
                    }, cancellationToken);

                return Unit.Value;
            }
        }
    }
}