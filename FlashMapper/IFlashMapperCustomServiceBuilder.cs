using System;
using FlashMapper.Internal.Utils;
using FlashMapper.Services;
using FlashMapper.Services.ParsingUserInput;

namespace FlashMapper
{
    public interface IFlashMapperCustomServiceBuilder
    {
        /// <summary>
        /// Allows you to register you own implementation of any internal service
        /// </summary>
        /// <typeparam name="TService">Service type like <see cref="IUserInputParser"/></typeparam>
        /// <param name="serviceRegistration">Service factory method. You can use dependancy resolver to obtain other services</param>
        /// <returns>Self</returns>
        IFlashMapperCustomServiceBuilder RegisterService<TService>(
            Func<IFlashMapperDependencyResolver, TService> serviceRegistration) where TService : class, IFlashMapperService;
        /// <summary>
        /// Used internaly to resolve registred earlier services
        /// </summary>
        /// <returns></returns>
        IFlashMapperDependencyResolver GetResultDependencyResolver();
    }
}