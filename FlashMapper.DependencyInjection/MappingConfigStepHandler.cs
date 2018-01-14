using System;
using System.Collections.Generic;
using System.Linq;

namespace FlashMapper.DependencyInjection
{
    internal class MappingConfigStepHandler : IMappingConfigStepHandler
    {
        private readonly IEnumerable<ISpecificMappingConfigStepHandler> specificHandlers;

        public MappingConfigStepHandler(IEnumerable<ISpecificMappingConfigStepHandler> specificHandlers)
        {
            this.specificHandlers = specificHandlers;
        }
        
        public void ProcessStep<TBuilder>(IMappingConfigStep step, TBuilder builder, IMappingConfiguration currentMappingConfiguration, IMappingConfiguration previousMappingConfiguration)
        {
            if (!specificHandlers.Any(h => h.TryProcessStep(step, builder, currentMappingConfiguration, previousMappingConfiguration)))
                throw new Exception($"No handler was found to process step {step?.GetType().Name}.");
        }
    }
}