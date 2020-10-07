using Messenger.Domain.Interfaces;

namespace Messenger.Persistence.Options
{
    public class MongoConnectionOptions : IMongoConnectionOptions
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}