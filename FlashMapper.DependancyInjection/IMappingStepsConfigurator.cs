using System.Collections.Generic;

namespace FlashMapper.DependancyInjection
{
    internal interface IMappingStepsConfigurator
    {
        void ProcessSteps<TBuilder>(List<IMappingConfigStep> steps, IMappingConfiguration mappingConfiguration, TBuilder builder);
    }
}