using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Visitor.Model.Entity.Custom;
using Visitor.Model.Entity.Table;

namespace Visitor.Dal.Table
{
    /// <summary>
    /// 来访记录相关数据库操作
    /// </summary>
    public class Business_VisitRecordDal
    {
        /// <summary>
        /// 分页获取来访记录列表
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="condition">条件</param>
        /// <param name="page">页数</param>
        /// <param name="size">分页大小</param>
        /// <param name="total">总数据数</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public IEnumerable<T> GetBusiness_VisitRecord<T>(string condition, long page, long size, out long total, DbConnection conn, DbTransaction tran = null)
        {
            #region sql
            var sql = string.Format(@"
                SELECT  COUNT( * ) Total
                FROM    dbo.Business_VisitRecord WITH ( NOLOCK )
                {0}
                SELECT  *
                FROM    (
                            SELECT  * ,
                                    ROW_NUMBER() OVER ( ORDER BY RegisterTime ASC ,
                                                                 Room ASC
                                                      ) row
                            FROM    dbo.Business_VisitRecord WITH ( NOLOCK )
                            {0}
                        ) temp
                WHERE   temp.row BETWEEN @Skip AND @Take", string.IsNullOrEmpty(condition) ? "" : @"
                WHERE   Reason LIKE '%' + @Reason + '%'
                        OR  DeviceNTools LIKE '%' + @DeviceNTools + '%'");
            #endregion
            var result = DbFactory.QueryMultiple(conn, sql, new { Param = condition, Skip = (page - 1) * size + 1, Take = page * size }, tran);
            total = result.Read<long>().Single();
            return result.Read<T>();
        }

        /// <summary>
        /// 获取来访记录
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="visitRecordID">来访记录ID</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public T GetBusiness_VisitRecord<T>(int visitRecordID, DbConnection conn, DbTransaction tran = null)
        {
            #region sql
            var sql = @"
                SELECT  *
                FROM    dbo.Business_VisitRecord WITH ( NOLOCK )
                WHERE   VisitRecordID = @VisitRecordID";
            #endregion
            return DbFactory.QueryFirst<T>(conn, sql, new { VisitRecordID = visitRecordID }, tran);
        }

        /// <summary>
        /// 分页获取最新来访记录
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="conn">数据库连接</param>
        /// <param name="page">页数</param>
        /// <param name="size">分页大小</param>
        /// <param name="tran">数据库连接</param>
        /// <param name="tran">排序方式</param>
        /// <param name="splitOn">分隔字段</param>
        /// <returns></returns>
        public IEnumerable<VisitRecord> GetBusiness_VisitRecord( DbConnection conn, long page = 1, long size = 20, DbTransaction tran = null, string orderBy = "ASC", string splitOn = "VisitRecordID")
        {
            #region sql
            var sql = string.Format(@"
                SELECT temp1.*, v5.StaffID, v5.StaffName, v6.*
                FROM (
	                SELECT DISTINCT v1.VisitRecordID, v1.ReasonCode, v1.ReasonName, v1.Reason, v1.Areas
		                , v1.DeviceNTools, v1.RegisterTime, v1.EnterGuide, v2.VisitorRecordID, v2.CertID
		                , v2.VisitOrgization, v2.EnterTime, v2.LeaveTime, v2.Phone, v2.LeaveGuide
		                , v4.CertTypeCode, v4.CertTypeName, v4.Name, v4.Gender, v4.Nation
		                , v4.BornDay, v4.CertAddress, v4.CertOrg, v4.EffDate, v4.ExpDate
		                , v3.StaffID, v3.StaffName
	                FROM dbo.Business_VisitRecord v1, dbo.Business_VisitorRecord v2, dbo.Business_Staff v3, dbo.Business_CertInfo v4 WITH (NOLOCK)
	                WHERE v1.VisitRecordID = v2.VisitRecordID
		                AND v2.VisitRecordID IN (
			                SELECT temp.VisitRecordID
			                FROM (
				                SELECT VisitRecordID, ROW_NUMBER() OVER (ORDER BY RegisterTime {0}) AS row
				                FROM dbo.Business_VisitRecord WITH (NOLOCK)
			                ) temp
			                WHERE temp.row BETWEEN @Skip AND @Take
		                )
		                AND v1.EnterGuide = v3.StaffID
		                AND v4.CertID = v2.CertID
                ) temp1
	                LEFT JOIN dbo.Business_Staff v5 ON temp1.LeaveGuide = v5.StaffID
	                INNER JOIN dbo.Data_Area v6 ON v6.ID IN (
			                SELECT value
			                FROM OPENJSON(temp1.Areas)
		                )
                ORDER BY temp1.RegisterTime {0}", orderBy);
            #endregion
            var visitRecordDictionary = new Dictionary<int, VisitRecord>();
            return DbFactory.Query<VisitRecord, Visitor.Model.Entity.Custom.Visitor, Business_Staff, Business_Staff, Data_Area,VisitRecord>(conn, sql, (visitRecord, visitor,enterStaff,leaveStaff, dataArea) =>
            {
                visitor.LeaveGuideEntity = leaveStaff;
                VisitRecord visitRecordEntity = null;
                if (!visitRecordDictionary.TryGetValue(visitRecord.VisitRecordID, out visitRecordEntity))
                {
                    visitRecordEntity = visitRecord;
                    visitRecordEntity.VisitorList = new List<Visitor.Model.Entity.Custom.Visitor>();
                    visitRecordEntity.DataAreaList = new List<Data_Area>();
                    visitRecordDictionary.Add(visitRecord.VisitRecordID, visitRecordEntity);
                }
                if (!visitRecordEntity.VisitorList.Contains(visitor))
                {
                    visitRecordEntity.VisitorList.Add(visitor);
                }
                if (!visitRecordEntity.DataAreaList.Contains(dataArea))
                {
                    visitRecordEntity.DataAreaList.Add(dataArea);
                }
                visitRecordEntity.EnterGuideEntity = enterStaff;
                return visitRecordEntity;
            }, new { Skip = (page - 1) * size + 1, Take = page * size }, tran, splitOn: "VisitorRecordID,StaffID,StaffID,ID").Distinct();
        }

        /// <summary>
        /// 获取来访记录
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="authCode">来访授权码</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public T GetBusiness_VisitRecord<T>(string authCode, DbConnection conn, DbTransaction tran = null)
        {
            #region sql
            var sql = @"
                SELECT  *
                FROM    dbo.Business_VisitRecord WITH ( NOLOCK )
                WHERE   AuthCode = @AuthCode";
            #endregion
            return DbFactory.QueryFirst<T>(conn, sql, new { AuthCode = authCode }, tran);
        }

        /// <summary>
        /// 新增/更新来访记录列表
        /// </summary>
        /// <param name="visitRecords">来访记录列表</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public int UpsertBusiness_VisitRecords(IEnumerable<Business_VisitRecord> visitRecords, DbConnection conn, DbTransaction tran = null)
        {
            #region sql
            var sql = @"
                IF (
                       SELECT   COUNT( * )
                       FROM     dbo.Business_VisitRecord
                       WHERE    VisitRecordID = @VisitRecordID
                   ) = 0
                    BEGIN
                        INSERT INTO dbo.Business_VisitRecord
                        (
                            AuthCode ,
                            ReasonCode ,
                            ReasonName ,
                            Reason ,
                            Areas ,
                            DeviceNTools ,
                            RegisterTime,
                            EnterGuide
                        )
                        VALUES
                        (
                            @AuthCode ,
                            @ReasonCode ,
                            @ReasonName ,
                            @Reason ,
                            @Areas ,
                            @DeviceNTools ,
                            GETDATE(),
                            @EnterGuide
                        )
                    END
                ELSE
                    BEGIN
                        UPDATE  dbo.Business_VisitRecord
                        SET     ReasonCode = @ReasonCode ,
                                ReasonName = @ReasonName ,
                                Reason = @Reason ,
                                Areas = @Areas ,
                                DeviceNTools = @DeviceNTools
                        WHERE   VisitRecordID = @VisitRecordID
                    END";
            #endregion
            return DbFactory.Execute(conn, sql, visitRecords, tran);
        }

        /// <summary>
        /// 新增/更新来访记录
        /// </summary>
        /// <param name="visitRecord">来访记录</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public int UpsertBusiness_VisitRecord(Business_VisitRecord visitRecord, DbConnection conn, DbTransaction tran = null)
        {
            #region sql
            var sql = @"
                IF (
                       SELECT   COUNT( * )
                       FROM     dbo.Business_VisitRecord
                       WHERE    VisitRecordID = @VisitRecordID
                   ) = 0
                    BEGIN
                        INSERT INTO dbo.Business_VisitRecord
                        (
                            AuthCode ,
                            ReasonCode ,
                            ReasonName ,
                            Reason ,
                            Areas ,
                            DeviceNTools ,
                            RegisterTime,
                            EnterGuide
                        )
                        VALUES
                        (
                            @AuthCode ,
                            @ReasonCode ,
                            @ReasonName ,
                            @Reason ,
                            @Areas ,
                            @DeviceNTools ,
                            GETDATE(),
                            @EnterGuide
                        )
                        SELECT  @@IDENTITY
                    END
                ELSE
                    BEGIN
                        UPDATE  dbo.Business_VisitRecord
                        SET     ReasonCode = @ReasonCode ,
                                ReasonName = @ReasonName ,
                                Reason = @Reason ,
                                Areas = @Areas ,
                                DeviceNTools = @DeviceNTools,
                                EnterGuide = @EnterGuide
                        WHERE   VisitRecordID = @VisitRecordID
                    END";
            #endregion
            return DbFactory.QueryFirst<int>(conn, sql, visitRecord, tran);
        }

        /// <summary>
        /// 删除来访记录列表
        /// </summary>
        /// <param name="visitRecords">来访记录列表</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public int DeleteBusiness_VisitRecords(IEnumerable<Business_VisitRecord> visitRecords, DbConnection conn, DbTransaction tran = null)
        {
            #region sql
            var sql = @"
                DELETE  dbo.Business_VisitRecord
                WHERE   VisitRecordID = @VisitRecordID";
            #endregion
            return DbFactory.Execute(conn, sql, visitRecords, tran);
        }
    }
}
