namespace Fishing
{
    public enum EnumGrid : int
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,
        /// <summary>
        /// 战利品
        /// </summary>
        Bag = 1,
        /// <summary>
        /// 道具
        /// </summary>
        Prop = 2,
        /// <summary>
        /// 遗物
        /// </summary>
        Relic = 3,
        /// <summary>
        /// 商店
        /// </summary>
        Store = 4,
        /// <summary>
        /// 强化材料
        /// </summary>
        Fortify = 5,
    }
    public enum EnumGridImage : int
    {
        /// <summary>
        /// 默认
        /// </summary>
        Default = 0,
        Disable = 1,
        Unknown = 2,
        Select = 3,
    }
    public enum EnumItemType : int
    {
        /// <summary>
        /// 矿物
        /// </summary>
        Mineral = 0,
        /// <summary>
        /// 遗物
        /// </summary>
        Relic = 1,
        /// <summary>
        /// 材料
        /// </summary>
        Fortify = 2,
        /// <summary>
        /// 道具
        /// </summary>
        Prop = 3,
    }

    public enum EnumBoolData : int
    {
        IsNewGame = 1,
        IsTutorialFinish = 0,
    }
    // public enum EnumVector3Data
    // {
    //     PlayerPosition,
    // }
    public enum EnumDayState : int
    {
        清晨 = 0,
        工作时间 = 1,
        黄昏 = 2,
    }
    public enum EnumTimeLine : int
    {
        TestTimeLine = 0,
    }
}
