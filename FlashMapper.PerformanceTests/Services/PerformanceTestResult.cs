using System;

namespace FlashMapper.PerformanceTests.Services
{
    public class PerformanceTestResult
    {
        public TimeSpan InitializationTime { get; set; }
        public ParticipantTestResult[] ParticipantsResults { get; set; }
    }
}