using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Visitor.Dal;
using Visitor.Dal.Table;
using Visitor.Model.Entity.Custom;
using Visitor.Model.Entity.Table;
using Visitor.Model.Entity.View;

namespace Visitor.Bll.Business
{
    /// <summary>
    /// 身份证件信息相关业务
    /// </summary>
    public class CertBll
    {
        /// <summary>
        /// 实例化对象
        /// </summary>
        public static readonly CertBll Instance = new CertBll();

        /// <summary>
        /// 获取身份证件信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        public ResponseData GetBusiness_CertInfo(QueryParams query)
        {
            try
            {
                string certID = query.Param.CertID ?? "";
                using (var conn = DbFactory.CreateConnection(BllParams.ConnString))
                {
                    var certInfo = new Business_CertInfoDal().GetBusiness_CertInfo<Business_CertInfo>(certID, conn);
                    return new ResponseData() { DataJSON = JsonConvert.SerializeObject(certInfo) };
                }
            }
            catch (Exception ex)
            {
                return new ResponseData() { errcode = -1, errmsg = "读取数据失败。" + ex.Message };
            }
        }

        /// <summary>
        /// 新增/更新身份证件信息列表
        /// </summary>
        /// <param name="certs">身份证件信息列表</param>
        /// <returns></returns>
        public ResponseData UpsertElements(IEnumerable<View_Cert> certs)
        {
            var data = new ResponseData();
            try
            {
                using (var conn = DbFactory.CreateConnection(BllParams.ConnString))
                {
                    var tran = conn.BeginTransaction();
                    var certUpsertCount = new Business_CertInfoDal().UpsertBusiness_CertInfos(certs, conn, tran)
                        + new Business_CertExtInfoDal().UpsertBusiness_CertExtInfos(certs, conn, tran);
                    if (certUpsertCount != certs.Count() * 2)
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
        /// 删除身份证件信息列表
        /// </summary>
        /// <param name="certs">身份证件信息列表</param>
        /// <returns></returns>
        public ResponseData DeleteElements(IEnumerable<View_Cert> certs)
        {
            var data = new ResponseData();
            try
            {
                using (var conn = DbFactory.CreateConnection(BllParams.ConnString))
                {
                    var tran = conn.BeginTransaction();
                    var certDelCount = new Business_CertInfoDal().DeleteBusiness_CertInfos(certs, conn, tran)
                        + new Business_CertExtInfoDal().DeleteBusiness_CertExtInfos(certs, conn, tran);
                    if (certDelCount != certs.Count())
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
