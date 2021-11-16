using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visitor.Dal.Table
{
    /// <summary>
    /// 员工相关数据库操作
    /// </summary>
    public class Business_StaffDal
    {
        /// <summary>
        /// 获取员工信息
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public IEnumerable<T> GetBusiness_Staff<T>(DbConnection conn, DbTransaction tran = null)
        {
            #region sql
            var sql = @"
                SELECT  *
                FROM dbo.Business_Staff WITH ( NOLOCK )";
            #endregion
            return DbFactory.Query<T>(conn, sql, tran);
        }

        /// <summary>
        /// 根据id获取员工信息
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public T GetBusiness_StaffById<T>(int staffID,DbConnection conn, DbTransaction tran = null)
        {
            #region sql
            var sql = @"
                SELECT  *
                FROM      Business_Staff WITH (NOLOCK)
                WHERE   (StaffID = @StaffID)";
            #endregion
            return DbFactory.QueryFirst<T>(conn, sql, new { StaffID = staffID }, tran);
        }
    }
}
