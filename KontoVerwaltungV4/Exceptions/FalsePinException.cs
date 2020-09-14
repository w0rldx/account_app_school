using System;

namespace KontoVerwaltungV4.Exceptions
{
    public class FalsePinException : Exception
    {
        public FalsePinException()
        {
        }

        public FalsePinException(string message)
            : base(message)
        {
        }

        public FalsePinException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}