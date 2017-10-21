(function (window, p_undefined) {
    var
        st_version = "1.0",
        st_jq = p_undefined,
        _slstool = function (jq) {
            return new _slstool.fn.init(selector, context, rootjQuery);
        }
        ;

    _slstool.fn = _slstool.prototype = {

        // 版本信息
        varsion: st_version,

        constructor: _slstool,

        init: function (jq) {
            st_jq = jq;
        }
    };

    // Give the init function the jQuery prototype for later instantiation
    _slstool.fn.init.prototype = _slstool.fn;

    // 扩展的方法。把输入对象的信息扩展（或克隆）到目标中，当参数仅有一个对象时，则是对该类型的扩展
    _slstool.extend = _slstool.fn.extend = function () {
        var src, copyIsArray, copy, name, options, clone,
            target = arguments[0] || {}, // 被扩展的对象
            i = 1,
            length = arguments.length, // 参数的长度
            deep = false; // 是否深度遍历以进行扩展（否，则进行浅度扩展）
        
        // 通过第一个参数是否为布尔值，来判断是否进行深度遍历来进行扩展。
        if (typeof target === "boolean") {
            deep = target;
            target = arguments[1] || {};
            // skip the boolean and the target
            i = 2;
        }
        
        // 当目标是字符串或是一些可能处于深度拷贝中的对象，则重建对象
        // 判断为既不是一个实体也不是一个函数
        if (typeof target !== "object" && !jQuery.isFunction(target)) {
            target = {};
        }
        
        // 如果只传递一个参数, 则扩展 slstool 本身
        if (length === i) {
            target = this;
            --i;
        }

        for (; i < length; i++) {
            if ((options = arguments[i]) != null) { // 仅处理非空（未定义）的值
                for (name in options) {
                    src = target[name];
                    copy = options[name];

                    // 防止死循环（要扩展的属性为自身）
                    if (target === copy) {
                        continue;
                    }

                    // Recurse if we're merging plain objects or arrays
                    if (deep && copy && (jQuery.isPlainObject(copy) || (copyIsArray = jQuery.isArray(copy)))) {
                        if (copyIsArray) {
                            copyIsArray = false;
                            clone = src && jQuery.isArray(src) ? src : [];

                        } else {
                            clone = src && jQuery.isPlainObject(src) ? src : {};
                        }

                        // Never move original objects, clone them
                        target[name] = jQuery.extend(deep, clone, copy);

                        // Don't bring in undefined values
                    } else if (copy !== undefined) {
                        target[name] = copy;
                    }
                }
            }
        }

        // Return the modified object
        return target;
    };

    window.SlsTool = _slstool;

})(window)