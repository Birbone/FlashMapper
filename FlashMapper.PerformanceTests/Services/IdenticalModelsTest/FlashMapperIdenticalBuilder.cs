using FlashMapper.DependancyInjection;
using FlashMapper.PerformanceTests.Models;

namespace FlashMapper.PerformanceTests.Services.IdenticalModelsTest
{
    public class FlashMapperIdenticalBuilder : FlashMapperBuilder<IdenticalTestSource, Destination, FlashMapperIdenticalBuilder>, IFlashMapperIdenticalBuilder
    {
        public FlashMapperIdenticalBuilder(IMappingConfiguration mappingConfiguration) : base(mappingConfiguration)
        {
        }

        protected override void ConfigureMapping(IFlashMapperBuilderConfigurator<IdenticalTestSource, Destination> configurator)
        {
            configurator.CreateMapping(s => new Destination());
        }
    }
}