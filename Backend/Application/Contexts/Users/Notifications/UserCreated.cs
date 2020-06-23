using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Interfaces.Configurations;
using Domain.Entities;
using MediatR;

namespace Application.Contexts.Users.Notifications
{
    public class UserCreated : INotification
    {
        public User User { get; set; }
        
        public class UserCreatedHandler : INotificationHandler<UserCreated>
        {

            private readonly IMainDbContext _dbContext;
            private readonly IAppConfiguration _appConfiguration;
            
            public UserCreatedHandler(IMainDbContext dbContext, IAppConfiguration configuration)
            {
                _dbContext = dbContext;
                _appConfiguration = configuration;
            }
            
            public async Task Handle(UserCreated notification, CancellationToken cancellationToken)
            {
                VerificationToken verificationToken = new VerificationToken
                {
                    Token = Guid.NewGuid(),
                    UserId = notification.User.Id,
                    ExpirationDate = DateTime.Now.AddSeconds(_appConfiguration.VerificationTokenExpiresIn),
                };
                await _dbContext.VerificationTokens.AddAsync(verificationToken, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}