using System;
using System.Runtime.Serialization;

namespace Trsys.Frontend.Domain
{
    [Serializable]
    public class EaOrderTextFormatException : Exception
    {
        public EaOrderTextFormatException()
        {
        }

        public EaOrderTextFormatException(string message) : base(message)
        {
        }

        public EaOrderTextFormatException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EaOrderTextFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}