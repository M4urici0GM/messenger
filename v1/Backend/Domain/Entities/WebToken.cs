using System;

namespace Domain.Entities
{
    public class WebToken
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string RefreshToken { get; set; }
    }
}