using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows;
using FlashMapper.PerformanceTests.Models;
using FlashMapper.PerformanceTests.Services;
using FlashMapper.PerformanceTests.Services.IdenticalModelsTest;
using FlashMapper.Services;

namespace FlashMapper.PerformanceTests
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly PerformanceTest<IdenticalTestSource> identicalModelsTest;
        private readonly PerformanceTest<IgnoreTestSource> ignoreTest;
        private readonly IPerformanceTestDataProvider<IdenticalTestSource> identicalModelsDataProvider;
        private readonly IPerfomanceTestConfiguration configuration;

        public MainWindow(PerformanceTest<IdenticalTestSource> identicalModelsTest,
            PerformanceTest<IgnoreTestSource> ignoreTest, 
            IPerformanceTestDataProvider<IdenticalTestSource> identicalModelsDataProvider,
            IPerfomanceTestConfiguration configuration)
        {
            this.identicalModelsTest = identicalModelsTest;
            this.ignoreTest = ignoreTest;
            this.identicalModelsDataProvider = identicalModelsDataProvider;
            this.configuration = configuration;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var testResults = identicalModelsTest.Execute();
            ResultsGrid.ItemsSource = testResults.ParticipantsResults;
            TestName.Content = "IdenticalModelsTest results";
            TestInfo.Content = $"Initialilzation time {testResults.InitializationTime:c}";
        }

        private void IgnoreTestButton_Click(object sender, RoutedEventArgs e)
        {
            var testResults = ignoreTest.Execute();
            ResultsGrid.ItemsSource = testResults.ParticipantsResults;
            TestName.Content = "IgnoreTest results";
            TestInfo.Content = $"Initialilzation time {testResults.InitializationTime:c}";
        }

        private void FlashMapperInternal_Click(object sender, RoutedEventArgs e)
        {
            var stopwatch = new Stopwatch();
            var mappingConfiguration = new MappingConfiguration();
            mappingConfiguration.CreateMapping<IdenticalTestSource, Destination>();
            var storage = (IMappingsStorage)typeof(MappingConfiguration)
                .GetField("mappingsStorage", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(mappingConfiguration);
            stopwatch.Start();
            var testData = identicalModelsDataProvider.GetData();
            stopwatch.Stop();
            TestName.Content = "InternalTest results";
            TestInfo.Content = $"Initialilzation time {stopwatch.Elapsed:c}";
            stopwatch.Reset();
            var numberOfExecutions = configuration.NumberOfExecutions;
            var testResults = new List<Tuple<string, TimeSpan>>();
            stopwatch.Start();
            for (int i = 0; i < numberOfExecutions; i++)
            {
                storage.GetMapping<IdenticalTestSource, Destination>();
            }
            stopwatch.Stop();
            testResults.Add(new Tuple<string, TimeSpan>("GetMapping", stopwatch.Elapsed));
            stopwatch.Reset();
            var convertMethod = storage.GetMapping<IdenticalTestSource, Destination>().BuildFunction;
            stopwatch.Start();
            for (int i = 0; i < numberOfExecutions; i++)
            {
                convertMethod(testData[i]);
            }
            stopwatch.Stop();
            testResults.Add(new Tuple<string, TimeSpan>("Execute", stopwatch.Elapsed));
            stopwatch.Reset();
            var manualBuilder = new ManualIdenticalBuilder();
            stopwatch.Start();
            for (int i = 0; i < numberOfExecutions; i++)
            {
                manualBuilder.Build(testData[i]);
            }
            stopwatch.Stop();
            testResults.Add(new Tuple<string, TimeSpan>("ManualExecute", stopwatch.Elapsed));
            Expression<Func<IdenticalTestSource, Destination>> manualMethodCall = i => manualBuilder.Build(i);
            var manualMethodCallCompiled = manualMethodCall.Compile();
            stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < numberOfExecutions; i++)
            {
                manualMethodCallCompiled(testData[i]);
            }
            stopwatch.Stop();
            testResults.Add(new Tuple<string, TimeSpan>("ManualRecompiled", stopwatch.Elapsed));
            ResultsGrid.ItemsSource = testResults.Select(r => new
            {
                Name = r.Item1,
                ExecutionTime = r.Item2
            });
        }
    }
}
