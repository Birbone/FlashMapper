using FlashMapper.PerformanceTests.Models;

namespace FlashMapper.PerformanceTests.Services.IdenticalModelsTest
{
    public class FlashMapperBuilderIdenticalTestParticipant : IPerformanceTestParticipant<IdenticalTestSource>
    {
        private readonly FlashMapperIdenticalBuilder builder;
        private readonly Destination destination;
        public FlashMapperBuilderIdenticalTestParticipant()
        {
            builder = new FlashMapperIdenticalBuilder(new MappingConfiguration());
            destination = new Destination();
        }

        public string Name => "FlashMapperBuilder";
        public void Initialize()
        {
            builder.RegisterMapping();
        }

        public void Convert(IdenticalTestSource source)
        {
            builder.Build(source);
        }

        public void MapData(IdenticalTestSource source)
        {
            builder.MapData(source, destination);
        }
    }
}