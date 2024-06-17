namespace Models
{
    public class User
    {
        public string _id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Content Content { get; set; }
    }

    public class Content
    {
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
