using System;
using FlashMapper.Tests.Models;

namespace FlashMapper.Tests.Data
{
    public class PeopleCollection
    {
        private static readonly Person JohnSmithData = new Person
        {
            Address = "28 Red Fox Dr",
            BirthDate = new DateTime(1989, 10, 6),
            City = "Georgetown",
            FirstName = "John",
            LastName = "Smith",
            Height = 1.85M,
            PersonId = 1,
            PhoneNumber = "+1487674564",
            Timestamp = 39656183
        };

        private static readonly Person BrentJohnsonData = new Person
        {
            Address = "32 Red Fox Dr",
            BirthDate = new DateTime(1989, 7, 31),
            City = "Georgetown",
            FirstName = "Brent",
            LastName = "Johnson",
            Height = 1.73M,
            PersonId = 2,
            PhoneNumber = "+123579846",
            Timestamp = 39656184
        };

        public Person JohnSmith => JohnSmithData;
        public Person BrentJohnson => BrentJohnsonData;
    }
}