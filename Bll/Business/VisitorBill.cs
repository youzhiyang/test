using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visitor.Dal;
using Visitor.Dal.Table;
using Visitor.Model.Entity.Custom;
using Visitor.Model.Entity.Table;

namespace Visitor.Bll.Business
{
    /// <summary>
    /// 访客相关业务
    /// </summary>
    public class VisitorBill
    {
        /// <summary>
        /// 实例化对象
        /// </summary>
        public static readonly VisitorBill Instance = new VisitorBill();

        /// <summary>
        /// 根据身份证id获取登记记录
        /// </summary>
        /// <param name="visitRecords">访客实体类</param>
        /// <returns></returns>
        public ResponseData getVisitorRecord(Business_VisitorRecord visitorRecord)
        {
            var data = new ResponseData();
            try
            {
                using (var conn = DbFactory.CreateConnection(BllParams.ConnString))
                {
                    var visitorInfo = new Business_VisitorRecordDal().GetBusiness_VisitorRecords< Visitor.Model.Entity.Custom.Visitor >(visitorRecord.CertID, conn);
                    if (visitorInfo == null)
                    {
                        data.errcode = -2;
                        data.errmsg = "您没有登记记录";
                        return data;
                    }
                    data.DataJSON = JsonConvert.SerializeObject(visitorInfo);
                }
            }
            catch (Exception ex)
            {
                data.errcode = -1;
                data.errmsg = "获取信息失败。" + ex.Message;
            }
            return data;
        }

        /// <summary>
        /// 判断是否登记
        /// </summary>
        /// <param name="visitRecords">访客实体类</param>
        /// <returns></returns>
        public ResponseData getIsEnter(Visitor.Model.Entity.Custom.Visitor visitor)
        {
            var data = new ResponseData();
            try
            {
                using (var conn = DbFactory.CreateConnection(BllParams.ConnString))
                {
                    var visitorInfo = new Business_VisitorRecordDal().GetBusiness_VisitorRecords<Visitor.Model.Entity.Custom.Visitor>(visitor.CertID, conn);
                    if (visitorInfo == null)
                    {
                        data.errcode = -2;
                        data.errmsg = visitor.Name + "您没有登记记录";
                        return data;
                    }
                    data.DataJSON = JsonConvert.SerializeObject(visitorInfo);
                }
            }
            catch (Exception ex)
            {
                data.errcode = -1;
                data.errmsg = "获取信息失败。" + ex.Message;
            }
            return data;
        }

        /// <summary>
        /// 获取历史来访信息(入场登记检测)
        /// </summary>
        /// <param name="visitRecords">访客实体类</param>
        /// <returns></returns>
        public ResponseData getHistoryVisitor(Business_VisitorRecord visitorRecord)
        {
            var data = new ResponseData();
            try
            {
                using (var conn = DbFactory.CreateConnection(BllParams.ConnString))
                {
                    var visitorInfo = new Business_VisitorRecordDal().GetBusiness_VisitorRecords<Visitor.Model.Entity.Custom.Visitor>(visitorRecord.CertID, conn);
                    if (visitorInfo != null)
                    {
                        data.errcode = -2;
                        data.errmsg = "您有未离场记录，请先离场";
                        return data;
                    }
                    visitorInfo = new Business_VisitorRecordDal().GetBusiness_HistoryVisitorRecords<Visitor.Model.Entity.Custom.Visitor>(visitorRecord.CertID, conn);
                    data.DataJSON = JsonConvert.SerializeObject(visitorInfo);
                }
            }
            catch (Exception ex)
            {
                data.errcode = -1;
                data.errmsg = "获取信息失败。" + ex.Message;
            }
            return data;
        }

        /// <summary>
        /// 判断是否离场
        /// </summary>
        /// <param name="visitRecords">访客实体类</param>
        /// <returns></returns>
        public ResponseData getIsLeave(Visitor.Model.Entity.Custom.Visitor visitor)
        {
            var data = new ResponseData();
            try
            {
                using (var conn = DbFactory.CreateConnection(BllParams.ConnString))
                {
                    var visitorInfo = new Business_VisitorRecordDal().GetBusiness_VisitorRecords<Visitor.Model.Entity.Custom.Visitor>(visitor.CertID, conn);
                    if (visitorInfo != null)
                    {
                        data.errcode = -2;
                        data.errmsg = visitor.Name + "您有未离场记录，请先离场";
                        return data;
                    }
                    data.DataJSON = JsonConvert.SerializeObject(visitorInfo);
                }
            }
            catch (Exception ex)
            {
                data.errcode = -1;
                data.errmsg = "获取信息失败。" + ex.Message;
            }
            return data;
        }
    }
}
