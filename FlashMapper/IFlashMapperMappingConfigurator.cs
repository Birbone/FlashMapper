namespace FlashMapper
{
    public interface IFlashMapperMappingConfigurator<out TSource, out TDestination> : 
        IFlashMapperSettingsBuilder<IFlashMapperMappingConfigurator<TSource, TDestination>>,
        IFlashMapperMappingCallbacksBuilder<TSource, TDestination, IFlashMapperMappingConfigurator<TSource, TDestination>>, 
        IFlashMapperCustomServiceBuilder<IFlashMapperMappingConfigurator<TSource, TDestination>>
    {
    }
}