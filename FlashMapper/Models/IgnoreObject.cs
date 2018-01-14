using System;

namespace FlashMapper.Models
{
    public class IgnoreObject
    {
        private static Exception MethodCallException()
        {
            return new Exception("This method is not supposed to be called.");
        }

        public static implicit operator string(IgnoreObject ignoreObject)
        {
            throw MethodCallException();
        }

        public static implicit operator bool(IgnoreObject ignoreObject)
        {
            throw MethodCallException();
        }

        public static implicit operator byte(IgnoreObject ignoreObject)
        {
            throw MethodCallException();
        }

        public static implicit operator ushort(IgnoreObject ignoreObject)
        {
            throw MethodCallException();
        }

        public static implicit operator Guid(IgnoreObject ignoreObject)
        {
            throw MethodCallException();
        }

        public static implicit operator DateTime(IgnoreObject ignoreObject)
        {
            throw MethodCallException();
        }

        public static implicit operator DateTimeOffset(IgnoreObject ignoreObject)
        {
            throw MethodCallException();
        }

        public static implicit operator TimeSpan(IgnoreObject ignoreObject)
        {
            throw MethodCallException();
        }

        public TResult As<TResult>()
        {
            throw MethodCallException();
        }
    }
}