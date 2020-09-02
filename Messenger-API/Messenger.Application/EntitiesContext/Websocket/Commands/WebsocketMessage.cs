using MediatR;

namespace Messenger.Application.EntitiesContext.Websocket.Commands
{
    public class WebsocketMessage : IRequest
    {
        public byte[] Buffer { get; set; }
    }
}