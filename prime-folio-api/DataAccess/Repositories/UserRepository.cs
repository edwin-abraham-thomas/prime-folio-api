using core.Database.MongoDB;
using entities;
using Interfaces.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DataAccess.Repositories
{
    public class UserRepository : BaseMongoRepository<User>, IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;

        private FilterDefinition<User> CreateIdFilter(string id)
        {
            return Builders<User>.Filter.Eq(s => s._id, id);
        }

        public UserRepository(IOptions<MongoConfig> mongoConfig, ILogger<UserRepository> logger) : base(mongoConfig)
        {
            _logger = logger;
        }

        public async Task InsertUserAsync(User user, CancellationToken cancellationToken)
        {
            await _mongoCollection.InsertOneAsync(user, cancellationToken: cancellationToken);
        }

        public async Task<User?> UpdateUserAsync(User user, CancellationToken cancellationToken)
        {
            var updateResult = await _mongoCollection.ReplaceOneAsync(CreateIdFilter(user._id), user);

            if (!updateResult.IsAcknowledged)
            {
                return null;
            }

            var updatedEntity = await GetUserAsync(user._id, cancellationToken);

            return updatedEntity;

        }

        public async Task<User?> GetUserAsync(string userId, CancellationToken cancellationToken)
        {
            var userCursor = await _mongoCollection.FindAsync(CreateIdFilter(userId));

            var user = await userCursor.FirstOrDefaultAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(string userId, CancellationToken cancellationToken)
        {
            var deleteResult = await _mongoCollection.DeleteOneAsync(CreateIdFilter(userId));

            return deleteResult.IsAcknowledged;
        }
    }
}
