// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Reflection;
// using System.Text;
// using System.Threading;
// using System.Threading.Tasks;
// using AutoMapper.Internal;
// using MediatR;
// using Messenger.Application.DataTransferObjects.Users;
// using Messenger.Application.EntitiesContext.UserContext.Commands;
// using Messenger.Application.Extensions;
// using Messenger.Domain.Entities;
// using Microsoft.AspNetCore.Server.HttpSys;
// using Newtonsoft.Json;
//
// namespace Messenger.Application.EntitiesContext.Websocket.Commands
// {
//     public class AcceptWebsocketMessage : IRequest<object>
//     {
//         public byte[] Buffer { get; set; }
//
//         public AcceptWebsocketMessage(byte[] buffer)
//         {
//             Buffer = buffer;
//         }
//
//         public class AcceptWebsocketMessageHandler : IRequestHandler<AcceptWebsocketMessage, object>
//         {
//             private readonly IMediator _mediator;
//
//             public AcceptWebsocketMessageHandler(IMediator mediator)
//             {
//                 _mediator = mediator;
//             }
//             
//             public async Task<object> Handle(AcceptWebsocketMessage request, CancellationToken cancellationToken)
//             {
//                 
//                 WebsocketMessage websocketMessage =
//                     JsonConvert.DeserializeObject<WebsocketMessage>(Encoding.UTF8.GetString(request.Buffer));
//                 
//                 string type = websocketMessage.Type;
//
//                 switch (type)
//                 {
//                     case nameof(GetUser):
//                         string value = websocketMessage.Params.GetValueOrDefault("UserId") as string;
//                         if (value == null)
//                             throw new Exception();
//                         
//                         Guid guid = Guid.Parse(value);
//                         UserDto user = await _mediator.Send(new GetUser { Id = guid });
//                         return user;
//                 }
//                 
//                 // if (string.IsNullOrEmpty(type))
//                 //     throw new InvalidOperationException("Missing message type");
//                 //
//                 // Type assemblyType = Assembly.GetExecutingAssembly().GetTypes()
//                 //     .FirstOrDefault(x => x.Name == type);
//                 //
//                 //
//                 // if (assemblyType == null || !assemblyType.ImplementsGenericInterface(typeof(IRequest<>)))
//                 //     throw new ApplicationException("Request Type not found.");
//                 //
//                 // IBaseRequest requestInstance = (IBaseRequest) Activator.CreateInstance(assemblyType);
//                 //
//                 // if (requestInstance == null)
//                 //     throw new Exception("Failed at creating request type instance.");
//                 //
//                 // requestInstance.MapParams(websocketMessage.Params);
//                 // object result = await _mediator.Send(requestInstance, cancellationToken);
//                 //
//                 return null;
//             }
//         }
//     }
// }