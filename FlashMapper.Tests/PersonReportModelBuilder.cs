using FlashMapper.DependancyInjection;
using FlashMapper.Tests.Models;

namespace FlashMapper.Tests
{
    public class PersonReportModelBuilder: FlashMapperBuilder<Person, PersonReportModel, PersonReportModelBuilder>
    {
        private readonly int age;
        
        public PersonReportModelBuilder(IMappingConfiguration mappingConfiguration, int age) : base(mappingConfiguration)
        {
            this.age = age;
        }

        protected override void ConfigureMapping(IFlashMapperBuilderConfigurator<Person, PersonReportModel> configurator)
        {
            configurator.CreateMapping(
                    p => new PersonReportModel
                    {
                        Age = age
                    })
                .UnresolvedBehavior(UnresolvedPropertyBehavior.Ignore)
                .SourceNamingConvention(NamingConventionType.CamelCase);
        }
    }
}