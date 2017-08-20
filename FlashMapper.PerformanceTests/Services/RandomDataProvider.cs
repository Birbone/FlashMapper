using System;

namespace FlashMapper.PerformanceTests.Services
{
    public class RandomDataProvider : IRandomDataProvider
    {
        private readonly Random rand;
        public RandomDataProvider()
        {
            rand = new Random();
        }

        public int GetInt()
        {
            return rand.Next();
        }

        public byte GetByte()
        {
            return (byte) rand.Next(byte.MaxValue);
        }

        public double GetDouble()
        {
            return rand.NextDouble();
        }

        public string GetString()
        {
            return Guid.NewGuid().ToString();
        }
    }
}