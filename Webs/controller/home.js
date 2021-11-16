(function (parent, home) {
    /**
     * @description tab控件
     */
    var _tabStrip = undefined;

    /**
     * @description 是否第一次验证
     */
    var _isFirstTimeCheck = true;

    /**
     * @description 页面加载
     */
    $(document).ready(function () {
        initUI();
    });

    /********************* UI begin **********************/

    /**
     * @description 初始化UI
     */
    function initUI() {
        kendo.culture("zh-CN");
        _tabStrip = $("#homeTabStrip").kendoTabStrip().data("kendoTabStrip");
        getTime();
        //getWeather();
        //checkAuth();
        _tabStrip.append(
            [
                {
                    contentUrl: "business/enter.html",
                    encoded: false,
                    text: "访客登记"
                }
            ]
        );
        _tabStrip.activateTab("li:first");
    }

    /**
     * @description 加载菜单
     */
    function loadMenu() {
        //$.getJSON("../json/menu.json", function (data) {
        //    if (Array.where(window.userAuthority.RoleList, "RoleID", "=", "dbcaad7f-63da-4bde-9784-c7938ef891ac").length > 0) {
        //        $("#homeNav").kendoMenu({
        //            dataSource: data
        //        });
        //    } else {
        //        $("#homeNav").kendoMenu({
        //            dataSource: getAuthorizedMenu(data)
        //        });
        //    }
        //});
        //$.getJSON("../json/systemMenu.json", function (data) {
        //    if (Array.where(window.userAuthority.RoleList, "RoleID", "=", "dbcaad7f-63da-4bde-9784-c7938ef891ac").length > 0) {
        //        $("#homeSystemNav").kendoMenu({
        //            dataSource: data
        //        });
        //    } else {
        //        $("#homeSystemNav").kendoMenu({
        //            dataSource: getAuthorizedMenu(data)
        //        });
        //    }
        //});
    }

    /**
     * @description 打开标签
     * @param {String} url URL
     * @param {String} title 页面标题
     */
    function openTab(url, title) {
        var openedTab = $("#homeTabStrip [data-content-url='" + url + "']");
        if (openedTab.length === 0) {
            //打开新标签
            _tabStrip.append(
                [
                    {
                        contentUrl: url,
                        encoded: false,
                        text: title + "<a class=\"pmfont pm-guanbi close_tab\"></a>"
                    }
                ]
            );
            _tabStrip.activateTab("li:last");
        } else {
            //跳转已有标签
            _tabStrip.activateTab(_tabStrip.activateTab(openedTab.parent()));
        }
    }

    /**
     * @description 获取当前时间
     */
    function getTime() {
        $("#homeTime").text(new Date().format("yyyy/MM/dd w HH:mm", formatWeek));
        setTimeout(getTime, 5000);
    }

    /**
     * @description 格式化星期数
     * @param {Number} day 星期数
     * @return {String} 格式化后的星期数
     */
    function formatWeek(day) {
        var week = ["星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"];
        return week[day];
    }

    /********************* UI end **********************/

    /********************* Event begin **********************/

    /**
     * @description 退出登录
     */
    $("#home_logout").on("click", function () {
        layer.open({
            btn: ["确定", "取消"],
            content: "确定退出登录吗？",
            yes: function (index) {
                system.ajax.get(system.api.account.logout, function (data, status, xhr) {
                    if (data.errcode === 0) {
                        if (system.params.useSSO === true) {
                            window.location.replace(system.params.login);
                        }
                        else {
                            window.location.replace("index.html");
                        }
                    } else {
                        layer.open({
                            btn: "确定",
                            content: data.errmsg
                        });
                    }
                });
                layer.close(index);
            }
        });
        return false;
    });

    /**
     * @description 点击链接
     */
    $("#homeNav").on("click", "li.k-item.link", function () {
        var title = $("span", $(this)).text();
        var url = $(this).attr("url");
        if (!String.isNullOrEmpty(url)) {
            openTab(url, title);
        } else {
            console.log("no url");
        }
    });

    /**
     * @description 点击链接
     */
    $("#homeNav").on("click", "li.k-item.window", function () {
        var title = $("span", $(this)).text();
        var url = $(this).attr("url");
        if (!String.isNullOrEmpty(url)) {
            window.open(url, title);
        } else {
            console.log("no url");
        }
    });

    /**
     * @description 点击系统设置链接
     */
    $("#homeSystemNav").on("click", "li.k-item.link", function () {
        var title = $("span", $(this)).text();
        var url = $(this).attr("url");
        if (!String.isNullOrEmpty(url)) {
            openTab(url, title);
        } else {
            console.log("no url");
        }
    });

    /**
     * @description 点击系统设置链接
     */
    $("#homeSystemNav").on("click", "li.k-item.window", function () {
        var title = $("span", $(this)).text();
        var url = $(this).attr("url");
        if (!String.isNullOrEmpty(url)) {
            window.open(url, title);
        } else {
            console.log("no url");
        }
    });

    /**
     * @description 关闭标签
     */
    $("#homeTabStrip").on("click", ".close_tab", function () {
        _tabStrip.remove($(this).parent().parent());
        if ($(".k-tabstrip-items li", $("#homeTabStrip")).length > 0) {
            _tabStrip.activateTab("li:last");
        }
    });

    /**
     * @description 点击Welcome页链接
     */
    $("#homeTabStrip").on("click", ".welcome_menu_grid.link.authorized", function () {
        var title = $(".welcome_menu_grid_title", $(this)).text();
        var url = $(this).attr("url");
        if (!String.isNullOrEmpty(url)) {
            openTab(url, title);
        } else {
            console.log("no url");
        }
    });

    /**
     * @description 点击Welcome页链接
     */
    $("#homeTabStrip").on("click", ".welcome_menu_grid.window.authorized", function () {
        var title = $(".welcome_menu_grid_title", $(this)).text();
        var url = $(this).attr("url");
        if (!String.isNullOrEmpty(url)) {
            window.open(url, title);
        } else {
            console.log("no url");
        }
    });

    /********************* Event end **********************/

    /********************* Business begin **********************/

    /**
     * @description 获取天气数据
     */
    function getWeather() {
        system.ajax.post(system.api.common.getWeather, null, function (data, status, xhr) {
            if (data.errcode === 0) {
                var weather = JSON.parse(data.DataJSON).data.result.桂林;
                var weatherCode = JSON.parse(data.DataJSON).weatherCode;
                $("#homeWeatherIcon").attr("title", weatherCode[weather.forecast24h.dayweather]);
                $("#homeWeatherIcon .weatherfont use").attr("xlink:href", "#" + system.params.weatherClass[weatherCode[weather.forecast24h.dayweather]]);
                $("#homeCurrentTemp").text(weather.obs.tem + "℃");
                $("#homeWeatherDetail").html("&emsp;" + weather.forecast24h.mintem + "℃&nbsp;~&nbsp;" + weather.forecast24h.maxtem + "℃&emsp;" + weather.obs.wd + "&nbsp;" + weather.obs.ws);
            }
        }, function (xhr, textStatus, errorThrown) {
            layer.open({
                btn: "确定",
                content: xhr.responseText,
                icon: 2,
                yes: function (index) {
                    //权限错误
                    if (xhr.statusText === "Unauthorized") {
                        if (system.params.useSSO === true) {
                            window.location.replace(system.params.login + "&rtn=7c2b445b-5a62-4eac-921d-f407bc7e4c03");
                        }
                        else {
                            window.location.replace("../index.html?rtn=7c2b445b-5a62-4eac-921d-f407bc7e4c03");
                        }
                    } else {
                        layer.close(index);
                    }
                }
            });
        }, function () {
            setTimeout(getWeather, system.params.weatherTimespan);
        });
    }

    /**
     * @description 验证身份
     */
    function checkAuth() {
        system.ajax.post(system.api.home.validateCookie, null, function (data, status, xhr) {
            if (data.errcode === 0) {
                $("#homeUserName").text(JSON.parse(JSON.parse(data.DataJSON).UserData).Name);
                getUserAuthorities();
                if (window.userAuthority === undefined) {
                    _tabStrip.append(
                        [
                            {
                                contentUrl: "index.html",
                                encoded: false,
                                text: "首页"
                            }
                        ]
                    );
                    _tabStrip.activateTab("li:last");
                }
            } else if (_isFirstTimeCheck === false) {
                layer.open({
                    btn: "确定",
                    content: data.errmsg,
                    icon: 2,
                    yes: function () {
                        if (system.params.useSSO === true) {
                            window.location.replace(system.params.login);
                        }
                        else {
                            window.location.replace("index.html");
                        }
                    }
                });
            } else {
                if (system.params.useSSO === true) {
                    window.location.replace(system.params.login);
                }
                else {
                    window.location.replace("index.html");
                }
            }
            _isFirstTimeCheck = false;
        }, function () { }, function () {
            //每5分钟检测一次身份有效性
            setTimeout(checkAuth, 1000 * 60 * 5)
        });
    }

    /**
     * @description 获取用户权限
     */
    function getUserAuthorities() {
        system.ajax.post(system.api.home.getUserAuthorities, null, function (data, status, xhr) {
            if (data.errcode === 0) {
                window.userAuthority = JSON.parse(data.DataJSON);
                loadMenu();
            } else {
                layer.open({
                    btn: "确定",
                    content: data.errmsg,
                    icon: 2,
                    yes: function () {
                        if (system.params.useSSO === true) {
                            window.location.replace(system.params.login);
                        }
                        else {
                            window.location.replace("index.html");
                        }
                    }
                });
            }
        }, function () { }, function () { });
    }

    /**
     * @description 获取授权的菜单
     * @param {Object} menuItem 待验证的菜单
     * @returns {Object} 授权的菜单
     */
    function getAuthorizedMenu(menuItem) {
        var authorizedMenu = [];
        for (var i = 0; i < menuItem.length; i++) {
            if (menuItem[i].id) {
                //有ID则为页面链接
                for (var j = 0; j < window.userAuthority.PageList.length; j++) {
                    if (menuItem[i].id.toLowerCase() === window.userAuthority.PageList[j].PageID.toLowerCase()) {
                        //有授权
                        authorizedMenu.push(menuItem[i]);
                    }
                }
            } else {
                //没有ID则为菜单
                var subMenu = getAuthorizedMenu(menuItem[i].items);
                if (subMenu.length > 0) {
                    var mainMenu = JSON.parse(JSON.stringify(menuItem[i]));
                    mainMenu.items = subMenu;
                    authorizedMenu.push(mainMenu);
                }
            }
        }
        return authorizedMenu;
    }

    /********************* Business end **********************/
}(window, window.home = {}));