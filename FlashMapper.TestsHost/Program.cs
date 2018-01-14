using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashMapper.Tests;

namespace FlashMapper.TestsHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var tests = new DependancyInjectionTests();
            tests.DependancyInjectionTest();
        }
    }
}
