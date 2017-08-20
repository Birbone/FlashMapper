using FlashMapper.PerformanceTests.Models;

namespace FlashMapper.PerformanceTests.Services.IgnoreTest
{
    public class FlashMapperBuilderIgnoreTestParticipant : IPerformanceTestParticipant<IgnoreTestSource>
    {
        private readonly FlashMapperIgnoreBuilder builder;
        private readonly Destination destination;
        public FlashMapperBuilderIgnoreTestParticipant()
        {
            builder = new FlashMapperIgnoreBuilder(new MappingConfiguration());
            destination = new Destination();
        }

        public string Name => "FlashMapperBuilder";
        public void Initialize()
        {
            builder.RegisterMapping();
        }

        public void Convert(IgnoreTestSource source)
        {
            builder.Build(source);
        }

        public void MapData(IgnoreTestSource source)
        {
            builder.MapData(source, destination);
        }
    }
}