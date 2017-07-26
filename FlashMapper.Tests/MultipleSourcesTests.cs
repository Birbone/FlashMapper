using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlashMapper.Tests.Data;
using FlashMapper.Tests.Models;

namespace FlashMapper.Tests
{
    [TestClass]
    public class MultipleSourcesTests
    {
        [TestMethod]
        public void MultipleSourcesTest()
        {
            var mapperConfiguration = new MappingConfiguration();

            mapperConfiguration.CreateMapping<User, Person, UserInfo>((u, p) => new UserInfo
            {
                PersonName = $"{p.FirstName} {p.LastName}",
                Age = TestHelpers.CalculateAge(p.BirthDate),
            });

            var resultUser = mapperConfiguration.Convert<User, Person, UserInfo>(TestData.Users.JohnUser, TestData.People.JohnSmith);
            CheckUserInfo(TestData.Users.JohnUser, TestData.People.JohnSmith, resultUser);
        }

        [TestMethod]
        public void PrimitiveSourceTest()
        {
            var mapperConfiguration = new MappingConfiguration();

            mapperConfiguration.CreateMapping<UserInfo, string, User>((u, p) => new User
            {
                Login = u.Login,
                Email = u.Email,
                Password = p
            });
            mapperConfiguration.CreateMapping<User, Person, UserInfo>((u, p) => new UserInfo
            {
                PersonName = $"{p.FirstName} {p.LastName}",
                Age = TestHelpers.CalculateAge(p.BirthDate),
            });
            var password = "qwerty1234";
            var userInfo = mapperConfiguration.Convert<User, Person, UserInfo>(TestData.Users.JohnUser, TestData.People.JohnSmith);
            var resultUser = mapperConfiguration.Convert<UserInfo, string, User>(userInfo, password);
            Assert.AreEqual(password, resultUser.Password);
        }

        [TestMethod]
        public void IgnoreTest()
        {
            var mapperConfiguration = new MappingConfiguration();

            mapperConfiguration.CreateMapping<User, Person, UserWeirdModel>(
                (u, p) => new UserWeirdModel
                {
                    home_address = p.Address,
                    johns_password = p.FirstName == "John" ? u.Password : MappingOptions.Ignore(),
                    x_some_field = MappingOptions.Ignore()
                }, 
            s => s.SourceNamingConvention(NamingConventionType.CamelCase)
                .DestinationNamingConvention(NamingConventionType.UnderscoreSeparated, "s_", "x_"));
            var johnUser = mapperConfiguration.Convert(TestData.Users.JohnUser, TestData.People.JohnSmith).To<UserWeirdModel>();
            CheckUserWierdModel(TestData.Users.JohnUser, TestData.People.JohnSmith, johnUser);
            var brentUser = johnUser;
            mapperConfiguration.MapData(TestData.Users.BrentUser, TestData.People.BrentJohnson, brentUser);
            CheckUserWierdModel(TestData.Users.BrentUser, TestData.People.BrentJohnson, brentUser);
        }

        [TestMethod]
        public void BuildFromScratch()
        {
            var mapperConfiguration = new MappingConfiguration();
            mapperConfiguration.CreateMapping<string, string, string, int, UserInfo>((login, email, phoneNumber, age) => new UserInfo
            {
                Login = login,
                Email = email,
                Age = age,
                PhoneNumber = phoneNumber,
                PersonName = MappingOptions.Ignore()
            });
            var johnUser = TestData.Users.JohnUser;
            var johnSmith = TestData.People.JohnSmith;
            var resultUserInfo = mapperConfiguration.Convert<string, string, string, int, UserInfo>(johnUser.Login,
                johnUser.Email, johnSmith.PhoneNumber, TestHelpers.CalculateAge(johnSmith.BirthDate));
            CheckUserInfo(johnUser, johnSmith, resultUserInfo, true);
        }

        [TestMethod]
        public void CollisionsTest()
        {
            var mapperConfiguration = new MappingConfiguration();
            mapperConfiguration.CreateMapping<Order, OrderPosition, StoreItem, SinglePositionOrder>(
                (o, p, i) => new SinglePositionOrder(), c => c.CollisionBehavior(SelectSourceCollisionBehavior.ChooseAny));

            var order = TestData.Orders.BrentOrdersPencil;
            var orderPosition = TestData.OrderPositions.BrentOrdersSinglePencil;
            var storeItem = TestData.StoreItems.Pencil;
            var resultOrder = mapperConfiguration.Convert(order, orderPosition, storeItem)
                .To<SinglePositionOrder>();
            Assert.AreEqual(order.Id, resultOrder.Id);
            Assert.AreEqual(order.Address, resultOrder.Address);
            Assert.AreEqual(orderPosition.Amount, resultOrder.Amount);
            Assert.AreEqual(storeItem.Name, resultOrder.Name);
            Assert.AreEqual(order.OrderNumber, resultOrder.OrderNumber);
            Assert.AreEqual(order.Recipient, resultOrder.Recipient);
        }

        private void CheckUserWierdModel(User user, Person person, UserWeirdModel userWeirdModel)
        {
            Assert.AreEqual(person.Address, userWeirdModel.home_address);
            Assert.AreEqual(person.LastName, userWeirdModel.s_last_name);
            Assert.AreEqual(user.Email, userWeirdModel.s_email);
            Assert.AreEqual(user.Login, userWeirdModel.s_login);
            Assert.AreEqual(TestData.Users.JohnUser.Password, userWeirdModel.johns_password);
            Assert.IsNull(userWeirdModel.x_some_field);
        }

        private void CheckUserInfo(User user, Person person, UserInfo userInfo, bool ignorePersonName = false)
        {
            if (!ignorePersonName)
                Assert.AreEqual($"{person.FirstName} {person.LastName}", userInfo.PersonName);
            Assert.AreEqual(user.Login, userInfo.Login);
            Assert.AreEqual(user.Email, userInfo.Email);
            Assert.AreEqual(person.PhoneNumber, userInfo.PhoneNumber);
            Assert.AreEqual(TestHelpers.CalculateAge(person.BirthDate), userInfo.Age);
        }
    }
}