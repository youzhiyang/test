(function (parent, action) {
    /**
     * @description 正在执行操作
     */
    var _doing = false;

    /**
     * @description 数据源
     */
    var _dataSource = undefined;

    /**
     * @description 页面加载
     */
    $(document).ready(function () {
        if (system.common.checkPageAuthorized("baca1af5-ec74-4f73-a997-359c6a512924") === false) {
            window.location.replace("../unauthorized.html");
        }
        initDataSource();
        initUI();
        system.common.checkPageElementAuthorized("baca1af5-ec74-4f73-a997-359c6a512924");
    });

    /********************* UI begin **********************/

    /**
     * @description 初始化UI
     */
    function initUI() {
        kendo.culture("zh-CN");
        $("#actionToolbar").kendoToolBar({
            items: [
                { attributes: { "class": "action_refresh_data k-button" }, template: "<span class=\"pmfont pm-shuaxin\">刷新数据</span>" },
                { attributes: { "class": "action_sync_data k-button operator editor unauthorized" }, template: "<span class=\"pmfont pm-tongbushuju\">同步数据</span>" }
            ]
        });
        $("#actionData").kendoGrid({
            columns: [
                {
                    attributes: { style: "text-align: center;" },
                    field: "IsValid",
                    template: function (data) {
                        return "<span class=\"switch_state pmfont pm_nopad pm-jinzhi" + ((data.IsValid === true) ? "" : " disabled")
                            + "\" controller=\"" + data.Controller + "\" action=\"" + data.Action + "\"></span>";
                    },
                    title: "禁用",
                    width: 60
                }, {
                    attributes: { title: "#: Controller === null ? '' : Controller #" },
                    field: "Controller",
                    title: "Controller",
                    width: 500
                }, {
                    attributes: { title: "#: Action === null ? '' : Action #" },
                    field: "Action",
                    title: "Action",
                    width: 400
                }, {
                    attributes: { title: "#: Description === null ? '' : Description #" },
                    field: "Description",
                    title: "描述",
                    width: 500
                }, {}],
            dataBound: function (e) { system.common.checkPageElementAuthorized("baca1af5-ec74-4f73-a997-359c6a512924"); },
            dataSource: _dataSource,
            groupable: false,
            pageable: {
                buttonCount: 5,
                messages: {
                    display: "{0} - {1} 行 共 {2} 行",
                    empty: "没有数据",
                    itemsPerPage: "行 / 页"
                },
                pageSizes: [10, 20, 50],
                refresh: false
            },
            sortable: false
        });
    }

    /********************* UI end **********************/

    /********************* Event begin **********************/

    /**
     * @description 刷新数据
     */
    $("#actionToolbar").on("click", ".action_refresh_data", refreshData);

    /**
     * @description 同步数据
     */
    $("#actionToolbar").on("click", ".action_sync_data.authorized", syncData);

    /**
     * @description 切换状态
     */
    $("#actionData").on("click", ".switch_state.authorized", function () {
        var controller = $(this).attr("controller");
        var action = $(this).attr("action");
        var data = { Controller: controller, Action: action };
        $(this).toggleClass("disabled");
        if ($(this).hasClass("disabled")) {
            data.IsValid = false;
        } else {
            data.IsValid = true;
        }
        swicthActionState(data);
    });

    /********************* Event end **********************/

    /********************* Business begin **********************/

    /**
     * @description 初始化数据源
     */
    function initDataSource() {
        _dataSource = new kendo.data.DataSource({
            error: function (e) {
                layer.open({
                    btn: "确定",
                    content: e.xhr.responseText,
                    icon: 2,
                    yes: function (index) {
                        //权限错误
                        if (e.xhr.statusText === "Unauthorized") {
                            window.location.replace("index.html");
                        } else {
                            layer.close(index);
                        }
                    }
                });
            },
            pageSize: 20,
            requestEnd: function (e) {
                _doing = false;
            },
            requestStart: function (e) {
                if (_doing === true) {
                    layer.open({
                        btn: "确定",
                        content: "请等待当前操作完成。",
                        icon: 0
                    });
                    return false;
                }
                _doing = true;
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
                        Controller: { editable: false, nullable: false, type: "string" },
                        Action: { editable: false, nullable: false, type: "string" },
                        Description: { editable: false, nullable: true, type: "string" },
                        IsValid: { editable: true, nullable: true, type: "bool" }
                    }
                },
                total: function (response) {
                    if (response.Total) {
                        return response.Total;
                    } else {
                        if (!String.isNullOrEmpty(response.DataJSON)) {
                            return JSON.parse(response.DataJSON).length;
                        } else {
                            return 0;
                        }
                    }
                }
            },
            transport: {
                read: {
                    contentType: "application/json; charset=utf-8",
                    crossDomain: true,
                    dataType: "json",
                    type: "post",
                    url: system.api.systemdata.action.getActions,
                    xhrFields: {
                        withCredentials: true
                    }
                }
            }
        });
    }

    /**
     * @description 刷新数据
     */
    function refreshData() {
        if (_doing === true) {
            layer.open({
                btn: "确定",
                content: "请等待当前操作完成。",
                icon: 0
            });
            return;
        }
        _dataSource.read();
        return false;
    }

    /**
     * @description 同步数据
     */
    function syncData() {
        if (_doing === true) {
            layer.open({
                btn: "确定",
                content: "请等待当前操作完成。",
                icon: 0
            });
            return;
        }
        _doing = true;
        kendo.ui.progress($("#actionData"), true);
        system.ajax.post(system.api.systemdata.action.syncAction, null, function (data, status, xhr) {
            _doing = false;
            kendo.ui.progress($("#actionData"), false);
            if (data.errcode === 0) {
                refreshData();
            } else {
                layer.open({
                    btn: "确定",
                    content: data.errmsg,
                    icon: 2
                });
            }
        }, function (xhr, textStatus, errorThrown) {
            _doing = false;
            kendo.ui.progress($("#actionData"), false);
            layer.open({
                btn: "确定",
                content: xhr.responseText,
                icon: 2,
                yes: function (index) {
                    //权限错误
                    if (xhr.statusText === "Unauthorized") {
                        window.location.replace("index.html");
                    } else {
                        layer.close(index);
                    }
                }
            });
        }, function () { });
    }

    /**
     * @description 切换Action启用状态
     * @param {Object} action Action
     */
    function swicthActionState(action) {
        if (_doing === true) {
            layer.open({
                btn: "确定",
                content: "请等待当前操作完成。",
                icon: 0
            });
            return;
        }
        _doing = true;
        kendo.ui.progress($("#actionData"), true);
        system.ajax.post(system.api.systemdata.action.updateActionState, action, function (data, status, xhr) {
            _doing = false;
            kendo.ui.progress($("#actionData"), false);
            if (data.errcode === 0) {
                refreshData();
            } else {
                layer.open({
                    btn: "确定",
                    content: data.errmsg,
                    icon: 2
                });
            }
        }, function (xhr, textStatus, errorThrown) {
            _doing = false;
            kendo.ui.progress($("#actionData"), false);
            layer.open({
                btn: "确定",
                content: xhr.responseText,
                icon: 2,
                yes: function (index) {
                    //权限错误
                    if (xhr.statusText === "Unauthorized") {
                        window.location.replace("index.html");
                    } else {
                        layer.close(index);
                    }
                }
            });
        }, function () { });
    }

    /********************* Business end **********************/
}(window.systemdata = window.systemdata || {}, window.systemdata.action = {}));