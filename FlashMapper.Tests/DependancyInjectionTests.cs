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

        //[TestMethod]
        //public void ExtraPositionTest()
        //{
        //    var configuration = new MappingConfiguration();
        //    var builder = new SinglePositionOrderBuilder(configuration);
        //    builder.RegisterMapping();
        //    var orderPosition = TestData.OrderPositions.BrentOrdersSinglePencil;
        //    var order = TestData.Orders.BrentOrdersPencil;
        //    var storeItem = TestData.StoreItems.Pencil;
        //    var resultOrder = builder.Build(orderPosition);
        //    Assert.AreEqual(order.Id, resultOrder.Id);
        //    Assert.AreEqual(order.Address, resultOrder.Address);
        //    Assert.AreEqual(orderPosition.Amount, resultOrder.Amount);
        //    Assert.AreEqual(storeItem.Name, resultOrder.Name);
        //    Assert.AreEqual(order.OrderNumber, resultOrder.OrderNumber);
        //    Assert.AreEqual(order.Recipient, resultOrder.Recipient);
        //}
    }
}