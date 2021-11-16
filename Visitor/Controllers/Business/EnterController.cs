using DingTalk.Api.Request;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Http;
using Visitor.Bll;
using Visitor.Bll.Business;
using Visitor.Bll.Table;
using Visitor.Model.Entity.Custom;
using Visitor.Model.Entity.Table;

namespace Visitor.Controllers.Business
{
    /// <summary>
    /// 来访登记接口
    /// </summary>
    [RoutePrefix("api/business/enter")]
    public class EnterController : Core.ApiBase
    {
        /// <summary>
        /// 获取区域列表
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("getareas"), Description("获取区域列表")]
        public ResponseData GetAreas()
        {
            return Data_AreaBll.Instance.GetAreas();
        }

        /// <summary>
        /// 获取数据字典列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("getdictionaries"), Description("获取数据字典列表")]
        public ResponseData GetDictionaries(QueryParams query)
        {
            return Data_DictionaryBll.Instance.GetDictionariesByDataType(query);
        }

        /// <summary>
        /// 新增/更新来访信息列表
        /// </summary>
        /// <param name="visitRecords">来访信息列表</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("upsertvisits"), Description("新增/更新来访信息列表")]
        public ResponseData UpsertVisits(IEnumerable<VisitRecord> visitRecords)
        {
            return VisitBll.Instance.UpsertVisits(visitRecords);
        }

        /// <summary>
        /// 获取员工信息
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("getStaff"), Description("获取员工信息")]
        public ResponseData GetStaff()
        {
            return Data_StaffBll.Instance.GetStaff();
        }

        /// <summary>
        /// 获取设备工具
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("getDeviceTools"), Description("获取设备工具信息")]
        public ResponseData GetDeviceTools()
        {
            return Data_DeviceTools.Instance.GetDeviceTools();
        }

        /// <summary>
        /// 获取历史来访信息
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("getHistoryVisitor"), Description("获取历史来访信息")]
        public ResponseData getHistoryVisitor(Business_VisitorRecord visitorRecord)
        {
            return VisitorBill.Instance.getHistoryVisitor(visitorRecord);
        }

        /// <summary>
        /// 判断是否离场
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("getIsLeave"), Description("判断是否离场")]
        public ResponseData getIsLeave(Visitor.Model.Entity.Custom.Visitor visitor)
        {
            return VisitorBill.Instance.getIsLeave(visitor);
        }
    }
}
