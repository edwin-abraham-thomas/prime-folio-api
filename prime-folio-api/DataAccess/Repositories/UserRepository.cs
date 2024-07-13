using core.Database.MongoDB;
using entities;
using Interfaces.Repositories;
using Microsoft.Extensions.Options;

namespace DataAccess.Repositories
{
    public class UserRepository : BaseMongoRepository<User>, IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(IOptions<MongoConfig> mongoConfig, ILogger<UserRepository> logger) : base(mongoConfig)
        {
            _logger = logger;
        }
    }
}
