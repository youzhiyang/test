(function (parent, element) {
    /**
     * @description 正在执行操作
     */
    var _doing = false;

    /**
     * @description 数据源
     */
    var _dataSource = undefined;

    /**
     * @description 页面数据源
     */
    var _pageDataSource = undefined;

    /**
     * @description 页面加载
     */
    $(document).ready(function () {
        if (system.common.checkPageAuthorized("1773da2c-3785-47e0-8a87-fb644bd744fd") === false) {
            window.location.replace("../unauthorized.html");
        }
        initDataSource();
        initUI();
        system.common.checkPageElementAuthorized("1773da2c-3785-47e0-8a87-fb644bd744fd");
    });

    /********************* UI begin **********************/

    /**
     * @description 初始化UI
     */
    function initUI() {
        kendo.culture("zh-CN");
        $("#elementToolbar").kendoToolBar({
            items: [
                { attributes: { "class": "element_add_row k-button operator editor unauthorized" }, template: "<span class=\"pmfont pm-xinzeng\">新增</span>" },
                { attributes: { "class": "element_save_change k-button operator editor unauthorized" }, template: "<span class=\"pmfont pm-baocun\">保存</span>" },
                { attributes: { "class": "element_cancel_change k-button operator editor unauthorized" }, template: "<span class=\"pmfont pm-jinzhi\">取消</span>" },
                { attributes: { "class": "toolbar_placeholder operator editor unauthorized" }, type: "separator" },
                { template: "<input class=\"element_condition\" placeholder=\"页面名称/选择器\" />" },
                { attributes: { "class": "element_refresh_data k-button" }, template: "<span class=\"pmfont pm-chaxun\">查询</span>" }
            ]
        });
        $("#elementData").kendoGrid({
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
                            + "\" elementID=\"" + data.ElementID + "\"></span>";
                    },
                    title: "禁用",
                    width: 60
                }, {
                    attributes: { title: "#: PageName === null ? '' : PageName #" },
                    editor: pageEditor,
                    field: "PageID",
                    template: function (data) { return data.PageName; },
                    title: "页面名称",
                    width: 300
                }, {
                    attributes: { title: "#: Selector === null ? '' : Selector #" },
                    field: "Selector",
                    title: "选择器",
                    width: 500
                }, {
                    attributes: { title: "#: Remark === null ? '' : Remark #" },
                    field: "Remark",
                    title: "描述",
                    width: 500
                }, {}],
            dataBound: function (e) { system.common.checkPageElementAuthorized("1773da2c-3785-47e0-8a87-fb644bd744fd"); },
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
            save: savingData,
            sortable: false
        });
    }

    /**
     * @description 页面编辑器
     * @param {Object} container 容器
     * @param {Object} options 参数
     */
    function pageEditor(container, options) {
        $("<input required name=\"" + options.field + "\"/>").appendTo(container).kendoDropDownList({
            autoBind: true,
            dataSource: _pageDataSource,
            dataTextField: "Name",
            dataValueField: "PageID",
            noDataTemplate: "没有数据"
        });
    }

    /********************* UI end **********************/

    /********************* Event begin **********************/

    /**
     * @description 新增行
     */
    $("#elementToolbar").on("click", ".element_add_row.authorized", function () {
        $("#elementData").data("kendoGrid").addRow();
    });

    /**
     * @description 保存修改
     */
    $("#elementToolbar").on("click", ".element_save_change.authorized", saveChanges);

    /**
     * @description 取消修改
     */
    $("#elementToolbar").on("click", ".element_cancel_change.authorized", function () {
        $("#elementData").data("kendoGrid").cancelChanges();
    });

    /**
     * @description 查询数据
     */
    $("#elementToolbar").on("keypress", ".element_condition", function (e) {
        if ((e.keyCode === 13) && (e.shiftKey === false) && (e.ctrlKey === false) && (e.altKey === false)) {
            refreshData();
        }
    });

    /**
     * @description 刷新数据
     */
    $("#elementToolbar").on("click", ".element_refresh_data", refreshData);

    /**
     * @description 切换状态
     */
    $("#elementData").on("click", ".switch_state.authorized", function () {
        var elementID = $(this).attr("elementID");
        var data = { ElementID: elementID };
        $(this).toggleClass("disabled");
        if ($(this).hasClass("disabled")) {
            data.IsValid = false;
        } else {
            data.IsValid = true;
        }
        swicthElementState(data);
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
                        PageID: { editable: true, nullable: false, type: "string" },
                        PageName: { editable: false, nullable: false, type: "string" },
                        Selector: { editable: true, nullable: false, type: "string" },
                        IsValid: { editable: false, nullable: true, type: "bool" },
                        Remark: { editable: true, nullable: true, type: "string" }
                    },
                    id: "ElementID"
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
                        query.Param = { Condition: $(".element_condition").val() };
                        return JSON.stringify(query);
                    }
                },
                read: {
                    contentType: "application/json; charset=utf-8",
                    crossDomain: true,
                    dataType: "json",
                    type: "post",
                    url: system.api.systemdata.element.getElements,
                    xhrFields: {
                        withCredentials: true
                    }
                }
            }
        });
        _pageDataSource = new kendo.data.DataSource({
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
            requestEnd: function (e) {
            },
            requestStart: function (e) {
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
                        PageID: { type: "string" },
                        Name: { type: "string" }
                    },
                    id: "PageID"
                }
            },
            serverPaging: false,
            serverSorting: false,
            serverFiltering: false,
            transport: {
                parameterMap: function (data, type) {
                },
                read: {
                    contentType: "application/json; charset=utf-8",
                    crossDomain: true,
                    dataType: "json",
                    type: "post",
                    url: system.api.systemdata.element.getPages,
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
                $("#elementData").data("kendoGrid").removeRow($(e.target).parent().parent());
                layer.close(index);
            }
        });
        return false;
    }

    /**
     * @description 切换页面启用状态
     * @param {Object} element 页面控件
     */
    function swicthElementState(element) {
        if (_doing === true) {
            layer.open({
                btn: "确定",
                content: "请等待当前操作完成。",
                icon: 0
            });
            return;
        }
        _doing = true;
        kendo.ui.progress($("#elementData"), true);
        system.ajax.post(system.api.systemdata.element.updateElementState, element, function (data, status, xhr) {
            _doing = false;
            kendo.ui.progress($("#elementData"), false);
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
            kendo.ui.progress($("#elementData"), false);
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
        kendo.ui.progress($("#elementData"), true);
        var counter = { value: 0, count: 2, time: 200 };
        var upsertData = Array.where($("#elementData").data("kendoGrid").dataSource.data(), "dirty", "=", true);
        var removeData = $("#elementData").data("kendoGrid").dataSource.destroyed();
        system.ajax.post(system.api.systemdata.element.deleteElements, removeData, function (data, status, xhr) {
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
            system.ajax.post(system.api.systemdata.element.upsertElements, upsertData, function (data, status, xhr) {
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
            kendo.ui.progress($("#elementData"), false);
            if (result === 1) {
                refreshData();
            }
        });
    }

    /**
     * @description 保存数据时
     * @param {Object} e 事件
     */
    function savingData(e) {
        if (e.container.find("input[name=PageID]").length > 0) {
            e.model.PageName = e.container.find("input[name=PageID]").data("kendoDropDownList").text();
        }
    }

    /********************* Business end **********************/
}(window.systemdata = window.systemdata || {}, window.systemdata.element = {}));