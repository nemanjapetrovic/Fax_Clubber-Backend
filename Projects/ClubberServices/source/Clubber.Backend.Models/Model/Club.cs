using MongoDB.Bson;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using Clubber.Common.ValidationAttributes.ValidationAttributes;

namespace Clubber.Backend.Models.Model
{
    public class Club
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The name is required")]
        [StringLength(100, ErrorMessage = "The length of name is over max value")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The city is required")]
        [StringLength(50, ErrorMessage = "The length of city name is over max value")]
        public string City { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The country is required")]
        [StringLength(50, ErrorMessage = "The length of country name is over max value")]
        public string Country { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The postal code is required")]
        [RegularExpression(@"(?i)^[a-z0-9][a-z0-9\- ]{0,10}[a-z0-9]$", ErrorMessage = "Invalid postal code")]
        public string PostalCode { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The address is required")]
        [StringLength(200, ErrorMessage = "The length of address is over max value")]
        public string Address { get; set; }

        [Required(ErrorMessage = "The email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [PhoneNumber]
        public List<string> PhoneNumber { get; set; }

        [Url(ErrorMessage = "Invalid Website url")]
        public string Website { get; set; }

        [Url(ErrorMessage = "Invalid Facebook url")]
        public string Facebook { get; set; }

        [Url(ErrorMessage = "Invalid Instagram url")]
        public string Instagram { get; set; }

        [Url(ErrorMessage = "Invalid Twitter url")]
        public string Twitter { get; set; }

        public Club()
        {
            PhoneNumber = new List<string>();
        }

        /// <summary>
        /// Returns club name without spaces in it.
        /// </summary>
        /// <returns>String: club name without spaces.</returns>
        public string GetNameWithoutSpaces()
        {
            return Regex.Replace(this.Name, @"\s+", "").ToLower();
        }
    }
}
