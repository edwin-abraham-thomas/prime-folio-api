using core.Database.MongoDB;
using entities;
using Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DataAccess.Repositories
{
    public class ContentRepository : BaseMongoRepository<Content>, IContentRepository
    {
        private readonly ILogger<UserRepository> _logger;

        public ContentRepository(IOptions<MongoConfig> mongoConfig, ILogger<UserRepository> logger) : base(mongoConfig)
        {
            _logger = logger;
        }

        protected override void CreateIndexes()
        {
            var options = new CreateIndexOptions { Unique = true };
            var indexModel = new CreateIndexModel<Content>(Builders<Content>.IndexKeys.Descending(t => t.UserId), options);
            _mongoCollection.Indexes.CreateOne(indexModel);
        }
    }
}
