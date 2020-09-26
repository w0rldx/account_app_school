using System;

namespace KontoVerwaltungV4.Exceptions
{
    /// <summary>
    /// Exception für Keine Text Eingabe
    /// </summary>
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