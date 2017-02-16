using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Clubber.Backend.Models.DomainModels
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The username is required")]
        [StringLength(50, ErrorMessage = "The length of username is over max value")]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The first name is required")]
        [StringLength(50, ErrorMessage = "The length of first name is over max value")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The last name is required")]
        [StringLength(50, ErrorMessage = "The length of last name is over max value")]
        public string LastName { get; set; }
    }
}
