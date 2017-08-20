using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FlashMapper.PerformanceTests.Services.IdenticalModelsTest;

namespace FlashMapper.PerformanceTests.Services
{
    public class PerformanceTest<TModel> : IPerformanceTest
    {
        private readonly IPerformanceTestDataProvider<TModel> dataProvider;
        private readonly IEnumerable<IPerformanceTestParticipant<TModel>> participants;

        public PerformanceTest(IEnumerable<IPerformanceTestParticipant<TModel>> participants, IPerformanceTestDataProvider<TModel> dataProvider)
        {
            this.dataProvider = dataProvider;
            this.participants = participants.ToArray();
        }

        public PerformanceTestResult Execute()
        {
            var testResult = new PerformanceTestResult();
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var testData = dataProvider.GetData();
            stopwatch.Stop();
            testResult.InitializationTime = stopwatch.Elapsed;
            var participantsTestResults = new List<ParticipantTestResult>();
            foreach (var participant in participants)
            {
                var participantTestResult = new ParticipantTestResult { ParticipantName = participant.Name, NumberOfExecutions = testData.Length };
                stopwatch.Reset();
                stopwatch.Start();
                participant.Initialize();
                stopwatch.Stop();
                participantTestResult.InitializationTime = stopwatch.Elapsed;
                stopwatch.Reset();
                stopwatch.Start();
                foreach (var model in testData)
                {
                    participant.Convert(model);
                }
                stopwatch.Stop();
                participantTestResult.TotalConvertionTime = stopwatch.Elapsed;
                stopwatch.Reset();
                stopwatch.Start();
                foreach (var model in testData)
                {
                    participant.MapData(model);
                }
                stopwatch.Stop();
                participantTestResult.TotalMapTime = stopwatch.Elapsed;
                participantsTestResults.Add(participantTestResult);
            }
            testResult.ParticipantsResults = participantsTestResults.ToArray();
            return testResult;
        }
    }
}