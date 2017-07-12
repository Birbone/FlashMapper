using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlashMapper.Tests.Data;
using FlashMapper.Tests.Models;

namespace FlashMapper.Tests
{
    [TestClass]
    public class IgnoreTests
    {
        [TestMethod]
        public void IgnoreProperty()
        {
            var mapperConfiguration = new MappingConfiguration();
            mapperConfiguration.CreateMapping<Person, PersonReportModel>(p => new PersonReportModel
            {
                Name = $"{p.FirstName} {p.LastName}",
                Age = TestHelpers.CalculateAge(p.BirthDate),
                Address = MappingOptions.Ignore()
            });

            var reportModel = mapperConfiguration.Convert<Person, PersonReportModel>(TestData.People.JohnSmith);
            Assert.IsNull(reportModel.Address);
            const string doNotChange = "DO NOT CHANGE THIS";
            var existingModel = new PersonReportModel
            {
                Name = "Test",
                Address = doNotChange,
                City = "Test",
                PhoneNumber = "Test",
                Age = 5
            };
            mapperConfiguration.MapData(TestData.People.JohnSmith, existingModel);
            Assert.AreEqual(doNotChange, existingModel.Address);
        }

        [TestMethod]
        public void ConditionalIgnore()
        {
            var mapperConfiguration = new MappingConfiguration();
            mapperConfiguration.CreateMapping<Person, PersonReportModel>(p => new PersonReportModel
            {
                Name = p.FirstName != "Brent" ? "Not Brent" : "Brent",
                Age = p.FirstName == "Brent" ? 6 : 5, 
                Address = p.FirstName == "John" ? p.Address : MappingOptions.Ignore(),
                City = p.FirstName == "John" ? MappingOptions.Ignore() : p.City,
                PhoneNumber = p.FirstName != "John" ? MappingOptions.Ignore() : p.PhoneNumber
            });
            mapperConfiguration.CreateMapping<Person, Person>(p => new Person());
            var reportModel = mapperConfiguration.Convert<Person, PersonReportModel>(TestData.People.JohnSmith);
            Assert.AreEqual(TestData.People.JohnSmith.Address, reportModel.Address);
            Assert.IsNull(reportModel.City);
            Assert.AreEqual(TestData.People.JohnSmith.PhoneNumber, reportModel.PhoneNumber);
            Assert.AreEqual("Not Brent", reportModel.Name);
            Assert.AreEqual(5, reportModel.Age);

            const string doNotChange = "DO NOT CHANGE THIS";

            var existingModel = new PersonReportModel
            {
                Name = "Brent",
                Address = doNotChange,
                PhoneNumber = doNotChange,
                City = "Test",
                Age = 5
            };
            var brent = mapperConfiguration.Convert<Person, Person>(TestData.People.JohnSmith);
            brent.FirstName = "Brent";
            mapperConfiguration.MapData(brent, existingModel);
            Assert.AreEqual(doNotChange, existingModel.Address);
            Assert.AreEqual(doNotChange, existingModel.PhoneNumber);
            Assert.AreEqual(brent.City, existingModel.City);
            Assert.AreEqual(6, existingModel.Age);
        }

        [TestMethod]
        public void IgnoreToNullable()
        {
            var mapperConfiguration = new MappingConfiguration();
            mapperConfiguration.CreateMapping<Person, PersonViewModel>(p => new PersonViewModel
            {
                PersonId = MappingOptions.Ignore(),
            });
            var viewModel = mapperConfiguration.Convert(TestData.People.JohnSmith).To<PersonViewModel>();
            Assert.IsNull(viewModel.PersonId);
        }
    }
}