using System;
using System.Threading;
using System.Threading.Tasks;
using Messenger.Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Messenger.Persistence.Generics.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        Task<T> Add(T entity, CancellationToken cancellationToken);
        Task<T> Get(Guid id, CancellationToken cancellationToken);
        Task<T> Add(T entity);
        Task<T> Get(Guid id);
        Task<bool> Delete(Guid id);
        Task<bool> Delete(Guid id, CancellationToken cancellationToken);
        Task<T> Update(T entity);
        Task<T> Update(T entity, CancellationToken cancellationToken);
        Task<T> Get(FilterDefinition<T> filterDefinition, CancellationToken cancellationToken);
        Task Delete(FilterDefinition<T> filter, CancellationToken cancellationToken);
    }
}