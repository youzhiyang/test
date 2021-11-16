using System.Collections.Generic;
using System.Data.Common;
using Visitor.Model.Entity.Table;

namespace Visitor.Dal.Table
{
    /// <summary>
    /// 访客记录相关数据库操作
    /// </summary>
    public class Business_VisitorRecordDal
    {
        /// <summary>
        /// 获取访客信息
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="visitRecordID">来访记录ID</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public IEnumerable<T> GetBusiness_VisitRecords<T>(int visitRecordID, DbConnection conn, DbTransaction tran = null)
        {
            #region sql
            var sql = @"
                SELECT  VisitorRecordID ,
                        VisitRecordID ,
                        CertID ,
                        VisitOrgization ,
                        EnterTime ,
                        LeaveTime
                FROM    dbo.Business_VisitorRecord WITH(NOLOCK)
                WHERE   VisitRecordID = @VisitRecordID";
            #endregion
            return DbFactory.Query<T>(conn, sql, new { VisitRecordID = visitRecordID }, tran);
        }

        /// <summary>
        /// 获取最新未离场访客信息
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="certID">身份证件号</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public T GetBusiness_VisitorRecords<T>(string certID, DbConnection conn, DbTransaction tran = null)
        {
            #region sql
            var sql = @"
                SELECT *
                FROM dbo.Business_VisitorRecord v, dbo.Business_CertInfo c, dbo.Business_CertExtInfo b WITH (NOLOCK)
                WHERE EnterTime = (
		                SELECT MAX(EnterTime) AS 'maxEnterTime'
		                FROM dbo.Business_VisitorRecord WITH (NOLOCK)
		                WHERE CertID = @CertID
		                GROUP BY CertID
	                )
	                AND v.CertID = c.CertID
                    AND c.CertID = b.CertID
	                AND v.CertID = @CertID
	                AND v.LeaveTime IS NULL";
            #endregion
            return DbFactory.QueryFirst<T>(conn, sql, new { CertID = certID }, tran);
        }

        /// <summary>
        /// 获取最新历史来访记录
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="certID">身份证件号</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public T GetBusiness_HistoryVisitorRecords<T>(string certID, DbConnection conn, DbTransaction tran = null)
        {
            #region sql
            var sql = @"
                SELECT *
                FROM dbo.Business_VisitorRecord v, dbo.Business_CertInfo c, dbo.Business_CertExtInfo b WITH (NOLOCK)
                WHERE EnterTime = (
		                SELECT MAX(EnterTime) AS 'maxEnterTime'
		                FROM dbo.Business_VisitorRecord WITH (NOLOCK)
		                WHERE CertID = @CertID
		                GROUP BY CertID
	                )
	                AND v.CertID = c.CertID
                    AND c.CertID = b.CertID
	                AND v.CertID = @CertID
	                AND v.LeaveTime IS NOT NULL";
            #endregion
            return DbFactory.QueryFirst<T>(conn, sql, new { CertID = certID }, tran);
        }

        /// <summary>
        /// 新增/更新访客记录列表
        /// </summary>
        /// <param name="visitorRecords">访客记录列表</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public int UpsertBusiness_VisitorRecords(IEnumerable<Business_VisitorRecord> visitorRecords, DbConnection conn, DbTransaction tran = null)
        {
            #region sql
            var sql = @"
                IF (
                       SELECT   COUNT( * )
                       FROM     dbo.Business_VisitorRecord
                       WHERE    VisitRecordID = @VisitRecordID
                                AND CertID = @CertID
                                AND VisitorRecordID <> @VisitorRecordID
                   ) > 0
                    BEGIN
                        DECLARE @Err NVARCHAR(100) = '访客已登记。'
                        RAISERROR( @Err, 16, 1 )
                    END
                ELSE IF (
                            SELECT  COUNT( * )
                            FROM    dbo.Business_VisitorRecord
                            WHERE   VisitorRecordID = @VisitorRecordID
                        ) = 0
                         BEGIN
                             INSERT INTO    dbo.Business_VisitorRecord
                             (
                                 VisitRecordID ,
                                 CertID ,
                                 VisitOrgization ,
                                 EnterTime ,
                                 LeaveTime,
                                 Phone
                             )
                             VALUES
                             (
                                 @VisitRecordID ,
                                 @CertID ,
                                 @VisitOrgization ,
                                 @EnterTime ,
                                 @LeaveTime,
                                 @Phone
                             )
                         END
                ELSE
                         BEGIN
                             UPDATE dbo.Business_VisitorRecord
                             SET    VisitRecordID = @VisitRecordID ,
                                    CertID = @CertID ,
                                    VisitOrgization = @VisitOrgization ,
                                    EnterTime = @EnterTime ,
                                    LeaveTime = @LeaveTime,
                                    Phone = @Phone
                             WHERE  VisitorRecordID = @VisitorRecordID
                         END";
            #endregion
            return DbFactory.Execute(conn, sql, visitorRecords, tran);
        }

        /// <summary>
        /// 新增/更新访客记录
        /// </summary>
        /// <param name="visitorRecord">访客记录</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public int UpsertBusiness_VisitorRecord(Business_VisitorRecord visitorRecord, DbConnection conn, DbTransaction tran = null)
        {
            #region sql
            var sql = @"
                IF (
                       SELECT   COUNT( * )
                       FROM     dbo.Business_VisitorRecord
                       WHERE    VisitRecordID = @VisitRecordID
                                AND CertID = @CertID
                                AND VisitorRecordID <> @VisitorRecordID
                   ) > 0
                    BEGIN
                        DECLARE @Err NVARCHAR(100) = '访客已登记。'
                        RAISERROR( @Err, 16, 1 )
                    END
                ELSE IF (
                            SELECT  COUNT( * )
                            FROM    dbo.Business_VisitorRecord
                            WHERE   VisitorRecordID = @VisitorRecordID
                        ) = 0
                         BEGIN
                             INSERT INTO    dbo.Business_VisitorRecord
                             (
                                 VisitRecordID ,
                                 CertID ,
                                 VisitOrgization ,
                                 EnterTime ,
                                 LeaveTime,
                                 Phone
                             )
                             VALUES
                             (
                                 @VisitRecordID ,
                                 @CertID ,
                                 @VisitOrgization ,
                                 @EnterTime ,
                                 @LeaveTime,
                                 @Phone
                             )
                             SELECT @@IDENTITY
                         END
                ELSE
                         BEGIN
                             UPDATE dbo.Business_VisitorRecord
                             SET    VisitRecordID = @VisitRecordID ,
                                    CertID = @CertID ,
                                    VisitOrgization = @VisitOrgization ,
                                    EnterTime = @EnterTime ,
                                    LeaveTime = @LeaveTime,
                                    Phone = @Phone
                             WHERE  VisitorRecordID = @VisitorRecordID
                         END";
            #endregion
            return DbFactory.QueryFirst<int>(conn, sql, visitorRecord, tran);
        }

        /// <summary>
        /// 更新访客进入时间
        /// </summary>
        /// <param name="visitorRecord">访客进入时间列表</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public int UpdateEnterTime(Business_VisitorRecord visitorRecord, DbConnection conn, DbTransaction tran = null)
        {
            #region sql
            var sql = @"
                UPDATE  dbo.Business_VisitorRecord
                SET     EnterTime = GETDATE()
                WHERE   VisitorRecordID = @VisitorRecordID";
            #endregion
            return DbFactory.Execute(conn, sql, visitorRecord, tran);
        }

        /// <summary>
        /// 更新访客离开时间
        /// </summary>
        /// <param name="visitorRecord">访客离开时间</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public int UpdateLeaveTime(Business_VisitorRecord visitorRecord, DbConnection conn, DbTransaction tran = null)
        {
            #region sql
            var sql = @"
                UPDATE  dbo.Business_VisitorRecord
                SET     LeaveTime = GETDATE(),
                        LeaveGuide = @LeaveGuide
                WHERE   VisitorRecordID = @VisitorRecordID";
            #endregion
            return DbFactory.Execute(conn, sql, visitorRecord, tran);
        }

        /// <summary>
        /// 更新访客离开时间
        /// </summary>
        /// <param name="certs">访客离开时间</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public int UpdateLeaveTime(string certID, DbConnection conn, DbTransaction tran = null)
        {
            #region sql
            var sql = @"
                UPDATE  dbo.Business_VisitorRecord
                SET     LeaveTime = GETDATE()
                WHERE   CertID = @CertID";
            #endregion
            return DbFactory.Execute(conn, sql, new { CertID  = certID }, tran);
        }

        /// <summary>
        /// 删除访客记录列表
        /// </summary>
        /// <param name="visitorRecords">访客记录列表</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public int DeleteBusiness_VisitorRecords(IEnumerable<Business_VisitorRecord> visitorRecords, DbConnection conn, DbTransaction tran = null)
        {
            #region sql
            var sql = @"
                DELETE  dbo.Business_VisitorRecord
                WHERE   VisitorRecordID = @VisitorRecordID";
            #endregion
            return DbFactory.Execute(conn, sql, visitorRecords, tran);
        }
    }
}
