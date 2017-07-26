using System.Collections.Generic;

namespace FlashMapper.DependancyInjection
{
    public class FlashMapperBuildersRegistrationService : IFlashMapperBuildersRegistrationService
    {
        private readonly IEnumerable<IFlashMapperBuilder> modelMapperConfigurationServices;

        public FlashMapperBuildersRegistrationService(IEnumerable<IFlashMapperBuilder> modelMapperConfigurationServices)
        {
            this.modelMapperConfigurationServices = modelMapperConfigurationServices;
        }

        public void RegisterAllBuilders()
        {
            foreach (var modelMapperConfigurationService in modelMapperConfigurationServices)
            {
                modelMapperConfigurationService.RegisterMapping();
            }
        }
    }
}
