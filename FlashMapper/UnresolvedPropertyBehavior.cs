namespace FlashMapper
{
    /// <summary>
    /// Describes FlashMapper behavior when property in destination model cannot be mapped from source model
    /// </summary>
    public enum UnresolvedPropertyBehavior
    {
        Inherit = 0,
        Fail = 1,
        Ignore = 2,
    }
}