namespace FlashMapper.Models
{
    public enum PropertyNameCompareRank
    {
        DoNotMatch = 0,
        IgnoreCaseMatch = 100,
        ExactMatch = 200,
        UserSpecifiedMatch = 300,
    }
}