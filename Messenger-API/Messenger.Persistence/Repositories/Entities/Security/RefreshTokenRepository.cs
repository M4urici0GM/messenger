using Messenger.Domain.Entities;
using Messenger.Domain.Interfaces;
using Messenger.Persistence.Generics.Data;
using Messenger.Persistence.Generics.Interfaces;
using Messenger.Persistence.Repositories.Interfaces.Security;

namespace Messenger.Persistence.Repositories.Entities.Security
{
    public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(IMongoDbContext mongoDbContext) : base(mongoDbContext)
        {}
    }
}