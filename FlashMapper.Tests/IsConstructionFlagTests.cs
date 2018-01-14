using FlashMapper.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlashMapper.Tests
{
    [TestClass]
    public class IsConstructionFlagTests
    {
        [TestMethod]
        public void IsConstructionFlag()
        {
            var mapperConfiguration = new MappingConfiguration();
            mapperConfiguration.CreateMapping<PersonFrontModel, Person>(
                f => new Person
                {
                    Height = MappingOptions.IsConstruction() ? 6 : 7,
                    Timestamp = MappingOptions.Ignore()
                },
                s => s.SourceNamingConvention(NamingConventionType.CamelCase)
                    .DestinationNamingConvention(NamingConventionType.CamelCase));
            var frontModel = new PersonFrontModel();
            var convertedPerson = mapperConfiguration.Convert(frontModel).To<Person>();
            var existingPerson = new Person();
            mapperConfiguration.MapData(frontModel, existingPerson);
            Assert.AreEqual(6, convertedPerson.Height);
            Assert.AreEqual(7, existingPerson.Height);
        }

        [TestMethod]
        public void IsConstructionWithIgnoreTest()
        {
            var mapperConfiguration = new MappingConfiguration();
            mapperConfiguration.CreateMapping<PersonFrontModel, Person>(
                f => new Person
                {
                    PersonId = MappingOptions.IsConstruction() ? 10 : MappingOptions.Ignore(),
                    Timestamp = MappingOptions.Ignore()
                },
                s => s.SourceNamingConvention(NamingConventionType.CamelCase)
                    .DestinationNamingConvention(NamingConventionType.CamelCase));

            var frontModel = new PersonFrontModel();
            var convertedPerson = mapperConfiguration.Convert(frontModel).To<Person>();
            var existingPerson = new Person { PersonId = 9 };
            mapperConfiguration.MapData(frontModel, existingPerson);
            Assert.AreEqual(10, convertedPerson.PersonId);
            Assert.AreEqual(9, existingPerson.PersonId);
        }
    }
}