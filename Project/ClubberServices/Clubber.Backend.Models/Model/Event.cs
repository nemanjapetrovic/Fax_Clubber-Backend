using MongoDB.Bson;
using System;

namespace Clubber.Backend.Models.Model
{
    public class Event
    {
        //Standrad data        
        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Address { get; set; }
    }
}
