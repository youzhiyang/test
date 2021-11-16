(function (parent, register) {
    /**
     * @description 连接地址
     */
    var _connectUrl = "http://127.0.0.1:19196/OpenDevice";

    /**
     * @description 读卡地址
     */
    var _readUrl = "http://127.0.0.1:19196/readcard";

    /**
     * @description 开启功能kl。筽。
     */
    var _func = null;

    /**
     * @description 数据源。
     */
    var _dataSource = [];

    /**
     * @description 正在执行操作
     */
    var _doing = false;

    /**
     * @description 滚动条定时器
     */
    var timer = null;

    /**
     * @description 滚动条延时器
     */
    var timeOut = null;

    /**
     * @description 是否复制过
     */
    var isCopy = false;


    /**
     * @description 页面加载
     */
    $(document).ready(function () {
        initDataSource();
        initUI();
        initReader();
    });

    /**
     * @description 返回
     */
    register.return = function () {
        _func = null;
        $(".register_container").css("display", "flex");
        $("#registerEnterContainer").css("display", "none");
        $("#registerLeaveContainer").css("display", "none");
    };

    /********************* UI begin **********************/

    /**
     * @description 初始化UI
     */
    function initUI() {
        kendo.culture("zh-CN");
        $("#registerEnter").kendoButton();
        $("#registerLeave").kendoButton();
        $.get('business/enter.html', function (data) {
            data = data.slice(data.indexOf('<html>') + 6, data.indexOf('</html>'));
            $("#registerEnterContainer").html(data);
        });
        $.get('business/leave.html', function (data) {
            data = data.slice(data.indexOf('<html>') + 6, data.indexOf('</html>'));
            $("#registerLeaveContainer").html(data);
        });
        getTime();
        getWeather();
        $("#registerListView").kendoListView({
            dataSource: _dataSource,
            scrollable: true,
            template: "<div>#:Name#</div>"
        });
        initScroll();
    }

    /**
     * @description 获取当前时间
     */
    function getTime() {
        $("#registerTime").text(new Date().format("yyyy/MM/dd w HH:mm", formatWeek));
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

    /**
     * @description 获取天气数据
     */
    function getWeather() {
        system.ajax.post(system.api.common.getWeather, null, function (data, status, xhr) {
            if (data.errcode === 0) {
                var weather = JSON.parse(data.DataJSON).data;
                var weatherCode = JSON.parse(data.DataJSON).weatherCode;
                $("#registerWeatherIcon").attr("title", weatherCode[weather.weather]);
                $("#registerWeatherIcon .weatherfont use").attr("xlink:href", "#" + system.params.weatherClass[weather.weather]);
                $("#registerCurrentTemp").text(weather.temp + "℃");
                $("#registerWeatherDetail").html("&emsp;" + weather.tempn + "℃&nbsp;~&nbsp;" + weather.temp + "℃&emsp;" + weather.wd + "&nbsp;" + weather.ws);
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

    /********************* UI end **********************/

    /********************* Event begin **********************/

    /**
     * @description 返回
     */
    $("#registerReturn").on("click", function (e) {
        _func = null;
        $(".register_container").css("display", "flex");
        $("#registerEnterContainer").css("display", "none");
        $("#registerLeaveContainer").css("display", "none");
    });

    /**
     * @description 入场登记
     */
    $("#registerEnter").on("click", function (e) {
        _func = window.business.enter;
        _func.clearForm();
        $(".register_container").css("display", "none");
        $("#registerEnterContainer").css("display", "flex");
        $("#registerLeaveContainer").css("display", "flex");
        stopScroll();
    });

    /**
     * @description 离场登记
     */
    $("#registerLeave").on("click", function (e) {
        _func = window.business.leave;
        _func.clearForm();
        $(".register_container").css("display", "none");
        $("#registerEnterContainer").css("display", "none");
        $("#registerLeaveContainer").css("display", "flex");
        stopScroll();
    });

    /********************* Event end **********************/

    /********************* scroll end **********************/
    /**
     * 滚动列表
      */
    function scrollView() {
        var con1 = $(".k-listview-content");
        //复制滚动列表元素
        if (!isCopy) {
            isCopy = true;
            con1.append(con1.html());
        }
        if (con1[0].scrollHeight >= con1[0].offsetHeight) {
            var count = 0, childrenCount = 0, scrollHeight = 0, outerHeight = 0, speed = 50;
            timer = setInterval(scrol, speed);
            function scrol() {
                //判断是否是最后一条公告（所有子元素滚动完一轮之后，效果和初始化滚动列表效果一致）
                if (scrollHeight >= con1[0].scrollHeight / 2) {
                    if (outerHeight == count) {
                        init();
                    } else {
                        count++;
                        con1.scrollTop(scrollHeight++);
                        if (scrollHeight >= con1[0].scrollHeight) {
                            init();
                        }
                    }
                } else {
                    //获取每个滚动元素高度
                    outerHeight = $(con1.children()[childrenCount]).outerHeight(true);
                    //判断滚动的距离刚好为一条公告的高度时停掉定时器，隔2s之后重新启动计时器即可实现公告滚动停留效果
                    if (outerHeight == count) {
                        childrenCount++;
                        count = 0;
                        clearInterval(timer);
                        timeOut = setTimeout(() => {
                            timer = setInterval(scrol, speed);
                        }, 2000);
                    }
                    count++;
                    con1.scrollTop(scrollHeight++);
                }
            }
            function init() {
                //将最后一条公告变为第一条公告
                childrenCount = 0;
                //滚动高度置为0
                scrollHeight = 0;
                con1.scrollTop(scrollHeight);
                count = 0;
                clearInterval(timer);
                timeOut = setTimeout(() => {
                    timer = setInterval(scrol, speed);
                }, 2000);
            }
        }
    }

    /**
     * @description 停止滚动列表
     */
    function stopScroll() {
        clearInterval(timer);
        clearTimeout(timeOut);
    }

    /**
     * @description 开启滚动列表
     */
    register.startScroll = function() {
        scrollView();
    }

    /**
     * @description 初始化滚动列表
     */
    function initScroll() {
        var doing = setInterval(function () {
            if (!_doing) {
                clearInterval(doing);
                scrollView();
            }
        }, 200);
    }
    /********************* scroll end **********************/

    /********************* Business begin **********************/

    /**
     * @description 初始化数据源
     */
    function initDataSource() {
        _dataSource = new kendo.data.DataSource({
            schema: {
                data: function (response) {  
                    if (response !== null) {  
                        if (response.errcode === 0) {
                            return parseVisitorRecord(response.DataJSON);
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
                        Name: { type: "string" }
                    }
                }
            },
            serverPaging: false,
            serverSorting: false,
            serverFiltering: false,
            requestEnd: function (e) {
                _doing = false;
            },
            requestStart: function (e) {
                _doing = true;
            },
            transport: {
                read: {
                    contentType: "application/json; charset=utf-8",
                    crossDomain: true,
                    dataType: "json",
                    type: "get",
                    url: system.api.business.register.getVisitRecord,
                    xhrFields: {
                        withCredentials: true
                    }
                }
            }
        });
    }

    /**
     * @description 解析访客记录
     */
    function parseVisitorRecord(visitRecord) {
        //2021/11/3 17:03:53公安有限公司的李博轩携带包，电脑，手机进入2-2机房进行设备调试 引导人:梁中源 2021/11/3 17:54:38李博轩离开 引导人:周游
        var viewListArr = [];
        var visitArr = JSON.parse(visitRecord);
        for (var i = 0; i < visitArr.length; i++) {
            var visitMessage = new Date(visitArr[i].RegisterTime).format("yyyy-MM-dd hh:mm:ss");
            var map = new Map();
            var map1 = new Map();
            for (var j = 0; j < visitArr[i].VisitorList.length;j++) {
                var visitOrgization = visitArr[i].VisitorList[j].VisitOrgization;
                if (map.has(visitOrgization)) {
                    var enterArr = map.get(visitOrgization);
                    enterArr.push(visitArr[i].VisitorList[j].Name);
                } else {
                    var enterArr = [visitArr[i].VisitorList[j].Name];
                    map.set(visitOrgization, enterArr);
                }
                if (visitArr[i].VisitorList[j].LeaveGuideEntity != null) {
                    var leaveGuide = visitArr[i].VisitorList[j].LeaveGuideEntity.StaffName;
                    var leaveTimeStr = new Date(visitArr[i].VisitorList[j].LeaveTime).format("yyyy-MM-dd hh:mm:ss");
                    var key = leaveTimeStr + "=" + leaveGuide;
                    if (map1.has(key)) {
                        var leaveArr = map1.get(key);
                        leaveArr.push(visitArr[i].VisitorList[j].Name);
                    } else {
                        var leaveArr = [visitArr[i].VisitorList[j].Name];
                        map1.set(key, leaveArr);
                    }
                } else {
                    if (map1.has("noleave")) {
                        var leaveArr = map1.get("noleave");
                        leaveArr.push(visitArr[i].VisitorList[j].Name);
                    } else {
                        var leaveArr = [visitArr[i].VisitorList[j].Name];
                        map1.set("noleave", leaveArr);
                    }
                }
            }
            map.forEach(function (value, key) {
                visitMessage += key + "的" + value.join("、");
            });
            if (!visitArr[i].DeviceNTools.trim().isNullOrEmpty()) {
                visitMessage += "携带" + visitArr[i].DeviceNTools.trim();
            }
            visitMessage += "进入";
            var count = 0;
            for (var m = 0; m < visitArr[i].DataAreaList.length; m++) {
                if (count == visitArr[i].DataAreaList.length - 1) {
                    visitMessage += visitArr[i].DataAreaList[m].Name;
                } else {
                    visitMessage += visitArr[i].DataAreaList[m].Name + "、";
                    count++;
                }
            }
            visitMessage += "进行" + visitArr[i].ReasonName + " 引导人:" + visitArr[i].EnterGuideEntity.StaffName;
            map1.forEach(function (value, key) {
                if (key === "noleave") {
                    visitMessage += " 未发现" + value.join("、") + "的离场记录";
                } else {
                    visitMessage += " " + key.split("=")[0] + value.join("、") + "离开" + " 引导人:" + key.split("=")[1];
                }
            });
            viewListArr.push({ Name: visitMessage});
        }
        return viewListArr;
    }

    /**
     * @description 初始化读卡器
     */
    function initReader() {
        var options = new Object();
        options.type = "GET";
        options.url = _connectUrl + "&t=" + Math.random();
        options.timeout = 5000;
        options.onSuccess = function () {
            readCert();
        };
        ajax(options);
    }

    /**
     * @description 读卡
     */
    function readCert() {
        try {
            function onSuccess(data) {
                if (_func) {
                    var cert = JSON.parse(data);
                    _func.readCard(cert);
                }
                //var endDt = new Date();
                //document.getElementById("timeElapsed").value = (endDt.getTime() - startDt.getTime()) + "毫秒";
                //var errorMsg = data.match("\"errorMsg\" : (.*)")[1];
                //var resultFlag = data.match("\"resultFlag\" : (.*)")[1];
                //document.getElementById("result").value = "提示:" + errorMsg + "\n返回值：" + resultFlag;

                //var certType = data.match("\"certType\" : \"(.*?)\"")[1];
                //if (certType == " ") {
                //    certType = "身份证";
                //}
                //else if (certType == "I") {
                //    certType = "外国人居住证";
                //}
                //else if (certType == "J") {
                //    certType = "港澳台居住证";
                //}
                //else {
                //    certType = "未知";
                //}
                //document.getElementById("certType").value = certType;

                //document.getElementById("partyName").value = data.match("\"partyName\" : \"(.*?)\"")[1];
                //document.getElementById("gender").value = data.match("\"gender\" : \"(.*?)\"")[1];
                //document.getElementById("nation").value = data.match("\"nation\" : \"(.*?)\"")[1];
                //document.getElementById("bornDay").value = data.match("\"bornDay\" : \"(.*?)\"")[1];
                //document.getElementById("certAddress").value = data.match("\"certAddress\" : \"(.*?)\"")[1];
                //document.getElementById("certNumber").value = data.match("\"certNumber\" : \"(.*?)\"")[1];
                //document.getElementById("certOrg").value = data.match("\"certOrg\" : \"(.*?)\"")[1];
                //document.getElementById("effDate").value = data.match("\"effDate\" : \"(.*?)\"")[1];
                //document.getElementById("expDate").value = data.match("\"expDate\" : \"(.*?)\"")[1];
                //document.all['PhotoDisplay'].src = 'data:image/jpeg;base64,' + data.match("\"identityPic\" : \"(.*?)\"")[1];
            }
            var options = new Object();
            options.type = "GET";
            options.url = _readUrl + "&t=" + Math.random();
            options.timeout = 5000;
            options.onSuccess = onSuccess;
            ajax(options);
        } catch (e) {
        }
        finally {
            setTimeout(readCert, 2000);
        }
    }

    /**
     * @description 发起ajax访问
     * @param {Object} options 参数
     */
    function ajax(options) {
        if (options.type == null) {
            options.type = "POST";
        }

        if (options.url == null) {
            options.url = "";
        }

        if (options.timeout == null) {
            options.timeout = 5000;
        }

        if (options.onComplate == null) {
            options.onComplate = function () {
            }
        }

        if (options.onError == null) {
            options.onError = function () {
            }
        }

        if (options.onSuccess == null) {
            options.onSuccess = function () {
            }
        }

        if (options.data) {
            options.data = "";
        }

        var xml;
        if (typeof ActiveXObject != 'undefined') {
            var aVersions = ["Microsoft.XMLHTTP", "Msxml2.XMLHttp.6.0", "Msxml2.XMLHttp.5.0", "Msxml2.XMLHttp.4.0", "Msxml2.XMLHttp.3.0"];
            for (var i = 0; i < aVersions.length; i++) {
                try {
                    xml = new ActiveXObject(aVersions[i]);
                } catch (e) { }
            }
        } else if (typeof XMLHttpRequest != 'undefined') {
            xml = new XMLHttpRequest();
        }

        xml.open(options.type, options.url, true);

        var timeoutLength = options.timeout;

        var requestDone = false;

        setTimeout(function () {
            requestDone = true;
        }, timeoutLength);

        xml.onreadystatechange = function () {

            if (xml.readyState == 4 && !requestDone) {

                if (httpSuccess(xml)) {

                    options.onSuccess(httpData(xml));
                }

                else {
                    options.onError();
                }

                options.onComplate();

                xml = null;
            }
        };

        xml.send();

        function httpSuccess(r) {
            try {
                return !r.status && location.protocol == "file:"
                    ||
                    (r.status >= 200 && r.status <= 300)
                    ||
                    r.status == 304
                    ||

                    navigator.userAgent.indexOf("Safari") >= 0
                    && typeof r.status == "undefined";
            } catch (e) {
            }
            return false;
        }

        function httpData(r) {
            try {
               /* var ct = r.getResponseHeader("responseType");
                if (ct) {
                    if (ct == "script") {
                        eval.call(window, data);
                    }
                    if (ct == "xml") {
                        return r.responseXML;
                    }
                    if (ct == "json") {
                        return JSON.parse(r.responseText);
                    }
                }*/
                return r.responseText;
            } catch (e) {
                return r.responseText;
            }
        }
    }
    /********************* Business end **********************/
}(window, window.register = {}));