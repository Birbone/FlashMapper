using FlashMapper.PerformanceTests.Models;

namespace FlashMapper.PerformanceTests.Services.IdenticalModelsTest
{
    public class ManualIdenticalTestParticipant : IPerformanceTestParticipant<IdenticalTestSource>
    {
        private IManualIdenticalBuilder manualIdenticalBuilder;
        private readonly Destination destination;
        public ManualIdenticalTestParticipant()
        {
            destination = new Destination();
        }

        public string Name => "Manual";

        public void Initialize()
        {
            manualIdenticalBuilder = new ManualIdenticalBuilder();
        }

        public void Convert(IdenticalTestSource source)
        {
            manualIdenticalBuilder.Build(source);
        }

        public void MapData(IdenticalTestSource source)
        {
            manualIdenticalBuilder.MapData(source, destination);
        }
    }
}