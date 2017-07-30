using System.Collections.Generic;

namespace FlashMapper.DependancyInjection
{
    internal class FlashMapperBuilderConfiguratorContext : IFlashMapperBuilderConfiguratorContext
    {
        public FlashMapperBuilderConfiguratorContext(IFlashMapperSettingsBuilder modelMapperSettingsBuilder)
        {
            SettingsBuilder = modelMapperSettingsBuilder;
            Steps = new List<IMappingConfigStep>();
        }

        public IFlashMapperSettingsBuilder SettingsBuilder { get; }
        public List<IMappingConfigStep> Steps { get; }
    }
}