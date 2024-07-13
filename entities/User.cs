using core.Database.MongoDB.Interfaces;

namespace entities
{
    public class User : IMongoDocument
    {
        public string _id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
