using MongoDB.Bson;
using System;
using System.Text.RegularExpressions;

namespace Clubber.Backend.Models.Model
{
    public class Event
    {
        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
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
