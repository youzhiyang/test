(function (parent, login) {
    /**
     * @description 登录后返回URL
     */
    var _rtn = "";

    /**
     * @description 登录后返回URL参数
     */
    var _rtnParam = "";

    /**
     * @description 临时授权码
     */
    var _tempCode = undefined;

    /**
     * @description 登录令牌
     */
    var _token = undefined;

    /**
     * @description 页面就绪
     */
    window.onload = function () {
        var query = window.location.search.substr(1);
        //if (query.indexOf("&") === 36) {
        //    _rtn = query.substr(0, query.indexOf("&"));
        //}
        if (query.indexOf("@R@") >= 0) {
            //_rtn = query.substr(query.indexOf("@R@") + 3, query.indexOf("@R$@") - (query.indexOf("@R@") + 3));
            var temp = query.match(/@R@(\S*)@R\$@/);
            if (temp !== null) {
                temp = temp[1];
                var param = temp.match(/@P@(\S*)@P\$@/);
                if (param !== null) {
                    _rtnParam = param[1].replace(/@eq@/, '=');
                    _rtn = temp.match(/(\S*)@P@/)[1];
                } else {
                    _rtn = temp;
                }
            }
        }
        _token = common.getUrlParam("tk");
        if (!String.isNullOrEmpty(_token)) {
            //单点登录跳转
            ssoLogin();
        } else {
            //钉钉登录跳转
            _tempCode = common.getUrlParam("code");
            if (!_tempCode) {
                window.location.replace("../index.html");
                return;
            }
            dingLogin();
        }
    }

    /**
     * @description 钉钉登录
     */
    function dingLogin() {
        var query = system.api.query;
        query.Param = { TempAuthCode: _tempCode };
        if (!String.isNullOrEmpty(_rtn)) {
            query.Param.PageID = _rtn;
        }
        if (!String.isNullOrEmpty(_rtnParam)) {
            query.Param.PageParam = _rtnParam;
        }
        console.log(query)
        system.ajax.post(system.api.account.dingLogin, query, function (data, status, xhr) {
            if (data.errcode === 0) {
                var page = JSON.parse(data.DataJSON);
                if (page && page.constructor === Object) {
                    window.location.replace("../" + page.Url);
                } else {
                    window.location.replace("../home.html");
                }
            } else {
                if (String.isNullOrEmpty(data.errmsg)) {
                    layer.open({
                        btn: "确定",
                        content: "登录失败，请重试。",
                        icon: 2,
                        yes: function () {
                            window.location.replace("../index.html");
                        }
                    });
                } else {
                    layer.open({
                        btn: "确定",
                        content: data.errmsg,
                        icon: 2,
                        yes: function () {
                            window.location.replace("../index.html");
                        }
                    });
                }
            }
        }, function () { });
    }

    /**
     * @description 单点登录
     */
    function ssoLogin() {
        var query = system.api.query;
        query.Param = { TempAuthCode: _tempCode };
        if (!String.isNullOrEmpty(_token)) {
            query.Param.Token = _token;
        }
        if (!String.isNullOrEmpty(_rtn)) {
            query.Param.PageID = _rtn;
        }
        if (!String.isNullOrEmpty(_rtnParam)) {
            query.Param.PageParam = _rtnParam;
        }
        system.ajax.post(system.api.account.ssoLogin, query, function (data, status, xhr) {
            if (data.errcode === 0) {
                var page = JSON.parse(data.DataJSON);
                console.log(page);
                if (page && page.constructor === Object) {
                    window.location.replace("../" + page.Url);
                } else {
                    window.location.replace("../home.html");
                }
            } else {
                if (String.isNullOrEmpty(data.errmsg)) {
                    layer.open({
                        btn: "确定",
                        content: "登录失败，请重试。",
                        icon: 2,
                        yes: function () {
                            window.location.replace("../index.html");
                        }
                    });
                } else {
                    layer.open({
                        btn: "确定",
                        content: data.errmsg,
                        icon: 2,
                        yes: function () {
                            window.location.replace("../index.html");
                        }
                    });
                }
            }
        }, function () { });
    }
}(window.account = window.account || {}, window.account.login = {}));