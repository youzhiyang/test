using System.Collections.Generic;
using System.Data.Common;
using Visitor.Model.Entity.Table;
using Visitor.Model.Entity.View;

namespace Visitor.Dal.Table
{
    /// <summary>
    /// 身份证件信息相关数据库操作
    /// </summary>
    public class Business_CertInfoDal
    {
        /// <summary>
        /// 获取身份证件信息
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="certID">身份证件号</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public T GetBusiness_CertInfo<T>(string certID, DbConnection conn, DbTransaction tran = null)
        {
            #region sql
            var sql = @"
                SELECT  *
                FROM    dbo.Business_CertInfo WITH ( NOLOCK )
                WHERE   CertID = @CertID";
            #endregion
            return DbFactory.QueryFirst<T>(conn, sql, new { CertID = certID }, tran);
        }

        /// <summary>
        /// 新增/更新身份证件信息列表
        /// </summary>
        /// <param name="certInfos">身份证件信息列表</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public int UpsertBusiness_CertInfos(IEnumerable<Business_CertInfo> certInfos, DbConnection conn, DbTransaction tran = null)
        {
            #region sql
            var sql = @"
                IF (
                       SELECT   COUNT( * )
                       FROM     dbo.Business_CertInfo
                       WHERE    CertID = @CertID
                   ) = 0
                    BEGIN
                        INSERT INTO dbo.Business_CertInfo
                        (
                            CertID ,
                            CertTypeCode ,
                            CertTypeName ,
                            [Name] ,
                            Gender ,
                            Nation ,
                            BornDay ,
                            CertAddress ,
                            CertOrg ,
                            EffDate ,
                            ExpDate
                        )
                        VALUES
                        (
                            @CertID ,
                            @CertTypeCode ,
                            @CertTypeName ,
                            @Name ,
                            @Gender ,
                            @Nation ,
                            @BornDay ,
                            @CertAddress ,
                            @CertOrg ,
                            @EffDate ,
                            @ExpDate
                        )
                    END
                ELSE
                    BEGIN
                        UPDATE  dbo.Business_CertInfo
                        SET     CertTypeCode = @CertTypeCode ,
                                CertTypeName = @CertTypeName ,
                                [Name] = @Name ,
                                Gender = @Gender ,
                                Nation = @Nation ,
                                BornDay = @BornDay ,
                                CertAddress = @CertAddress ,
                                CertOrg = @CertOrg ,
                                EffDate = @EffDate ,
                                ExpDate = @ExpDate
                        WHERE   CertID = @CertID
                    END";
            #endregion
            return DbFactory.Execute(conn, sql, certInfos, tran);
        }

        /// <summary>
        /// 新增/更新身份证件信息列表
        /// </summary>
        /// <param name="certInfos">身份证件信息列表</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public int UpsertBusiness_CertInfos(IEnumerable<View_Cert> certInfos, DbConnection conn, DbTransaction tran = null)
        {
            #region sql
            var sql = @"
                IF (
                       SELECT   COUNT( * )
                       FROM     dbo.Business_CertInfo
                       WHERE    CertID = @CertID
                   ) = 0
                    BEGIN
                        INSERT INTO dbo.Business_CertInfo
                        (
                            CertID ,
                            CertTypeCode ,
                            CertTypeName ,
                            [Name] ,
                            Gender ,
                            Nation ,
                            BornDay ,
                            CertAddress ,
                            CertOrg ,
                            EffDate ,
                            ExpDate
                        )
                        VALUES
                        (
                            @CertID ,
                            @CertTypeCode ,
                            @CertTypeName ,
                            @Name ,
                            @Gender ,
                            @Nation ,
                            @BornDay ,
                            @CertAddress ,
                            @CertOrg ,
                            @EffDate ,
                            @ExpDate
                        )
                    END
                ELSE
                    BEGIN
                        UPDATE  dbo.Business_CertInfo
                        SET     CertTypeCode = @CertTypeCode ,
                                CertTypeName = @CertTypeName ,
                                [Name] = @Name ,
                                Gender = @Gender ,
                                Nation = @Nation ,
                                BornDay = @BornDay ,
                                CertAddress = @CertAddress ,
                                CertOrg = @CertOrg ,
                                EffDate = @EffDate ,
                                ExpDate = @ExpDate
                        WHERE   CertID = @CertID
                    END";
            #endregion
            return DbFactory.Execute(conn, sql, certInfos, tran);
        }

        /// <summary>
        /// 删除身份证件信息列表
        /// </summary>
        /// <param name="certInfos">身份证件信息列表</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public int DeleteBusiness_CertInfos(IEnumerable<Business_CertInfo> certInfos, DbConnection conn, DbTransaction tran = null)
        {
            #region sql
            var sql = @"
                DELETE  dbo.Business_CertInfo
                WHERE   CertID = @CertID";
            #endregion
            return DbFactory.Execute(conn, sql, certInfos, tran);
        }

        /// <summary>
        /// 删除身份证件信息列表
        /// </summary>
        /// <param name="certInfos">身份证件信息列表</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public int DeleteBusiness_CertInfos(IEnumerable<View_Cert> certInfos, DbConnection conn, DbTransaction tran = null)
        {
            #region sql
            var sql = @"
                DELETE  dbo.Business_CertInfo
                WHERE   CertID = @CertID";
            #endregion
            return DbFactory.Execute(conn, sql, certInfos, tran);
        }
    }
}
