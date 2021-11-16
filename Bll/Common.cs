namespace Visitor.Bll
{
    /// <summary>
    /// BLL公共参数
    /// </summary>
    public static class BllParams
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string ConnString { get; set; }

        /// <summary>
        /// 文件根目录
        /// </summary>
        public static string FileRoot { get; set; }

        /// <summary>
        /// 前台页面地址
        /// </summary>
        public static string View { get; set; }
    }

    /// <summary>
    /// 查询条件
    /// </summary>
    public class QueryParams
    {
        /// <summary>
        /// 参数对象
        /// </summary>
        public dynamic Param { get; set; }

        /// <summary>
        /// 跳过数据数
        /// </summary>
        public long Skip { get; set; }

        /// <summary>
        /// 分页大小
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        public QueryParams()
        {
            Skip = 0;
            Size = 0;
        }
    }
}
