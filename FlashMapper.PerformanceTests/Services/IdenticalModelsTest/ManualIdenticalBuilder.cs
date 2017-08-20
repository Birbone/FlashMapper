using FlashMapper.PerformanceTests.Models;

namespace FlashMapper.PerformanceTests.Services.IdenticalModelsTest
{
    public class ManualIdenticalBuilder : IManualIdenticalBuilder
    {
        public Destination Build(IdenticalTestSource source)
        {
            var result = new Destination();
            MapData(source, result);
            return result;
        }

        public void MapData(IdenticalTestSource source, Destination destination)
        {
            destination.Data1 = source.Data1;
            destination.Data2 = source.Data2;
            destination.Data3 = source.Data3;
            destination.Data4 = source.Data4;
            destination.Data5 = source.Data5;
            destination.Data6 = source.Data6;
            destination.Data7 = source.Data7;
            destination.EighthData = source.EighthData;
        }
    }
}