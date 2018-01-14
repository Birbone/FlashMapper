using FlashMapper.DependencyInjection;
using FlashMapper.Tests.Models;

namespace FlashMapper.Tests
{
    public class PersonBuilderWithAfterMap : FlashMapperBuilder<PersonFrontModel, Person, PersonBuilderWithAfterMap>
    {
        private readonly decimal height;

        public PersonBuilderWithAfterMap(IMappingConfiguration mappingConfiguration, decimal height) : base(mappingConfiguration)
        {
            this.height = height;
        }

        protected override void ConfigureMapping(IFlashMapperBuilderConfigurator<PersonFrontModel, Person> configurator)
        {
            configurator.CreateMapping(f => new Person
                {
                    Timestamp = MappingOptions.Ignore()
                })
                .AfterMap((f, p) =>
                {
                    p.Height = height;
                })
                .SourceNamingConvention(NamingConventionType.CamelCase)
                .DestinationNamingConvention(NamingConventionType.CamelCase);
        }
    }
}