using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Security;
using Visitor.Model.Entity.Custom;

namespace Visitor.Controllers
{
    /// <summary>
    /// 通用静态方法
    /// </summary>
    public class Common
    {
        /// <summary>
        /// 获取有效的身份验证Cookie
        /// </summary>
        /// <returns></returns>
        public static HttpCookie GetCookie()
        {
            try
            {
                var cookie = HttpContext.Current.Request.Cookies[SystemParams.AppID];
                if (cookie != null)
                {
                    var ticket = FormsAuthentication.Decrypt(cookie.Value);
                    if (ticket.Expired)
                    {
                        return null;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(SystemParams.SSO))
                        {
                            //本地登录
                            return cookie;
                        }
                        else
                        {
                            //单点登录
                            var token = ticket.UserData;
                            var request = WebRequest.Create(SystemParams.SSO + "/account/getuserticket?token=" + token);
                            var response = request.GetResponse();
                            var getStream = response.GetResponseStream();
                            var reader = new StreamReader(getStream);
                            var content = reader.ReadToEnd();
                            var responseData = JsonConvert.DeserializeObject<ResponseData>(content);
                            if (responseData.errcode == 0)
                            {
                                var dyTicket = JsonConvert.DeserializeObject<dynamic>(responseData.DataJSON);
                                //Bus_User userData = JsonConvert.DeserializeObject<Bus_User>(dyTicket.UserData.ToString());
                                var ssoTicket = new FormsAuthenticationTicket(Convert.ToInt32(dyTicket.Version), dyTicket.Name.ToString(), Convert.ToDateTime(dyTicket.IssueDate),
                                    Convert.ToDateTime(dyTicket.Expiration), Convert.ToBoolean(dyTicket.IsPersistent), dyTicket.UserData.ToString(), dyTicket.CookiePath.ToString());
                                var ssoCookie = new HttpCookie(SystemParams.AppID, FormsAuthentication.Encrypt(ssoTicket));
                                return ssoCookie;
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        ///// <summary>
        ///// 获取登录用信息
        ///// </summary>
        ///// <returns></returns>
        //public static Bus_User GetLoginUser()
        //{
        //    //var cookie = HttpContext.Current.Request.Cookies[CookieName];
        //    var cookie = GetCookie();
        //    if (cookie != null)
        //    {
        //        var ticket = FormsAuthentication.Decrypt(cookie.Value);
        //        if (ticket.Expired)
        //        {
        //            return null;
        //        }
        //        else
        //        {
        //            var loginInfo = JsonConvert.DeserializeObject<Bus_User>(ticket.UserData);
        //            return loginInfo;
        //        }
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        /// <summary>
        /// 续订单点登录的用户Ticket
        /// </summary>
        /// <param name="token">登录令牌</param>
        public static void RenewSSOTichet(string token)
        {
            var request = WebRequest.Create(SystemParams.SSO + "/account/renewuserticket?token=" + token);
            var response = request.GetResponse();
        }

        /// <summary>
        /// 注销登录
        /// </summary>
        public static void Logout()
        {
            var cookie = Common.GetCookie();
            if (cookie != null)
            {
                if (string.IsNullOrEmpty(SystemParams.SSO))
                {
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    HttpContext.Current.Response.Cookies.Add(cookie);
                    FormsAuthentication.SignOut();
                }
                else
                {
                    var ticket = FormsAuthentication.Decrypt(cookie.Value);
                    var request = WebRequest.Create(SystemParams.SSO + "/account/logout?token=" + ticket.Name);
                    var response = request.GetResponse();
                }
            }
        }
    }
    /// <summary>
    /// 系统公共参数
    /// </summary>
    public static class SystemParams
    {
        /// <summary>
        /// 系统缓存键
        /// </summary>
        public enum CacheKey
        {
            /// <summary>
            /// 萤石平台获取的Access Token
            /// </summary>
            YingShiAccessToken
        }

        /// <summary>
        /// 单点登录地址
        /// </summary>
        public static string SSO { get; set; }

        /// <summary>
        /// 程序ID
        /// </summary>
        public static string AppID { get; set; }

        /// <summary>
        /// 钉钉App Id
        /// </summary>
        public static string DingAppID { get; set; }
    }
}