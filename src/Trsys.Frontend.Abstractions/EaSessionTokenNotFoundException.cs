using System;
using System.Runtime.Serialization;

namespace Trsys.Frontend.Abstractions
{
    [Serializable]
    public class EaSessionTokenNotFoundException : Exception
    {
        public EaSessionTokenNotFoundException()
        {
        }

        public EaSessionTokenNotFoundException(string message) : base(message)
        {
        }

        public EaSessionTokenNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EaSessionTokenNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}