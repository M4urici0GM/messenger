using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;

namespace Application.Contexts.Users.Notifications
{
    public class UserCreated : INotification
    {
        public User User { get; set; }
        
        public class UserCreatedHandler : INotificationHandler<UserCreated>
        {
            public UserCreatedHandler()
            {
                
            }
            
            public Task Handle(UserCreated notification, CancellationToken cancellationToken)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}