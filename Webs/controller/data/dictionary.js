(function (parent, dictionary) {
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
        if (system.common.checkPageAuthorized("4475c37f-01c8-4df1-b4fe-02a8b90d6e3f") === false) {
            window.location.replace("../unauthorized.html");
        }
        initDataSource();
        initUI();
        system.common.checkPageElementAuthorized("4475c37f-01c8-4df1-b4fe-02a8b90d6e3f");
    });

    /********************* UI begin **********************/

    /**
     * @description 初始化UI
     */
    function initUI() {
        kendo.culture("zh-CN");
        $("#dictToolbar").kendoToolBar({
            items: [
                { attributes: { "class": "dict_add_row k-button operator editor unauthorized" }, template: "<span class=\"pmfont pm-xinzeng\">新增</span>" },
                { attributes: { "class": "dict_save_change k-button operator editor unauthorized" }, template: "<span class=\"pmfont pm-baocun\">保存</span>" },
                { attributes: { "class": "dict_cancel_change k-button operator editor unauthorized" }, template: "<span class=\"pmfont pm-jinzhi\">取消</span>" },
                { attributes: { "class": "toolbar_placeholder operator editor unauthorized" }, type: "separator" },
                { template: "<input class=\"dict_condition\" placeholder=\"类型/代码/显示名/值\" />" },
                { attributes: { "class": "dict_refresh_data k-button" }, template: "<span class=\"pmfont pm-chaxun\">查询</span>" }
            ]
        });
        $("#dictData").kendoGrid({
            columns: [
                {
                    command: [{
                        className: "pmfont pm-guanbi operator delete unauthorized", click: deleteItem, name: "deleteItem", text: "删除",
                        visible: function (data) { return String.isNullOrEmpty(data.DictID) || (data.Editable === true); }
                    }],
                    title: "操作",
                    width: common.math.plus(common.math.multiply(common.math.plus(2, common.math.multiply(5.9, 1)), parseFloat($("body").css("fontSize"))), 1)
                }, {
                    attributes: { title: "#: DataType === null ? '' : DataType #" },
                    editable: function (data) { return String.isNullOrEmpty(data.DictID) || (data.Editable === true); },
                    field: "DataType",
                    title: "类型",
                    width: 200
                }, {
                    attributes: { title: "#: Code === null ? '' : Code #" },
                    editable: function (data) { return String.isNullOrEmpty(data.DictID) || (data.Editable === true); },
                    field: "Code",
                    title: "代码",
                    width: 200
                }, {
                    attributes: { title: "#: Name === null ? '' : Name #" },
                    editable: function (data) { return String.isNullOrEmpty(data.DictID) || (data.Editable === true); },
                    field: "Name",
                    title: "显示名",
                    width: 150
                }, {
                    attributes: { title: "#: Value === null ? '' : Value #" },
                    editable: function (data) { return String.isNullOrEmpty(data.DictID) || (data.Editable === true); },
                    field: "Value",
                    title: "值",
                    width: 150
                }, {
                    attributes: { title: "#: ExtValue1 === null ? '' : ExtValue1 #" },
                    editable: function (data) { return String.isNullOrEmpty(data.DictID) || (data.Editable === true); },
                    field: "ExtValue1",
                    title: "扩展值1",
                    width: 220
                }, {
                    attributes: { title: "#: ExtValue2 === null ? '' : ExtValue2 #" },
                    editable: function (data) { return String.isNullOrEmpty(data.DictID) || (data.Editable === true); },
                    field: "ExtValue2",
                    title: "扩展值2",
                    width: 220
                }, {
                    attributes: { title: "#: ExtValue3 === null ? '' : ExtValue3 #" },
                    editable: function (data) { return String.isNullOrEmpty(data.DictID) || (data.Editable === true); },
                    field: "ExtValue3",
                    title: "扩展值3",
                    width: 220
                }, {
                    attributes: { title: "#: Remark === null ? '' : Remark #" },
                    editable: function (data) { return String.isNullOrEmpty(data.DictID) || (data.Editable === true); },
                    field: "Remark",
                    title: "备注",
                    width: 200
                }, {}],
            dataBound: function (e) { system.common.checkPageElementAuthorized("4475c37f-01c8-4df1-b4fe-02a8b90d6e3f"); },
            dataSource: _dataSource,
            editable: {
                mode: "incell"
            },
            groupable: false,
            navigatable: true,
            pageable: {
                buttonCount: 5,
                messages: {
                    display: "{0} - {1} 行 共 {2} 行",
                    empty: "没有数据",
                    itemsPerPage: "行 / 页",
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
    $("#dictToolbar").on("click", ".dict_add_row.authorized", function () {
        $("#dictData").data("kendoGrid").addRow();
    });

    /**
     * @description 保存修改
     */
    $("#dictToolbar").on("click", ".dict_save_change.authorized", saveChanges);

    /**
     * @description 取消修改
     */
    $("#dictToolbar").on("click", ".dict_cancel_change.authorized", function () {
        $("#dictData").data("kendoGrid").cancelChanges();
    });

    /**
     * @description 查询数据
     */
    $("#dictToolbar").on("keypress", ".dict_condition", function (e) {
        if ((e.keyCode === 13) && (e.shiftKey === false) && (e.ctrlKey === false) && (e.altKey === false)) {
            refreshData();
        }
    });

    /**
     * @description 刷新数据
     */
    $("#dictToolbar").on("click", ".dict_refresh_data", refreshData);

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
                        DataType: { nullable: false, type: "string", validation: { maxlength: 50, required: true, title: "请输入类型。" } },
                        Code: { nullable: false, type: "string", validation: { maxlength: 50, required: true, title: "请输入代码。" } },
                        Name: { nullable: false, type: "string", validation: { maxlength: 100, required: true, title: "请输入显示名。" } },
                        Value: { nullable: true, type: "string", validation: { maxlength: 100 } },
                        ExtValue1: { nullable: true, type: "string", validation: {} },
                        ExtValue2: { nullable: true, type: "string", validation: {} },
                        ExtValue3: { nullable: true, type: "string", validation: {} },
                        Remark: { nullable: true, type: "string", validation: { maxlength: 100 } }
                    },
                    id: "DictID"
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
            serverPaging: true,
            serverSorting: true,
            serverFiltering: true,
            transport: {
                parameterMap: function (data, type) {
                    if (type === "read") {
                        var query = system.api.query;
                        query.Param = { Condition: $(".dict_condition").val() };
                        query.Skip = data.page;
                        query.Size = data.pageSize;
                        return JSON.stringify(query);
                    }
                },
                read: {
                    contentType: "application/json; charset=utf-8",
                    crossDomain: true,
                    dataType: "json",
                    type: "post",
                    url: system.api.data.dictionary.getPagedDictionaries,
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
                $("#dictData").data("kendoGrid").removeRow($(e.target).parent().parent());
                layer.close(index);
            }
        });
        return false;
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
        kendo.ui.progress($("#dictData"), true);
        var counter = { value: 0, count: 2, time: 200 };
        var upsertData = Array.where($("#dictData").data("kendoGrid").dataSource.data(), "dirty", "=", true);
        var removeData = $("#dictData").data("kendoGrid").dataSource.destroyed();
        system.ajax.post(system.api.data.dictionary.deleteDictionaries, removeData, function (data, status, xhr) {
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
            system.ajax.post(system.api.data.dictionary.upsertDictionaries, upsertData, function (data, status, xhr) {
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
            kendo.ui.progress($("#dictData"), false);
            if (result === 1) {
                refreshData();
            }
        });
    }

    /********************* Business end **********************/
}(window.data = window.data || {}, window.data.dictionary = {}));