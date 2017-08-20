using FlashMapper.PerformanceTests.Models;
using FlashMapper.PerformanceTests.Services;
using FlashMapper.PerformanceTests.Services.IdenticalModelsTest;
using FlashMapper.PerformanceTests.Services.IgnoreTest;
using Ninject.Modules;

namespace FlashMapper.PerformanceTests
{
    public class PerformanceAppModule: NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IPerfomanceTestConfiguration>().To<PerfomanceTestConfiguration>();
            Kernel.Bind<IRandomDataProvider>().To<RandomDataProvider>();
            Kernel.Bind(typeof(PerformanceTest<>)).ToSelf();
            Kernel.Bind<MainWindow>().ToSelf();
            Kernel.Bind<IPerformanceTestDataProvider<IdenticalTestSource>>().To<IdenticalTestDataProvider>();
            Kernel.Bind<IPerformanceTestParticipant<IdenticalTestSource>>().To<ManualIdenticalTestParticipant>();
            Kernel.Bind<IPerformanceTestParticipant<IdenticalTestSource>>().To<AutoMapperIdenticalTestParticipant>();
            Kernel.Bind<IPerformanceTestParticipant<IdenticalTestSource>>().To<FlashMapperIdenticalTestParticipant>();
            Kernel.Bind<IPerformanceTestParticipant<IdenticalTestSource>>().To<FlashMapperBuilderIdenticalTestParticipant>();
            Kernel.Bind<IManualIdenticalBuilder>().To<ManualIdenticalBuilder>();
            Kernel.Bind<IFlashMapperIdenticalBuilder>().To<FlashMapperIdenticalBuilder>();

            Kernel.Bind<IPerformanceTestDataProvider<IgnoreTestSource>>().To<IgnoreTestDataProvider>();
            Kernel.Bind<IPerformanceTestParticipant<IgnoreTestSource>>().To<ManualIgnoreTestParticipant>();
            Kernel.Bind<IPerformanceTestParticipant<IgnoreTestSource>>().To<AutoMapperIgnoreTestParticipant>();
            Kernel.Bind<IPerformanceTestParticipant<IgnoreTestSource>>().To<FlashMapperIgnoreTestParticipant>();
            Kernel.Bind<IPerformanceTestParticipant<IgnoreTestSource>>().To<FlashMapperBuilderIgnoreTestParticipant>();
        }
    }
}
