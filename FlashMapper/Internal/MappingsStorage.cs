using System;
using System.Collections.Generic;
using FlashMapper.Models;
using FlashMapper.Services;

namespace FlashMapper.Internal
{
    public class MappingsStorage : IMappingsStorage
    {
        private readonly MappingConfiguration mappingConfiguration;
        public MappingsStorage(MappingConfiguration mappingConfiguration)
        {
            this.mappingConfiguration = mappingConfiguration;
        }

        public Mapping<TSource, TDestination> GetMapping<TSource, TDestination>()
        {
            return MappingsStorage<TSource, TDestination>.GetMapping(mappingConfiguration);
        }

        public void SetMapping<TSource, TDestination>(Mapping<TSource, TDestination> mapping)
        {
            MappingsStorage<TSource, TDestination>.SetMapping(mappingConfiguration, mapping);
        }
    }

    public static class MappingsStorage<TSource, TDestination>
    {
        private static readonly IDictionary<Guid, Mapping<TSource, TDestination>> Mappings = new Dictionary<Guid, Mapping<TSource, TDestination>>();

        public static Mapping<TSource, TDestination> GetMapping(MappingConfiguration mappingConfiguration)
        {
            lock (Mappings)
            {
                Mapping<TSource, TDestination> result;
                if (!Mappings.TryGetValue(mappingConfiguration.InstanceId, out result))
                    throw new FlashMapperException($"Mapping from {typeof(TSource).Name} to {typeof(TDestination).Name} is not configured.");
                return result;
            }
        }

        public static void SetMapping(MappingConfiguration mappingConfiguration, Mapping<TSource, TDestination> mapping)
        {
            lock (Mappings)
            {
                Mappings[mappingConfiguration.InstanceId] = mapping;
            }
        }
    }
}