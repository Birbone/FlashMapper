namespace FlashMapper.DependancyInjection
{
    internal interface ISpecificMappingConfigStepHandler
    {
        bool TryProcessStep<TBuilder>(IMappingConfigStep step, 
            TBuilder builder, 
            IMappingConfiguration currentMappingConfiguration, 
            IMappingConfiguration previousMappingConfiguration, 
            DeferredFlashMapperSettingsBuilder settingsBuilder);
    }
}