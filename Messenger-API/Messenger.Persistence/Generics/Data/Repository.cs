using System;
using System.Collections.Immutable;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Messenger.Domain.Interfaces;
using Messenger.Persistence.Generics.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Messenger.Persistence.Generics.Data
{
    public class Repository<T> : IRepository<T> where T : IEntity
    {
        private readonly IMongoDbContext _mongoDbContext;
        private readonly IMongoDatabase _database;
        
        public Repository(IMongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
            _database = mongoDbContext.GetConnection();
        }

        public async Task<T> Add(T entity, CancellationToken cancellationToken)
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            await _database.GetCollection<T>(typeof(T).Name)
                .InsertOneAsync(entity, cancellationToken: cancellationToken);

            return entity;
        }

        public async Task<T> Get(FilterDefinition<T> filterDefinition, CancellationToken cancellationToken)
        {
            var result = await _database.GetCollection<T>(typeof(T).Name)
                .FindAsync(filterDefinition, cancellationToken: cancellationToken);

            return await result.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task Delete(FilterDefinition<T> filter, CancellationToken cancellationToken)
        {
            await _database.GetCollection<T>(typeof(T).Name)
                .DeleteManyAsync(filter, cancellationToken);
        }

        public async Task<T> Get(Guid id, CancellationToken cancellationToken)
        {
            var result = await _database.GetCollection<T>(typeof(T).Name)
                .FindAsync(entity => entity.Id == id, cancellationToken: cancellationToken);

            return await result.FirstOrDefaultAsync(cancellationToken);
        }
        
        public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
        {

            FilterDefinition<T> filterDefinition = Builders<T>.Filter
                .Eq(x => x.Id, id);

            await _database.GetCollection<T>(typeof(T).Name)
                .DeleteOneAsync(filterDefinition, cancellationToken);

            return true;
        }

        public async Task<T> Update(T entity, CancellationToken cancellationToken)
        {
            T currentEntity = await Get(entity.Id, cancellationToken);
            UpdateDefinitionBuilder<T> updateBuilder = Builders<T>.Update;
            FilterDefinition<T> filter = Builders<T>.Filter
                .Eq(x => x.Id, entity.Id);

            PropertyInfo[] tProperties = typeof(T).GetProperties();
            PropertyInfo[] cTPropertyInfos = currentEntity.GetType().GetProperties();
            
            if (tProperties.Length != cTPropertyInfos.Length)
                throw new Exception("Invalid entity type");

            UpdateDefinition<T> updateDefinition = null;
            
            foreach (PropertyInfo property in tProperties)
            {
                var currentValue = property.GetValue(currentEntity);
                var newValue = property.GetValue(entity);

                if (currentValue == newValue)
                    continue;

                updateDefinition = updateBuilder.Set(property.Name, newValue);
            }

            entity.UpdatedAt = DateTime.UtcNow;

            if (updateDefinition == null)
                return entity;

            await _database.GetCollection<T>(typeof(T).Name)
                .UpdateOneAsync(filter, updateDefinition, new UpdateOptions(), cancellationToken);
            
            return entity;
        }
        
        public Task<T> Add(T entity) => Add(entity, CancellationToken.None);
        public Task<T> Get(Guid id) => Get(id, CancellationToken.None);
        public Task<T> Update(T entity) => Update(entity, CancellationToken.None);
        public Task<bool> Delete(Guid id) => Delete(id, CancellationToken.None);
    }
}