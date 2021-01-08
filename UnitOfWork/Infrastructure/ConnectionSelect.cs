namespace UnitOfWorks.Infrastructure
{
    /// <summary>
    /// 資料庫連線字串
    /// </summary>
    public enum ConnectionSelect
    {
        /// <summary>
        /// 測試機
        /// </summary>
        MsSqlTest,

        /// <summary>
        /// 正式機
        /// </summary>
        MsSqlLive,

        /// <summary>
        /// Sqlite
        /// </summary>
        SqlLiteLocalDb
    }
}
