using System;
using System.Collections.Generic;
using FlashMapper.Internal.Utils;
using FlashMapper.Services;

namespace FlashMapper
{
    public interface IFlashMapperCustomServiceBuilder
    {
        IFlashMapperCustomServiceBuilder RegisterService<TService>(
            Func<IFlashMapperDependencyResolver, TService> serviceRegistration) where TService : class, IFlashMapperService;
        IFlashMapperDependencyResolver GetResultDependencyResolver();
    }
}