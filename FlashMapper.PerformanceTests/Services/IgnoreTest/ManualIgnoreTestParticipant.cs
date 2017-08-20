using FlashMapper.PerformanceTests.Models;

namespace FlashMapper.PerformanceTests.Services.IgnoreTest
{
    public class ManualIgnoreTestParticipant : IPerformanceTestParticipant<IgnoreTestSource>
    {
        private ManualIgnoreBuilder manualIdenticalBuilder;
        private readonly Destination destination;
        public ManualIgnoreTestParticipant()
        {
            destination = new Destination();
        }

        public string Name => "Manual";

        public void Initialize()
        {
            manualIdenticalBuilder = new ManualIgnoreBuilder();
        }

        public void Convert(IgnoreTestSource source)
        {
            manualIdenticalBuilder.Build(source);
        }

        public void MapData(IgnoreTestSource source)
        {
            manualIdenticalBuilder.MapData(source, destination);
        }
    }
}