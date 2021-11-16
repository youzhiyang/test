(function (parent, page) {
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
        if (system.common.checkPageAuthorized("8f514eab-e34a-40d2-a752-780260d82aa5") === false) {
            window.location.replace("../unauthorized.html");
        }
        initDataSource();
        initUI();
        system.common.checkPageElementAuthorized("8f514eab-e34a-40d2-a752-780260d82aa5");
    });

    /********************* UI begin **********************/

    /**
     * @description 初始化UI
     */
    function initUI() {
        kendo.culture("zh-CN");
        $("#pageToolbar").kendoToolBar({
            items: [
                { attributes: { "class": "page_add_row k-button operator editor unauthorized" }, template: "<span class=\"pmfont pm-xinzeng\">新增</span>" },
                { attributes: { "class": "page_save_change k-button operator editor unauthorized" }, template: "<span class=\"pmfont pm-baocun\">保存</span>" },
                { attributes: { "class": "page_cancel_change k-button operator editor unauthorized" }, template: "<span class=\"pmfont pm-jinzhi\">取消</span>" },
                { attributes: { "class": "toolbar_placeholder operator editor unauthorized" }, type: "separator" },
                { template: "<input class=\"page_condition\" placeholder=\"url\" />" },
                { attributes: { "class": "page_refresh_data k-button" }, template: "<span class=\"pmfont pm-chaxun\">查询</span>" }
            ]
        });
        $("#pageData").kendoGrid({
            columns: [
                {
                    command: [{
                        className: "pmfont pm-guanbi operator delete unauthorized", click: deleteItem, name: "deleteItem", text: "删除",
                    }],
                    title: "操作",
                    width: common.math.plus(common.math.multiply(common.math.plus(2, common.math.multiply(5.9, 1)), parseFloat($("body").css("fontSize"))), 1)
                }, {
                    attributes: { style: "text-align: center;" },
                    field: "IsValid",
                    template: function (data) {
                        return "<span class=\"switch_state pmfont pm_nopad pm-jinzhi" + ((data.IsValid === true) ? "" : " disabled")
                            + "\" pageID=\"" + data.PageID + "\"></span>";
                    },
                    title: "禁用",
                    width: 60
                }, {
                    field: "PageID",
                    title: "ID",
                    width: 400
                }, {
                    attributes: { title: "#: Name === null ? '' : Name #" },
                    field: "Name",
                    title: "名称",
                    width: 300
                }, {
                    attributes: { title: "#: Url === null ? '' : Url #" },
                    field: "Url",
                    title: "地址",
                    width: 500
                }, {
                    attributes: { title: "#: Remark === null ? '' : Remark #" },
                    field: "Remark",
                    title: "描述",
                    width: 500
                }, {}],
            dataBound: function (e) { system.common.checkPageElementAuthorized("8f514eab-e34a-40d2-a752-780260d82aa5"); },
            dataSource: _dataSource,
            editable: {
                confirmation: false,
                mode: "incell",
                update: true
            },
            groupable: false,
            navigatable: true,
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
     * @description 新增行
     */
    $("#pageToolbar").on("click", ".page_add_row.authorized", function () {
        $("#pageData").data("kendoGrid").addRow();
    });

    /**
     * @description 保存修改
     */
    $("#pageToolbar").on("click", ".page_save_change.authorized", saveChanges);

    /**
     * @description 取消修改
     */
    $("#pageToolbar").on("click", ".page_cancel_change.authorized", function () {
        $("#pageData").data("kendoGrid").cancelChanges();
    });

    /**
     * @description 查询数据
     */
    $("#pageToolbar").on("keypress", ".page_condition", function (e) {
        if ((e.keyCode === 13) && (e.shiftKey === false) && (e.ctrlKey === false) && (e.altKey === false)) {
            refreshData();
        }
    });

    /**
     * @description 刷新数据
     */
    $("#pageToolbar").on("click", ".page_refresh_data", refreshData);

    /**
     * @description 切换状态
     */
    $("#pageData").on("click", ".switch_state.authorized", function () {
        var pageID = $(this).attr("pageID");
        var data = { PageID: pageID };
        $(this).toggleClass("disabled");
        if ($(this).hasClass("disabled")) {
            data.IsValid = false;
        } else {
            data.IsValid = true;
        }
        swicthPageState(data);
    });

    /********************* Event end **********************/

    /********************* Business begin **********************/

    /**
     * @description 初始化数据源
     */
    function initDataSource() {
        _dataSource = new kendo.data.DataSource({
            batch: true,
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
                        PageID: { editable: false, nullable: false, type: "string" },
                        Name: { editable: true, nullable: false, type: "string" },
                        Url: { editable: true, nullable: false, type: "string" },
                        IsValid: { editable: false, nullable: true, type: "bool" },
                        Remark: { editable: true, nullable: true, type: "string" }
                    },
                    id: "PageID"
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
                parameterMap: function (data, type) {
                    if (type === "read") {
                        var query = system.api.query;
                        query.Param = { Condition: $(".page_condition").val() };
                        return JSON.stringify(query);
                    }
                },
                read: {
                    contentType: "application/json; charset=utf-8",
                    crossDomain: true,
                    dataType: "json",
                    type: "post",
                    url: system.api.systemdata.page.getPages,
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
     * @description 删除数据
     * @param {Object} e 事件
     */
    function deleteItem(e) {
        layer.open({
            btn: ["确定", "取消"],
            content: "确认删除吗？",
            icon: 3,
            yes: function (index) {
                $("#pageData").data("kendoGrid").removeRow($(e.target).parent().parent());
                layer.close(index);
            }
        });
        return false;
    }

    /**
     * @description 切换页面启用状态
     * @param {Object} page 页面
     */
    function swicthPageState(page) {
        if (_doing === true) {
            layer.open({
                btn: "确定",
                content: "请等待当前操作完成。",
                icon: 0
            });
            return;
        }
        _doing = true;
        kendo.ui.progress($("#pageData"), true);
        system.ajax.post(system.api.systemdata.page.updatePageState, page, function (data, status, xhr) {
            _doing = false;
            kendo.ui.progress($("#pageData"), false);
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
            kendo.ui.progress($("#pageData"), false);
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
     * @description 保存修改
     */
    function saveChanges() {
        if (_doing === true) {
            layer.open({
                btn: "确定",
                content: "请等待当前操作完成。",
                icon: 0
            });
            return;
        }
        _doing = true;
        var result = true;
        kendo.ui.progress($("#pageData"), true);
        var counter = { value: 0, count: 2, time: 200 };
        var upsertData = Array.where($("#pageData").data("kendoGrid").dataSource.data(), "dirty", "=", true);
        var removeData = $("#pageData").data("kendoGrid").dataSource.destroyed();
        system.ajax.post(system.api.systemdata.page.deletePages, removeData, function (data, status, xhr) {
            if (data.errcode === 0) {
                result &= true;
            } else {
                result &= false;
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
                        window.location.replace("index.html");
                    } else {
                        layer.close(index);
                    }
                }
            });
        }, function () {
            counter.value++;
            system.ajax.post(system.api.systemdata.page.upsertPages, upsertData, function (data, status, xhr) {
                if (data.errcode === 0) {
                    result &= true;
                } else {
                    result &= false;
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
                            window.location.replace("index.html");
                        } else {
                            layer.close(index);
                        }
                    }
                });
            }, function () {
                counter.value++;
            });
        });
        common.waiting(counter, function () {
            _doing = false;
            kendo.ui.progress($("#pageData"), false);
            if (result === 1) {
                refreshData();
            }
        });
    }

    /********************* Business end **********************/
}(window.systemdata = window.systemdata || {}, window.systemdata.page = {}));