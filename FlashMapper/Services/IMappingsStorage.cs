using System;
using FlashMapper.Models;

namespace FlashMapper.Services
{
    /// <summary>
    /// Service that used to store configured mappings
    /// </summary>
    public interface IMappingsStorage: IDisposable
    {
        /// <summary>
        /// Returns earlier configured mapping
        /// </summary>
        /// <typeparam name="TSource">Original model</typeparam>
        /// <typeparam name="TDestination">Result model</typeparam>
        /// <returns>Mapping methods</returns>
        Mapping<TSource, TDestination> GetMapping<TSource, TDestination>();
        /// <summary>
        /// Saves mapping for later use
        /// </summary>
        /// <typeparam name="TSource">Original model</typeparam>
        /// <typeparam name="TDestination">Result model</typeparam>
        /// <param name="mapping">Mapping methods</param>
        void SetMapping<TSource, TDestination>(Mapping<TSource, TDestination> mapping);
    }
}