namespace FlashMapper
{
    public interface IFlashMapperGenericConverter
    {
        /// <summary>
        /// Allows you to specify destination model and get result
        /// </summary>
        /// <typeparam name="TDestination">Result model</typeparam>
        /// <returns>Result model</returns>
        TDestination To<TDestination>();
    }
}