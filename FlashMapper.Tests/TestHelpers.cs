using System;

namespace FlashMapper.Tests
{
    public static class TestHelpers
    {
        public static DateTime CurrentDate()
        {
            return new DateTime(2017, 7, 2);
        }

        public static int CalculateAge(DateTime birthDate)
        {
            var now = CurrentDate();
            var age = now.Year - birthDate.Year;
            if (birthDate > now.AddYears(-age))
                return age - 1;
            return age;
        }
    }
}