(function (parent, welcome) {
    /**
     * @description 备用颜色
     */
    var _gridColors = ["back_blue", "back_green", "back_orange", "back_purple", "back_red"];

    /**
     * @description 备用颜色
     */
    var _gridSize = ["small", "medium", "large"];

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
        getUserAuthorities();
    }

    /**
     * @description 加载菜单
     */
    function loadMenu() {
        $.getJSON("../json/menu.json", function (data) {
            data = Array.orderBy(getMenus(data), "order");
            for (var i = 0; i < data.length; i++) {
                var grid = $("#welcomeTemplate .welcome_menu_grid").clone();
                ////随机选择一个颜色
                //var color = Math.ceil(Math.random() * _gridColors.length);
                //grid.addClass(_gridColors[color - 1]);
                ////随机选择一个尺寸
                //var size = Math.ceil(Math.random() * _gridSize.length);
                //grid.addClass(_gridSize[size - 1]);
                if (data[i].authorized === true) {
                    grid.addClass(data[i].cssClass);
                } else {
                    grid.addClass("back_gray");
                }
                grid.attr("url", data[i].attr.url);
                $(".welcome_menu_grid_icon", grid).addClass(data[i].icon);
                $(".welcome_menu_grid_title", grid).text(data[i].text);
                $(".welcome_menu_grid_title", grid).attr("title", data[i].text);
                $(".welcome_menu_grid_description", grid).html(data[i].description);
                $(".welcome_menu_container").append(grid);
            }
        });
    }

    /********************* UI end **********************/

    /********************* Event begin **********************/

    /********************* Event end **********************/

    /********************* Business begin **********************/

    /**
     * @description 获取用户权限
     */
    function getUserAuthorities() {
        if (window.userAuthority) {
            loadMenu();
        } else {
            //等待home页获取用户权限
            setTimeout(getUserAuthorities, 500);
        }
    }

    /**
     * @description 获取有链接的菜单
     * @param {Array} data 待筛选的菜单
     * @returns {Array} 有链接的菜单
     */
    function getMenus(data) {
        var menus = [];
        if (data.constructor !== Array) {
            data = [data];
        }
        for (var i = 0; i < data.length; i++) {
            if (data[i].id) {
                //有ID则为页面链接
                for (var j = 0; j < window.userAuthority.PageList.length; j++) {
                    if (data[i].id.toLowerCase() === window.userAuthority.PageList[j].PageID.toLowerCase()) {
                        data[i].cssClass += " authorized";
                        data[i].authorized = true;
                    }
                }
                menus.push(data[i]);
            } else if ((data[i].items != undefined) && (data[i].items.length > 0)) {
                menus.push.apply(menus, getMenus(data[i].items));
            }
        }
        return menus;
    }

    /********************* Business end **********************/
}(window, window.welcome = {}));