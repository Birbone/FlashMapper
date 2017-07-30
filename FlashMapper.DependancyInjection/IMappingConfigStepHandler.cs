namespace FlashMapper.DependancyInjection
{
    internal interface IMappingConfigStepHandler
    {
        void ProcessStep<TBuilder>(IMappingConfigStep step, 
            TBuilder builder, 
            IMappingConfiguration currentMappingConfiguration, 
            IMappingConfiguration previousMappingConfiguration, 
            DeferredFlashMapperSettingsBuilder settingsBuilder);
    }
}