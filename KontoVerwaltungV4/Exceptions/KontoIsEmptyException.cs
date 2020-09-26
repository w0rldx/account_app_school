using System;

namespace KontoVerwaltungV4.Exceptions
{
    /// <summary>
    /// Exception für Konto ist Leer
    /// </summary>
    public class KontoIsEmptyException : Exception
    {
        public KontoIsEmptyException()
        {
        }

        public KontoIsEmptyException(string message)
            : base(message)
        {
        }

        public KontoIsEmptyException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}