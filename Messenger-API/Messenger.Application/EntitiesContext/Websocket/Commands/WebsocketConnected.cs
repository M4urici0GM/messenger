using System;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Messenger.Application.Interfaces;
using Messenger.Domain.Entities;
using Messenger.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Application.EntitiesContext.Websocket.Commands
{
    public class WebsocketConnected : IRequest
    {
        public WebSocket WebSocket { get; set; }
        public Guid UserId { get; set; }
        public TaskCompletionSource<object> TaskCompletionSource { get; set; }
        
        protected class WebsocketConnectedHandler : IRequestHandler<WebsocketConnected>
        {

            private readonly IWebsocketManager _websocketManager;
            private readonly IMainDbContext _mainDb;
            
            public WebsocketConnectedHandler(IWebsocketManager websocketManager, IMainDbContext dbContext)
            {
                _websocketManager = websocketManager;
                _mainDb = dbContext;
            }
            
            public async Task<Unit> Handle(WebsocketConnected request, CancellationToken cancellationToken)
            {
                User user = await _mainDb.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

                if (user == null)
                    throw new InvalidOperationException("User not found");
                
                _websocketManager.AddWebsocket(new WebsocketUser
                {
                    User = user,
                    WebSocket = request.WebSocket,
                });
                
                return Unit.Value;
            }
        }
    }
}