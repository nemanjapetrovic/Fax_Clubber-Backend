using System;

namespace Clubber.Common.Exceptions.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException()
        {
        }

        public BadRequestException(string message)
        : base(message)
        {
        }

        public BadRequestException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}
