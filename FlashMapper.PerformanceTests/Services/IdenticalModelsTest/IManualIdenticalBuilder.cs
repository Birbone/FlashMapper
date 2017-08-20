using FlashMapper.PerformanceTests.Models;

namespace FlashMapper.PerformanceTests.Services.IdenticalModelsTest
{
    public interface IManualIdenticalBuilder
    {
        Destination Build(IdenticalTestSource source);
        void MapData(IdenticalTestSource source, Destination destination);
    }
}