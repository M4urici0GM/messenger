using System;

namespace Application.DataTransferObjects
{
    public class MessageDto
    {
        public string Content { get; set; }
        public string MimeType { get; set; }
        public bool IsSeen { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}