using System;
using FlashMapper.Models;

namespace FlashMapper.Services
{
    public interface IMappingsStorage: IDisposable
    {
        Mapping<TSource, TDestination> GetMapping<TSource, TDestination>();
        void SetMapping<TSource, TDestination>(Mapping<TSource, TDestination> mapping);
    }
}