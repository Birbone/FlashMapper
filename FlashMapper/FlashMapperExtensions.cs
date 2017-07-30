namespace FlashMapper
{
    public static class FlashMapperExtensions
    {
        /// <summary>
        /// Converts source model into destination with mapping defined earlier. Allows you to specify destination model later
        /// </summary>
        /// <typeparam name="TSource">Original model</typeparam>
        /// <param name="mappingConfiguration"></param>
        /// <param name="source">Original model</param>
        /// <returns>Object that can be later used to convert source model and specify what it should be converted to</returns>
        public static IFlashMapperGenericConverter Convert<TSource>(this IMappingConfiguration mappingConfiguration, TSource source)
        {
            return new FlashMapperGenericConverter<TSource>(mappingConfiguration, source);
        }

        /// <summary>
        /// Create mapping from <see cref="TSource"/> to <see cref="TDestination"/> without any configurations
        /// </summary>
        /// <typeparam name="TSource">Original model</typeparam>
        /// <typeparam name="TDestination">Result model</typeparam>
        /// <param name="mappingConfiguration"></param>
        public static void CreateMapping<TSource, TDestination>(this IMappingConfiguration mappingConfiguration) where TDestination: new()
        {
            mappingConfiguration.CreateMapping<TSource, TDestination>(s => new TDestination());
        }
    }
}
