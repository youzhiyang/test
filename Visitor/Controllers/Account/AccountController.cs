using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Http;
using Visitor.Model.Entity.Custom;

namespace Visitor.Controllers.Account
{
    /// <summary>
    /// 用户账户接口
    /// </summary>
    [RoutePrefix("api/account")]
    public class AccountController : Core.ApiBase
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("getdingappid"), Description("获取钉钉appid")]
        public ResponseData GetDingAppId()
        {
            return new ResponseData() { DataJSON = SystemParams.DingAppID };
        }
    }
}