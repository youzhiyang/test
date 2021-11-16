using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Security;

namespace Visitor.Controllers.Core
{
    /// <summary>
    /// API权限
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class ApiAuthority : ActionFilterAttribute
    {
        /// <summary>
        /// 票据
        /// </summary>
        private FormsAuthenticationTicket _ticket;

        /// <summary>
        /// 接到请求时
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //SaveAccessLog(actionContext, null);
            try
            {
                //允许匿名访问
                if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Count > 0)
                {
                    base.OnActionExecuting(actionContext);
                    return;
                }
                //登录时保存的Cookie
                //var cookie = HttpContext.Current.Request.Cookies[Common.CookieName];
                var cookie = Common.GetCookie();
                if (cookie == null)
                {
                    //无cookie则未登录返回401错误，无权限
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                    {
                        Content = new StringContent("您的身份验证已过期，请重新登录。", Encoding.GetEncoding("UTF-8"), "application/json")
                    };
                    base.OnActionExecuting(actionContext);
                    return;
                }
                if (cookie.Name == SystemParams.AppID)
                {
                    //读取令牌信息
                    try
                    {
                        _ticket = FormsAuthentication.Decrypt(cookie.Value);
                        if (_ticket.Expired)
                        {
                            //过期
                            actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                            {
                                Content = new StringContent("您的身份验证已过期，请重新登录。", Encoding.GetEncoding("UTF-8"), "application/json")
                            };
                            base.OnActionExecuting(actionContext);
                            return;
                        }
                        //续订ticket
                        RenewTicket(HttpContext.Current.Request.Cookies[SystemParams.AppID], _ticket);
                        ////验证Action权限
                        //var action = actionContext.ActionDescriptor.ActionName;
                        ////var controller = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                        //var controller = actionContext.ActionDescriptor.ControllerDescriptor.ControllerType.FullName;
                        //controller = controller.Substring(0, controller.Length - 10);
                        //var user = JsonConvert.DeserializeObject<Bus_User_UserActions>(
                        //    UserBll.Instance.GetUserRoles(
                        //        new QueryParams()
                        //        {
                        //            Param = JsonConvert.DeserializeObject("{ \"UnionID\" : \"" + JsonConvert.DeserializeObject<dynamic>(_ticket.UserData).UnionID.ToString() + "\" }")
                        //        }).DataJSON);

                        //if (/*!user.IsAdmin && */
                        //    (user.RoleList.FirstOrDefault(o => o.RoleID == new Guid("dbcaad7f-63da-4bde-9784-c7938ef891ac")) == null)
                        //    && (user.ActionList.FirstOrDefault(o => (o.Controller == controller) && (o.Action == action)) == null))
                        //{
                        //    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                        //    {
                        //        Content = new StringContent("您未被授权使用该功能。", Encoding.GetEncoding("UTF-8"), "application/json")
                        //    };
                        //}
                    }
                    catch
                    {
                        //解密令牌失败返回未登录错误
                        actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                        {
                            Content = new StringContent("您的身份验证已过期，请重新登录。", Encoding.GetEncoding("UTF-8"), "application/json")
                        };
                        base.OnActionExecuting(actionContext);
                        return;
                    }
                }
                if (_ticket == null)
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                    {
                        Content = new StringContent("您的身份验证已过期，请重新登录。", Encoding.GetEncoding("UTF-8"), "application/json")
                    };
                    base.OnActionExecuting(actionContext);
                    return;
                }
                base.OnActionExecuting(actionContext);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 续订票据
        /// </summary>
        /// <param name="cookie">Cookie</param>
        /// <param name="ticket"></param>
        public void RenewTicket(HttpCookie cookie, FormsAuthenticationTicket ticket)
        {
            if (string.IsNullOrEmpty(SystemParams.SSO))
            {
                //刷新本地身份有效期
                var expiration = DateTime.Now.AddHours(2);
                var newTicket = new FormsAuthenticationTicket(1, ticket.Name, ticket.IssueDate, expiration, false, ticket.UserData);
                cookie.Value = FormsAuthentication.Encrypt(newTicket);
                cookie.Expires = expiration;
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            else
            {
                //刷新远端身份有效期
                Common.RenewSSOTichet(ticket.Name);
            }
        }

        /// <summary>
        /// Action执行完成后
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuted(HttpActionExecutedContext actionContext)
        {
            //SaveAccessLog(null, actionContext);
        }

        /// <summary>
        /// 保存网站访问日志
        /// </summary>
        /// <param name="executing">接到的请求</param>
        /// <param name="executed">返回的响应</param>
        private void SaveAccessLog(HttpActionContext executing, HttpActionExecutedContext executed)
        {
            //var appccessLogBll = new AppAccessLogBll();
            //var msg = "";
            //if (executing != null)
            //{
            //    appccessLogBll.SaveAccessLog(
            //        new AppAccessLog()
            //        {
            //            Platform = HttpContext.Current.Request.Browser.Platform,
            //            Browser = HttpContext.Current.Request.Browser.Browser + "( version "
            //                + HttpContext.Current.Request.Browser.Version + " )",
            //            IsMobileDevice = HttpContext.Current.Request.Browser.IsMobileDevice,
            //            DeviceManufacturer = HttpContext.Current.Request.Browser.MobileDeviceManufacturer,
            //            DeviceModel = HttpContext.Current.Request.Browser.MobileDeviceModel,
            //            HttpType = executing.Request.Method.Method,
            //            Controller = executing.ControllerContext.Controller.ToString(),
            //            Action = executing.ActionDescriptor.ActionName,
            //            Content = Esquel.Util.Json.JsonHelper.ToJson(executing.ActionArguments)
            //        }, ref msg);
            //}
            //else if (executed != null)
            //{
            //    if (executed.Exception == null)
            //    {
            //        appccessLogBll.SaveAccessLog(
            //            new AppAccessLog()
            //            {
            //                Platform = HttpContext.Current.Request.Browser.Platform,
            //                Browser = HttpContext.Current.Request.Browser.Browser + "( version "
            //                    + HttpContext.Current.Request.Browser.Version + " )",
            //                IsMobileDevice = HttpContext.Current.Request.Browser.IsMobileDevice,
            //                DeviceManufacturer = HttpContext.Current.Request.Browser.MobileDeviceManufacturer,
            //                DeviceModel = HttpContext.Current.Request.Browser.MobileDeviceModel,
            //                HttpType = "Response",
            //                Controller = executed.ActionContext.ControllerContext.Controller.ToString(),
            //                Action = executed.ActionContext.ActionDescriptor.ActionName,
            //                Content = Esquel.Util.Json.JsonHelper.ToJson(executed.Response.Content)
            //            }, ref msg);
            //    }
            //    else
            //    {
            //        appccessLogBll.SaveAccessLog(
            //            new AppAccessLog()
            //            {
            //                Platform = HttpContext.Current.Request.Browser.Platform,
            //                Browser = HttpContext.Current.Request.Browser.Browser + "( version "
            //                    + HttpContext.Current.Request.Browser.Version + " )",
            //                IsMobileDevice = HttpContext.Current.Request.Browser.IsMobileDevice,
            //                DeviceManufacturer = HttpContext.Current.Request.Browser.MobileDeviceManufacturer,
            //                DeviceModel = HttpContext.Current.Request.Browser.MobileDeviceModel,
            //                HttpType = "Exception",
            //                Controller = executed.ActionContext.ControllerContext.Controller.ToString(),
            //                Action = executed.ActionContext.ActionDescriptor.ActionName,
            //                Content = Esquel.Util.Json.JsonHelper.ToJson(executed.Exception)
            //            }, ref msg);
            //    }
            //}
        }
    }
}