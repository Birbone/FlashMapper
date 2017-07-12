using System.Collections.Generic;

namespace FlashMapper.DependancyInjection
{
    public class FlashMapperBuildersRegistrationService : IFlashMapperBuildersRegistrationService
    {
        private readonly IEnumerable<IFlashMapperConfigurationService> modelMapperConfigurationServices;

        public FlashMapperBuildersRegistrationService(IEnumerable<IFlashMapperConfigurationService> modelMapperConfigurationServices)
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
