using System;

namespace KontoVerwaltungV4.Exceptions
{
    public class IsEmptyException : Exception
    {
        public IsEmptyException()
        {
        }

        public IsEmptyException(string message)
            : base(message)
        {
        }

        public IsEmptyException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}