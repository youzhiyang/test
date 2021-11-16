using Visitor.Model.Entity.Custom;

namespace Visitor
{
    /// <summary>
    /// 系统配置
    /// </summary>
    public class SystemConfig
    {
        /// <summary>
        /// 系统配置
        /// </summary>
        public SystemConfig()
        {
            LIVE = new Config();
            UAT = new Config();
            DEV = new Config();
        }

        /// <summary>
        /// 发布版本
        /// </summary>
        public Config LIVE { get; }

        /// <summary>
        /// 测试版本
        /// </summary>
        public Config UAT { get; }

        /// <summary>
        /// 开发版本
        /// </summary>
        public Config DEV { get; }

        /// <summary>
        /// 系统配置
        /// </summary>
        public class Config
        {
            /// <summary>
            /// 数据库
            /// </summary>
            public string DB { get; set; }

            /// <summary>
            /// 钉钉企业ID
            /// </summary>
            public string DingCorpID { get; set; }

            /// <summary>
            /// 钉钉企业秘钥
            /// </summary>
            public string DingCorpSecret { get; set; }

            /// <summary>
            /// 钉钉开放APP ID
            /// </summary>
            public string DingAppID { get; set; }

            /// <summary>
            /// 钉钉开放APP秘钥
            /// </summary>
            public string DingAppSecret { get; set; }

            /// <summary>
            /// 微应用AgentID
            /// </summary>
            public string AgentID { get; set; }

            /// <summary>
            /// 萤石云Key
            /// </summary>
            public string YsAppKey { get; set; }

            /// <summary>
            /// 萤石云秘钥
            /// </summary>
            public string YsAppSecret { get; set; }

            /// <summary>
            /// 文件根目录
            /// </summary>
            public string FileRoot { get; set; }

            /// <summary>
            /// 单点登录地址
            /// </summary>
            public string SSO { get; set; }

            /// <summary>
            /// 程序ID
            /// </summary>
            public string AppID { get; set; }

            /// <summary>
            /// 前台页面地址
            /// </summary>
            public string View { get; set; }

            /// <summary>
            /// 自定义机器人加签密匙
            /// </summary>
            public string RebotSecret { get; set; }

            /// <summary>
            /// 自定义机器人Webhook地址
            /// </summary>
            public string RebotWebhook { get; set; }
        }
    }
}