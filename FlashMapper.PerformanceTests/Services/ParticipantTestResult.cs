using System;

namespace FlashMapper.PerformanceTests.Services
{
    public class ParticipantTestResult
    {
        public string ParticipantName { get; set; }
        public TimeSpan InitializationTime { get; set; }
        public TimeSpan TotalConvertionTime { get; set; }
        public TimeSpan TotalMapTime { get; set; }
        public int NumberOfExecutions { get; set; }
    }
}