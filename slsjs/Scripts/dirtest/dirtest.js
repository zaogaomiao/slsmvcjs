(function (window) {
    $(document).ready(function () {
        // 绑定模板
        var tps = $("#vue-template-1").children();
        var dir_methods = {
            "my-dirline": {
                // 使用ajax加载目录信息
                load_dir: function (dir, item, event) {
                    if (!item.children) {
                        $.ajax({
                            url: "/VueTest/DirTestGet",
                            context: item,
                            data: {
                                dir: dir
                            },
                            dataType: "json",
                            method: "post",
                            beforeSend: function () {
                                item.isloading = true;
                            },
                            success: function (data, textStatus, jqXHR) {
                                item.isloading = false;
                                Vue.set(this, "children", data.children);
                                //var r = JSON.stringify(data)
                                //alert(r);
                            }
                        });
                    }
                }
            }
        };
        tps.each(function (idx) {
            var jq_t = $(this);
            var t_name = jq_t.data("templateName");
            var t_props = jq_t.data("templateProps").split("|");
            var t_html = jq_t.html();
            Vue.component(t_name, {
                props: t_props,
                template: t_html,
                methods: dir_methods[t_name]
            });
        });

        // 绑定数据
        var vm = new Vue({
            el: "#file-list",
            data: window.mydata.root_dir
        });
    });
})(window);