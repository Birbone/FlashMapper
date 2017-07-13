using System;
using System.Collections.Generic;
using FlashMapper.Models;
using FlashMapper.Services;

namespace FlashMapper.Internal
{
    public class MappingsStorage : IMappingsStorage
    {
        private readonly MappingConfiguration mappingConfiguration;
        private readonly Queue<IMappingStorageCleaner> storagesToCleanUp;

        public MappingsStorage(MappingConfiguration mappingConfiguration)
        {
            this.mappingConfiguration = mappingConfiguration;
            storagesToCleanUp = new Queue<IMappingStorageCleaner>();
        }

        public Mapping<TSource, TDestination> GetMapping<TSource, TDestination>()
        {
            return MappingsStorage<TSource, TDestination>.GetMapping(mappingConfiguration);
        }

        public void SetMapping<TSource, TDestination>(Mapping<TSource, TDestination> mapping)
        {
            var storage = MappingsStorage<TSource, TDestination>.SetMapping(mappingConfiguration, mapping);
            storagesToCleanUp.Enqueue(storage);
            
        }

        public void Dispose()
        {
            while (storagesToCleanUp.Count > 0)
            {
                storagesToCleanUp.Dequeue().CleanUp();
            }
        }
    }

    internal static class MappingsStorage<TSource, TDestination>
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

        public static IMappingStorageCleaner SetMapping(MappingConfiguration mappingConfiguration, Mapping<TSource, TDestination> mapping)
        {
            lock (Mappings)
            {
                Mappings[mappingConfiguration.InstanceId] = mapping;
            }
            return new MappingStorageCleaner<TSource, TDestination>(mappingConfiguration);
        }

        public static void RemoveConfiguration(MappingConfiguration mappingConfiguration)
        {
            lock (Mappings)
            {
                if (Mappings.ContainsKey(mappingConfiguration.InstanceId))
                    Mappings.Remove(mappingConfiguration.InstanceId);
            }
        }
    }

    internal interface IMappingStorageCleaner
    {
        void CleanUp();
    }

    internal class MappingStorageCleaner<TSource, TDestination> : IMappingStorageCleaner
    {
        private readonly MappingConfiguration mappingConfiguration;

        public MappingStorageCleaner(MappingConfiguration mappingConfiguration)
        {
            this.mappingConfiguration = mappingConfiguration;
        }

        public void CleanUp()
        {
            MappingsStorage<TSource, TDestination>.RemoveConfiguration(mappingConfiguration);
        }
    }
}