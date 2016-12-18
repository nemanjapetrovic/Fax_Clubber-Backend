using MongoDB.Bson;

namespace Clubber.Backend.Models.Model
{
    public class User
    {
        public ObjectId ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
    }
}
