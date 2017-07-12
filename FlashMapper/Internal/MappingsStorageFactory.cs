using FlashMapper.Services;

namespace FlashMapper.Internal
{
    public class MappingsStorageFactory : IMappingsStorageFactory
    {
        public IMappingsStorage Create(MappingConfiguration mappingConfiguration)
        {
            return new MappingsStorage(mappingConfiguration);
        }
    }
}