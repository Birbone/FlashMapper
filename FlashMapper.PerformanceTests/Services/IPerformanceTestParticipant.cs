namespace FlashMapper.PerformanceTests.Services
{
    public interface IPerformanceTestParticipant<in TModel>
    {
        string Name { get; }
        void Initialize();
        void Convert(TModel source);
        void MapData(TModel source);
    }
}