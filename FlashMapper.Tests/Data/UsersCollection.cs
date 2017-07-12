using FlashMapper.Tests.Models;

namespace FlashMapper.Tests.Data
{
    public class UsersCollection
    {
        private static readonly User JohnUserData = new User
        {
            Email = "john@example.com",
            Login = "john",
            Password = "qwerty"
        };

        private static readonly User BrentUserData = new User
        {
            Login = "brent",
            Email = "brent@example.com",
            Password = "123456"
        };

        public User JohnUser => JohnUserData;
        public User BrentUser => BrentUserData;
    }
}