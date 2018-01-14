using System;
using FlashMapper.Internal.Utils;
using FlashMapper.Services;

namespace FlashMapper.DependencyInjection
{
    internal interface IServiceRegistrationHelper
    {
        void RegisterService<TService>(Func<IFlashMapperDependencyResolver, TService> serviceRegistration)
            where TService : class, IFlashMapperService;

        Delegate GetInitializationDelegate();
    }
}