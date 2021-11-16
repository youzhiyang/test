(function (system) {
    /**
     * @description params 系统参数
     */
    (function (params) {
        /**
         * @description 系统环境
         */
        params.environment = "dev";

        /**
         * @description 系统ID
         */
        params.appID = (params.environment === "live") ? "f3f0d58b-0e01-446c-81c0-1fcaee9b230d"
            : ((params.environment === "uat") ? "fc26a092-8110-4434-87f0-1bc2fc5c2cd8"
                : "54639e00-c1e6-4dbf-b35a-abb3e4ff55e0");

        /**
         * @description 是否使用单点登录
         */
        params.useSSO = false;

        /**
         * @description SSO API服务ID
         */
        params.ssoID = (params.environment === "live") ? "46dd008a-fb74-42bc-bfba-427756fb5ab6"
            : ((params.environment === "uat") ? "46dd008a-fb74-42bc-bfba-427756fb5ab6"
                : "46dd008a-fb74-42bc-bfba-427756fb5ab6");

        /**
         * @description 登录地址
         */
        params.login = //((params.environment === "live") ? "http://login.gljkkg.com/view/account/login.html"
            //: ((params.environment === "uat") ? "http://test.login.gljkkg.com/view/account/login.html"
            //    : "http://test.login.gljkkg.com/view/account/login.html"))
            "http://login.gljkkg.com/view/account/login.html"
            + "?@S@" + params.appID + "@S$@";

        /**
         * @description SSO API服务器地址
         */
        params.sso = //(params.environment === "live") ? "http://login.gljkkg.com/api/api"
            //: ((params.environment === "uat") ? "http://test.login.gljkkg.com/api/api"
            //    : "http://localhost:50732/api");
            "http://login.gljkkg.com/api/api";

        /**
         * @description WEB服务器地址
         */
        params.web = (params.environment === "live") ? "http://project.gljkkg.com"
            : ((params.environment === "uat") ? "http://test.project.gljkkg.com"
                : "http://localhost:52483");

        /**
         * @description API服务器地址
         */
        params.api = (params.environment === "live") ? "http://project.gljkkg.com/api/api"
            : ((params.environment === "uat") ? "http://test.project.gljkkg.com/api/api"
                : "http://localhost:50461/api");

        /**
         * @description 看板刷新时间
         */
        params.kanbanTimespan = (params.environment === "live") ? (10 * 60 * 1000)
            : ((params.environment === "uat") ? (60 * 1000)
                : (60 * 1000));

        /**
         * @description 天气刷新时间
         */
        params.weatherTimespan = (params.environment === "live") ? (60 * 60 * 1000)
            : ((params.environment === "uat") ? (5 * 60 * 1000)
                : (5 * 60 * 1000));

        /**
         * @description 单位换算
         */
        params.unit = {
            /**
             * @description 单位名
             */
            name: "亿",

            /**
             * @description 单位比例
             */
            rate: 100000000
        };

        /**
         * @description 通用颜色
         */
        params.color = { blue: "#21abab", gray: "#808080", green: "#6aa529"/*"#7bd11e"*/, orange: "#d18c0e"/*"#ffa400"*//*"#d67f3f"*/, purple: "#9e408d", red: "#ff5555" };

        /**
        * @description 计划状态
        */
        params.planStatus = {
            /**
             * @description 未计划
             */
            noplan: params.color.blue,

            /**
             * @description 未开始
             */
            notstart: params.color.gray,

            /**
             * @description 提前
             */
            advance: params.color.blue,

            /**
             * @description 正常
             */
            normal: params.color.green,

            /**
             * @description 风险
             */
            warning: params.color.orange,

            /**
             * @description 逾期
             */
            delay: params.color.red,

            /**
             * @description 暂停
             */
            pause: params.color.blue
        }

        /**
         * @description 天气css类
         */
        params.weatherClass = {
            晴: "weather-qing",
            多云: "weather-duoyun",
            多云转阵雨: "weather-duoyun",
            阴: "weather-yin",

            阵雨: "weather-zhenyu",
            小雨: "weather-xiaoyu",
            小到中雨: "weather-zhongyu",
            中雨: "weather-zhongyu",
            中到大雨: "weather-dayu",
            大雨: "weather-dayu",
            大到暴雨: "weather-baoyu",
            暴雨: "weather-baoyu",
            暴雨到大暴雨: "weather-baoyu",
            大暴雨: "weather-baoyu",
            大暴雨到特大暴雨: "weather-baoyu",
            特大暴雨: "weather-baoyu",
            雷阵雨: "weather-leizhenyu",
            雷阵雨伴有冰雹: "weather-bingbao",
            冻雨: "weather-yujiaxue",
            雨夹雪: "weather-yujiaxue",

            阵雪: "weather-xiaoxue",
            小雪: "weather-xiaoxue",
            小到中雪: "weather-zhongxue",
            中雪: "weather-zhongxue",
            中到大雪: "weather-daxue",
            大雪: "weather-daxue",
            大到暴雪: "weather-baoxue",
            暴雪: "weather-bingbao",

            雾: "weather-wu",
            霾: "weather-wumai",

            浮尘: "weather-fuchen",
            扬沙: "weather-fuchen",
            沙尘暴: "weather-shachenbao",
            强沙尘暴: "weather-shachenbao",
        }
    }(system.params = {}));

    /**
     * @description 通用方法
     */
    (function (common) {
        /**
         * @description 获取用户信息
         */
        common.getUserInfo = function () {
        };

        /**
         * @description 验证页面是否被授权访问
         * @param {String} pageID 页面ID
         * @returns {Boolean} 是否授权
         */
        common.checkPageAuthorized = function (pageID) {
            return true;
            if (window.userAuthority) {
                for (var i = 0; i < window.userAuthority.PageList.length; i++) {
                    if (pageID.toLowerCase() === window.userAuthority.PageList[i].PageID.toLowerCase()) {
                        return true;
                    }
                }
            }
            return false;
        }

        /**
         * @description 验证页面控件是否被授权访问
         * @param {String} pageID 页面ID
         */
        common.checkPageElementAuthorized = function (pageID) {
            if (window.userAuthority) {
                var eList = [];
                if (pageID) {
                    eList = Array.where(window.userAuthority.ElementList, "PageID", "=", pageID);
                } else {
                    eList = window.userAuthority.ElementList;
                }
                for (var i = 0; i < eList.length; i++) {
                    $(eList[i].Selector).toggleClass("unauthorized", false);
                    $(eList[i].Selector).toggleClass("authorized", true);
                }
            }
        }

        /**
         * @description 验证指定控件是否被授权访问
         * @param {String} pageID 页面ID
         * @param {String} filter 过滤条件
         * @returns {Boolean} 是否授权
         */
        common.checkElementAuthorized = function (pageID, filter) {
            if (window.userAuthority) {
                var eList = [];
                if (pageID) {
                    eList = Array.where(window.userAuthority.ElementList, "PageID", "=", pageID);
                } else {
                    eList = window.userAuthority.ElementList;
                }
                for (var i = 0; i < eList.length; i++) {
                    if (eList[i].Selector === filter) {
                        return true;
                    }
                }
            }
            return false;
        }

        /**
         * @description 初始化数据字典数据源
         * @param {String} dataType 数据类型
         * @param {String} url API地址
         * @param {Function} start 请求发起前事件
         * @param {Function} end 请求结束后事件
         * @returns {Object} 数据源
         */
        common.initDictionaryDataSource = function (dataType, url, start, end) {
            return new kendo.data.DataSource({
                error: function (e) {
                    layer.open({
                        btn: "确定",
                        content: e.xhr.responseText,
                        icon: 2,
                        yes: function () {
                            //权限错误
                            if (e.xhr.statusText === "Unauthorized") {
                                window.location.replace("index.html");
                            }
                        }
                    });
                },
                requestEnd: function (e) {
                    if (end) {
                        end(e);
                    }
                },
                requestStart: function (e) {
                    if (start) {
                        start(e);
                    }
                },
                schema: {
                    data: function (response) {
                        if (response !== null) {
                            if (response.errcode === 0) {
                                return JSON.parse(response.DataJSON);
                            } else {
                                layer.open({
                                    btn: "确定",
                                    content: response.errmsg,
                                    icon: 2
                                });
                                return [];
                            }
                        }
                    },
                    model: {
                        fields: {
                            Name: { type: "string" },
                        },
                        id: "Code"
                    }
                },
                serverPaging: false,
                serverSorting: true,
                serverFiltering: true,
                transport: {
                    parameterMap: function (data, type) {
                        if (type === "read") {
                            var query = system.api.query;
                            query.Param = { DataType: dataType };
                            return JSON.stringify(query);
                        }
                    },
                    read: {
                        contentType: "application/json; charset=utf-8",
                        crossDomain: true,
                        dataType: "json",
                        type: "post",
                        url: url,
                        xhrFields: {
                            withCredentials: true
                        }
                    }
                }
            });
        };
    }(system.common = {}));

    /**
     * @description 后台API接口
     */
    (function (api) {
        /**
         * @description 服务器地址
         */
        server = system.params.api;

        /**
         * @description 查询条件
         */
        api.query = { Param: {}, Skip: 0, Size: 0 }

        /**
         * @description 通用
         */
        api.common = {
            /**
             * @description 获取天气数据
             */
            getWeather: server + "/common/getweather"
        };

        /**
         * @description 账号
         */
        api.account = {
            /**
             * @description 获取钉钉AppID
             */
            getDingAppID: server + "/account/getdingappid",

            /**
             * @description 钉钉登录
             */
            dingLogin: server + "/account/dinglogin",

            /**
             * @description 单点登录
             */
            ssoLogin: server + "/account/ssologin",

            /**
             * @description 获取用户信息
             */
            getUserInfo: server + "/account/getuserinfo",

            /**
             * @description 注销登录
             */
            logout: server + "/account/logout"
        };

        /**
         * @description 主页
         */
        api.home = {
            /**
             * @description 验证Cookie是否有效
             */
            validateCookie: server + "/home/validatecookie",

            /**
             * @description 获取用户权限
             */
            getUserAuthorities: server + "/home/getuserauthorities"
        };

        /**
         * @description 业务数据
         */
        api.business = {
            /**
             * @description 来访登记
             */
            enter: {
                /**
                 * @description 获取数据字典列表
                 */
                getAreas: server + "/business/enter/getareas",

                /**
                 * @description 获取数据字典列表
                 */
                getDictionaries: server + "/business/enter/getdictionaries",

                /**
                 * @description 新增/更新来访信息列表
                 */
                upsertVisits: server + "/business/enter/upsertvisits",
                /**
                 * @description 获取引导人列表
                 */
                getStaff: server + "/business/enter/getStaff",
                /**
                 * @description 获取设备工具列表
                 */
                getDeviceTools: server + "/business/enter/getDeviceTools",
                /**
                 * @description 获取历史来访信息
                 */
                getHistoryVisitor: server + "/business/enter/getHistoryVisitor",
                /**
                 * @description 判断是否离场
                 */
                getIsLeave: server + "/business/enter/getIsLeave"
            },

            /**
             * @description 离场登记
             */
            leave: {
                /**
                 * @description 新增/更新离场信息列表
                 */
                upsertLeaves: server + "/business/leave/upsertleaves",
                /**
                 * @description 根据身份证id获取访客信息
                 */
                getVisitorRecord: server + "/business/leave/getVisitorRecord",
                /**
                 * @description 判断是否登记
                 */
                getIsEnter: server + "/business/leave/getIsEnter"
            },
            register: {
                getVisitRecord: server + "/business/register/getVisitRecord"
            }
        };
    }(system.api = {}));

    /**
     * @description AJAX
     */
    (function (ajax) {
        /**
         * @description GET
         * @param {String} url 请求链接
         * @param {Function} success 成功函数
         * @param {Function} fail 失败函数
         * @param {Function} complete 完成函数
         * @param {Object} header 头
         * @param {Boolean} async 是否异步，默认异步
         * @param {Number} timeout 超时时长
         */
        ajax.get = function (url, success, fail, complete, header, async, timeout) {
            if (timeout) {
                //默认值
                timeout = 10000;
            }
            if ((async === undefined) || (async === null)) {
                //默认值
                async = true;
            } else if (async.constructor === Number) {
                //没有录入async
                timeout = async;
                //默认值
                async = true;
            }
            if (!header) {
                //默认值
                header = {};
            } else if (header.constructor === Boolean) {
                //没有录入header
                async = header;
                //默认值
                header = {};
            } else if (header.constructor === Number) {
                //没有录入header/async
                timeout = header;
                //默认值
                header = {};
            }
            $.ajax({
                async: async,
                beforeSend: function (xhr) {
                    xhr.withCredentials = true;
                },
                contentType: "application/json; charset=utf-8",
                crossDomain: true,
                dataType: "json",
                headers: header,
                timeout: timeout,
                type: "GET",
                url: url,
                xhrFields: {
                    withCredentials: true
                },
                success: function (data, status, xhr) {
                    if (success) {
                        success(data, status, xhr);
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    if (fail) {
                        fail(xhr, textStatus, errorThrown);
                    }
                },
                complete: function () {
                    if (complete) {
                        complete();
                    }
                }
            });
        }

        /**
         * @description POST
         * @param {String} url 请求链接
         * @param {Object} data 参数
         * @param {Function} success 成功函数
         * @param {Function} fail 失败函数
         * @param {Function} complete 完成函数
         * @param {Object} header 头
         * @param {Boolean} async 是否异步，默认异步
         * @param {Number} timeout 超时时长
         */
        ajax.post = function (url, data, success, fail, complete, header, async, timeout) {
            if (timeout) {
                //默认值
                timeout = 10000;
            }
            if ((async === undefined) || (async === null)) {
                //默认值
                async = true;
            } else if (async.constructor === Number) {
                //没有录入async
                timeout = async;
                //默认值
                async = true;
            }
            if (!header) {
                //默认值
                header = {};
            } else if (header.constructor === Boolean) {
                //没有录入header
                async = header;
                //默认值
                header = {};
            } else if (header.constructor === Number) {
                //没有录入header/async
                timeout = header;
                //默认值
                header = {};
            }
            $.ajax({
                async: async,
                beforeSend: function (xhr) {
                    xhr.withCredentials = true;
                },
                contentType: "application/json; charset=utf-8",
                crossDomain: true,
                data: JSON.stringify(data),
                dataType: "json",
                headers: header,
                timeout: timeout,
                type: "POST",
                url: url,
                xhrFields: {
                    withCredentials: true
                },
                success: function (data, textStatus, xhr) {
                    if (success) {
                        success(data, textStatus, xhr);
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    if (fail) {
                        fail(xhr, textStatus, errorThrown);
                    }
                },
                complete: function () {
                    if (complete) {
                        complete();
                    }
                }
            });
        }
    }(system.ajax = {}));

    /**
     * @description Local Storage 操作
     */
    (function (storage) {
        /**
         * @description 获取localstorage数据
         * @param {String} key 关键字
         * @return {String} 值
         */
        storage.get = function (key) {
            var data = window.localStorage.getItem(key) || null;
            return data;
        };

        /**
         * @description 获取localstorage数据
         * @param {String} key 关键字
         * @return {Object} 对象
         */
        storage.getObject = function (key) {
            var data = window.localStorage.getItem(key) || null;
            return JSON.parse(data);
        };

        /**
         * @description 保存localstorage数据
         * @param {String} key 关键字
         * @param {Object} data 数据
         */
        storage.set = function (key, data) {
            data = data || {};
            window.localStorage.setItem(key, JSON.stringify(data));
        };

        /**
         * @description 移除localstorage数据
         * @param {String} key 关键字
         */
        storage.remove = function (key) {
            window.localStorage.removeItem(key);
        };
    }(system.storage = {}));
}(window.system = {}));