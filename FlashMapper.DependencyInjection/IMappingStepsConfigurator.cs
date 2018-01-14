using System.Collections.Generic;

namespace FlashMapper.DependencyInjection
{
    internal interface IMappingStepsConfigurator
    {
        void ProcessSteps<TBuilder>(List<IMappingConfigStep> steps, IMappingConfiguration mappingConfiguration, TBuilder builder);
    }
}