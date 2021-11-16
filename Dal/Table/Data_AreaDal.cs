using System.Collections.Generic;
using System.Data.Common;

namespace Visitor.Dal.Table
{
    /// <summary>
    /// 区域相关数据库操作
    /// </summary>
    public class Data_AreaDal
    {
        /// <summary>
        /// 获取区域列表
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public IEnumerable<T> GetAreas<T>(DbConnection conn, DbTransaction tran = null)
        {
            #region sql
            var sql = @"
                SELECT      *
                FROM        dbo.Data_Area WITH ( NOLOCK )
                ORDER BY    [Index]";
            #endregion
            return DbFactory.Query<T>(conn, sql, tran);
        }

        /// <summary>
        /// 根据ids获取区域列表
        /// </summary>
        /// <typeparam name="ids">区域id列表</typeparam>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public IEnumerable<T> GetAreasByIds<T>(int[] ids, DbConnection conn, DbTransaction tran = null)
        {
            #region sql
            var sql = @"
            SELECT  *
            FROM      Data_Area
            WHERE   (ID IN @Ids)";
            #endregion
            return DbFactory.Query<T>(conn, sql, new { Ids = ids }, tran);
        }
    }
}
