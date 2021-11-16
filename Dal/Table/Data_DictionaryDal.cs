using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Visitor.Model.Entity.Table;

namespace Visitor.Dal.Table
{
    /// <summary>
    /// 数据字典相关数据库操作
    /// </summary>
    public class Data_DictionaryDal
    {
        /// <summary>
        /// 分页获取数据字典列表
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="condition">条件</param>
        /// <param name="page">页数</param>
        /// <param name="size">分页大小</param>
        /// <param name="total">总数据数</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public IEnumerable<T> GetDictionaries<T>(string condition, long page, long size, out long total, DbConnection conn, DbTransaction tran = null)
        {
            #region sql
            var sql = string.Format(@"
                SELECT  COUNT(*) Total
                FROM    dbo.Data_Dictionary WITH ( NOLOCK )
                {0}
                SELECT  *
                FROM    ( SELECT    * ,
                                    ROW_NUMBER() OVER ( ORDER BY [Order] ASC, Editable ASC, DataType ASC, Code ASC ) row
                          FROM      dbo.Data_Dictionary WITH ( NOLOCK )
                          {0}
                        ) temp
                WHERE   temp.row BETWEEN @Skip AND @Take", string.IsNullOrEmpty(condition) ? "" : @"
                WHERE   DataType LIKE '%' + @Param + '%'
                        OR Code LIKE '%' + @Param + '%'
                        OR Name LIKE '%' + @Param + '%'
                        OR Value LIKE '%' + @Param + '%'");
            #endregion
            var result = DbFactory.QueryMultiple(conn, sql, new { Param = condition, Skip = (page - 1) * size + 1, Take = page * size }, tran);
            total = result.Read<long>().Single();
            return result.Read<T>();
        }

        /// <summary>
        /// 获取数据字典列表
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="dict">查询条件</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public IEnumerable<T> GetDictionaries<T>(Data_Dictionary dict, DbConnection conn, DbTransaction tran = null)
        {
            #region sql
            var condition = "";
            if (!string.IsNullOrEmpty(dict.DataType))
            {
                condition += " AND DataType = @DataType";
            }
            if (!string.IsNullOrEmpty(dict.Code))
            {
                condition += " AND Code = @Code";
            }
            if (!string.IsNullOrEmpty(dict.Value))
            {
                condition += " AND Value = @Value";
            }
            if (!string.IsNullOrEmpty(dict.ExtValue1))
            {
                condition += " AND ExtValue1 = @ExtValue1";
            }
            if (!string.IsNullOrEmpty(dict.ExtValue2))
            {
                condition += " AND ExtValue2 = @ExtValue2";
            }
            if (!string.IsNullOrEmpty(dict.ExtValue3))
            {
                condition += " AND ExtValue3 = @ExtValue3";
            }
            var sql = string.Format(@"
                SELECT      *
                FROM        dbo.Data_Dictionary WITH ( NOLOCK )
                WHERE       1 = 1
                            {0}
                ORDER BY    [Order] ASC", condition);
            #endregion
            return DbFactory.Query<T>(conn, sql, dict, tran);
        }

        /// <summary>
        /// 获取数据字典列表
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="type">数据类型</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public IEnumerable<T> GetDictionaries<T>(string type, DbConnection conn, DbTransaction tran = null)
        {
            #region sql
            var sql = @"
                SELECT  *
                FROM    dbo.Data_Dictionary WITH ( NOLOCK )
                WHERE   DataType = @DataType
                ORDER BY [Order] ASC ,
                        Value ASC ,
                        Name ASC";
            #endregion
            return DbFactory.Query<T>(conn, sql, new { DataType = type }, tran);
        }

        /// <summary>
        /// 获取数据字典
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="type">数据类型</param>
        /// <param name="code">字典代码</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public T GetDictionary<T>(string type, string code, DbConnection conn, DbTransaction tran = null)
        {
            #region sql
            var sql = @"
                SELECT      *
                FROM        dbo.Data_Dictionary WITH ( NOLOCK )
                WHERE       DataType = @DataType
                            AND Code = @Code
                ORDER BY    [Order] ASC";
            #endregion
            return DbFactory.QueryFirst<T>(conn, sql, new { DataType = type, Code = code }, tran);
        }

        /// <summary>
        /// 新增/更新数据字典列表
        /// </summary>
        /// <param name="dictionaries">数据字典列表</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public int UpsertDictionaries(IEnumerable<Data_Dictionary> dictionaries, DbConnection conn, DbTransaction tran = null)
        {
            #region sql
            var sql = @"
                IF ( SELECT COUNT(*)
                     FROM   dbo.Data_Dictionary
                     WHERE  DataType = @DataType
                            AND Code = @Code
                            AND ( DictID != @DictID
                                  OR @DictID = '00000000-0000-0000-0000-000000000000'
                                )
                   ) > 0
                    BEGIN
                        DECLARE @Err NVARCHAR(100)= '类型为' + @DataType + '且代码为' + @Code
                            + '的记录已存在。'
                        RAISERROR (@Err,16,1)
                    END
                ELSE
                    IF ( SELECT COUNT(*)
                         FROM   dbo.Data_Dictionary
                         WHERE  DictID = @DictID
                       ) = 0
                        BEGIN
                            INSERT  INTO dbo.Data_Dictionary
                                    ( DataType ,
                                      Code ,
                                      Name ,
                                      Value ,
                                      ExtValue1 ,
                                      ExtValue2 ,
                                      ExtValue3 ,
                                      Remark
                                    )
                            VALUES  ( @DataType ,
                                      @Code ,
                                      @Name ,
                                      @Value ,
                                      @ExtValue1 ,
                                      @ExtValue2 ,
                                      @ExtValue3 ,
                                      @Remark
                                    )
                        END
                    ELSE
                        BEGIN
                            UPDATE  dbo.Data_Dictionary
                            SET     DataType = @DataType ,
                                    Code = @Code ,
                                    Name = @Name ,
                                    Value = @Value ,
                                    ExtValue1 = @ExtValue1 ,
                                    ExtValue2 = @ExtValue2 ,
                                    ExtValue3 = @ExtValue3 ,
                                    Remark = @Remark
                            WHERE   DictID = @DictID
                        END";
            #endregion
            return DbFactory.Execute(conn, sql, dictionaries, tran);
        }

        /// <summary>
        /// 删除数据字典列表
        /// </summary>
        /// <param name="dictionaries">数据字典列表</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public int DeleteDictionaries(IEnumerable<Data_Dictionary> dictionaries, DbConnection conn, DbTransaction tran = null)
        {
            #region sql
            var sql = @"DELETE dbo.Data_Dictionary WHERE DictID = @DictID";
            #endregion
            return DbFactory.Execute(conn, sql, dictionaries, tran);
        }
    }
}
