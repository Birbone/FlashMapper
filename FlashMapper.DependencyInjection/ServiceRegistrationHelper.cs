using System;
using FlashMapper.Internal.Utils;

namespace FlashMapper.DependencyInjection
{
    internal class ServiceRegistrationHelper<TInternalConfigurator> : 
        DeferredFlashMapperMappingConfiguratorBase<ServiceRegistrationHelper<TInternalConfigurator>, TInternalConfigurator>, IServiceRegistrationHelper
        where TInternalConfigurator : IFlashMapperSettingsBuilder<TInternalConfigurator>, IFlashMapperCustomServiceBuilder<TInternalConfigurator>
    {
        protected override ServiceRegistrationHelper<TInternalConfigurator> This => this;
        void IServiceRegistrationHelper.RegisterService<TService>(Func<IFlashMapperDependencyResolver, TService> serviceRegistration)
        {
            RegisterService(serviceRegistration);
        }

        public Delegate GetInitializationDelegate()
        {
            Func<TInternalConfigurator, TInternalConfigurator> result = Initialize;
            return result;
        }
    }
}