using System.Collections.Generic;
using System.Data.Common;
using Visitor.Model.Entity.Table;

namespace Visitor.Dal.Table
{
    /// <summary>
    /// 访客时间线相关数据库操作
    /// </summary>
    public class Business_VisitorTimelineDal
    {
        /// <summary>
        /// 新增/更新访客时间线列表
        /// </summary>
        /// <param name="visitorTimelines">访客时间线列表</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public int UpsertBusiness_VisitorTimelines(IEnumerable<Business_VisitorTimeline> visitorTimelines, DbConnection conn, DbTransaction tran = null)
        {
            #region sql
            var sql = @"
                INSERT INTO dbo.Business_VisitorTimeline
                (
                    RecordTime ,
                    IsLeaveTime
                )
                VALUES
                (
                    @RecordTime ,
                    @IsLeaveTime
                )";
            #endregion
            return DbFactory.Execute(conn, sql, visitorTimelines, tran);
        }
    }
}
