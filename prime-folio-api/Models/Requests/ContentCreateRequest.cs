using System.ComponentModel.DataAnnotations;

namespace Models.Requests
{
    public class ContentCreateRequest
    {
        [Required]
        public string UserId { get; set; }
        public Dimension Dimension { get; set; }
        public List<Tile> Tiles { get; set; }
    }
}
