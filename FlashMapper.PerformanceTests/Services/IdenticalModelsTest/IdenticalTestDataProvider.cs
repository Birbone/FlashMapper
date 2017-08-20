using System.Collections.Generic;
using System.Linq;
using FlashMapper.PerformanceTests.Models;

namespace FlashMapper.PerformanceTests.Services.IdenticalModelsTest
{
    public class IdenticalTestDataProvider : IPerformanceTestDataProvider<IdenticalTestSource>
    {
        private readonly IPerfomanceTestConfiguration configuration;
        private readonly IRandomDataProvider randomDataProvider;

        public IdenticalTestDataProvider(IPerfomanceTestConfiguration configuration, IRandomDataProvider randomDataProvider)
        {
            this.configuration = configuration;
            this.randomDataProvider = randomDataProvider;
        }
        
        private IEnumerable<IdenticalTestSource> IterateData()
        {
            var numberOfExecutions = configuration.NumberOfExecutions;
            for (int i = 0; i < numberOfExecutions; i++)
            {
                yield return new IdenticalTestSource
                {
                    Data1 = randomDataProvider.GetString(),
                    Data2 = i,
                    Data3 = randomDataProvider.GetDouble(),
                    Data4 = randomDataProvider.GetByte(),
                    Data5 = randomDataProvider.GetString(),
                    Data6 = randomDataProvider.GetString(),
                    Data7 = randomDataProvider.GetString(),
                    EighthData = randomDataProvider.GetString()
                };
            }
        }

        public IdenticalTestSource[] GetData()
        {
            return IterateData().ToArray();
        }
    }
}