using MongoDB.Bson;

namespace Clubber.Backend.Models.Model
{
    public class User
    {
        public ObjectId _id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
