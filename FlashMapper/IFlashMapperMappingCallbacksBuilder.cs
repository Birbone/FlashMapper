using System;

namespace FlashMapper
{
    public interface IFlashMapperMappingCallbacksBuilder<out TSource, out TDestination, out TConfigurator> 
        where TConfigurator: IFlashMapperMappingCallbacksBuilder<TSource, TDestination, TConfigurator>
    {
        TConfigurator AfterMap(Action<TSource, TDestination> afterMapCallback);
        TConfigurator BeforeMap(Action<TSource, TDestination> beforeMapCallback);
    }
}