using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlashMapper.Tests.Data;
using FlashMapper.Tests.Models;

namespace FlashMapper.Tests
{
    [TestClass]
    public class PropertyNameResolvingTests
    {
        [TestMethod]
        public void NameConventionsConvertTest()
        {
            var mapperConfiguration = new MappingConfiguration();
            mapperConfiguration.CreateMapping<Person, PersonFrontModel>(p => new PersonFrontModel(),
                s => s.SourceNamingConvention(NamingConventionType.CamelCase)
                    .DestinationNamingConvention(NamingConventionType.CamelCase));
            var frontEndModel = mapperConfiguration.Convert<Person, PersonFrontModel>(TestData.People.JohnSmith);
            CheckFrontModel(TestData.People.JohnSmith, frontEndModel);
        }

        [TestMethod]
        public void AbbrevationTest()
        {
            var mapperConfiguration = new MappingConfiguration();
            mapperConfiguration.CreateMapping<CompanyData, CompanyDataFrontModel>(c => new CompanyDataFrontModel(),
                s => s.SourceNamingConvention(NamingConventionType.CamelCase)
                    .DestinationNamingConvention(NamingConventionType.CamelCase));

            var frontEndModel = mapperConfiguration.Convert<CompanyData, CompanyDataFrontModel>(TestData.CompanyData.SomeCompanyData);
            CheckFrontModel(TestData.CompanyData.SomeCompanyData, frontEndModel);
        }

        [TestMethod]
        public void PrefixesTest()
        {
            var mapperConfiguration = new MappingConfiguration();
            mapperConfiguration.CreateMapping<Person, PersonWeirdModel>(p => new PersonWeirdModel(),
                s => s.SourceNamingConvention(NamingConventionType.CamelCase)
                    .DestinationNamingConvention(NamingConventionType.UnderscoreSeparated, "s_", "i_", "copy_"));

            var otherModel = mapperConfiguration.Convert<Person, PersonWeirdModel>(TestData.People.JohnSmith);
            CheckWeirdModel(TestData.People.JohnSmith, otherModel);
            mapperConfiguration.CreateMapping<PersonWeirdModel, Person>(p => new Person(),
                s => s.UnresolvedBehavior(UnresolvedPropertyBehavior.Ignore)
                    .CollisionBehavior(SelectSourceCollisionBehavior.ChooseAny)
                    .SourceNamingConvention(NamingConventionType.UnderscoreSeparated, "s_", "i_", "copy_")
                    .DestinationNamingConvention(NamingConventionType.CamelCase));
            var newPerson = new Person();
            mapperConfiguration.MapData(otherModel, newPerson);
            CheckFromWeirdModel(otherModel, newPerson);
        }

        [TestMethod]
        public void PropertiesCollisionErrorTest()
        {
            var mapperConfiguration = new MappingConfiguration();
            try
            {
                mapperConfiguration.CreateMapping<PersonWeirdModel, Person>(p => new Person(), 
                    s => s.UnresolvedBehavior(UnresolvedPropertyBehavior.Ignore)
                        .SourceNamingConvention(NamingConventionType.UnderscoreSeparated, "s_", "i_", "copy_")
                        .DestinationNamingConvention(NamingConventionType.CamelCase));
            }
            catch (PropertyIsNotMappedException e)
            {
                Assert.AreEqual(nameof(Person.LastName), e.PropertyName);
                return;
            }
            Assert.Fail("Expected error did not happen.");
        }

        [TestMethod]
        public void NotMappedPropertyErrorTest()
        {
            var mapperConfiguration = new MappingConfiguration();
            try
            {
                mapperConfiguration.CreateMapping<PersonFrontModel, Person>(p => new Person(),
                    s => s.SourceNamingConvention(NamingConventionType.CamelCase)
                        .DestinationNamingConvention(NamingConventionType.CamelCase));
            }
            catch (PropertyIsNotMappedException e)
            {
                Assert.AreEqual(nameof(Person.Timestamp), e.PropertyName);
                return;
            }
            Assert.Fail("Expected error did not happen.");
        }

        private void CheckFrontModel(CompanyData sourceModel, CompanyDataFrontModel resultModel)
        {
            Assert.AreEqual(sourceModel.DataForUIF, resultModel.dataForUIF);
            Assert.AreEqual(sourceModel.GIDData, resultModel.gidData);
            Assert.AreEqual(sourceModel.MKDIT, resultModel.mkdit);
        }

        private void CheckFrontModel(Person sourceModel, PersonFrontModel resultModel)
        {
            Assert.AreEqual(sourceModel.FirstName, resultModel.firstName);
            Assert.AreEqual(sourceModel.LastName, resultModel.lastName);
            Assert.AreEqual(sourceModel.Address, resultModel.address);
            Assert.AreEqual(sourceModel.BirthDate, resultModel.birthDate);
            Assert.AreEqual(sourceModel.City, resultModel.city);
            Assert.AreEqual(sourceModel.Height, resultModel.height);
            Assert.AreEqual(sourceModel.PersonId, resultModel.personId);
            Assert.AreEqual(sourceModel.PhoneNumber, resultModel.phoneNumber);
        }

        private void CheckWeirdModel(Person sourceModel, PersonWeirdModel resultModel)
        {
            Assert.AreEqual(sourceModel.FirstName, resultModel.s_first_name);
            Assert.AreEqual(sourceModel.LastName, resultModel.s_last_name);
            Assert.AreEqual(sourceModel.LastName, resultModel.copy_last_name);
            Assert.AreEqual(sourceModel.Height, resultModel.height);
            Assert.AreEqual(sourceModel.PersonId, resultModel.i_person_id);
        }

        private void CheckFromWeirdModel(PersonWeirdModel sourceModel, Person resultModel)
        {
            Assert.AreEqual(sourceModel.s_first_name, resultModel.FirstName);
            Assert.IsTrue(resultModel.LastName.In(sourceModel.copy_last_name, sourceModel.s_last_name));
            Assert.AreEqual(sourceModel.height, resultModel.Height);
            Assert.AreEqual(sourceModel.i_person_id, resultModel.PersonId);
        }
    }
}
