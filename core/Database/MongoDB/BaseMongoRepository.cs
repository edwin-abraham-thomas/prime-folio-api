using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Microsoft.Extensions.Options;
using core.Database.MongoDB.Interfaces;
using System.Linq.Expressions;

namespace core.Database.MongoDB;

public abstract class BaseMongoRepository<TDocument> : IBaseMongoRepository<TDocument> where TDocument : IMongoDocument
{
    protected readonly IMongoClient _mongoClient;
    protected readonly IMongoDatabase _mongoDb;
    protected readonly IMongoCollection<TDocument> _mongoCollection;

    protected ClusteredIndexOptions<TDocument>? _clusteredIndexOptions;

    protected BaseMongoRepository(IOptions<IBaseMongoConfig> mongoConfig)
    {
        _mongoClient = new MongoClient(mongoConfig.Value.MongoConnectionString);
        _mongoDb = _mongoClient.GetDatabase(mongoConfig.Value.MongoDatabaseName);
        _mongoCollection = _mongoDb.GetCollection<TDocument>(typeof(TDocument).Name.ToLower());
        CreateIndexes();
    }

    protected virtual void CreateIndexes()
    {

    }

    private FilterDefinition<TDocument> CreateIdFilter(string id)
    {
        return Builders<TDocument>.Filter.Eq(s => s._id, id);
    }

    public async Task InsertAsync(TDocument entity, CancellationToken cancellationToken)
    {
        await _mongoCollection.InsertOneAsync(entity, cancellationToken: cancellationToken);
    }

    public async Task<TDocument> GetAsync(string id, CancellationToken cancellationToken)
    {
        var userCursor = await _mongoCollection.FindAsync(CreateIdFilter(id));

        var user = await userCursor.FirstOrDefaultAsync();
        return user;
    }

    public async Task<TDocument> UpdateAsync(TDocument entity, CancellationToken cancellationToken)
    {
        var updateResult = await _mongoCollection.ReplaceOneAsync(CreateIdFilter(entity._id), entity);

        if (!updateResult.IsAcknowledged || updateResult.MatchedCount == 0)
        {
            throw new KeyNotFoundException($"{entity._id} not found");
        }

        var updatedEntity = await GetAsync(entity._id, cancellationToken);

        return updatedEntity;
    }

    public async Task<TDocument> UpdateAsync(TDocument entity, FilterDefinition<TDocument> documentIdentifierFilterDefinition, CancellationToken cancellationToken)
    {
        var updateResult = await _mongoCollection.ReplaceOneAsync(documentIdentifierFilterDefinition, entity);

        if (!updateResult.IsAcknowledged || updateResult.MatchedCount == 0)
        {
            throw new KeyNotFoundException();
        }

        var updatedEntity = await GetAsync(entity._id, cancellationToken);

        return updatedEntity;
    }

    public async Task<bool> DeleteAsync(string id, CancellationToken cancellationToken)
    {
        var deleteResult = await _mongoCollection.DeleteOneAsync(CreateIdFilter(id));

        return deleteResult.IsAcknowledged;
    }

    public async Task<bool> DeleteAsync(FilterDefinition<TDocument> filterDefinition, CancellationToken cancellationToken)
    {
        var deleteResult = await _mongoCollection.DeleteOneAsync(filterDefinition, cancellationToken);

        return deleteResult.IsAcknowledged;
    }

    public async Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression, CancellationToken cancellationToken)
    {
        var document = await _mongoCollection.AsQueryable()
            .Where(filterExpression)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        return document;
    }
}
