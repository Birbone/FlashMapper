using System.Collections.Generic;

namespace FlashMapper.DependancyInjection
{
    internal interface IFlashMapperBuilderConfiguratorContext
    {
        List<IMappingConfigStep> Steps { get; }
    }
}