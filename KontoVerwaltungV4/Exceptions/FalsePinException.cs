using System;

namespace KontoVerwaltungV4.Exceptions
{
    /// <summary>
    /// Exception für falsche Pin Eingabe
    /// </summary>
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