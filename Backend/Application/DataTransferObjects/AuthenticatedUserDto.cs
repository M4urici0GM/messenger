using Domain.Entities;

namespace Application.DataTransferObjects
{
    public class AuthenticatedUserDto
    {
        public UserDto User { get; set; }
        public WebToken WebToken { get; set; }
    }
}