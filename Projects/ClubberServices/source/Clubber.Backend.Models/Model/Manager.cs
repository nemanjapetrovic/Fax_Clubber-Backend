using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Clubber.Backend.Models.Model
{
    public class Manager
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The password is required")]
        [RegularExpression(@"^((?=\S*?[A-Z])(?=\S*?[a-z])(?=\S*?[0-9]).{6,})\S$", ErrorMessage = "Invalid password")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The first name is required")]
        [StringLength(50, ErrorMessage = "The length of first name is over max value")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The last name is required")]
        [StringLength(50, ErrorMessage = "The length of last name is over max value")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Name of the city is required")]
        [StringLength(50, ErrorMessage = "The length of city name is over max value")]
        public string City { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The country name is required")]
        [StringLength(50, ErrorMessage = "The length of country name is over max value")]
        public string Country { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The postal code is required")]
        [RegularExpression(@"(?i)^[a-z0-9][a-z0-9\- ]{0,10}[a-z0-9]$", ErrorMessage = "Invalid postal code")]
        public string PostalCode { get; set; }
    }
}
