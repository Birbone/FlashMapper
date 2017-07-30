using System.Collections.Generic;

namespace FlashMapper.DependancyInjection
{
    internal interface IFlashMapperBuilderConfiguratorContext
    {
        IFlashMapperSettingsBuilder SettingsBuilder { get; }
        List<IMappingConfigStep> Steps { get; }
    }
}