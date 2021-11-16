using Dapper;
using DingTalkService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace Visitor.Dal
{
    /// <summary>
    /// DbFactory
    /// </summary>
    public class DbFactory
    {
        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <param name="connStr">数据库连接字符串</param>
        /// <returns></returns>
        public static DbConnection CreateConnection(string connStr)
        {
            var conn = new SqlConnection(connStr);
            conn.Open();
            return conn;
        }

        /// <summary>
        /// 查询并返回第一个结果
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="conn">数据库连接</param>
        /// <param name="sql">SQL</param>
        /// <param name="parameter">参数</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public static T QueryFirst<T>(DbConnection conn, string sql, object parameter = null, DbTransaction tran = null)
        {
            return conn.Query<T>(sql, parameter, tran).FirstOrDefault();
        }

        /// <summary>
        /// 一对多映射
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="conn">数据库连接</param>
        /// <param name="sql">SQL</param>
        /// <param name="parameter">参数</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth,TReturn>(DbConnection conn, string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth,TReturn> map, object parameter = null, DbTransaction tran = null, string splitOn = "Id")
        {
            return conn.Query<TFirst, TSecond, TThird, TFourth, TFifth,TReturn>(sql, map, parameter, tran, splitOn: splitOn);
        }

        /// <summary>
        /// 查询并返回结果
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="conn">数据库连接</param>
        /// <param name="sql">SQL</param>
        /// <param name="parameter">参数</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public static IEnumerable<T> Query<T>(DbConnection conn, string sql, object parameter = null, DbTransaction tran = null)
        {
            return conn.Query<T>(sql, parameter, tran);
        }

        /// <summary>
        /// 获取复合结果
        /// </summary>
        /// <param name="conn">数据库连接</param>
        /// <param name="sql">SQL</param>
        /// <param name="parameter">参数</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public static SqlMapper.GridReader QueryMultiple(DbConnection conn, string sql, object parameter = null, DbTransaction tran = null)
        {
            return conn.QueryMultiple(sql, parameter, tran);
        }

        /// <summary>
        /// 执行并返回受影响的行数
        /// </summary>
        /// <param name="conn">数据库连接</param>
        /// <param name="sql">SQL</param>
        /// <param name="parameter">参数</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public static int Execute(DbConnection conn, string sql, object parameter = null, DbTransaction tran = null)
        {
            return conn.Execute(sql, parameter, tran);
        }

        /// <summary>
        /// 执行并返回1个结果
        /// </summary>
        /// <param name="conn">数据库连接</param>
        /// <param name="sql">SQL</param>
        /// <param name="parameter">参数</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public static T ExecuteScalar<T>(DbConnection conn, string sql, object parameter = null, DbTransaction tran = null)
        {
            return conn.ExecuteScalar<T>(sql, parameter, tran);
        }

        /// <summary>
        /// 执行并返回DataReader
        /// </summary>
        /// <param name="conn">数据库连接</param>
        /// <param name="sql">SQL</param>
        /// <param name="parameter">参数</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public static IDataReader ExecuteReader(DbConnection conn, string sql, object parameter = null, DbTransaction tran = null)
        {
            return conn.ExecuteReader(sql, parameter, tran);
        }
    }
}
