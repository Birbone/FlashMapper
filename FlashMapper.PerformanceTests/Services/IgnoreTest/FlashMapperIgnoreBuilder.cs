using FlashMapper.DependencyInjection;
using FlashMapper.PerformanceTests.Models;

namespace FlashMapper.PerformanceTests.Services.IgnoreTest
{
    public class FlashMapperIgnoreBuilder: FlashMapperBuilder<IgnoreTestSource, Destination, FlashMapperIgnoreBuilder>
    {
        public FlashMapperIgnoreBuilder(IMappingConfiguration mappingConfiguration) : base(mappingConfiguration)
        {
        }

        protected override void ConfigureMapping(IFlashMapperBuilderConfigurator<IgnoreTestSource, Destination> configurator)
        {
            configurator.CreateMapping(s => new Destination
            {
                Data2 = MappingOptions.Ignore(),
                Data4 = MappingOptions.Ignore(),
                Data5 = s.Data3 > 0.5 ? s.Data5 : MappingOptions.Ignore(),
                Data7 = MappingOptions.Ignore(),
                EighthData = MappingOptions.Ignore()
            });
        }
    }
}