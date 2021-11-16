using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Http;
using Visitor.Bll;
using Visitor.Bll.Business;
using Visitor.Bll.Table;
using Visitor.Model.Entity.Custom;

namespace Visitor.Controllers.Business
{
    /// <summary>
    /// 登记接口
    /// </summary>
    [RoutePrefix("api/business/register")]
    public class RegisterController : Core.ApiBase
    {
        /// <summary>
        /// 获取访客列表
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("getVisitRecord"), Description("获取访客列表")]
        public ResponseData getVisitRecord()
        {
            return VisitBll.Instance.GetBusiness_VisitRecord();
        }
    }
}