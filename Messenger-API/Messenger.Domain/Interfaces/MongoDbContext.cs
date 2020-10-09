using MongoDB.Driver;

namespace Messenger.Domain.Interfaces
{
    public interface IMongoDbContext
    {
        IMongoDatabase GetConnection();
    }
}