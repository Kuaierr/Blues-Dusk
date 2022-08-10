using System;
using System.Runtime.Serialization;

namespace GameKit
{
    [Serializable]
    public class GameKitException : Exception
    {
        public GameKitException() : base()
        {

        }

        public GameKitException(string message) : base(message)
        {

        }

        public GameKitException(string message, Exception innerException) : base(message, innerException)
        {

        }

        protected GameKitException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            
        }
    }
}
