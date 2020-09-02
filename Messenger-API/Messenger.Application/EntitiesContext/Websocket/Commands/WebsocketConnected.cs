using System.Net.WebSockets;
using System.Threading.Tasks;
using MediatR;

namespace Messenger.Application.EntitiesContext.Websocket.Commands
{
    public class WebsocketConnected : IRequest
    {
        public WebSocket WebSocket { get; set; }
        public TaskCompletionSource<object> TaskCompletionSource { get; set; }
    }
}