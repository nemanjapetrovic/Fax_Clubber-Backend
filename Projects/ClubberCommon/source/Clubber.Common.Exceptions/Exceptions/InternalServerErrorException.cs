﻿using System;

namespace Clubber.Common.Exceptions.Exceptions
{
    public class InternalServerErrorException : Exception
    {
        public InternalServerErrorException()
        {
        }

        public InternalServerErrorException(string message)
        : base(message)
        {
        }

        public InternalServerErrorException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}
