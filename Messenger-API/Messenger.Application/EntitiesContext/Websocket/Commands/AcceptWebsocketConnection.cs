// using System;
// using System.Linq;
// using System.Net.Mime;
// using System.Net.WebSockets;
// using System.Text;
// using System.Threading;
// using System.Threading.Tasks;
// using MediatR;
// using Messenger.Application.Interfaces;
// using Messenger.Domain.Entities;
// using Messenger.Domain.Enums;
// using Messenger.Domain.Interfaces;
// using Microsoft.EntityFrameworkCore;
// using Newtonsoft.Json;
//
// namespace Messenger.Application.EntitiesContext.Websocket.Commands
// {
//     public class AcceptWebsocketConnection : IRequest
//     {
//         public WebSocket WebSocket { get; set; }
//         public Guid UserId { get; set; }
//         public TaskCompletionSource<object> TaskCompletionSource { get; set; }
//         
//         protected class WebsocketConnectedHandler : IRequestHandler<AcceptWebsocketConnection>
//         {
//
//             private readonly IWebsocketManager _websocketManager;
//             private readonly IMainDbContext _mainDb;
//             private readonly IMediator _mediator;
//             
//             public WebsocketConnectedHandler(IWebsocketManager websocketManager, IMainDbContext dbContext, IMediator mediator)
//             {
//                 _websocketManager = websocketManager;
//                 _mainDb = dbContext;
//                 _mediator = mediator;
//             }
//             
//             public async Task<Unit> Handle(AcceptWebsocketConnection request, CancellationToken cancellationToken)
//             {
//                 User user = await _mainDb.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
//
//                 if (user == null)
//                     throw new InvalidOperationException("User not found");
//
//                 WebsocketUser websocketUser = new WebsocketUser()
//                 {
//                     WebSocket = request.WebSocket,
//                 };
//                 
//                 Guid webSocketId = _websocketManager.AddWebsocket(websocketUser);
//
//                 while (request.WebSocket.State == WebSocketState.Open)
//                 {
//                     byte[] buffer = new byte[4096];
//                     var result = await request.WebSocket.ReceiveAsync(buffer, cancellationToken);
//                     if (result.Count == 0)
//                         continue;
//
//                     string parsedBuffer = Encoding.UTF8.GetString(buffer);
//                     WebsocketMessage receivedMessage = JsonConvert.DeserializeObject<WebsocketMessage>(parsedBuffer);
//                     
//                     try
//                     {
//                         bool messageSent = true;
//
//                         if (!messageSent)
//                             continue;
//
//                         WebsocketMessage websocketMessage = new WebsocketMessage
//                         {
//                             MessageType = WebsocketMessageType.Acknowledge,
//                             RequestId = receivedMessage.RequestId,
//                         };
//                     
//                         byte[] message = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(websocketMessage));
//                     
//                         await _websocketManager.SendMessage(message, webSocketId, cancellationToken);
//                     }
//                     catch (Exception e)
//                     {
//                         WebsocketMessage websocketMessage = new WebsocketMessage
//                         {
//                             MessageType = WebsocketMessageType.Error,
//                             RequestId = receivedMessage.RequestId,
//                             Content = e.Message,
//                             ContentType = "plain/text"
//                         };
//                     
//                         byte[] message = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(websocketMessage));
//                     
//                         await _websocketManager.SendMessage(message, webSocketId, cancellationToken);
//                     }
//                 }
//
//                 await _websocketManager.CloseWebsocket(user, cancellationToken);
//
//                 request.TaskCompletionSource.SetResult(true);
//                 return Unit.Value;
//             }
//         }
//     }
// }