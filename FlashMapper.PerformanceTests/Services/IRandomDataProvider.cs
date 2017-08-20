namespace FlashMapper.PerformanceTests.Services
{
    public interface IRandomDataProvider
    {
        int GetInt();
        byte GetByte();
        double GetDouble();
        string GetString();
    }
}