using System.Web.Http;

namespace Visitor.Controllers.Core
{
    /// <summary>
    /// 
    /// </summary>
    [ApiAuthority]
    public abstract class ApiBase : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestContext"></param>
        protected override void Initialize(System.Web.Http.Controllers.HttpControllerContext requestContext)
        {
            base.Initialize(requestContext);
        }
    }
}