namespace FlashMapper.DependencyInjection
{
    internal interface ISpecificMappingConfigStepHandler
    {
        bool TryProcessStep<TBuilder>(IMappingConfigStep step, 
            TBuilder builder, 
            IMappingConfiguration currentMappingConfiguration, 
            IMappingConfiguration previousMappingConfiguration);
    }
}