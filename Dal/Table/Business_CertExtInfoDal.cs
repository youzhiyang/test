using System.Collections.Generic;
using System.Data.Common;
using Visitor.Model.Entity.Table;
using Visitor.Model.Entity.View;

namespace Visitor.Dal.Table
{
    /// <summary>
    /// 身份证件扩展信息相关数据库操作
    /// </summary>
    public class Business_CertExtInfoDal
    {
        /// <summary>
        /// 获取身份证件扩展信息
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="certID">身份证件号</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public T GetBusiness_CertExtInfo<T>(string certID, DbConnection conn, DbTransaction tran = null)
        {
            #region sql
            var sql = @"
                SELECT  *
                FROM    dbo.Business_CertExtInfo WITH ( NOLOCK )
                WHERE   CertID = @CertID";
            #endregion
            return DbFactory.QueryFirst<T>(conn, sql, new { CertID = certID }, tran);
        }

        /// <summary>
        /// 新增/更新身份证件扩展信息列表
        /// </summary>
        /// <param name="certExtInfos">身份证件扩展信息列表</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public int UpsertBusiness_CertExtInfos(IEnumerable<Business_CertExtInfo> certExtInfos, DbConnection conn, DbTransaction tran = null)
        {
            #region sql
            var sql = @"
                IF (
                       SELECT   COUNT( * )
                       FROM     dbo.Business_CertExtInfo
                       WHERE    CertID = @CertID
                   ) = 0
                    BEGIN
                        INSERT INTO dbo.Business_CertExtInfo
                        (
                            CertID ,
                            IdentityPic ,
                            PicFront ,
                            PicBack
                        )
                        VALUES
                        (
                            @CertID ,
                            @IdentityPic ,
                            @PicFront ,
                            @PicBack
                        )
                    END
                ELSE
                    BEGIN
                        UPDATE  dbo.Business_CertExtInfo
                        SET     IdentityPic = @IdentityPic ,
                                PicFront = @PicFront ,
                                PicBack = @PicBack
                        WHERE   CertID = @CertID
                    END";
            #endregion
            return DbFactory.Execute(conn, sql, certExtInfos, tran);
        }

        /// <summary>
        /// 新增/更新身份证件扩展信息列表
        /// </summary>
        /// <param name="certExtInfos">身份证件扩展信息列表</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public int UpsertBusiness_CertExtInfos(IEnumerable<View_Cert> certExtInfos, DbConnection conn, DbTransaction tran = null)
        {
            #region sql
            var sql = @"
                IF (
                       SELECT   COUNT( * )
                       FROM     dbo.Business_CertExtInfo
                       WHERE    CertID = @CertID
                   ) = 0
                    BEGIN
                        INSERT INTO dbo.Business_CertExtInfo
                        (
                            CertID ,
                            IdentityPic ,
                            PicFront ,
                            PicBack
                        )
                        VALUES
                        (
                            @CertID ,
                            @IdentityPic ,
                            @PicFront ,
                            @PicBack
                        )
                    END
                ELSE
                    BEGIN
                        UPDATE  dbo.Business_CertExtInfo
                        SET     IdentityPic = @IdentityPic ,
                                PicFront = @PicFront ,
                                PicBack = @PicBack
                        WHERE   CertID = @CertID
                    END";
            #endregion
            return DbFactory.Execute(conn, sql, certExtInfos, tran);
        }

        /// <summary>
        /// 删除身份证件扩展信息列表
        /// </summary>
        /// <param name="certExtInfos">身份证件扩展信息列表</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public int DeleteBusiness_CertExtInfos(IEnumerable<Business_CertExtInfo> certExtInfos, DbConnection conn, DbTransaction tran = null)
        {
            #region sql
            var sql = @"
                DELETE  dbo.Business_CertExtInfo
                WHERE   CertID = @CertID";
            #endregion
            return DbFactory.Execute(conn, sql, certExtInfos, tran);
        }

        /// <summary>
        /// 删除身份证件扩展信息列表
        /// </summary>
        /// <param name="certExtInfos">身份证件扩展信息列表</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public int DeleteBusiness_CertExtInfos(IEnumerable<View_Cert> certExtInfos, DbConnection conn, DbTransaction tran = null)
        {
            #region sql
            var sql = @"
                DELETE  dbo.Business_CertExtInfo
                WHERE   CertID = @CertID";
            #endregion
            return DbFactory.Execute(conn, sql, certExtInfos, tran);
        }
    }
}
