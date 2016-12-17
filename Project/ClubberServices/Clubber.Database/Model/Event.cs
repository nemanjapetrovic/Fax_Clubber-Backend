﻿using MongoDB.Bson;
using System;

namespace Clubber.Database.Model
{
    public class Event
    {
        //Standrad data        
        public ObjectId ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Address { get; set; }

        //Connections
        public ObjectId ClubID { get; set; }
    }
}
