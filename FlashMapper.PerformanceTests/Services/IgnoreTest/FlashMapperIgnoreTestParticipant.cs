using FlashMapper.PerformanceTests.Models;

namespace FlashMapper.PerformanceTests.Services.IgnoreTest
{
    public class FlashMapperIgnoreTestParticipant : IPerformanceTestParticipant<IgnoreTestSource>
    {
        private readonly IMappingConfiguration mappingConfiguration;
        private readonly Destination destination;
        public FlashMapperIgnoreTestParticipant()
        {
            mappingConfiguration = new MappingConfiguration();
            destination = new Destination();
        }

        public string Name => "FlashMapper";
        public void Initialize()
        {
            mappingConfiguration.CreateMapping<IgnoreTestSource, Destination>(s => new Destination
            {
                Data2 = MappingOptions.Ignore(),
                Data4 = MappingOptions.Ignore(),
                Data5 = s.Data3 > 0.5 ? s.Data5 : MappingOptions.Ignore(),
                Data7 = MappingOptions.Ignore(),
                EighthData = MappingOptions.Ignore()
            });
        }

        public void Convert(IgnoreTestSource source)
        {
            mappingConfiguration.Convert<IgnoreTestSource, Destination>(source);
        }

        public void MapData(IgnoreTestSource source)
        {
            mappingConfiguration.MapData(source, destination);
        }
    }
}