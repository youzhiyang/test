(function (index) {
    /**
     * @description URL参数
     */
    var _params = "";

    /**
     * @description 登录后返回URL
     */
    var _rtn = "";

    /**
     * @description 页面就绪
     */
    window.onload = function () {
        if (system.params.useSSO === true) {
            window.location.replace(system.params.login + (String.isNullOrEmpty(window.location.search) ? "" : window.location.search.substr(1)));
            return;
        }
        $(".LogonMain").css("height", $(window).height() - 10);
        $(".LogonMain").css("max-height", $(window).height() - 10);
        $(".LogonMain").css("width", $(window).width() - 0);
        system.ajax.get(system.api.account.getDingAppID, function (data, status, xhr) {
            if (data.errcode === 0) {
                //_rtn = common.getUrlParam("rtn");
                //if (_rtn) {
                //    _params = "?@R@" + _rtn + "@#R#@";
                //} else {
                //    _params = "";
                //}
                var appID = data.DataJSON;
                initDingApi(appID);
                var obj = DDLogin({
                    goto: encodeURIComponent("https://" + "oapi.dingtalk.com/connect/oauth2/sns_authorize?appid=" + appID + "&response_type=code&scope=snsapi_login&state="
                        + new Date().getMilliseconds() /*STATE*/ + "&redirect_uri=" + system.params.web + "/view/account/login.html" + window.location.search),
                    height: "291",
                    id: "divDingLogin",
                    style: "border:none;background-color:#ffffff;",
                    width: "300"
                });
            } else {
                layer.open({
                    btn: "确定",
                    content: data.errmsg,
                    icon: 2
                });
            }
        }, function (xhr, textStatus, errorThrown) {
            layer.open({
                btn: "确定",
                content: xhr.responseText,
                icon: 2,
                yes: function (index) {
                    //权限错误
                    if (xhr.statusText === "Unauthorized") {
                        window.location.replace("unauthorized.html" + window.location.search);
                    } else {
                        layer.close(index);
                    }
                }
            });
        }, function () {
        });
    }

    /**
     * @description 初始化钉钉接口
     * @param {String} appID AppID
     */
    function initDingApi(appID) {
        /**
         * @description 扫码返回事件
         */
        var hanndleMessage = function (event) {
            var origin = event.origin;
            //判断是否来自ddLogin扫码事件。
            if (origin === ("https:" + "//login.dingtalk.com")) {
                //拿到loginTmpCode后就可以在这里构造跳转链接进行跳转了
                var loginTmpCode = event.data;
                window.location.replace("https://" + "oapi.dingtalk.com/connect/oauth2/sns_authorize?appid=" + appID + "&response_type=code&scope=snsapi_login&state="
                    + new Date().getMilliseconds() /*STATE*/ + "&loginTmpCode=" + loginTmpCode + "&redirect_uri=" + system.params.web + "/view/account/login.html" + _params);
            }
        };

        /**
         * @description 钉钉登录二维码
         */
        !function (window, document) {
            function d(a) {
                var e, c = document.createElement("iframe"),
                    d = "https:" + "//login.dingtalk.com/login/qrcode.htm?goto=" + a.goto;
                d += a.style ? "&style=" + encodeURIComponent(a.style) : "",
                    d += a.href ? "&href=" + a.href : "",
                    c.src = d,
                    c.frameBorder = "0",
                    c.allowTransparency = "true",
                    c.scrolling = "no",
                    c.width = a.width ? a.width + "px" : "365px",
                    c.height = a.height ? a.height + "px" : "400px",
                    e = document.getElementById(a.id),
                    e.innerHTML = "",
                    e.appendChild(c)
            }
            window.DDLogin = d
        }(window, document);

        if (typeof window.addEventListener != "undefined") {
            window.addEventListener("message", hanndleMessage, false);
        } else if (typeof window.attachEvent != "undefined") {
            window.attachEvent("onmessage", hanndleMessage); typeof ("")
        }
    }
}(window.index = {}));