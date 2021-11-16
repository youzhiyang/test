using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visitor.Dal.Table
{
    /// <summary>
    /// 设备工具相关数据库操作
    /// </summary>
    public class Business_DeviceToolsDal
    {
        /// <summary>
        /// 获取设备工具
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public IEnumerable<T> GetDeviceTools<T>(DbConnection conn, DbTransaction tran = null)
        {
            #region sql
            var sql = @"
                SELECT DISTINCT t.DictID, t.DeviceID, t.DeviceName, d.Name, d.[Order]
                FROM dbo.Business_DeviceTools t, dbo.Data_Dictionary d WITH ( NOLOCK )
                WHERE t.DictID = d.DictID";
            #endregion
            return DbFactory.Query<T>(conn, sql, tran);
        }
    }
}
