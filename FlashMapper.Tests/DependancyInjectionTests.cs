using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlashMapper.Tests.Data;
using FlashMapper.Tests.Models;

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

        [TestMethod]
        public void ExtraPositionTest()
        {
            var configuration = new MappingConfiguration();
            var registrationBuilder = new SinglePositionOrderBuilder(configuration, 0);
            registrationBuilder.RegisterMapping();
            var orderPosition = TestData.OrderPositions.BrentOrdersSinglePencil;
            var order = TestData.Orders.BrentOrdersPencil;
            var storeItem = TestData.StoreItems.Pencil;
            var johnSmith = TestData.People.JohnSmith;
            var builder = new SinglePositionOrderBuilder(configuration, johnSmith.PersonId);
            var resultOrder = builder.Build(orderPosition);
            CheckResultOrder(order, orderPosition, storeItem, johnSmith, resultOrder);
            var brent = TestData.People.BrentJohnson;
            var brentResult = new SinglePositionOrder();
            var brentBuilder = new SinglePositionOrderBuilder(configuration, brent.PersonId);
            brentBuilder.MapData(orderPosition, brentResult);
            CheckResultOrder(order, orderPosition, storeItem, brent, brentResult);
        }

        [TestMethod]
        public void AfterMapTest()
        {
            var configuration = new MappingConfiguration();
            var registrationBuilder = new PersonBuilderWithAfterMap(configuration, 0);
            registrationBuilder.RegisterMapping();
            var frontModel = new PersonFrontModel();
            var currentBuilder = new PersonBuilderWithAfterMap(configuration, 7);
            var newPerson = currentBuilder.Build(frontModel);
            Assert.AreEqual(7, newPerson.Height);
            var existingPerson = new Person{Height = 6};
            currentBuilder.MapData(frontModel, existingPerson);
            Assert.AreEqual(7, existingPerson.Height);
        }

        private static void CheckResultOrder(Order order, OrderPosition orderPosition, 
            StoreItem storeItem, Person person, SinglePositionOrder resultOrder)
        {
            Assert.AreEqual(order.Id, resultOrder.Id);
            Assert.AreEqual(person.Address, resultOrder.Address);
            Assert.AreEqual(orderPosition.Amount, resultOrder.Amount);
            Assert.AreEqual(storeItem.Name, resultOrder.Name);
            Assert.AreEqual(order.OrderNumber, resultOrder.OrderNumber);
            Assert.AreEqual(order.Recipient, resultOrder.Recipient);
        }
    }    
}