using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using core.Database.MongoDB.Interfaces;

namespace entities
{
    public class Content : IMongoDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string UserId { get; set; }
        public Dimension Dimension { get; set; }
        public List<Tile> Tiles { get; set; }
    }

    public class Tile
    {
        public TileContent Content { get; set; }
        public Dimension Dimension { get; set; }
    }

    public class Dimension
    {
        public int Rowspan { get; set; }
        public int Colspan { get; set; }
    }

    public class TileContent
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ContentType ContentType { get; set; }
    }

    public enum ContentType
    {
        header,
        section
    }

}
