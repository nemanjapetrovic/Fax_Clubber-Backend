using MongoDB.Bson;
using System;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace Clubber.Backend.Models.Model
{
    public class Event
    {
        public ObjectId _id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The name is required")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The description is required")]
        [StringLength(5000, ErrorMessage = "The length of description is over max value")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The start date is required")]
        public DateTime StartDateTime { get; set; }

        [Required(ErrorMessage = "The end date is required")]
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
