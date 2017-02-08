using MongoDB.Bson;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

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
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid postal code")]
        public string PostalCode { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The address is required")]
        [StringLength(200, ErrorMessage = "The length of address is over max value")]
        public string Address { get; set; }

        [Required(ErrorMessage = "The email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        //[RegularExpression(@"^\(?(?P<prefix>(?=1)|\+|(?:0(?:0(?:0|1|9)?|1(?:0|1))?|119))[-. ]?\(?(?P<CC>1([-. ]?)[0-9]{3}|2(?:0|[0-9]{2})|3(?:[0-469]|[0-9]{2})|4(?:[013-9]|[0-9]{2})|5(?:[1-8]|[0-9]{2})|6(?:[0-6]|[0-9]{2})|7(?:[-. ]?[67]|[0-9]{3})|8(?:[1246]|[0-9]{2})|9(?:[0-58]|[0-9]{2}))(?:\)?[-. ])?(?P<number>(?:[0-9]+[-. ]?)+)$", ErrorMessage = "Invalid phone number")]
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
