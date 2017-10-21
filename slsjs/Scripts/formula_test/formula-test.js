
(function () {
    var formula_factory = {
        "base": { // 基本结构，不表示什么内容
            title: "基本",
            operation: "",
            struct: {
                direction: "none",
                frame: "div",
                count: "0"
            },
            result: function () { return undefined; } // 这个不应当返回一个确定的值
        },
        "value": { // 数值
            title: "数值",
            operator: "=",
            struct: {
                direction: "none",
                frame: "div",
                count: "1"
            },
            result: function () { return this.value; }
        },
        "argu": { // arguments，变量
            title: "变量",
            operator: "=",
            struct: {
                direction: "none",
                frame: "div",
                count: "1"
            },
            result: function () {
                var argu_list = []; // todo: 找到公有变量列表
                return argu_list[this.name]; // 通过变量名获取变量值
            }
        },
        "add": { // Addition，加法
            title: "加法",
            operator: "+",
            struct: {
                direction: "h", // 算式表达的方向
                frame: "table", // 构建时使用的框架
                count: "-1" // 算式子的数量 single double
            },
            result: function () { // this全指向当前算式对象
                var sum = 0;
                $.each(this.argus, function (idx, v) { sum += formula_getResult(v, 0); });
                return sum;
            }
        },
        "mul": { // multiplication，乘法
            title: "乘法",
            operator: "*",
            struct: {
                direction: "h",
                frame: "table",
                count: "-1"
            },
            result: function () {
                var m = 1;
                $.each(this.argus, function (idx, v) { m *= formula_getResult(v, 1); });
                return m;
            }
        }
    };
    // 类型列表
    var formula_types = $.map(formula_factory, function (value, key) {
        return {
            key: key,
            title: value.title
        };
    });
    // 节点的基本信息(构造)
    var formula_node = function (fdata) {
        if (fdata) {
            return {
                ftype: fdata.ftype || "base",
                value: fdata.value || undefined,
                name: fdata.name || "",
                argus: fdata.argus && $.map(fdata.argus, function (value, key) { return new formula_node(value); })
            };
        }
        else {
            return {
                ftype: "base",
                value: undefined,
                name: "",
                argus: []
            };
        }
    };

    // 获取结果
    // emptyVal: 结果为空时，以该值作为结果替换
    var formula_getResult = function (fnode, emptyVal) {
        if (!fnode.factory) fnode.factory = formula_factory[fnode.ftype];
        return fnode.factory.result.call(fnode) || emptyVal;
    };
    //
    //var formula_data = @(Html.Raw(Model.FormulaData));
    var fdata = {
        ftype: "value",
        value: 0,
        argus: []
    };
    //fdata = new formula_node();
    //
    // 绑定模板
    var tps = $("#formula_template").children();
    
    // 上下文
    var formulaContext = {
        edit_node: undefined, // 最近一个进行编辑的对象
        set_edit_node: function (node) {
            if (this.edit_node) {
                this.edit_node.isEdit = false;
            }
            this.edit_node = node;
            node.isEdit = true;
        }
    };
    // 全局对象
    var formulaGlobal = {
        factory: formula_factory,
        types: formula_types
    };
    Vue.component("formula-item", {
        template: "#formula-item",
        props: ["item"],
        data: function () {
            return {
                context: formulaContext,
                global: formulaGlobal,
                isEdit: false,
                isHover: false
            }
        },
        computed: {
            factory: function () { return this.global.factory[this.item.ftype] || this.global.factory["base"]; },
            // 获取对应对应算式类型
            result: function () { return this.factory.result; },
            // 获取运算符号
            operator: function () { return this.factory.operator; },
            // 获取算式格式
            struct: function () { return this.factory.struct; }
        },
        methods: {
            // 设置编辑器
            set_edit: function () {
                this.context.set_edit_node(this);
                //this.$emit("set-formula-edit", this);
            }
        }
    });
    Vue.component("formula-editor", {
        template: "#formula-editor",
        props: ["item"],
        data: function () {
            return {
                global: formulaGlobal
            }
        },
        computed: {
            factory: function () { return this.global.factory[this.item.ftype] || this.global.factory["base"]; },
            // 获取对应对应算式类型
            result: function () { return this.factory.result; },
            // 获取运算符号
            operator: function () { return this.factory.operator; },
            // 获取算式格式
            struct: function () { return this.factory.struct; },
            types: function () { return this.global.types; }
        },
        methods: {
            // 添加一个参数
            argu_add: function () {
                if (!!this.item.argus) Vue.set(this.item, "argus", []);
                this.item.argus.push(new formula_node());
            }
        }
    })
    //
    // 绑定数据
    var vm = new Vue({
        el: "#formula-root",
        data: {
            context: formulaContext,
            argument: [],
            formula: fdata,
            reading: false,
            saving: false
        },
        computed: {
            edit_item: function () { return this.context.edit_node && this.context.edit_node.item; }
        },
        methods: {
            fml_read: function () {
                if (this.reading) return;
                $.ajax({
                    url: "/VueTest/FmRead",
                    context: this,
                    data: {
                    },
                    dataType: "json",
                    method: "post",
                    beforeSend: function () {
                        this.reading = true;
                    },
                    success: function (data, textStatus, jqXHR) {
                        //alert(data);
                        //this.formula = data;
                        Vue.set(this, "formula", data);
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        alert("error:\n textStatus:" + textStatus + "\n errorThrown:" + jqXHR);
                    },
                    complete: function (jqXHR, textStatus) {
                        this.reading = false;
                    }
                });
            },
            fml_save: function () {
                var f = new formula_node(this.formula);
                $.ajax({
                    url: "/VueTest/FmSave",
                    context: this,
                    data: {
                        fvalue: JSON.stringify(f)
                    },
                    dataType: "text",
                    method: "post",
                    beforeSend: function () {
                        this.saving = true;
                    },
                    success: function (data, textStatus, jqXHR) {
                        alert(data);
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        alert("error:\n textStatus:" + textStatus + "\n errorThrown:" + jqXHR);
                    },
                    complete: function (jqXHR, textStatus) {
                        this.saving = false;
                    }
                });
            }
        }
    });
})();