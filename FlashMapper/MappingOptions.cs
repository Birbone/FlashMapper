using System;
using FlashMapper.Models;

namespace FlashMapper
{
    public static class MappingOptions
    {
        public static TResult Ignore<TResult>()
        {
            throw new Exception("This method is not supposed to be called.");
        }

        public static IgnoreObject Ignore()
        {
            return new IgnoreObject();
        }
    }
}