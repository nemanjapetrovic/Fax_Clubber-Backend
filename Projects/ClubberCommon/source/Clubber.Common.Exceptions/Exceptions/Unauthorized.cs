using System;

namespace Clubber.Common.Exceptions.Exceptions
{
    public class Unauthorized : Exception
    {
        public Unauthorized() : base("Unauthorized Error, Error 401") { }
    }
}
