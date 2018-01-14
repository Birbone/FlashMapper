using System.Collections.Generic;

namespace FlashMapper.DependencyInjection
{
    internal interface IFlashMapperBuilderConfiguratorContext
    {
        List<IMappingConfigStep> Steps { get; }
    }
}