using System;

namespace Clubber.Common.Exceptions.Exceptions
{
    public class BadRequest : Exception
    {
        public BadRequest() : base("Bad Request, Error 400") { }
    }
}
