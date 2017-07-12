using System;

namespace FlashMapper.Models
{
    public class Mapping<TSource, TDestination>
    {
        public Mapping(Func<TSource, TDestination> buildFunction, Action<TSource, TDestination> mapDataFunction, IFlashMapperSettings settings)
        {
            Settings = settings;
            MapDataFunction = mapDataFunction;
            BuildFunction = buildFunction;
        }

        public Func<TSource, TDestination> BuildFunction { get; }
        public Action<TSource, TDestination> MapDataFunction { get; }
        public IFlashMapperSettings Settings { get; }
    }
}