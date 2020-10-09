namespace Messenger.Domain.Interfaces
{
    public interface IMongoConnectionOptions
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}