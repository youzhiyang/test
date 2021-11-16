using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Visitor.Dal;
using Visitor.Dal.Table;
using Visitor.Model.Entity.Custom;
using Visitor.Model.Entity.Table;

namespace Visitor.Bll.Table
{
    /// <summary>
    /// 数据字典相关业务
    /// </summary>
    public class Data_DictionaryBll
    {
        /// <summary>
        /// 实例化对象
        /// </summary>
        public static readonly Data_DictionaryBll Instance = new Data_DictionaryBll();

        /// <summary>
        /// 分页获取数据字典列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        public ResponseData GetPagedDictionaries(QueryParams query)
        {
            try
            {
                string condition = query.Param.Condition ?? "";
                using (var conn = DbFactory.CreateConnection(BllParams.ConnString))
                {
                    return new ResponseData()
                    {
                        DataJSON = JsonConvert.SerializeObject(new Data_DictionaryDal().GetDictionaries<Data_Dictionary>(condition, query.Skip, query.Size, out long total, conn)),
                        Total = total
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseData() { errcode = -1, errmsg = "读取数据失败。" + ex.Message };
            }
        }

        /// <summary>
        /// 根据数据类型获取数据字典列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        public ResponseData GetDictionariesByDataType(QueryParams query)
        {
            try
            {
                string type = query.Param.DataType ?? "";
                using (var conn = DbFactory.CreateConnection(BllParams.ConnString))
                {
                    return new ResponseData() { DataJSON = JsonConvert.SerializeObject(new Data_DictionaryDal().GetDictionaries<Data_Dictionary>(type, conn)) };
                }
            }
            catch (Exception ex)
            {
                return new ResponseData() { errcode = -1, errmsg = "读取数据失败。" + ex.Message };
            }
        }

        /// <summary>
        /// 根据数据类型获取数据字典列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        public ResponseData GetDictionaries(QueryParams query)
        {
            try
            {
                var dict = new Data_Dictionary
                {
                    DataType = query.Param.DataType ?? "",
                    Code = query.Param.Code ?? "",
                    Value = query.Param.Value ?? "",
                    ExtValue1 = query.Param.ExtValue1 ?? "",
                    ExtValue2 = query.Param.ExtValue2 ?? "",
                    ExtValue3 = query.Param.ExtValue3 ?? ""
                };
                using (var conn = DbFactory.CreateConnection(BllParams.ConnString))
                {
                    return new ResponseData() { DataJSON = JsonConvert.SerializeObject(new Data_DictionaryDal().GetDictionaries<Data_Dictionary>(dict, conn)) };
                }
            }
            catch (Exception ex)
            {
                return new ResponseData() { errcode = -1, errmsg = "读取数据失败。" + ex.Message };
            }
        }

        /// <summary>
        /// 获取数据字典
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        public ResponseData GetDictionary(QueryParams query)
        {
            try
            {
                string type = query.Param.DataType ?? "";
                string code = query.Param.Code ?? "";
                using (var conn = DbFactory.CreateConnection(BllParams.ConnString))
                {
                    return new ResponseData() { DataJSON = JsonConvert.SerializeObject(new Data_DictionaryDal().GetDictionary<Data_Dictionary>(type, code, conn)) };
                }
            }
            catch (Exception ex)
            {
                return new ResponseData() { errcode = -1, errmsg = "读取数据失败。" + ex.Message };
            }
        }

        /// <summary>
        /// 新增/更新数据字典列表
        /// </summary>
        /// <param name="dictionaries">数据字典列表</param>
        /// <returns></returns>
        public ResponseData UpsertDictionaries(IEnumerable<Data_Dictionary> dictionaries)
        {
            var data = new ResponseData();
            try
            {
                using (var conn = DbFactory.CreateConnection(BllParams.ConnString))
                {
                    var tran = conn.BeginTransaction();
                    dictionaries = dictionaries.OrderByDescending(o => o.DictID);
                    var dictUpsertCount = new Data_DictionaryDal().UpsertDictionaries(dictionaries, conn, tran);
                    if (dictUpsertCount != dictionaries.Count())
                    {
                        tran.Rollback();
                        data.errcode = -2;
                        data.errmsg = "保存失败。";
                    }
                    else
                    {
                        tran.Commit();
                        data.errcode = 0;
                        data.errmsg = "保存成功。";
                    }
                }
            }
            catch (Exception ex)
            {
                data.errcode = -1;
                data.errmsg = "保存失败。" + ex.Message;
            }
            return data;
        }

        /// <summary>
        /// 删除数据字典列表
        /// </summary>
        /// <param name="dictionaries">数据字典列表</param>
        /// <returns></returns>
        public ResponseData DeleteDictionaries(IEnumerable<Data_Dictionary> dictionaries)
        {
            var data = new ResponseData();
            try
            {
                using (var conn = DbFactory.CreateConnection(BllParams.ConnString))
                {
                    var tran = conn.BeginTransaction();
                    var dictDelCount = new Data_DictionaryDal().DeleteDictionaries(dictionaries, conn, tran);
                    if (dictDelCount != dictionaries.Count())
                    {
                        tran.Rollback();
                        data.errcode = -2;
                        data.errmsg = "保存失败。";
                    }
                    else
                    {
                        tran.Commit();
                        data.errcode = 0;
                        data.errmsg = "保存成功。";
                    }
                }
            }
            catch (Exception ex)
            {
                data.errcode = -1;
                data.errmsg = "保存失败。" + ex.Message;
            }
            return data;
        }
    }
}
