using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlashMapper.Tests.Data;
using FlashMapper.Tests.Models;

namespace FlashMapper.Tests
{
    [TestClass]
    public class BasicTests
    {
        [TestMethod]
        public void MapSimpleModel()
        {
            var mapperConfiguration = new MappingConfiguration();

            mapperConfiguration.CreateMapping<Person, PersonReportModel>(p => new PersonReportModel
            {
                Name = $"{p.FirstName} {p.LastName}",
                Age = TestHelpers.CalculateAge(p.BirthDate),
            });

            var reportModel = mapperConfiguration.Convert<Person, PersonReportModel>(TestData.People.JohnSmith);
            CheckPersonReportModel(TestData.People.JohnSmith, reportModel);

            var existingReportModel = new PersonReportModel();
            mapperConfiguration.MapData(TestData.People.JohnSmith, existingReportModel);
            CheckPersonReportModel(TestData.People.JohnSmith, existingReportModel);
        }


        [TestMethod]
        public void MapModelWithCtor()
        {
            var mapperConfiguration = new MappingConfiguration();
            mapperConfiguration.CreateMapping<Person, PersonWithCtor>(p => new PersonWithCtor(p.FirstName, p.LastName, p.PersonId)
            {
                StreetAddress = p.Address,
            });

            var modelWithCtor = mapperConfiguration.Convert<Person, PersonWithCtor>(TestData.People.JohnSmith);
            CheckPersonWithCtor(TestData.People.JohnSmith, modelWithCtor);

            var existingModelWithCtor = new PersonWithCtor(TestData.People.JohnSmith.FirstName,
                TestData.People.JohnSmith.LastName, TestData.People.JohnSmith.PersonId);
            mapperConfiguration.MapData(TestData.People.JohnSmith, existingModelWithCtor);
            CheckPersonWithCtor(TestData.People.JohnSmith, existingModelWithCtor);
        }

        [TestMethod]
        public void UserOverrides()
        {
            var mapperConfiguration = new MappingConfiguration();
            mapperConfiguration.CreateMapping<Person, PersonReportModel>(p => new PersonReportModel
            {
                Name = $"{p.FirstName} {p.LastName}",
                Address = $"Address: {p.Address}",
                City = $"City: {p.City}",
                PhoneNumber = $"Phone number: {p.PhoneNumber}",
                Age = TestHelpers.CalculateAge(p.BirthDate),
            });

            var johnSmith = TestData.People.JohnSmith;
            var reportModel = mapperConfiguration.Convert(johnSmith).To<PersonReportModel>();
            Assert.AreEqual($"Address: {johnSmith.Address}", reportModel.Address);
            Assert.AreEqual($"City: {johnSmith.City}", reportModel.City);
            Assert.AreEqual($"Phone number: {johnSmith.PhoneNumber}", reportModel.PhoneNumber);
        }

        [TestMethod]
        public void UnmappedPropertiesAssertTest()
        {
            var mapperConfiguration = new MappingConfiguration();
            try
            {
                mapperConfiguration.CreateMapping<Person, PersonWithCtor>(p => new PersonWithCtor(p.FirstName, p.LastName, p.PersonId));
            }
            catch (PropertyIsNotMappedException e)
            {
                Assert.AreEqual(nameof(PersonWithCtor.StreetAddress), e.PropertyName);
                return;
            }
            Assert.Fail("Unmapped property was not detected");
        }

        [TestMethod]
        public void ImplicitTypeConvertionTest()
        {
            var mapperConfiguration = new MappingConfiguration();
            mapperConfiguration.CreateMapping<Person, PersonViewModel>();
            var viewModel = mapperConfiguration.Convert(TestData.People.JohnSmith).To<PersonViewModel>();
            CheckViewModel(TestData.People.JohnSmith, viewModel);
        }

        private void CheckViewModel(Person sourceModel, PersonViewModel resultModel)
        {
            Assert.IsNotNull(resultModel.PersonId);
            Assert.IsNotNull(resultModel.BirthDate);
            Assert.AreEqual(sourceModel.PersonId, resultModel.PersonId.Value);
            Assert.AreEqual(sourceModel.FirstName, resultModel.FirstName);
            Assert.AreEqual(sourceModel.LastName, resultModel.LastName);
            Assert.AreEqual(sourceModel.Height, resultModel.Height);
            Assert.AreEqual(sourceModel.Address, resultModel.Address);
            Assert.AreEqual(sourceModel.City, resultModel.City);
            Assert.AreEqual(sourceModel.PhoneNumber, resultModel.PhoneNumber);
        }

        private void CheckPersonReportModel(Person sourceModel, PersonReportModel resultModel)
        {
            Assert.AreEqual($"{sourceModel.FirstName} {sourceModel.LastName}", resultModel.Name);
            Assert.AreEqual(sourceModel.Address, resultModel.Address);
            Assert.AreEqual(sourceModel.City, resultModel.City);
            Assert.AreEqual(sourceModel.PhoneNumber, resultModel.PhoneNumber);
            Assert.AreEqual(TestHelpers.CalculateAge(sourceModel.BirthDate), resultModel.Age);
        }

        private void CheckPersonWithCtor(Person sourceModel, PersonWithCtor resultModel)
        {
            Assert.AreEqual(sourceModel.FirstName, resultModel.FirstName);
            Assert.AreEqual(sourceModel.LastName, resultModel.LastName);
            Assert.AreEqual(sourceModel.Address, resultModel.StreetAddress);
            Assert.AreEqual(sourceModel.City, resultModel.City);
            Assert.AreEqual(sourceModel.PhoneNumber, resultModel.PhoneNumber);
            Assert.AreEqual(sourceModel.PersonId, resultModel.PersonId);
        }
    }
}
