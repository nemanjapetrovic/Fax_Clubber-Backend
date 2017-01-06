using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clubber.Backend.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Click enter to add club");
            Console.ReadLine();
            MongoTest mongo = new MongoTest();
            //mongo.AddClub();
            //mongo.DeleteClub();
            mongo.UpdateClub();
        }
    }
}
