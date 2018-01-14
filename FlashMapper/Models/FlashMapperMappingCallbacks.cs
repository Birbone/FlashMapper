using System;

namespace FlashMapper.Models
{
    public class FlashMapperMappingCallbacks<TSource, TDestination> : IFlashMapperMappingCallbacks<TSource, TDestination>
    {
        public FlashMapperMappingCallbacks(Action<TSource, TDestination> afterMapCallback,
            Action<TSource, TDestination> beforeMapCallback)
        {
            AfterMapCallback = afterMapCallback;
            BeforeMapCallback = beforeMapCallback;
        }

        public Action<TSource, TDestination> AfterMapCallback { get; }
        public Action<TSource, TDestination> BeforeMapCallback { get; }
    }
}