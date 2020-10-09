using System;

namespace Messenger.Domain.Entities
{
    public class WebToken
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}