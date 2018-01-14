namespace FlashMapper.DependencyInjection
{
    internal static class ModuleConfiguration
    {
        public static IMappingConfigStepHandler MappingConfigStepHandler { get; }
        public static IMappingStepsConfigurator MappingStepsConfigurator { get; }
        static ModuleConfiguration()
        {
            MappingConfigStepHandler = new MappingConfigStepHandler(new ISpecificMappingConfigStepHandler[]
            {
                new ResolveExtraParameterStepHandler(), 
                new ResultMappingConfigStepHandler(), 
            });
            MappingStepsConfigurator = new MappingStepsConfigurator(MappingConfigStepHandler);
        }
    }
}