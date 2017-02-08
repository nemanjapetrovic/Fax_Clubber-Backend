using MongoDB.Bson;
using System;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace Clubber.Backend.Models.Model
{
    public class Event
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The name is required")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The description is required")]
        [StringLength(5000, ErrorMessage = "The length of description is over max value")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The start date is required")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime StartDateTime { get; set; }

        [Required(ErrorMessage = "The end date is required")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime EndDateTime { get; set; }

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
