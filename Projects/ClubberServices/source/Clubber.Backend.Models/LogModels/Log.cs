using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Clubber.Backend.Models.LogModels
{
    public class Log
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        [Required(ErrorMessage = "The date is required")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime DateTime { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The URL is required")]
        public string URL { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The Log message is required")]
        public string LogMessage { get; set; }
    }
}
