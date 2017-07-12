using System;
using System.Collections.Generic;
using System.Linq;
using FlashMapper.Services;

namespace FlashMapper.Internal.Utils
{
    public class FlashMapperDependencyResolver : IFlashMapperDependencyResolver
    {
        private readonly IFlashMapperDependencyResolver internalConfiguration;
        private readonly IDictionary<Type, List<Func<IFlashMapperDependencyResolver, object>>> serviceResolvers;
        private readonly List<IFlashMapperService> customServices;

        public FlashMapperDependencyResolver(): this(null, Enumerable.Empty<IFlashMapperService>())
        {
        }

        public FlashMapperDependencyResolver(IFlashMapperDependencyResolver internalConfiguration, IEnumerable<IFlashMapperService> customServices)
        {
            this.customServices = customServices.ToList();
            serviceResolvers = new Dictionary<Type, List<Func<IFlashMapperDependencyResolver, object>>>();
            this.internalConfiguration = internalConfiguration;
        }
        
        public void RegisterService<TService>(Func<IFlashMapperDependencyResolver, TService> service) where TService: class
        {
            var serviceType = typeof(TService);
            List<Func<IFlashMapperDependencyResolver, object>> typeServices;
            if (!serviceResolvers.TryGetValue(serviceType, out typeServices))
                serviceResolvers[serviceType] = typeServices = new List<Func<IFlashMapperDependencyResolver, object>>();
            typeServices.Add(service);
        }
        
        public TService GetService<TService>() where TService: class
        {
            var serviceType = typeof(TService);
            var allServices = GetServices<TService>().ToArray();
            if (allServices.Length == 0)
                throw new FlashMapperException($"FlashMapper service of type {serviceType.Name} is not registred.");
            return allServices[0];
        }
        

        public IEnumerable<TService> GetServices<TService>() where TService: class
        {
            return GetServices<TService>(this);
        }

        public IEnumerable<TService> GetServices<TService>(IFlashMapperDependencyResolver currentInstnace)
            where TService : class
        {
            var serviceType = typeof(TService);
            List<Func<IFlashMapperDependencyResolver, object>> typeServiceResolvers;
            if (serviceResolvers.TryGetValue(serviceType, out typeServiceResolvers))
            {
                foreach (var typeServiceResolver in typeServiceResolvers)
                    yield return (TService)typeServiceResolver(currentInstnace);
            }
            var typeCustomServices = customServices.OfType<TService>();
            foreach (var customService in typeCustomServices)
                yield return customService;


            if (internalConfiguration != null)
            {
                var internalServices = internalConfiguration.GetServices<TService>(currentInstnace);
                foreach (var internalService in internalServices)
                    yield return internalService;
            }
        }
    }
}