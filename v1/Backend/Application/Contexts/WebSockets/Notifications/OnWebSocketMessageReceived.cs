using System;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Contexts.Users.Notifications;
using Application.DataTransferObjects;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebSocketMessageType = Domain.Enum.WebSocketMessageType;

namespace Application.Contexts.WebSockets.Notifications
{
    public class OnWebSocketMessageReceived : INotification
    {
        public WebSocketReceiveResult ReceivedMessage { get; set; }
        public byte[] Buffer { get; set; }
        public class OnWebSocketMessageReceivedHandler : INotificationHandler<OnWebSocketMessageReceived>
        {
            private readonly ILogger<OnWebSocketMessageReceivedHandler> _logger;
            private readonly IMediator _mediator;

            public OnWebSocketMessageReceivedHandler(ILogger<OnWebSocketMessageReceivedHandler> logger, IMediator mediator)
            {
                _logger = logger;
                _mediator = mediator;
            }
            
            public async Task Handle(OnWebSocketMessageReceived notification, CancellationToken cancellationToken)
            {

                byte[] buffer = HandleMessageBuffer(notification.Buffer);
                string content = Encoding.UTF8.GetString(buffer);
                
                WebSocketMessage webSocketMessage = JsonConvert.DeserializeObject<WebSocketMessage>(content);

                if (webSocketMessage.MessageType == WebSocketMessageType.Message)
                {
                    string messageContent = Encoding.UTF8.GetString(webSocketMessage.Content);
                    MessageDto messageDto = JsonConvert.DeserializeObject<MessageDto>(messageContent);
                    await HandleWebSocketMessage(messageDto);
                    return;
                }
                
                // TODO: Yet have to implement notification type messages.
                return;
            }

            private async Task HandleWebSocketMessage(MessageDto message)
            {
                await _mediator.Publish(new NewUserMessageNotification() {Message = message});
            }
            
            private byte[] HandleMessageBuffer(byte[] buffer)
            {
                return buffer
                    .Where(b => !b.Equals(0))
                    .ToArray();
            }
        }
    }
}