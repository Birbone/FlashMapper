using FlashMapper.PerformanceTests.Models;

namespace FlashMapper.PerformanceTests.Services.IgnoreTest
{
    public class ManualIgnoreBuilder
    {
        public Destination Build(IgnoreTestSource source)
        {
            var result = new Destination();
            MapData(source, result);
            return result;
        }

        public void MapData(IgnoreTestSource source, Destination destination)
        {
            destination.Data1 = source.Data1;
            destination.Data3 = source.Data3;
            if (source.Data3 > 0.5)
                destination.Data5 = source.Data5;
            destination.Data6 = source.Data6;
        }
    }
}