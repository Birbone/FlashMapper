namespace FlashMapper.Services
{
    /// <summary>
    /// Service that creates instance of <see cref="IMappingsStorage"/> that is used to store configured mappings
    /// </summary>
    public interface IMappingsStorageFactory: IFlashMapperService
    {
        /// <summary>
        /// Creates instance of storage
        /// </summary>
        /// <param name="mappingConfiguration"><see cref="MappingConfiguration"/> that will use this storage</param>
        /// <returns>New storage for specified <see cref="MappingConfiguration"/></returns>
        IMappingsStorage Create(MappingConfiguration mappingConfiguration);
    }
}