using System.ComponentModel;
using System.Web.Http;
using Visitor.Bll;
using Visitor.Model.Entity.Custom;

namespace Visitor.Controllers
{
    /// <summary>
    /// 通用
    /// </summary>
    [RoutePrefix("api/common")]
    public class CommonController : Core.ApiBase
    {
        /// <summary>
        /// 获取天气数据
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("getweather"), Description("获取天气数据")]
        public ResponseData GetWeather()
        {
            return CommonBll.Instance.GetWeather();
        }
    }
}
