using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Messenger.Domain.Entities;

namespace Messenger.Application.EntitiesContext.Websocket.Commands
{
    public class AcceptWebsocketMessage : IRequest<bool>
    {
        public byte[] Buffer { get; set; }
        public WebsocketUser WebsocketUser { get; set; }

        public AcceptWebsocketMessage()
        {}
        
        public AcceptWebsocketMessage(byte[] buffer, WebsocketUser websocketUser)
        {
            Buffer = buffer;
            WebsocketUser = websocketUser;
        }
        
        public class AcceptWebsocketMessageHandler : IRequestHandler<AcceptWebsocketMessage, bool>
        {
            public async Task<bool> Handle(AcceptWebsocketMessage request, CancellationToken cancellationToken)
            {
                return true;
            }
        }
    }
}