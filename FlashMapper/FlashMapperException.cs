using System;

namespace FlashMapper
{
    [Serializable]
    public class FlashMapperException : Exception
    {
        public FlashMapperException() {}
        public FlashMapperException(string message): base(message) {}
        public FlashMapperException(string message, Exception innerException): base(message, innerException) {}
    }
}