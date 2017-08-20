using FlashMapper.PerformanceTests.Models;

namespace FlashMapper.PerformanceTests.Services.IdenticalModelsTest
{
    public class FlashMapperIdenticalTestParticipant : IPerformanceTestParticipant<IdenticalTestSource>
    {
        private readonly IMappingConfiguration mappingConfiguration;
        private readonly Destination destination;
        public FlashMapperIdenticalTestParticipant()
        {
            mappingConfiguration = new MappingConfiguration();
            destination = new Destination();
        }

        public string Name => "FlashMapper";
        public void Initialize()
        {
            mappingConfiguration.CreateMapping<IdenticalTestSource, Destination>(s => new Destination());
        }

        public void Convert(IdenticalTestSource source)
        {
            mappingConfiguration.Convert<IdenticalTestSource, Destination>(source);
        }

        public void MapData(IdenticalTestSource source)
        {
            mappingConfiguration.MapData(source, destination);
        }
    }
}