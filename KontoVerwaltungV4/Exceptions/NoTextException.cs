using System;

namespace KontoVerwaltungV4.Exceptions
{
    public class NoTextException : Exception
    {
        public NoTextException()
        {
        }

        public NoTextException(string message)
            : base(message)
        {
        }

        public NoTextException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}