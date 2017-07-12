using FlashMapper.Models;

namespace FlashMapper.Services
{
    public interface IMappingsStorage
    {
        Mapping<TSource, TDestination> GetMapping<TSource, TDestination>();
        void SetMapping<TSource, TDestination>(Mapping<TSource, TDestination> mapping);
    }
}