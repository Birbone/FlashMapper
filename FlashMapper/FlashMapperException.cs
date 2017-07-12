using System;

namespace FlashMapper
{
    public class FlashMapperException : Exception
    {
        public FlashMapperException() {}
        public FlashMapperException(string message): base(message) {}
        public FlashMapperException(string message, Exception innerException): base(message, innerException) {}
    }
}