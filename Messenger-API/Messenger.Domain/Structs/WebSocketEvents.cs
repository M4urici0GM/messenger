namespace Messenger.Domain.Structs
{
    public struct WebSocketEvents
    {
        public const string GetUser = "GetUser";
        public const string AuthenticateWebsocket = "AuthenticateWebsocket";
        public const string NewMessage = "newMessage";
        public const string ForceDisconnect = "forceDisconnect";
    }
}