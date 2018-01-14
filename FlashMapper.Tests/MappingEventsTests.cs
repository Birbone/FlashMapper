using FlashMapper.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlashMapper.Tests
{
    [TestClass]
    public class MappingEventsTests
    {
        [TestMethod]
        public void ChangeDestinationBeforeMapTest()
        {
            var mappingConfiguration = new MappingConfiguration();
            mappingConfiguration.CreateMapping<PersonFrontModel, Person>(
                f => new Person
                {
                    PersonId = MappingOptions.Ignore(),
                    Timestamp = MappingOptions.Ignore(),
                    FirstName = MappingOptions.Ignore()
                }, 
                s => s.SourceNamingConvention(NamingConventionType.CamelCase)
                    .DestinationNamingConvention(NamingConventionType.CamelCase)
                    .BeforeMap((f, p) =>
                    {
                        if (p.PersonId == 0)
                            p.PersonId = 10;
                        p.FirstName = "Brent";
                    }));
            var frontModel = new PersonFrontModel();
            var newPerson = mappingConfiguration.Convert(frontModel).To<Person>();
            Assert.AreEqual(10, newPerson.PersonId);
            Assert.AreEqual("Brent", newPerson.FirstName);
            var existingPerson = new Person{PersonId = 9};
            mappingConfiguration.MapData(frontModel, existingPerson);
            Assert.AreEqual(9, existingPerson.PersonId);
            Assert.AreEqual("Brent", existingPerson.FirstName);
        }

        [TestMethod]
        public void ChangeDestinationAfterMapTest()
        {
            var mappingConfiguration = new MappingConfiguration();
            mappingConfiguration.CreateMapping<PersonFrontModel, Person>(
                f => new Person
                {
                    PersonId = MappingOptions.Ignore(),
                    Timestamp = MappingOptions.Ignore()
                }, 
                s => s.SourceNamingConvention(NamingConventionType.CamelCase)
                    .DestinationNamingConvention(NamingConventionType.CamelCase)
                    .AfterMap((f, p) =>
                    {
                        if (p.PersonId == 0)
                            p.PersonId = 10;
                        p.FirstName = "Brent";
                    }));
            var frontModel = new PersonFrontModel();
            var newPerson = mappingConfiguration.Convert(frontModel).To<Person>();
            Assert.AreEqual(10, newPerson.PersonId);
            Assert.AreEqual("Brent", newPerson.FirstName);
            var existingPerson = new Person{PersonId = 9};
            mappingConfiguration.MapData(frontModel, existingPerson);
            Assert.AreEqual(9, existingPerson.PersonId);
            Assert.AreEqual("Brent", existingPerson.FirstName);
        }

        [TestMethod]
        public void ChangeSourceBeforeMapTest()
        {
            var mappingConfiguration = new MappingConfiguration();
            mappingConfiguration.CreateMapping<PersonFrontModel, Person>(
                f => new Person
                {
                    Timestamp = MappingOptions.Ignore()
                }, 
                s => s.SourceNamingConvention(NamingConventionType.CamelCase)
                    .DestinationNamingConvention(NamingConventionType.CamelCase)
                    .BeforeMap((f, p) =>
                    {
                        if (f.personId == 0)
                            f.personId = 10;
                        f.firstName = "Brent";
                    }));
            var frontModel = new PersonFrontModel();
            var newPerson = mappingConfiguration.Convert(frontModel).To<Person>();
            Assert.AreEqual(10, newPerson.PersonId);
            Assert.AreEqual("Brent", newPerson.FirstName);
            Assert.AreEqual(10, frontModel.personId);
            Assert.AreEqual("Brent", frontModel.firstName);
            var existingPerson = new Person{PersonId = 9};
            mappingConfiguration.MapData(frontModel, existingPerson);
            Assert.AreEqual(10, existingPerson.PersonId);
            Assert.AreEqual("Brent", existingPerson.FirstName);
        }

        [TestMethod]
        public void ChangeSourceAfterMapTest()
        {
            var mappingConfiguration = new MappingConfiguration();
            mappingConfiguration.CreateMapping<PersonFrontModel, Person>(
                f => new Person
                {
                    PersonId = MappingOptions.Ignore(),
                    Timestamp = MappingOptions.Ignore()
                }, 
                s => s.SourceNamingConvention(NamingConventionType.CamelCase)
                    .DestinationNamingConvention(NamingConventionType.CamelCase)
                    .AfterMap((f, p) =>
                    {
                        if (f.personId == 0)
                            f.personId = 10;
                        f.firstName = "Brent";
                    }));
            var frontModel = new PersonFrontModel();
            var newPerson = mappingConfiguration.Convert(frontModel).To<Person>();
            Assert.AreEqual(0, newPerson.PersonId);
            Assert.AreEqual(null, newPerson.FirstName);
            Assert.AreEqual(10, frontModel.personId);
            Assert.AreEqual("Brent", frontModel.firstName);
            var frontModel2 = new PersonFrontModel();
            var existingPerson = new Person{PersonId = 9};
            mappingConfiguration.MapData(frontModel2, existingPerson);
            Assert.AreEqual(9, existingPerson.PersonId);
            Assert.AreEqual(null, existingPerson.FirstName);
            Assert.AreEqual(10, frontModel2.personId);
            Assert.AreEqual("Brent", frontModel2.firstName);
        }

        [TestMethod]
        public void BeforeMapWithMultipleSources()
        {
            var mappingConfiguration = new MappingConfiguration();
            mappingConfiguration.CreateMapping<PersonFrontModel, int, Person>(
                (f, newId) => new Person
                {
                    PersonId = MappingOptions.Ignore(),
                    Timestamp = MappingOptions.Ignore(),
                    FirstName = MappingOptions.Ignore(),
                    LastName = "Johnson"
                },
                s => s.SourceNamingConvention(NamingConventionType.CamelCase)
                    .DestinationNamingConvention(NamingConventionType.CamelCase)
                    .BeforeMap((f, newId, p) =>
                    {
                        if (p.PersonId == 0)
                            p.PersonId = newId;
                        p.FirstName = "Brent";
                        p.LastName = "Smith";
                    }));
            var frontModel = new PersonFrontModel();
            var newPerson = mappingConfiguration.Convert(frontModel, 10).To<Person>();
            Assert.AreEqual(10, newPerson.PersonId);
            Assert.AreEqual("Brent", newPerson.FirstName);
            Assert.AreEqual("Johnson", newPerson.LastName);
            var existingPerson = new Person {PersonId = 9};
            mappingConfiguration.MapData(frontModel, 10, existingPerson);
            Assert.AreEqual(9, existingPerson.PersonId);
            Assert.AreEqual("Johnson", existingPerson.LastName);
        }

        [TestMethod]
        public void AfterMapWithMultipleSources()
        {
            var mappingConfiguration = new MappingConfiguration();
            mappingConfiguration.CreateMapping<PersonFrontModel, int, Person>(
                (f, newId) => new Person
                {
                    PersonId = MappingOptions.Ignore(),
                    Timestamp = MappingOptions.Ignore(),
                    FirstName = MappingOptions.Ignore(),
                    LastName = "Johnson"
                },
                s => s.SourceNamingConvention(NamingConventionType.CamelCase)
                    .DestinationNamingConvention(NamingConventionType.CamelCase)
                    .AfterMap((f, newId, p) =>
                    {
                        if (p.PersonId == 0)
                            p.PersonId = newId;
                        p.FirstName = "Brent";
                        p.LastName = "Smith";
                    }));
            var frontModel = new PersonFrontModel();
            var newPerson = mappingConfiguration.Convert(frontModel, 10).To<Person>();
            Assert.AreEqual(10, newPerson.PersonId);
            Assert.AreEqual("Brent", newPerson.FirstName);
            Assert.AreEqual("Smith", newPerson.LastName);
            var existingPerson = new Person {PersonId = 9};
            mappingConfiguration.MapData(frontModel, 10, existingPerson);
            Assert.AreEqual(9, existingPerson.PersonId);
            Assert.AreEqual("Smith", existingPerson.LastName);
        }
    }
}