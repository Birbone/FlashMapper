using System.Collections.Generic;

namespace FlashMapper.DependencyInjection
{
    internal class FlashMapperBuilderConfiguratorContext : IFlashMapperBuilderConfiguratorContext
    {
        public FlashMapperBuilderConfiguratorContext()
        {
            Steps = new List<IMappingConfigStep>();
        }

        public List<IMappingConfigStep> Steps { get; }
    }
}