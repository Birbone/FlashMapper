using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlashMapper.Tests.Data;

namespace FlashMapper.Tests
{
    [TestClass]
    public class DependancyInjectionTests
    {
        [TestMethod]
        public void DependancyInjectionTest()
        {
            var configuration = new MappingConfiguration();
            var registrationBuilder = new PersonReportModelBuilder(configuration, 0);
            registrationBuilder.RegisterMapping();
            var builder = new PersonReportModelBuilder(configuration, 28);
            var reportModel = builder.Build(TestData.People.JohnSmith);
            Assert.AreEqual(28, reportModel.Age);
        }
    }
}