namespace Messenger.Domain.Enums
{
    public enum WebsocketMessageType
    {
        Message,
        Acknowledge,
        Request,
        Error,
        Event,
    }
}