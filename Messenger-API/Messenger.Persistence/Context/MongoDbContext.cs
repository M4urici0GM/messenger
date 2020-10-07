using System;
using Messenger.Domain.Interfaces;
using MongoDB.Driver;

namespace Messenger.Persistence.Context
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoConnectionOptions _mongoConnectionOptions;
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;
        
        public MongoDbContext(IMongoConnectionOptions mongoConnectionOptions)
        {
            _mongoConnectionOptions = mongoConnectionOptions;
            _mongoClient = new MongoClient(_mongoConnectionOptions.ConnectionString);
            _mongoDatabase = _mongoClient.GetDatabase(_mongoConnectionOptions.DatabaseName);
        }

        public IMongoDatabase GetConnection() => _mongoDatabase;
    }
}