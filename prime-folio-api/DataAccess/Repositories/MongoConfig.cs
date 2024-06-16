using core.Database.MongoDB;

namespace DataAccess.Repositories;

public class MongoConfig : IBaseMongoConfig
{
    public string MongoConnectionString { get; set; }
    public string MongoDatabaseName { get; set; }
}
