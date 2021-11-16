using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Http;
using Visitor.Bll.Business;
using Visitor.Model.Entity.Custom;
using Visitor.Model.Entity.Table;

namespace Visitor.Controllers.Business
{
    /// <summary>
    /// 来访登记接口
    /// </summary>
    [RoutePrefix("api/business/leave")]
    public class LeaveController : Core.ApiBase
    {
        /// <summary>
        /// 更新离场信息列表
        /// </summary>
        /// <param name="visitRecords">来访信息列表</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("upsertleaves"), Description("更新离场信息列表")]
        public ResponseData UpsertLeaves(LeaveVisitor leaveVisitor)
        {
            return VisitBll.Instance.UpsertLeaves(leaveVisitor);
        }

        /// <summary>
        /// 根据身份证id获取访客信息
        /// </summary>
        /// <param name="visitorRecord">访客实体类</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("getVisitorRecord"), Description("根据身份证id获取访客信息")]
        public ResponseData getVisitorRecord(Business_VisitorRecord visitorRecord)
        {
            return VisitorBill.Instance.getVisitorRecord(visitorRecord);
        }

        /// <summary>
        /// 判断是否登记
        /// </summary>
        /// <param name="visitorRecord">访客实体类</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("getIsEnter"), Description("判断是否登记")]
        public ResponseData getIsEnter(Visitor.Model.Entity.Custom.Visitor visitor)
        {
            return VisitorBill.Instance.getIsEnter(visitor);
        }
    }
}
