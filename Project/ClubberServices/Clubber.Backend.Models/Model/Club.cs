using MongoDB.Bson;
using System.Collections.Generic;

namespace Clubber.Backend.Models.Model
{
    public class Club
    {
        //Standard data      
        public ObjectId ID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public IList<string> PhoneNumber { get; set; }
        public IList<string> Email { get; set; }
        public string Image { get; set; }
        public string Website { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }

        //Connections
        public ObjectId ManagerID { get; set; }

        public Club()
        {
            PhoneNumber = new List<string>();
            Email = new List<string>();
        }
    }
}
