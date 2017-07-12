using System;

namespace FlashMapper
{
    public class PropertyIsNotMappedException : FlashMapperException
    {
        public PropertyIsNotMappedException(string propertyName)
        {
            PropertyName = propertyName;
        }

        public PropertyIsNotMappedException(string propertyName, string message) : base(message)
        {
            PropertyName = propertyName;
        }

        public PropertyIsNotMappedException(string propertyName, string message, Exception innerException)
            : base(message, innerException)
        {
            PropertyName = propertyName;
        }

        public string PropertyName { get; }
    }
}