using System;

namespace FlashMapper.PerformanceTests.Services
{
    public interface IPerformanceTestDataProvider<out TModel>
    {
        TModel[] GetData();
    }
}