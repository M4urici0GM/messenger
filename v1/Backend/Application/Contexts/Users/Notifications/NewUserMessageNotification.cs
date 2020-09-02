using System.Threading;
using System.Threading.Tasks;
using Application.DataTransferObjects;
using MediatR;

namespace Application.Contexts.Users.Notifications
{
    public class NewUserMessageNotification : INotification
    {
        public MessageDto Message { get; set; }
        
        public class NewUserMessageNotificationHandler : INotificationHandler<NewUserMessageNotification>
        {
            public Task Handle(NewUserMessageNotification notification, CancellationToken cancellationToken)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}