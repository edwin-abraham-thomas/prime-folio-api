namespace Models
{
    public class Content
    {
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
        public int RowSpan { get; set; }
        public int ColSpan { get; set; }
    }

    public class TileContent
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public ContentType ContentType { get; set; }
    }

    public enum ContentType
    {
        /// <summary>
        /// Header
        /// </summary>
        header = 0,

        /// <summary>
        /// Section
        /// </summary>
        section = 1
    }
}
