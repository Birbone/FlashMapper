using System;

namespace FlashMapper.Models
{
    public interface IFlashMapperMappingCallbacks<in TSource, in TDestination>
    {
        Action<TSource, TDestination> AfterMapCallback { get; }
        Action<TSource, TDestination> BeforeMapCallback { get; }
    }
}