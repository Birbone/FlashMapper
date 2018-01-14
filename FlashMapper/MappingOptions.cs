using System;
using FlashMapper.Models;

namespace FlashMapper
{
    /// <summary>
    /// Class that provides some special functions which can be used inside mapping expression.
    /// </summary>
    public static class MappingOptions
    {
        /// <summary>
        /// Allows you to ignore property of type <see cref="TResult" /> in destination model
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        public static TResult Ignore<TResult>()
        {
            throw MethodCallException();
        }

        /// <summary>
        /// Allows you to ignore property of some system type in destination model
        /// </summary>
        public static IgnoreObject Ignore()
        {
            return new IgnoreObject();
        }

        public static bool IsConstruction()
        {
            throw MethodCallException();
        }

        private static Exception MethodCallException()
        {
            return new Exception("This method is not supposed to be called.");
        }
    }
}