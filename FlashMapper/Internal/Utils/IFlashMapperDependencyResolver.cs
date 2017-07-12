using System.Collections.Generic;

namespace FlashMapper.Internal.Utils
{
    public interface IFlashMapperDependencyResolver
    {
        TService GetService<TService>() where TService : class;
        IEnumerable<TService> GetServices<TService>() where TService : class;
        IEnumerable<TService> GetServices<TService>(IFlashMapperDependencyResolver currentInstnace) where TService : class;
    }
}