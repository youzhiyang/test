using CommonUtility.IOHelper;
using DingTalkService;
using DingTalkService.Models;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Visitor.Bll;
using Visitor.Controllers;
using Visitor.Controllers.util;
using Visitor.Controllers.Util;

namespace Visitor
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            InitSystem();
        }

        /// <summary>
        /// 
        /// </summary>
        protected void Application_BeginRequest()
        {
            //支持OPTIONS方式
            if (Request.Headers.AllKeys.Contains("Origin") && Request.HttpMethod == "OPTIONS")
            {
                Response.End();
            }
        }

        /// <summary>
        /// 初始化系统
        /// </summary>
        private void InitSystem() {
            var configStr = Cryptography.DecryptString(System.AppDomain.CurrentDomain.BaseDirectory, "secret.config", "桂林经开投资控股有限责任公司");
            var systemConfig = JsonConvert.DeserializeObject<SystemConfig>(configStr);
            SystemConfig.Config config;
            switch (ConfigurationManager.AppSettings["Environment"])
            {
                case "LIVE":
                    config = systemConfig.LIVE;
                    break;
                case "UAT":
                    config = systemConfig.UAT;
                    break;
                case "DEV":
                default:
                    config = systemConfig.DEV;
                    break;
            }

            SystemParams.SSO = config.SSO;
            SystemParams.AppID = config.AppID;
            SystemParams.DingAppID = config.DingAppID;
            BllParams.ConnString = config.DB;
            BllParams.FileRoot = config.FileRoot;
            BllParams.View = config.View;
            DingTalkAPIService.Instance.CorpId = config.DingCorpID;
            DingTalkAPIService.Instance.CorpSecret = config.DingCorpSecret;
            DingTalkAPIService.Instance.AgentId = config.AgentID;
            DingTalkAPIService.Instance.AppID = config.DingAppID;
            DingTalkAPIService.Instance.AppSecret = config.DingAppSecret;
            DingTalkAPIService.Instance.RebotSecret = config.RebotSecret;
            DingTalkAPIService.Instance.RebotWebhook = config.RebotWebhook;

            //启动系统线程
            SystemThread.Instance.RunThreads();
        }
    }
}
