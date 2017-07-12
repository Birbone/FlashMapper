using System;

namespace FlashMapper
{
    public class FlashMapperBadInputException: FlashMapperException
    {
        public FlashMapperBadInputException() { }
        public FlashMapperBadInputException(string message): base(message) { }
        public FlashMapperBadInputException(string message, Exception innerException): base(message, innerException) { }
    }
}