using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Clubber.Backend.Models.LogModels
{
    public enum LogType
    {
        Information = 0,
        Exception = 1
    };

    public class Log
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        [Required(ErrorMessage = "The date is required")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime DateTime { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The method is required")]
        public string Method { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The Log message is required")]
        public string Message { get; set; }

        [Required(ErrorMessage = "The type of log is required")]
        public LogType Type { get; set; }
    }
}
