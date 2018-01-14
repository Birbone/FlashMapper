using System.Collections.Generic;

namespace FlashMapper.DependencyInjection
{
    internal class MappingStepsConfigurator : IMappingStepsConfigurator
    {
        private readonly IMappingConfigStepHandler mappingConfigStepHandler;

        public MappingStepsConfigurator(IMappingConfigStepHandler mappingConfigStepHandler)
        {
            this.mappingConfigStepHandler = mappingConfigStepHandler;
        }

        public void ProcessSteps<TBuilder>(List<IMappingConfigStep> steps, IMappingConfiguration mappingConfiguration, TBuilder builder)
        {
            if (steps.Count == 0)
                return;
            var lastStep = steps[steps.Count - 1];
            if (!(lastStep is ResultMappingConfigStep))
                return;
            steps.Reverse();
            var stepsStack = new Stack<IMappingConfigStep>(steps);
            var firstStep = stepsStack.Pop();
            var internalConfiguration = stepsStack.Count == 0 ? null : mappingConfiguration.CreateDependantConfiguration();
            mappingConfigStepHandler.ProcessStep(firstStep, builder, mappingConfiguration, internalConfiguration);
            while (stepsStack.Count > 0)
            {
                var currentStep = stepsStack.Pop();
                mappingConfigStepHandler.ProcessStep(currentStep, builder, internalConfiguration, internalConfiguration);
            }
        }
    }
}