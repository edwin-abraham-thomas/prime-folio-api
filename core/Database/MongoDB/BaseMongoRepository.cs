using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace core.Database.MongoDB;

public abstract class BaseMongoRepository<TDocument> where TDocument : class
{
    protected readonly IMongoClient _mongoClient;
    protected readonly IMongoDatabase _mongoDb;
    protected readonly IMongoCollection<TDocument> _mongoCollection;

    protected BaseMongoRepository(IOptions<IBaseMongoConfig> mongoConfig)
    {
        _mongoClient = new MongoClient(mongoConfig.Value.MongoConnectionString);
        _mongoDb = _mongoClient.GetDatabase(mongoConfig.Value.MongoDatabaseName);
        _mongoCollection = _mongoDb.GetCollection<TDocument>(typeof(TDocument).Name.ToLower());
    }
}
