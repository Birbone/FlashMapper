using System;
using System.Linq;
using FlashMapper.Internal.Utils;
using FlashMapper.Services;

namespace FlashMapper
{
    [Obsolete]
    public class FlashMapperCustomServiceBuilder : IFlashMapperCustomServiceBuilder
    {
        private readonly FlashMapperDependencyResolver dependencyResolver;

        public FlashMapperCustomServiceBuilder(IFlashMapperDependencyResolver internalDependencyResolver)
        {
            dependencyResolver = new FlashMapperDependencyResolver(internalDependencyResolver, Enumerable.Empty<IFlashMapperService>());
        }

        public IFlashMapperCustomServiceBuilder RegisterService<TService>(Func<IFlashMapperDependencyResolver, TService> serviceRegistration) where TService : class, IFlashMapperService
        {
            dependencyResolver.RegisterService(serviceRegistration);
            return this;
        }

        public IFlashMapperDependencyResolver GetResultDependencyResolver()
        {
            return dependencyResolver;
        }
    }
}