namespace FlashMapper
{
    public class FlashMapperGenericConverter<TSource> : IFlashMapperGenericConverter
    {
        private readonly IMappingConfiguration mappingConfiguration;
        private readonly TSource source;

        public FlashMapperGenericConverter(IMappingConfiguration mappingConfiguration, TSource source)
        {
            this.mappingConfiguration = mappingConfiguration;
            this.source = source;
        }

        public TDestination To<TDestination>()
        {
            return mappingConfiguration.Convert<TSource, TDestination>(source);
        }
    }
}