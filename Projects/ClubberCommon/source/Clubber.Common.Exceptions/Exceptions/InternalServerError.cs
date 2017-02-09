using System;

namespace Clubber.Common.Exceptions.Exceptions
{
    public class InternalServerError : Exception
    {
        public InternalServerError() : base("Internal Server Error, Error 500") { }
    }
}
