namespace FlashMapper
{
    public static class FlashMapperExtensions
    {
        public static IFlashMapperGenericConverter Convert<TSource>(this IMappingConfiguration mappingConfiguration, TSource source)
        {
            return new FlashMapperGenericConverter<TSource>(mappingConfiguration, source);
        }

        public static void CreateMapping<TSource, TDestination>(this IMappingConfiguration mappingConfiguration) where TDestination: new()
        {
            mappingConfiguration.CreateMapping<TSource, TDestination>(s => new TDestination());
        }
    }
}
