using System;
using System.Threading.Tasks;
using Messenger.Domain.Entities;
using Messenger.Domain.Interfaces;
using Messenger.Persistence.Generics.Data;
using Messenger.Persistence.Generics.Interfaces;
using Messenger.Persistence.Repositories.Interfaces.Users;
using MongoDB.Driver;

namespace Messenger.Persistence.Repositories.Entities.Users
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IMongoDatabase _mongoDatabase;

        public UserRepository(IMongoDbContext mongoDbContext) : base(mongoDbContext)
        {}
    }
}


