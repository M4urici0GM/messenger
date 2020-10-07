using Messenger.Domain.Entities;

namespace Messenger.Application.DataTransferObjects.Users
{
    public class AuthenticatedUserDto
    {
        public UserDto User { get; set; }
        public WebToken Token { get; set; }
    }
}