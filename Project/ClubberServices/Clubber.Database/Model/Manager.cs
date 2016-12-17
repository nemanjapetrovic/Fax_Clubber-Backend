﻿using MongoDB.Bson;
using System.Collections.Generic;

namespace Clubber.Database.Model
{
    public class Manager
    {
        //Standard data
        public ObjectId ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }

        //Connections
        public IList<ObjectId> ClubID { get; set; }

        public Manager()
        {
            ClubID = new List<ObjectId>();
        }
    }
}
