namespace FlashMapper
{
    /// <summary>
    /// Describes FlashMapper behavior in case when property in destination model can be mapped from few properties in source model
    /// </summary>
    public enum SelectSourceCollisionBehavior
    {
        Inherit = 0,
        Fail = 1,
        ChooseAny = 2,
    }
}