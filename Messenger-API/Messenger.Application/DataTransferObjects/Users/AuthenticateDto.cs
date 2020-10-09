using System;

namespace Messenger.Application.DataTransferObjects.Users
{
    public class AuthenticateDto
    {
        public string GrantType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Guid Token { get; set; }
    }
}