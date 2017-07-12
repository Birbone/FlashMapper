namespace FlashMapper.Services
{
    public interface IMappingsStorageFactory: IFlashMapperService
    {
        IMappingsStorage Create(MappingConfiguration mappingConfiguration);
    }
}