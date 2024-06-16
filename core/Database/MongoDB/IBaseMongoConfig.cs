namespace core.Database.MongoDB;

public interface IBaseMongoConfig
{
    public string MongoConnectionString { get; set; }
    public string MongoDatabaseName { get; set; }
}
