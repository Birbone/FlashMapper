using System.Collections.Generic;
using System.Linq;
using FlashMapper.PerformanceTests.Models;

namespace FlashMapper.PerformanceTests.Services.IgnoreTest
{
    public class IgnoreTestDataProvider : IPerformanceTestDataProvider<IgnoreTestSource>
    {
        private readonly IPerfomanceTestConfiguration configuration;
        private readonly IRandomDataProvider randomDataProvider;

        public IgnoreTestDataProvider(IPerfomanceTestConfiguration configuration, IRandomDataProvider randomDataProvider)
        {
            this.configuration = configuration;
            this.randomDataProvider = randomDataProvider;
        }

        private IEnumerable<IgnoreTestSource> IterateData()
        {
            var numberOfExecutions = configuration.NumberOfExecutions;
            for (int i = 0; i < numberOfExecutions; i++)
            {
                yield return new IgnoreTestSource
                {
                    Data1 = randomDataProvider.GetString(),
                    Data3 = randomDataProvider.GetDouble(),
                    Data5 = randomDataProvider.GetString(),
                    Data6 = randomDataProvider.GetString(),
                };
            }
        }

        public IgnoreTestSource[] GetData()
        {
            return IterateData().ToArray();
        }
    }
}