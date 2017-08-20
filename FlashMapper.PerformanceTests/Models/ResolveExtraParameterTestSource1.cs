using System;

namespace FlashMapper.PerformanceTests.Models
{
    public class ResolveExtraParameterTestSource1
    {
        public Guid Id { get; set; }
        public string Data1 { get; set; }
        public int Data2 { get; set; }
        public double Data3 { get; set; }

        public Guid NextSourceId { get; set; }
    }
}