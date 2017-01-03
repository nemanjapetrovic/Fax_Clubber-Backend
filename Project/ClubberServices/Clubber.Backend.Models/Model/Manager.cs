using MongoDB.Bson;
using System.Collections.Generic;

namespace Clubber.Backend.Models.Model
{
    public class Manager
    {
        //Standard data
        public ObjectId _id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
    }
}
