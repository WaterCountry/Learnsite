/*! Built with IMPACT - impactjs.com */

(function(window) {
    "use strict";
    Number.prototype.map = function(istart, istop, ostart, ostop) {
        return ostart + (ostop - ostart) * ((this - istart) / (istop - istart));
    }
    ;
    Number.prototype.limit = function(min, max) {
        return Math.min(max, Math.max(min, this));
    }
    ;
    Number.prototype.round = function(precision) {
        precision = Math.pow(10, precision || 0);
        return Math.round(this * precision) / precision;
    }
    ;
    Number.prototype.floor = function() {
        return Math.floor(this);
    }
    ;
    Number.prototype.ceil = function() {
        return Math.ceil(this);
    }
    ;
    Number.prototype.toInt = function() {
        return ( this | 0) ;
    }
    ;
    Number.prototype.toRad = function() {
        return (this / 180) * Math.PI;
    }
    ;
    Number.prototype.toDeg = function() {
        return (this * 180) / Math.PI;
    }
    ;
    Object.defineProperty(Array.prototype, 'erase', {
        value: function(item) {
            for (var i = this.length; i--; ) {
                if (this[i] === item) {
                    this.splice(i, 1);
                }
            }
            return this;
        }
    });
    Object.defineProperty(Array.prototype, 'random', {
        value: function(item) {
            return this[Math.floor(Math.random() * this.length)];
        }
    });
    Function.prototype.bind = Function.prototype.bind || function(oThis) {
        if (typeof this !== "function") {
            throw new TypeError("Function.prototype.bind - what is trying to be bound is not callable");
        }
        var aArgs = Array.prototype.slice.call(arguments, 1)
          , fToBind = this
          , fNOP = function() {}
          , fBound = function() {
            return fToBind.apply((this instanceof fNOP && oThis ? this : oThis), aArgs.concat(Array.prototype.slice.call(arguments)));
        }
        ;
        fNOP.prototype = this.prototype;
        fBound.prototype = new fNOP();
        return fBound;
    }
    ;
    window.ig = {
        game: null ,
        debug: null ,
        version: '1.24',
        global: window,
        modules: {},
        resources: [],
        ready: false,
        baked: false,
        nocache: '',
        ua: {},
        prefix: (window.ImpactPrefix || ''),
        lib: 'lib/',
        _current: null ,
        _loadQueue: [],
        _waitForOnload: 0,
        $: function(selector) {
            return selector.charAt(0) == '#' ? document.getElementById(selector.substr(1)) : document.getElementsByTagName(selector);
        },
        $new: function(name) {
            return document.createElement(name);
        },
        copy: function(object) {
            if (!object || typeof (object) != 'object' || object instanceof HTMLElement || object instanceof ig.Class) {
                return object;
            } 
            else if (object instanceof Array) {
                var c = [];
                for (var i = 0, l = object.length; i < l; i++) {
                    c[i] = ig.copy(object[i]);
                }
                return c;
            } 
            else {
                var c = {};
                for (var i in object) {
                    c[i] = ig.copy(object[i]);
                }
                return c;
            }
        },
        merge: function(original, extended) {
            for (var key in extended) {
                var ext = extended[key];
                if (typeof (ext) != 'object' || ext instanceof HTMLElement || ext instanceof ig.Class || ext === null ) {
                    original[key] = ext;
                } 
                else {
                    if (!original[key] || typeof (original[key]) != 'object') {
                        original[key] = (ext instanceof Array) ? [] : {};
                    }
                    ig.merge(original[key], ext);
                }
            }
            return original;
        },
        ksort: function(obj) {
            if (!obj || typeof (obj) != 'object') {
                return [];
            }
            var keys = []
              , values = [];
            for (var i in obj) {
                keys.push(i);
            }
            keys.sort();
            for (var i = 0; i < keys.length; i++) {
                values.push(obj[keys[i]]);
            }
            return values;
        },
        setVendorAttribute: function(el, attr, val) {
            var uc = attr.charAt(0).toUpperCase() + attr.substr(1);
            el[attr] = el['ms' + uc] = el['moz' + uc] = el['webkit' + uc] = el['o' + uc] = val;
        },
        getVendorAttribute: function(el, attr) {
            var uc = attr.charAt(0).toUpperCase() + attr.substr(1);
            return el[attr] || el['ms' + uc] || el['moz' + uc] || el['webkit' + uc] || el['o' + uc];
        },
        normalizeVendorAttribute: function(el, attr) {
            var prefixedVal = ig.getVendorAttribute(el, attr);
            if (!el[attr] && prefixedVal) {
                el[attr] = prefixedVal;
            }
        },
        getImagePixels: function(image, x, y, width, height) {
            var canvas = ig.$new('canvas');
            canvas.width = image.width;
            canvas.height = image.height;
            var ctx = canvas.getContext('2d');
            ig.System.SCALE.CRISP(canvas, ctx);
            var ratio = ig.getVendorAttribute(ctx, 'backingStorePixelRatio') || 1;
            ig.normalizeVendorAttribute(ctx, 'getImageDataHD');
            var realWidth = image.width / ratio
              , realHeight = image.height / ratio;
            canvas.width = Math.ceil(realWidth);
            canvas.height = Math.ceil(realHeight);
            ctx.drawImage(image, 0, 0, realWidth, realHeight);
            return (ratio === 1) ? ctx.getImageData(x, y, width, height) : ctx.getImageDataHD(x, y, width, height);
        },
        module: function(name) {
            if (ig._current) {
                throw ("Module '" + ig._current.name + "' defines nothing");
            }
            if (ig.modules[name] && ig.modules[name].body) {
                throw ("Module '" + name + "' is already defined");
            }
            ig._current = {
                name: name,
                requires: [],
                loaded: false,
                body: null 
            };
            ig.modules[name] = ig._current;
            ig._loadQueue.push(ig._current);
            return ig;
        },
        requires: function() {
            ig._current.requires = Array.prototype.slice.call(arguments);
            return ig;
        },
        defines: function(body) {
            ig._current.body = body;
            ig._current = null ;
            ig._initDOMReady();
        },
        addResource: function(resource) {
            ig.resources.push(resource);
        },
        setNocache: function(set) {
            ig.nocache = set ? '?' + Date.now() : '';
        },
        log: function() {},
        assert: function(condition, msg) {},
        show: function(name, number) {},
        mark: function(msg, color) {},
        _loadScript: function(name, requiredFrom) {
            ig.modules[name] = {
                name: name,
                requires: [],
                loaded: false,
                body: null 
            };
            ig._waitForOnload++;
            var path = ig.prefix + ig.lib + name.replace(/\./g, '/') + '.js' + ig.nocache;
            var script = ig.$new('script');
            script.type = 'text/javascript';
            script.src = path;
            script.onload = function() {
                ig._waitForOnload--;
                ig._execModules();
            }
            ;
            script.onerror = function() {
                throw ('Failed to load module ' + name + ' at ' + path + ' ' + 'required from ' + requiredFrom);
            }
            ;
            ig.$('head')[0].appendChild(script);
        },
        _execModules: function() {
            var modulesLoaded = false;
            for (var i = 0; i < ig._loadQueue.length; i++) {
                var m = ig._loadQueue[i];
                var dependenciesLoaded = true;
                for (var j = 0; j < m.requires.length; j++) {
                    var name = m.requires[j];
                    if (!ig.modules[name]) {
                        dependenciesLoaded = false;
                        ig._loadScript(name, m.name);
                    } 
                    else if (!ig.modules[name].loaded) {
                        dependenciesLoaded = false;
                    }
                }
                if (dependenciesLoaded && m.body) {
                    ig._loadQueue.splice(i, 1);
                    m.loaded = true;
                    m.body();
                    modulesLoaded = true;
                    i--;
                }
            }
            if (modulesLoaded) {
                ig._execModules();
            } 
            else if (!ig.baked && ig._waitForOnload == 0 && ig._loadQueue.length != 0) {
                var unresolved = [];
                for (var i = 0; i < ig._loadQueue.length; i++) {
                    var unloaded = [];
                    var requires = ig._loadQueue[i].requires;
                    for (var j = 0; j < requires.length; j++) {
                        var m = ig.modules[requires[j]];
                        if (!m || !m.loaded) {
                            unloaded.push(requires[j]);
                        }
                    }
                    unresolved.push(ig._loadQueue[i].name + ' (requires: ' + unloaded.join(', ') + ')');
                }
                throw ("Unresolved (or circular?) dependencies. " + "Most likely there's a name/path mismatch for one of the listed modules " + "or a previous syntax error prevents a module from loading:\n" + 
                unresolved.join('\n'));
            }
        },
        _DOMReady: function() {
            if (!ig.modules['dom.ready'].loaded) {
                if (!document.body) {
                    return setTimeout(ig._DOMReady, 13);
                }
                ig.modules['dom.ready'].loaded = true;
                ig._waitForOnload--;
                ig._execModules();
            }
            return 0;
        },
        _boot: function() {
            if (document.location.href.match(/\?nocache/)) {
                ig.setNocache(true);
            }
            ig.ua.pixelRatio = window.devicePixelRatio || 1;
            ig.ua.viewport = {
                width: window.innerWidth,
                height: window.innerHeight
            };
            ig.ua.screen = {
                width: window.screen.availWidth * ig.ua.pixelRatio,
                height: window.screen.availHeight * ig.ua.pixelRatio
            };
            ig.ua.iPhone = /iPhone|iPod/i.test(navigator.userAgent);
            ig.ua.iPhone4 = (ig.ua.iPhone && ig.ua.pixelRatio == 2);
            ig.ua.iPad = /iPad/i.test(navigator.userAgent);
            ig.ua.android = /android/i.test(navigator.userAgent);
            ig.ua.winPhone = /Windows Phone/i.test(navigator.userAgent);
            ig.ua.iOS = ig.ua.iPhone || ig.ua.iPad;
            ig.ua.mobile = ig.ua.iOS || ig.ua.android || ig.ua.winPhone || /mobile/i.test(navigator.userAgent);
            ig.ua.touchDevice = (('ontouchstart' in window) || (window.navigator.msMaxTouchPoints));
        },
        _initDOMReady: function() {
            if (ig.modules['dom.ready']) {
                ig._execModules();
                return;
            }
            ig._boot();
            ig.modules['dom.ready'] = {
                requires: [],
                loaded: false,
                body: null 
            };
            ig._waitForOnload++;
            if (document.readyState === 'complete') {
                ig._DOMReady();
            } 
            else {
                document.addEventListener('DOMContentLoaded', ig._DOMReady, false);
                window.addEventListener('load', ig._DOMReady, false);
            }
        }
    };
    ig.normalizeVendorAttribute(window, 'requestAnimationFrame');
    if (window.requestAnimationFrame) {
        var next = 1
          , anims = {};
        window.ig.setAnimation = function(callback, element) {
            var current = next++;
            anims[current] = true;
            var animate = function() {
                if (!anims[current]) {
                    return;
                }
                window.requestAnimationFrame(animate, element);
                callback();
            }
            ;
            window.requestAnimationFrame(animate, element);
            return current;
        }
        ;
        window.ig.clearAnimation = function(id) {
            delete anims[id];
        }
        ;
    } 
    else {
        window.ig.setAnimation = function(callback, element) {
            return window.setInterval(callback, 1000 / 60);
        }
        ;
        window.ig.clearAnimation = function(id) {
            window.clearInterval(id);
        }
        ;
    }
    var initializing = false
      , fnTest = /xyz/.test(function() {
        xyz;
    }) ? /\bparent\b/ : /.*/;
    var lastClassId = 0;
    window.ig.Class = function() {}
    ;
    var inject = function(prop) {
        var proto = this.prototype;
        var parent = {};
        for (var name in prop) {
            if (typeof (prop[name]) == "function" && typeof (proto[name]) == "function" && fnTest.test(prop[name])) {
                parent[name] = proto[name];
                proto[name] = (function(name, fn) {
                    return function() {
                        var tmp = this.parent;
                        this.parent = parent[name];
                        var ret = fn.apply(this, arguments);
                        this.parent = tmp;
                        return ret;
                    }
                    ;
                })(name, prop[name]);
            } 
            else {
                proto[name] = prop[name];
            }
        }
    }
    ;
    window.ig.Class.extend = function(prop) {
        var parent = this.prototype;
        initializing = true;
        var prototype = new this();
        initializing = false;
        for (var name in prop) {
            if (typeof (prop[name]) == "function" && typeof (parent[name]) == "function" && fnTest.test(prop[name])) {
                prototype[name] = (function(name, fn) {
                    return function() {
                        var tmp = this.parent;
                        this.parent = parent[name];
                        var ret = fn.apply(this, arguments);
                        this.parent = tmp;
                        return ret;
                    }
                    ;
                })(name, prop[name]);
            } 
            else {
                prototype[name] = prop[name];
            }
        }
        function Class() {
            if (!initializing) {
                if (this.staticInstantiate) {
                    var obj = this.staticInstantiate.apply(this, arguments);
                    if (obj) {
                        return obj;
                    }
                }
                for (var p in this) {
                    if (typeof (this[p]) == 'object') {
                        this[p] = ig.copy(this[p]);
                    }
                }
                if (this.init) {
                    this.init.apply(this, arguments);
                }
            }
            return this;
        }
        Class.prototype = prototype;
        Class.prototype.constructor = Class;
        Class.extend = window.ig.Class.extend;
        Class.inject = inject;
        Class.classId = prototype.classId = ++lastClassId;
        return Class;
    }
    ;
    if (window.ImpactMixin) {
        ig.merge(ig, window.ImpactMixin);
    }
})(window);

// lib/impact/image.js
ig.baked = true;
ig.module('impact.image').defines(function() {
    "use strict";
    ig.Image = ig.Class.extend({
        data: null ,
        width: 0,
        height: 0,
        loaded: false,
        failed: false,
        loadCallback: null ,
        path: '',
        staticInstantiate: function(path) {
            return ig.Image.cache[path] || null ;
        },
        init: function(path) {
            this.path = path;
            this.load();
        },
        load: function(loadCallback) {
            if (this.loaded) {
                if (loadCallback) {
                    loadCallback(this.path, true);
                }
                return;
            } 
            else if (!this.loaded && ig.ready) {
                this.loadCallback = loadCallback || null ;
                this.data = new Image();
                this.data.onload = this.onload.bind(this);
                this.data.onerror = this.onerror.bind(this);
                this.data.src = ig.prefix + this.path + ig.nocache;
            } 
            else {
                ig.addResource(this);
            }
            ig.Image.cache[this.path] = this;
        },
        reload: function() {
            this.loaded = false;
            this.data = new Image();
            this.data.onload = this.onload.bind(this);
            this.data.src = this.path + '?' + Date.now();
        },
        onload: function(event) {
            this.width = this.data.width;
            this.height = this.data.height;
            this.loaded = true;
            if (ig.system.scale != 1) {
                this.resize(ig.system.scale);
            }
            if (this.loadCallback) {
                this.loadCallback(this.path, true);
            }
        },
        onerror: function(event) {
            this.failed = true;
            if (this.loadCallback) {
                this.loadCallback(this.path, false);
            }
        },
        resize: function(scale) {
            var origPixels = ig.getImagePixels(this.data, 0, 0, this.width, this.height);
            var widthScaled = this.width * scale;
            var heightScaled = this.height * scale;
            var scaled = ig.$new('canvas');
            scaled.width = widthScaled;
            scaled.height = heightScaled;
            var scaledCtx = scaled.getContext('2d');
            var scaledPixels = scaledCtx.getImageData(0, 0, widthScaled, heightScaled);
            for (var y = 0; y < heightScaled; y++) {
                for (var x = 0; x < widthScaled; x++) {
                    var index = (Math.floor(y / scale) * this.width + Math.floor(x / scale)) * 4;
                    var indexScaled = (y * widthScaled + x) * 4;
                    scaledPixels.data[indexScaled] = origPixels.data[index];
                    scaledPixels.data[indexScaled + 1] = origPixels.data[index + 1];
                    scaledPixels.data[indexScaled + 2] = origPixels.data[index + 2];
                    scaledPixels.data[indexScaled + 3] = origPixels.data[index + 3];
                }
            }
            scaledCtx.putImageData(scaledPixels, 0, 0);
            this.data = scaled;
        },
        draw: function(targetX, targetY, sourceX, sourceY, width, height) {
            if (!this.loaded) {
                return;
            }
            var scale = ig.system.scale;
            sourceX = sourceX ? sourceX * scale : 0;
            sourceY = sourceY ? sourceY * scale : 0;
            width = (width ? width : this.width) * scale;
            height = (height ? height : this.height) * scale;
            ig.system.context.drawImage(this.data, sourceX, sourceY, width, height, ig.system.getDrawPos(targetX), ig.system.getDrawPos(targetY), width, height);
            ig.Image.drawCount++;
        },
        drawTile: function(targetX, targetY, tile, tileWidth, tileHeight, flipX, flipY) {
            tileHeight = tileHeight ? tileHeight : tileWidth;
            if (!this.loaded || tileWidth > this.width || tileHeight > this.height) {
                return;
            }
            var scale = ig.system.scale;
            var tileWidthScaled = Math.floor(tileWidth * scale);
            var tileHeightScaled = Math.floor(tileHeight * scale);
            var scaleX = flipX ? -1 : 1;
            var scaleY = flipY ? -1 : 1;
            if (flipX || flipY) {
                ig.system.context.save();
                ig.system.context.scale(scaleX, scaleY);
            }
            ig.system.context.drawImage(this.data, (Math.floor(tile * tileWidth) % this.width) * scale, (Math.floor(tile * tileWidth / this.width) * tileHeight) * scale, tileWidthScaled, tileHeightScaled, ig.system.getDrawPos(targetX) * scaleX - (flipX ? tileWidthScaled : 0), ig.system.getDrawPos(targetY) * scaleY - (flipY ? tileHeightScaled : 0), tileWidthScaled, tileHeightScaled);
            if (flipX || flipY) {
                ig.system.context.restore();
            }
            ig.Image.drawCount++;
        }
    });
    ig.Image.drawCount = 0;
    ig.Image.cache = {};
    ig.Image.reloadCache = function() {
        for (var path in ig.Image.cache) {
            ig.Image.cache[path].reload();
        }
    }
    ;
});

// lib/impact/font.js
ig.baked = true;
ig.module('impact.font').requires('impact.image').defines(function() {
    "use strict";
    ig.Font = ig.Image.extend({
        widthMap: [],
        indices: [],
        firstChar: 32,
        alpha: 1,
        letterSpacing: 1,
        lineSpacing: 0,
        onload: function(ev) {
            this._loadMetrics(this.data);
            this.parent(ev);
        },
        widthForString: function(text) {
            if (text.indexOf('\n') !== -1) {
                var lines = text.split('\n');
                var width = 0;
                for (var i = 0; i < lines.length; i++) {
                    width = Math.max(width, this._widthForLine(lines[i]));
                }
                return width;
            } 
            else {
                return this._widthForLine(text);
            }
        },
        _widthForLine: function(text) {
            var width = 0;
            for (var i = 0; i < text.length; i++) {
                width += this.widthMap[text.charCodeAt(i) - this.firstChar] + this.letterSpacing;
            }
            return width;
        },
        heightForString: function(text) {
            return text.split('\n').length * (this.height + this.lineSpacing);
        },
        draw: function(text, x, y, align) {
            if (typeof (text) != 'string') {
                text = text.toString();
            }
            if (text.indexOf('\n') !== -1) {
                var lines = text.split('\n');
                var lineHeight = this.height + this.lineSpacing;
                for (var i = 0; i < lines.length; i++) {
                    this.draw(lines[i], x, y + i * lineHeight, align);
                }
                return;
            }
            if (align == ig.Font.ALIGN.RIGHT || align == ig.Font.ALIGN.CENTER) {
                var width = this._widthForLine(text);
                x -= align == ig.Font.ALIGN.CENTER ? width / 2 : width;
            }
            if (this.alpha !== 1) {
                ig.system.context.globalAlpha = this.alpha;
            }
            for (var i = 0; i < text.length; i++) {
                var c = text.charCodeAt(i);
                x += this._drawChar(c - this.firstChar, x, y);
            }
            if (this.alpha !== 1) {
                ig.system.context.globalAlpha = 1;
            }
            ig.Image.drawCount += text.length;
        },
        _drawChar: function(c, targetX, targetY) {
            if (!this.loaded || c < 0 || c >= this.indices.length) {
                return 0;
            }
            var scale = ig.system.scale;
            var charX = this.indices[c] * scale;
            var charY = 0;
            var charWidth = this.widthMap[c] * scale;
            var charHeight = (this.height - 2) * scale;
            ig.system.context.drawImage(this.data, charX, charY, charWidth, charHeight, ig.system.getDrawPos(targetX), ig.system.getDrawPos(targetY), charWidth, charHeight);
            return this.widthMap[c] + this.letterSpacing;
        },
        _loadMetrics: function(image) {
            this.height = image.height - 1;
            this.widthMap = [];
            this.indices = [];
            var px = ig.getImagePixels(image, 0, image.height - 1, image.width, 1);
            var currentChar = 0;
            var currentWidth = 0;
            for (var x = 0; x < image.width; x++) {
                var index = x * 4 + 3;
                if (px.data[index] > 127) {
                    currentWidth++;
                } 
                else if (px.data[index] < 128 && currentWidth) {
                    this.widthMap.push(currentWidth);
                    this.indices.push(x - currentWidth);
                    currentChar++;
                    currentWidth = 0;
                }
            }
            this.widthMap.push(currentWidth);
            this.indices.push(x - currentWidth);
        }
    });
    ig.Font.ALIGN = {
        LEFT: 0,
        RIGHT: 1,
        CENTER: 2
    };
});

// lib/impact/sound.js
ig.baked = true;
ig.module('impact.sound').defines(function() {
    "use strict";
    ig.SoundManager = ig.Class.extend({
        clips: {},
        volume: 1,
        format: null ,
        init: function() {
            if (!ig.Sound.enabled || !window.Audio) {
                ig.Sound.enabled = false;
                return;
            }
            var probe = new Audio();
            for (var i = 0; i < ig.Sound.use.length; i++) {
                var format = ig.Sound.use[i];
                if (probe.canPlayType(format.mime)) {
                    this.format = format;
                    break;
                }
            }
            if (!this.format) {
                ig.Sound.enabled = false;
            }
            if (ig.Sound.enabled && ig.Sound.useWebAudio) {
                this.audioContext = new AudioContext();
                this.boundWebAudioUnlock = this.unlockWebAudio.bind(this);
                document.addEventListener('touchend', this.boundWebAudioUnlock, false);
                document.addEventListener('click', this.boundWebAudioUnlock, false);
            }
        },
        unlockWebAudio: function() {
            var buffer = this.audioContext.createBuffer(1, 1, 44100);
            var source = this.audioContext.createBufferSource();
            source.buffer = buffer;
            source.connect(this.audioContext.destination);
            source.start(0);
            document.removeEventListener('touchend', this.boundWebAudioUnlock, false);
            document.removeEventListener('click', this.boundWebAudioUnlock, false);
        },
        load: function(path, multiChannel, loadCallback) {
            if (multiChannel && ig.Sound.useWebAudio) {
                return this.loadWebAudio(path, multiChannel, loadCallback);
            } 
            else {
                return this.loadHTML5Audio(path, multiChannel, loadCallback);
            }
        },
        loadWebAudio: function(path, multiChannel, loadCallback) {
            var realPath = ig.prefix + path.replace(/[^\.]+$/, this.format.ext) + ig.nocache;
            if (this.clips[path]) {
                return this.clips[path];
            }
            var audioSource = new ig.Sound.WebAudioSource()
            this.clips[path] = audioSource;
            var request = new XMLHttpRequest();
            request.open('GET', realPath, true);
            request.responseType = 'arraybuffer';
            var that = this;
            request.onload = function(ev) {
                that.audioContext.decodeAudioData(request.response, function(buffer) {
                    audioSource.buffer = buffer;
                    loadCallback(path, true, ev);
                }, function(ev) {
                    loadCallback(path, false, ev);
                });
            }
            ;
            request.onerror = function(ev) {
                loadCallback(path, false, ev);
            }
            ;
            request.send();
            return audioSource;
        },
        loadHTML5Audio: function(path, multiChannel, loadCallback) {
            var realPath = ig.prefix + path.replace(/[^\.]+$/, this.format.ext) + ig.nocache;
            if (this.clips[path]) {
                if (this.clips[path] instanceof ig.Sound.WebAudioSource) {
                    return this.clips[path];
                }
                if (multiChannel && this.clips[path].length < ig.Sound.channels) {
                    for (var i = this.clips[path].length; i < ig.Sound.channels; i++) {
                        var a = new Audio(realPath);
                        a.load();
                        this.clips[path].push(a);
                    }
                }
                return this.clips[path][0];
            }
            var clip = new Audio(realPath);
            if (loadCallback) {
                if (ig.ua.mobile) {
                    setTimeout(function() {
                        loadCallback(path, true, null );
                    }, 0);
                } 
                else {
                    clip.addEventListener('canplaythrough', function cb(ev) {
                        clip.removeEventListener('canplaythrough', cb, false);
                        loadCallback(path, true, ev);
                    }, false);
                    clip.addEventListener('error', function(ev) {
                        loadCallback(path, true, ev);
                    }, false);
                }
            }
            clip.preload = 'auto';
            clip.load();
            this.clips[path] = [clip];
            if (multiChannel) {
                for (var i = 1; i < ig.Sound.channels; i++) {
                    var a = new Audio(realPath);
                    a.load();
                    this.clips[path].push(a);
                }
            }
            return clip;
        },
        get: function(path) {
            var channels = this.clips[path];
            if (channels && channels instanceof ig.Sound.WebAudioSource) {
                return channels;
            }
            for (var i = 0, clip; clip = channels[i++]; ) {
                if (clip.paused || clip.ended) {
                    if (clip.ended) {
                        clip.currentTime = 0;
                    }
                    return clip;
                }
            }
            channels[0].pause();
            channels[0].currentTime = 0;
            return channels[0];
        }
    });
    ig.Music = ig.Class.extend({
        tracks: [],
        namedTracks: {},
        currentTrack: null ,
        currentIndex: 0,
        random: false,
        _volume: 1,
        _loop: true,
        _fadeInterval: 0,
        _fadeTimer: null ,
        _endedCallbackBound: null ,
        init: function() {
            this._endedCallbackBound = this._endedCallback.bind(this);
            Object.defineProperty(this, "volume", {
                get: this.getVolume.bind(this),
                set: this.setVolume.bind(this)
            });
            Object.defineProperty(this, "loop", {
                get: this.getLooping.bind(this),
                set: this.setLooping.bind(this)
            });
        },
        add: function(music, name) {
            if (!ig.Sound.enabled) {
                return;
            }
            var path = music instanceof ig.Sound ? music.path : music;
            var track = ig.soundManager.load(path, false);
            if (track instanceof ig.Sound.WebAudioSource) {
                ig.system.stopRunLoop();
                throw ("Sound '" + path + "' loaded as Multichannel but used for Music. " + "Set the multiChannel param to false when loading, e.g.: new ig.Sound(path, false)");
            }
            track.loop = this._loop;
            track.volume = this._volume;
            track.addEventListener('ended', this._endedCallbackBound, false);
            this.tracks.push(track);
            if (name) {
                this.namedTracks[name] = track;
            }
            if (!this.currentTrack) {
                this.currentTrack = track;
            }
        },
        next: function() {
            if (!this.tracks.length) {
                return;
            }
            this.stop();
            this.currentIndex = this.random ? Math.floor(Math.random() * this.tracks.length) : (this.currentIndex + 1) % this.tracks.length;
            this.currentTrack = this.tracks[this.currentIndex];
            this.play();
        },
        pause: function() {
            if (!this.currentTrack) {
                return;
            }
            this.currentTrack.pause();
        },
        stop: function() {
            if (!this.currentTrack) {
                return;
            }
            this.currentTrack.pause();
            this.currentTrack.currentTime = 0;
        },
        play: function(name) {
            if (name && this.namedTracks[name]) {
                var newTrack = this.namedTracks[name];
                if (newTrack != this.currentTrack) {
                    this.stop();
                    this.currentTrack = newTrack;
                }
            } 
            else if (!this.currentTrack) {
                return;
            }
            this.currentTrack.volume = this.volume;
            this.currentTrack.play();
        },
        getLooping: function() {
            return this._loop;
        },
        setLooping: function(l) {
            this._loop = l;
            for (var i in this.tracks) {
                this.tracks[i].loop = l;
            }
        },
        getVolume: function() {
            return this._volume;
        },
        setVolume: function(v) {
            this._volume = v.limit(0, 1);
            for (var i in this.tracks) {
                this.tracks[i].volume = this._volume;
            }
        },
        fadeToTrack: function(time, name) {
            if (this._fadeInterval) {
                clearInterval(this._fadeInterval);
            }
            if (name && this.namedTracks[name] == this.currentTrack) {
                this.currentTrack.volume = this._volume;
                return;
            }
            this._nextTrack = this.namedTracks[name];
            this._nextTrack.volume = 0;
            this._nextTrack.play();
            clearInterval(this._fadeInterval);
            this.fadeTimer = new ig.Timer(time);
            this._fadeInterval = setInterval(this._fadeStepNext.bind(this), 50);
        },
        _fadeStepNext: function() {
            var v = this.fadeTimer.delta().map(-this.fadeTimer.target, 0, 1, 0).limit(0, 1);
            if (v <= 0.01) {
                clearInterval(this._fadeInterval);
                if (this.currentTrack) {
                    this.currentTrack.pause();
                    this.currentTrack.volume = this._volume;
                }
                this.currentTrack = this._nextTrack;
                this.currentTrack.volume = this._volume;
            } 
            else {
                if (this.currentTrack) {
                    this.currentTrack.volume = v * this._volume;
                }
                this._nextTrack.volume = (1 - v) * this._volume;
            }
        },
        fadeOut: function(time) {
            if (!this.currentTrack) {
                return;
            }
            clearInterval(this._fadeInterval);
            this.fadeTimer = new ig.Timer(time);
            this._fadeInterval = setInterval(this._fadeStep.bind(this), 50);
        },
        _fadeStep: function() {
            var v = this.fadeTimer.delta().map(-this.fadeTimer.target, 0, 1, 0).limit(0, 1) * this._volume;
            if (v <= 0.01) {
                this.stop();
                this.currentTrack.volume = this._volume;
                clearInterval(this._fadeInterval);
            } 
            else {
                this.currentTrack.volume = v;
            }
        },
        _endedCallback: function() {
            if (this._loop) {
                this.play();
            } 
            else {
                this.next();
            }
        }
    });
    ig.Sound = ig.Class.extend({
        path: '',
        volume: 1,
        currentClip: null ,
        multiChannel: true,
        _loop: false,
        _fadeInterval: 0,
        init: function(path, multiChannel) {
            this.path = path;
            this.multiChannel = (multiChannel !== false);
            Object.defineProperty(this, "loop", {
                get: this.getLooping.bind(this),
                set: this.setLooping.bind(this)
            });
            this.load();
        },
        fadeIn: function(time) {
            this.currentClip = ig.soundManager.get(this.path);
            this.currentClip.loop = this._loop;
            this.currentClip.volume = 0;
            this.currentClip.play();
            clearInterval(this._fadeInterval);
            this.fadeTimer = new ig.Timer(time);
            this._fadeInterval = setInterval(this._fadeStep.bind(this), 100);
        },
        _fadeStep: function() {
            var v = this.fadeTimer.delta().map(-this.fadeTimer.target, 0, 0, 1).limit(0, 1) * this.volume;
            if (v <= 0.01) {
                this.stop();
                this.currentClip.volume = this.volume;
                clearInterval(this._fadeInterval);
            } 
            else {
                this.currentClip.volume = v;
            }
        },
        getLooping: function() {
            return this._loop;
        },
        setLooping: function(loop) {
            this._loop = loop;
            if (this.currentClip) {
                this.currentClip.loop = loop;
            }
        },
        load: function(loadCallback) {
            if (!ig.Sound.enabled) {
                if (loadCallback) {
                    loadCallback(this.path, true);
                }
                return;
            }
            if (ig.ready) {
                ig.soundManager.load(this.path, this.multiChannel, loadCallback);
            } 
            else {
                ig.addResource(this);
            }
        },
        play: function() {
            if (!ig.Sound.enabled) {
                return;
            }
            this.currentClip = ig.soundManager.get(this.path);
            this.currentClip.loop = this._loop;
            this.currentClip.volume = ig.soundManager.volume * this.volume;
            this.currentClip.play();
        },
        stop: function() {
            if (this.currentClip) {
                try {
                    this.currentClip.pause();
                    this.currentClip.currentTime = 0;
                } catch (err) {}
            }
        }
    });
    ig.Sound.WebAudioSource = ig.Class.extend({
        sources: [],
        gain: null ,
        buffer: null ,
        _loop: false,
        init: function() {
            this.gain = ig.soundManager.audioContext.createGain();
            this.gain.connect(ig.soundManager.audioContext.destination);
            Object.defineProperty(this, "loop", {
                get: this.getLooping.bind(this),
                set: this.setLooping.bind(this)
            });
            Object.defineProperty(this, "volume", {
                get: this.getVolume.bind(this),
                set: this.setVolume.bind(this)
            });
        },
        play: function() {
            if (!this.buffer) {
                return;
            }
            var source = ig.soundManager.audioContext.createBufferSource();
            source.buffer = this.buffer;
            source.connect(this.gain);
            source.loop = this._loop;
            var that = this;
            this.sources.push(source);
            source.onended = function() {
                that.sources.erase(source);
            }
            source.start(0);
        },
        pause: function() {
            for (var i = 0; i < this.sources.length; i++) {
                try {
                    this.sources[i].stop();
                } catch (err) {}
            }
        },
        getLooping: function() {
            return this._loop;
        },
        setLooping: function(loop) {
            this._loop = loop;
            for (var i = 0; i < this.sources.length; i++) {
                this.sources[i].loop = loop;
            }
        },
        getVolume: function() {
            return this.gain.gain.value;
        },
        setVolume: function(volume) {
            this.gain.gain.value = volume;
        }
    });
    ig.Sound.FORMAT = {
        MP3: {
            ext: 'mp3',
            mime: 'audio/mpeg'
        },
        M4A: {
            ext: 'm4a',
            mime: 'audio/mp4; codecs=mp4a'
        },
        OGG: {
            ext: 'ogg',
            mime: 'audio/ogg; codecs=vorbis'
        },
        WEBM: {
            ext: 'webm',
            mime: 'audio/webm; codecs=vorbis'
        },
        CAF: {
            ext: 'caf',
            mime: 'audio/x-caf'
        }
    };
    ig.Sound.use = [ig.Sound.FORMAT.OGG, ig.Sound.FORMAT.MP3];
    ig.Sound.channels = 4;
    ig.Sound.enabled = true;
    ig.normalizeVendorAttribute(window, 'AudioContext');
    ig.Sound.useWebAudio = !!window.AudioContext && !window.nwf;
});

// lib/impact/loader.js
ig.baked = true;
ig.module('impact.loader').requires('impact.image', 'impact.font', 'impact.sound').defines(function() {
    "use strict";
    ig.Loader = ig.Class.extend({
        resources: [],
        gameClass: null ,
        status: 0,
        done: false,
        _unloaded: [],
        _drawStatus: 0,
        _intervalId: 0,
        _loadCallbackBound: null ,
        init: function(gameClass, resources) {
            this.gameClass = gameClass;
            this.resources = resources;
            this._loadCallbackBound = this._loadCallback.bind(this);
            for (var i = 0; i < this.resources.length; i++) {
                this._unloaded.push(this.resources[i].path);
            }
        },
        load: function() {
            ig.system.clear('#000');
            if (!this.resources.length) {
                this.end();
                return;
            }
            for (var i = 0; i < this.resources.length; i++) {
                this.loadResource(this.resources[i]);
            }
            this._intervalId = setInterval(this.draw.bind(this), 16);
        },
        loadResource: function(res) {
            res.load(this._loadCallbackBound);
        },
        end: function() {
            if (this.done) {
                return;
            }
            this.done = true;
            clearInterval(this._intervalId);
            ig.system.setGame(this.gameClass);
        },
        draw: function() {
            this._drawStatus += (this.status - this._drawStatus) / 5;
            var s = ig.system.scale;
            var w = ig.system.width * 0.6;
            var h = ig.system.height * 0.1;
            var x = ig.system.width * 0.5 - w / 2;
            var y = ig.system.height * 0.5 - h / 2;
            ig.system.context.fillStyle = '#000';
            ig.system.context.fillRect(0, 0, 480, 320);
            ig.system.context.fillStyle = '#fff';
            ig.system.context.fillRect(x * s, y * s, w * s, h * s);
            ig.system.context.fillStyle = '#000';
            ig.system.context.fillRect(x * s + s, y * s + s, w * s - s - s, h * s - s - s);
            ig.system.context.fillStyle = '#fff';
            ig.system.context.fillRect(x * s, y * s, w * s * this._drawStatus, h * s);
        },
        _loadCallback: function(path, status) {
            if (status) {
                this._unloaded.erase(path);
            } 
            else {
                throw ('Failed to load resource: ' + path);
            }
            this.status = 1 - (this._unloaded.length / this.resources.length);
            if (this._unloaded.length == 0) {
                setTimeout(this.end.bind(this), 250);
            }
        }
    });
});

// lib/impact/timer.js
ig.baked = true;
ig.module('impact.timer').defines(function() {
    "use strict";
    ig.Timer = ig.Class.extend({
        target: 0,
        base: 0,
        last: 0,
        pausedAt: 0,
        init: function(seconds) {
            this.base = ig.Timer.time;
            this.last = ig.Timer.time;
            this.target = seconds || 0;
        },
        set: function(seconds) {
            this.target = seconds || 0;
            this.base = ig.Timer.time;
            this.pausedAt = 0;
        },
        reset: function() {
            this.base = ig.Timer.time;
            this.pausedAt = 0;
        },
        tick: function() {
            var delta = ig.Timer.time - this.last;
            this.last = ig.Timer.time;
            return ( this.pausedAt ? 0 : delta) ;
        },
        delta: function() {
            return (this.pausedAt || ig.Timer.time) - this.base - this.target;
        },
        pause: function() {
            if (!this.pausedAt) {
                this.pausedAt = ig.Timer.time;
            }
        },
        unpause: function() {
            if (this.pausedAt) {
                this.base += ig.Timer.time - this.pausedAt;
                this.pausedAt = 0;
            }
        }
    });
    ig.Timer._last = 0;
    ig.Timer.time = Number.MIN_VALUE;
    ig.Timer.timeScale = 1;
    ig.Timer.maxStep = 0.05;
    ig.Timer.step = function() {
        var current = Date.now();
        var delta = (current - ig.Timer._last) / 1000;
        ig.Timer.time += Math.min(delta, ig.Timer.maxStep) * ig.Timer.timeScale;
        ig.Timer._last = current;
    }
    ;
});

// lib/impact/system.js
ig.baked = true;
ig.module('impact.system').requires('impact.timer', 'impact.image').defines(function() {
    "use strict";
    ig.System = ig.Class.extend({
        fps: 30,
        width: 320,
        height: 240,
        realWidth: 320,
        realHeight: 240,
        scale: 1,
        tick: 0,
        animationId: 0,
        newGameClass: null ,
        running: false,
        delegate: null ,
        clock: null ,
        canvas: null ,
        context: null ,
        init: function(canvasId, fps, width, height, scale) {
            this.fps = fps;
            this.clock = new ig.Timer();
            this.canvas = ig.$(canvasId);
            this.resize(width, height, scale);
            this.context = this.canvas.getContext('2d');
            this.getDrawPos = ig.System.drawMode;
            if (this.scale != 1) {
                ig.System.scaleMode = ig.System.SCALE.CRISP;
            }
            ig.System.scaleMode(this.canvas, this.context);
        },
        resize: function(width, height, scale) {
            this.width = width;
            this.height = height;
            this.scale = scale || this.scale;
            this.realWidth = this.width * this.scale;
            this.realHeight = this.height * this.scale;
            this.canvas.width = this.realWidth;
            this.canvas.height = this.realHeight;
        },
        setGame: function(gameClass) {
            if (this.running) {
                this.newGameClass = gameClass;
            } 
            else {
                this.setGameNow(gameClass);
            }
        },
        setGameNow: function(gameClass) {
            ig.game = new (gameClass)();
            ig.system.setDelegate(ig.game);
        },
        setDelegate: function(object) {
            if (typeof (object.run) == 'function') {
                this.delegate = object;
                this.startRunLoop();
            } else {
                throw ('System.setDelegate: No run() function in object');
            }
        },
        stopRunLoop: function() {
            ig.clearAnimation(this.animationId);
            this.running = false;
        },
        startRunLoop: function() {
            this.stopRunLoop();
            this.animationId = ig.setAnimation(this.run.bind(this), this.canvas);
            this.running = true;
        },
        clear: function(color) {
            this.context.fillStyle = color;
            this.context.fillRect(0, 0, this.realWidth, this.realHeight);
        },
        run: function() {
            ig.Timer.step();
            this.tick = this.clock.tick();
            this.delegate.run();
            ig.input.clearPressed();
            if (this.newGameClass) {
                this.setGameNow(this.newGameClass);
                this.newGameClass = null ;
            }
        },
        getDrawPos: null 
    });
    ig.System.DRAW = {
        AUTHENTIC: function(p) {
            return Math.round(p) * this.scale;
        },
        SMOOTH: function(p) {
            return Math.round(p * this.scale);
        },
        SUBPIXEL: function(p) {
            return p * this.scale;
        }
    };
    ig.System.drawMode = ig.System.DRAW.SMOOTH;
    ig.System.SCALE = {
        CRISP: function(canvas, context) {
            ig.setVendorAttribute(context, 'imageSmoothingEnabled', false);
            canvas.style.imageRendering = '-moz-crisp-edges';
            canvas.style.imageRendering = '-o-crisp-edges';
            canvas.style.imageRendering = '-webkit-optimize-contrast';
            canvas.style.imageRendering = 'crisp-edges';
            canvas.style.msInterpolationMode = 'nearest-neighbor';
        },
        SMOOTH: function(canvas, context) {
            ig.setVendorAttribute(context, 'imageSmoothingEnabled', true);
            canvas.style.imageRendering = '';
            canvas.style.msInterpolationMode = '';
        }
    };
    ig.System.scaleMode = ig.System.SCALE.SMOOTH;
});

// lib/impact/input.js
ig.baked = true;
ig.module('impact.input').defines(function() {
    "use strict";
    ig.KEY = {
        'MOUSE1': -1,
        'MOUSE2': -3,
        'MWHEEL_UP': -4,
        'MWHEEL_DOWN': -5,
        'BACKSPACE': 8,
        'TAB': 9,
        'ENTER': 13,
        'PAUSE': 19,
        'CAPS': 20,
        'ESC': 27,
        'SPACE': 32,
        'PAGE_UP': 33,
        'PAGE_DOWN': 34,
        'END': 35,
        'HOME': 36,
        'LEFT_ARROW': 37,
        'UP_ARROW': 38,
        'RIGHT_ARROW': 39,
        'DOWN_ARROW': 40,
        'INSERT': 45,
        'DELETE': 46,
        '_0': 48,
        '_1': 49,
        '_2': 50,
        '_3': 51,
        '_4': 52,
        '_5': 53,
        '_6': 54,
        '_7': 55,
        '_8': 56,
        '_9': 57,
        'A': 65,
        'B': 66,
        'C': 67,
        'D': 68,
        'E': 69,
        'F': 70,
        'G': 71,
        'H': 72,
        'I': 73,
        'J': 74,
        'K': 75,
        'L': 76,
        'M': 77,
        'N': 78,
        'O': 79,
        'P': 80,
        'Q': 81,
        'R': 82,
        'S': 83,
        'T': 84,
        'U': 85,
        'V': 86,
        'W': 87,
        'X': 88,
        'Y': 89,
        'Z': 90,
        'NUMPAD_0': 96,
        'NUMPAD_1': 97,
        'NUMPAD_2': 98,
        'NUMPAD_3': 99,
        'NUMPAD_4': 100,
        'NUMPAD_5': 101,
        'NUMPAD_6': 102,
        'NUMPAD_7': 103,
        'NUMPAD_8': 104,
        'NUMPAD_9': 105,
        'MULTIPLY': 106,
        'ADD': 107,
        'SUBSTRACT': 109,
        'DECIMAL': 110,
        'DIVIDE': 111,
        'F1': 112,
        'F2': 113,
        'F3': 114,
        'F4': 115,
        'F5': 116,
        'F6': 117,
        'F7': 118,
        'F8': 119,
        'F9': 120,
        'F10': 121,
        'F11': 122,
        'F12': 123,
        'SHIFT': 16,
        'CTRL': 17,
        'ALT': 18,
        'PLUS': 187,
        'COMMA': 188,
        'MINUS': 189,
        'PERIOD': 190
    };
    ig.Input = ig.Class.extend({
        bindings: {},
        actions: {},
        presses: {},
        locks: {},
        delayedKeyup: {},
        isUsingMouse: false,
        isUsingKeyboard: false,
        isUsingAccelerometer: false,
        mouse: {
            x: 0,
            y: 0
        },
        accel: {
            x: 0,
            y: 0,
            z: 0
        },
        initMouse: function() {
            if (this.isUsingMouse) {
                return;
            }
            this.isUsingMouse = true;
            var mouseWheelBound = this.mousewheel.bind(this);
            ig.system.canvas.addEventListener('mousewheel', mouseWheelBound, false);
            ig.system.canvas.addEventListener('DOMMouseScroll', mouseWheelBound, false);
            ig.system.canvas.addEventListener('contextmenu', this.contextmenu.bind(this), false);
            ig.system.canvas.addEventListener('mousedown', this.keydown.bind(this), false);
            ig.system.canvas.addEventListener('mouseup', this.keyup.bind(this), false);
            ig.system.canvas.addEventListener('mousemove', this.mousemove.bind(this), false);
            if (ig.ua.touchDevice) {
                ig.system.canvas.addEventListener('touchstart', this.keydown.bind(this), false);
                ig.system.canvas.addEventListener('touchend', this.keyup.bind(this), false);
                ig.system.canvas.addEventListener('touchmove', this.mousemove.bind(this), false);
                ig.system.canvas.addEventListener('MSPointerDown', this.keydown.bind(this), false);
                ig.system.canvas.addEventListener('MSPointerUp', this.keyup.bind(this), false);
                ig.system.canvas.addEventListener('MSPointerMove', this.mousemove.bind(this), false);
                ig.system.canvas.style.msTouchAction = 'none';
            }
        },
        initKeyboard: function() {
            if (this.isUsingKeyboard) {
                return;
            }
            this.isUsingKeyboard = true;
            window.addEventListener('keydown', this.keydown.bind(this), false);
            window.addEventListener('keyup', this.keyup.bind(this), false);
        },
        initAccelerometer: function() {
            if (this.isUsingAccelerometer) {
                return;
            }
            this.isUsingAccelerometer = true;
            window.addEventListener('devicemotion', this.devicemotion.bind(this), false);
        },
        mousewheel: function(event) {
            var delta = event.wheelDelta ? event.wheelDelta : (event.detail * -1);
            var code = delta > 0 ? ig.KEY.MWHEEL_UP : ig.KEY.MWHEEL_DOWN;
            var action = this.bindings[code];
            if (action) {
                this.actions[action] = true;
                this.presses[action] = true;
                this.delayedKeyup[action] = true;
                event.preventDefault();
            }
        },
        mousemove: function(event) {
            var internalWidth = parseInt(ig.system.canvas.offsetWidth) || ig.system.realWidth;
            var scale = ig.system.scale * (internalWidth / ig.system.realWidth);
            var pos = {
                left: 0,
                top: 0
            };
            if (ig.system.canvas.getBoundingClientRect) {
                pos = ig.system.canvas.getBoundingClientRect();
            }
            var ev = event.touches ? event.touches[0] : event;
            this.mouse.x = (ev.clientX - pos.left) / scale;
            this.mouse.y = (ev.clientY - pos.top) / scale;
        },
        contextmenu: function(event) {
            if (this.bindings[ig.KEY.MOUSE2]) {
                event.preventDefault();
            }
        },
        keydown: function(event) {
            var tag = event.target.tagName;
            if (tag == 'INPUT' || tag == 'TEXTAREA') {
                return;
            }
            var code = event.type == 'keydown' ? event.keyCode : (event.button == 2 ? ig.KEY.MOUSE2 : ig.KEY.MOUSE1);
            if (code < 0 && !ig.ua.mobile) {
                window.focus();
            }
            if (event.type == 'touchstart' || event.type == 'mousedown') {
                this.mousemove(event);
            }
            var action = this.bindings[code];
            if (action) {
                this.actions[action] = true;
                if (!this.locks[action]) {
                    this.presses[action] = true;
                    this.locks[action] = true;
                }
                event.preventDefault();
            }
        },
        keyup: function(event) {
            var tag = event.target.tagName;
            if (tag == 'INPUT' || tag == 'TEXTAREA') {
                return;
            }
            var code = event.type == 'keyup' ? event.keyCode : (event.button == 2 ? ig.KEY.MOUSE2 : ig.KEY.MOUSE1);
            var action = this.bindings[code];
            if (action) {
                this.delayedKeyup[action] = true;
                event.preventDefault();
            }
        },
        devicemotion: function(event) {
            this.accel = event.accelerationIncludingGravity;
        },
        bind: function(key, action) {
            if (key < 0) {
                this.initMouse();
            } 
            else if (key > 0) {
                this.initKeyboard();
            }
            this.bindings[key] = action;
        },
        bindTouch: function(selector, action) {
            var element = ig.$(selector);
            var that = this;
            element.addEventListener('touchstart', function(ev) {
                that.touchStart(ev, action);
            }, false);
            element.addEventListener('touchend', function(ev) {
                that.touchEnd(ev, action);
            }, false);
            element.addEventListener('MSPointerDown', function(ev) {
                that.touchStart(ev, action);
            }, false);
            element.addEventListener('MSPointerUp', function(ev) {
                that.touchEnd(ev, action);
            }, false);
        },
        unbind: function(key) {
            var action = this.bindings[key];
            this.delayedKeyup[action] = true;
            this.bindings[key] = null ;
        },
        unbindAll: function() {
            this.bindings = {};
            this.actions = {};
            this.presses = {};
            this.locks = {};
            this.delayedKeyup = {};
        },
        state: function(action) {
            return this.actions[action];
        },
        pressed: function(action) {
            return this.presses[action];
        },
        released: function(action) {
            return !!this.delayedKeyup[action];
        },
        clearPressed: function() {
            for (var action in this.delayedKeyup) {
                this.actions[action] = false;
                this.locks[action] = false;
            }
            this.delayedKeyup = {};
            this.presses = {};
        },
        touchStart: function(event, action) {
            this.actions[action] = true;
            this.presses[action] = true;
            event.preventDefault();
            return false;
        },
        touchEnd: function(event, action) {
            this.delayedKeyup[action] = true;
            event.preventDefault();
            return false;
        }
    });
});

// lib/impact/impact.js
ig.baked = true;
ig.module('impact.impact').requires('dom.ready', 'impact.loader', 'impact.system', 'impact.input', 'impact.sound').defines(function() {
    "use strict";
    ig.main = function(canvasId, gameClass, fps, width, height, scale, loaderClass) {
        ig.system = new ig.System(canvasId,fps,width,height,scale || 1);
        ig.input = new ig.Input();
        ig.soundManager = new ig.SoundManager();
        ig.music = new ig.Music();
        ig.ready = true;
        var loader = new (loaderClass || ig.Loader)(gameClass,ig.resources);
        loader.load();
    }
    ;
});

// lib/impact/animation.js
ig.baked = true;
ig.module('impact.animation').requires('impact.timer', 'impact.image').defines(function() {
    "use strict";
    ig.AnimationSheet = ig.Class.extend({
        width: 8,
        height: 8,
        image: null ,
        init: function(path, width, height) {
            this.width = width;
            this.height = height;
            this.image = new ig.Image(path);
        }
    });
    ig.Animation = ig.Class.extend({
        sheet: null ,
        timer: null ,
        sequence: [],
        flip: {
            x: false,
            y: false
        },
        pivot: {
            x: 0,
            y: 0
        },
        frame: 0,
        tile: 0,
        loopCount: 0,
        alpha: 1,
        angle: 0,
        init: function(sheet, frameTime, sequence, stop) {
            this.sheet = sheet;
            this.pivot = {
                x: sheet.width / 2,
                y: sheet.height / 2
            };
            this.timer = new ig.Timer();
            this.frameTime = frameTime;
            this.sequence = sequence;
            this.stop = !!stop;
            this.tile = this.sequence[0];
        },
        rewind: function() {
            this.timer.set();
            this.loopCount = 0;
            this.frame = 0;
            this.tile = this.sequence[0];
            return this;
        },
        gotoFrame: function(f) {
            this.timer.set(this.frameTime * -f - 0.0001);
            this.update();
        },
        gotoRandomFrame: function() {
            this.gotoFrame(Math.floor(Math.random() * this.sequence.length))
        },
        update: function() {
            var frameTotal = Math.floor(this.timer.delta() / this.frameTime);
            this.loopCount = Math.floor(frameTotal / this.sequence.length);
            if (this.stop && this.loopCount > 0) {
                this.frame = this.sequence.length - 1;
            } 
            else {
                this.frame = frameTotal % this.sequence.length;
            }
            this.tile = this.sequence[this.frame];
        },
        draw: function(targetX, targetY) {
            var bbsize = Math.max(this.sheet.width, this.sheet.height);
            if (targetX > ig.system.width || targetY > ig.system.height || targetX + bbsize < 0 || targetY + bbsize < 0) {
                return;
            }
            if (this.alpha != 1) {
                ig.system.context.globalAlpha = this.alpha;
            }
            if (this.angle == 0) {
                this.sheet.image.drawTile(targetX, targetY, this.tile, this.sheet.width, this.sheet.height, this.flip.x, this.flip.y);
            } 
            else {
                ig.system.context.save();
                ig.system.context.translate(ig.system.getDrawPos(targetX + this.pivot.x), ig.system.getDrawPos(targetY + this.pivot.y));
                ig.system.context.rotate(this.angle);
                this.sheet.image.drawTile(-this.pivot.x, -this.pivot.y, this.tile, this.sheet.width, this.sheet.height, this.flip.x, this.flip.y);
                ig.system.context.restore();
            }
            if (this.alpha != 1) {
                ig.system.context.globalAlpha = 1;
            }
        }
    });
});

			
// lib/impact/entity.js
ig.baked = true;
ig.module('impact.entity').requires('impact.animation', 'impact.impact').defines(function() {
    "use strict";
    ig.Entity = ig.Class.extend({
        id: 0,
        settings: {},
        size: {
            x: 16,
            y: 16
        },
        offset: {
            x: 0,
            y: 0
        },
        pos: {
            x: 0,
            y: 0
        },
        last: {
            x: 0,
            y: 0
        },
        vel: {
            x: 0,
            y: 0
        },
        accel: {
            x: 0,
            y: 0
        },
        friction: {
            x: 0,
            y: 0
        },
        maxVel: {
            x: 100,
            y: 100
        },
        zIndex: 0,
        gravityFactor: 1,
        standing: false,
        bounciness: 0,
        minBounceVelocity: 40,
        anims: {},
        animSheet: null ,
        currentAnim: null ,
        health: 10,
        type: 0,
        checkAgainst: 0,
        collides: 0,
        _killed: false,
        slopeStanding: {
            min: (44).toRad(),
            max: (136).toRad()
        },
        init: function(x, y, settings) {
            this.id = ++ig.Entity._lastId;
            this.pos.x = this.last.x = x;
            this.pos.y = this.last.y = y;
            ig.merge(this, settings);
        },
        reset: function(x, y, settings) {
            var proto = this.constructor.prototype;
            this.pos.x = x;
            this.pos.y = y;
            this.last.x = x;
            this.last.y = y;
            this.vel.x = proto.vel.x;
            this.vel.y = proto.vel.y;
            this.accel.x = proto.accel.x;
            this.accel.y = proto.accel.y;
            this.health = proto.health;
            this._killed = proto._killed;
            this.standing = proto.standing;
            this.type = proto.type;
            this.checkAgainst = proto.checkAgainst;
            this.collides = proto.collides;
            ig.merge(this, settings);
        },
        addAnim: function(name, frameTime, sequence, stop) {
            if (!this.animSheet) {
                throw ('No animSheet to add the animation ' + name + ' to.');
            }
            var a = new ig.Animation(this.animSheet,frameTime,sequence,stop);
            this.anims[name] = a;
            if (!this.currentAnim) {
                this.currentAnim = a;
            }
            return a;
        },
        update: function() {
            this.last.x = this.pos.x;
            this.last.y = this.pos.y;
            this.vel.y += ig.game.gravity * ig.system.tick * this.gravityFactor;
            this.vel.x = this.getNewVelocity(this.vel.x, this.accel.x, this.friction.x, this.maxVel.x);
            this.vel.y = this.getNewVelocity(this.vel.y, this.accel.y, this.friction.y, this.maxVel.y);
            var mx = this.vel.x * ig.system.tick;
            var my = this.vel.y * ig.system.tick;
            var res = ig.game.collisionMap.trace(this.pos.x, this.pos.y, mx, my, this.size.x, this.size.y);
					
			
            this.handleMovementTrace(res);
            if (this.currentAnim) {
                this.currentAnim.update();
            }
        },
        getNewVelocity: function(vel, accel, friction, max) {
            if (accel) {
                return (vel + accel * ig.system.tick).limit(-max, max);
            } 
            else if (friction) {
                var delta = friction*10 * ig.system.tick;//wzsx*10
                if (vel - delta > 0) {
                    return vel - delta;
                } 
                else if (vel + delta < 0) {
                    return vel + delta;
                } 
                else {
                    return 0;
                }
            }
            return vel.limit(-max, max);
        },
        handleMovementTrace: function(res) {
            this.standing = false;
            if (res.collision.y) {
                if (this.bounciness > 0 && Math.abs(this.vel.y) > this.minBounceVelocity) {
                    this.vel.y *= -this.bounciness;
                } 
                else {
                    if (this.vel.y > 0) {
                        this.standing = true;
                    }
                    this.vel.y = 0;
                }
            }
            if (res.collision.x) {
                if (this.bounciness > 0 && Math.abs(this.vel.x) > this.minBounceVelocity) {
                    this.vel.x *= -this.bounciness;
                } 
                else {
                    this.vel.x = 0;
                }
            }
            if (res.collision.slope) {
                var s = res.collision.slope;
                if (this.bounciness > 0) {
                    var proj = this.vel.x * s.nx + this.vel.y * s.ny;
                    this.vel.x = (this.vel.x - s.nx * proj * 2) * this.bounciness;
                    this.vel.y = (this.vel.y - s.ny * proj * 2) * this.bounciness;
                } 
                else {
                    var lengthSquared = s.x * s.x + s.y * s.y;
                    var dot = (this.vel.x * s.x + this.vel.y * s.y) / lengthSquared;
                    this.vel.x = s.x * dot;
                    this.vel.y = s.y * dot;
                    var angle = Math.atan2(s.x, s.y);
                    if (angle > this.slopeStanding.min && angle < this.slopeStanding.max) {
                        this.standing = true;
                    }
                }
            }
            this.pos = res.pos;
        },
        draw: function() {
            if (this.currentAnim) {
                this.currentAnim.draw(this.pos.x - this.offset.x - ig.game._rscreen.x, this.pos.y - this.offset.y - ig.game._rscreen.y);
            }
        },
        kill: function() {
            ig.game.removeEntity(this);
        },
        receiveDamage: function(amount, from) {
            this.health -= amount;
            if (this.health <= 0) {
                this.kill();
            }
        },
        touches: function(other) {
            return !(this.pos.x >= other.pos.x + other.size.x || this.pos.x + this.size.x <= other.pos.x || this.pos.y >= other.pos.y + other.size.y || this.pos.y + this.size.y <= other.pos.y);
        },
        distanceTo: function(other) {
            var xd = (this.pos.x + this.size.x / 2) - (other.pos.x + other.size.x / 2);
            var yd = (this.pos.y + this.size.y / 2) - (other.pos.y + other.size.y / 2);
            return Math.sqrt(xd * xd + yd * yd);
        },
        angleTo: function(other) {
            return Math.atan2((other.pos.y + other.size.y / 2) - (this.pos.y + this.size.y / 2), (other.pos.x + other.size.x / 2) - (this.pos.x + this.size.x / 2));
        },
        check: function(other) {},
        collideWith: function(other, axis) {},
        ready: function() {},
        erase: function() {}
    });
    ig.Entity._lastId = 0;
    ig.Entity.COLLIDES = {
        NEVER: 0,
        LITE: 1,
        PASSIVE: 2,
        ACTIVE: 4,
        FIXED: 8
    };
    ig.Entity.TYPE = {
        NONE: 0,
        A: 1,
        B: 2,
        BOTH: 3
    };
    ig.Entity.checkPair = function(a, b) {
        if (a.checkAgainst & b.type) {
            a.check(b);
        }
        if (b.checkAgainst & a.type) {
            b.check(a);
        }
        if (a.collides && b.collides && a.collides + b.collides > ig.Entity.COLLIDES.ACTIVE) {
            ig.Entity.solveCollision(a, b);
        }
    }
    ;
    ig.Entity.solveCollision = function(a, b) {
        var weak = null ;
        if (a.collides == ig.Entity.COLLIDES.LITE || b.collides == ig.Entity.COLLIDES.FIXED) {
            weak = a;
        } 
        else if (b.collides == ig.Entity.COLLIDES.LITE || a.collides == ig.Entity.COLLIDES.FIXED) {
            weak = b;
        }
        if (a.last.x + a.size.x > b.last.x && a.last.x < b.last.x + b.size.x) {
            if (a.last.y < b.last.y) {
                ig.Entity.seperateOnYAxis(a, b, weak);
            } 
            else {
                ig.Entity.seperateOnYAxis(b, a, weak);
            }
            a.collideWith(b, 'y');
            b.collideWith(a, 'y');
        } 
        else if (a.last.y + a.size.y > b.last.y && a.last.y < b.last.y + b.size.y) {
            if (a.last.x < b.last.x) {
                ig.Entity.seperateOnXAxis(a, b, weak);
            } 
            else {
                ig.Entity.seperateOnXAxis(b, a, weak);
            }
            a.collideWith(b, 'x');
            b.collideWith(a, 'x');
        }
    }
    ;
    ig.Entity.seperateOnXAxis = function(left, right, weak) {
        var nudge = (left.pos.x + left.size.x - right.pos.x);
        if (weak) {
            var strong = left === weak ? right : left;
            weak.vel.x = -weak.vel.x * weak.bounciness + strong.vel.x;
            var resWeak = ig.game.collisionMap.trace(weak.pos.x, weak.pos.y, weak == left ? -nudge : nudge, 0, weak.size.x, weak.size.y);
            weak.pos.x = resWeak.pos.x;
        } 
        else {
            var v2 = (left.vel.x - right.vel.x) / 2;
            left.vel.x = -v2;
            right.vel.x = v2;
            var resLeft = ig.game.collisionMap.trace(left.pos.x, left.pos.y, -nudge / 2, 0, left.size.x, left.size.y);
            left.pos.x = Math.floor(resLeft.pos.x);
            var resRight = ig.game.collisionMap.trace(right.pos.x, right.pos.y, nudge / 2, 0, right.size.x, right.size.y);
            right.pos.x = Math.ceil(resRight.pos.x);
        }
    }
    ;
    ig.Entity.seperateOnYAxis = function(top, bottom, weak) {
        var nudge = (top.pos.y + top.size.y - bottom.pos.y);
        if (weak) {
            var strong = top === weak ? bottom : top;
            weak.vel.y = -weak.vel.y * weak.bounciness + strong.vel.y;
            var nudgeX = 0;
            if (weak == top && Math.abs(weak.vel.y - strong.vel.y) < weak.minBounceVelocity) {
                weak.standing = true;
                nudgeX = strong.vel.x * ig.system.tick;
            }
            var resWeak = ig.game.collisionMap.trace(weak.pos.x, weak.pos.y, nudgeX, weak == top ? -nudge : nudge, weak.size.x, weak.size.y);
            weak.pos.y = resWeak.pos.y;
            weak.pos.x = resWeak.pos.x;
        } 
        else if (ig.game.gravity && (bottom.standing || top.vel.y > 0)) {
            var resTop = ig.game.collisionMap.trace(top.pos.x, top.pos.y, 0, -(top.pos.y + top.size.y - bottom.pos.y), top.size.x, top.size.y);
            top.pos.y = resTop.pos.y;
            if (top.bounciness > 0 && top.vel.y > top.minBounceVelocity) {
                top.vel.y *= -top.bounciness;
            } 
            else {
                top.standing = true;
                top.vel.y = 0;
            }
        } 
        else {
            var v2 = (top.vel.y - bottom.vel.y) / 2;
            top.vel.y = -v2;
            bottom.vel.y = v2;
            var nudgeX = bottom.vel.x * ig.system.tick;
            var resTop = ig.game.collisionMap.trace(top.pos.x, top.pos.y, nudgeX, -nudge / 2, top.size.x, top.size.y);
            top.pos.y = resTop.pos.y;
            var resBottom = ig.game.collisionMap.trace(bottom.pos.x, bottom.pos.y, 0, nudge / 2, bottom.size.x, bottom.size.y);
            bottom.pos.y = resBottom.pos.y;
        }
    }
    ;
});

// lib/impact/map.js
ig.baked = true;
ig.module('impact.map').defines(function() {
    "use strict";
    ig.Map = ig.Class.extend({
        tilesize: 8,
        width: 1,
        height: 1,
        data: [[]],
        name: null ,
        init: function(tilesize, data) {
            this.tilesize = tilesize;
            this.data = data;
            this.height = data.length;
            this.width = data[0].length;
            this.pxWidth = this.width * this.tilesize;
            this.pxHeight = this.height * this.tilesize;
        },
        getTile: function(x, y) {
            var tx = Math.floor(x / this.tilesize);
            var ty = Math.floor(y / this.tilesize);
            if ((tx >= 0 && tx < this.width) && (ty >= 0 && ty < this.height)) {
                return this.data[ty][tx];
            } 
            else {
                return 0;
            }
        },
        setTile: function(x, y, tile) {
            var tx = Math.floor(x / this.tilesize);
            var ty = Math.floor(y / this.tilesize);
            if ((tx >= 0 && tx < this.width) && (ty >= 0 && ty < this.height)) {
                this.data[ty][tx] = tile;
            }
        }
    });
});

// lib/impact/collision-map.js
ig.baked = true;
ig.module('impact.collision-map').requires('impact.map').defines(function() {
    "use strict";
    ig.CollisionMap = ig.Map.extend({
        lastSlope: 1,
        tiledef: null ,
        init: function(tilesize, data, tiledef) {
            this.parent(tilesize, data);
            this.tiledef = tiledef || ig.CollisionMap.defaultTileDef;
            for (var t in this.tiledef) {
                if (t | 0 > this.lastSlope) {
                    this.lastSlope = t | 0;
                }
            }
        },
        trace: function(x, y, vx, vy, objectWidth, objectHeight) {
            var res = {
                collision: {
                    x: false,
                    y: false,
                    slope: false
                },
                pos: {
                    x: x,
                    y: y
                },
                tile: {
                    x: 0,
                    y: 0
                }
            };
            var steps = Math.ceil((Math.max(Math.abs(vx), Math.abs(vy)) + 0.1) / this.tilesize);
            if (steps > 1) {
                var sx = vx / steps;
                var sy = vy / steps;
                for (var i = 0; i < steps && (sx || sy); i++) {
                    this._traceStep(res, x, y, sx, sy, objectWidth, objectHeight, vx, vy, i);
                    x = res.pos.x;
                    y = res.pos.y;
                    if (res.collision.x) {
                        sx = 0;
                        vx = 0;
                    }
                    if (res.collision.y) {
                        sy = 0;
                        vy = 0;
                    }
                    if (res.collision.slope) {
                        break;
                    }
                }
            } 
            else {
                this._traceStep(res, x, y, vx, vy, objectWidth, objectHeight, vx, vy, 0);
            }
            return res;
        },
        _traceStep: function(res, x, y, vx, vy, width, height, rvx, rvy, step) {
            res.pos.x += vx;
            res.pos.y += vy;
            var t = 0;
            if (vx) {
                var pxOffsetX = (vx > 0 ? width : 0);
                var tileOffsetX = (vx < 0 ? this.tilesize : 0);
                var firstTileY = Math.max(Math.floor(y / this.tilesize), 0);
                var lastTileY = Math.min(Math.ceil((y + height) / this.tilesize), this.height);
                var tileX = Math.floor((res.pos.x + pxOffsetX) / this.tilesize);
                var prevTileX = Math.floor((x + pxOffsetX) / this.tilesize);
                if (step > 0 || tileX == prevTileX || prevTileX < 0 || prevTileX >= this.width) {
                    prevTileX = -1;
                }
                if (tileX >= 0 && tileX < this.width) {
                    for (var tileY = firstTileY; tileY < lastTileY; tileY++) {
                        if (prevTileX != -1) {
                            t = this.data[tileY][prevTileX];
                            if (t > 1 && t <= this.lastSlope && this._checkTileDef(res, t, x, y, rvx, rvy, width, height, prevTileX, tileY)) {
                                break;
                            }
                        }
                        t = this.data[tileY][tileX];
                        if (t == 1 || t > this.lastSlope || (t > 1 && this._checkTileDef(res, t, x, y, rvx, rvy, width, height, tileX, tileY))) {
                            if (t > 1 && t <= this.lastSlope && res.collision.slope) {
                                break;
                            }
                            res.collision.x = true;
                            res.tile.x = t;
                            x = res.pos.x = tileX * this.tilesize - pxOffsetX + tileOffsetX;
                            rvx = 0;
                            break;
                        }
                    }
                }
            }
            if (vy) {
                var pxOffsetY = (vy > 0 ? height : 0);
                var tileOffsetY = (vy < 0 ? this.tilesize : 0);
                var firstTileX = Math.max(Math.floor(res.pos.x / this.tilesize), 0);
                var lastTileX = Math.min(Math.ceil((res.pos.x + width) / this.tilesize), this.width);
                var tileY = Math.floor((res.pos.y + pxOffsetY) / this.tilesize);
                var prevTileY = Math.floor((y + pxOffsetY) / this.tilesize);
                if (step > 0 || tileY == prevTileY || prevTileY < 0 || prevTileY >= this.height) {
                    prevTileY = -1;
                }
                if (tileY >= 0 && tileY < this.height) {
                    for (var tileX = firstTileX; tileX < lastTileX; tileX++) {
                        if (prevTileY != -1) {
                            t = this.data[prevTileY][tileX];
                            if (t > 1 && t <= this.lastSlope && this._checkTileDef(res, t, x, y, rvx, rvy, width, height, tileX, prevTileY)) {
                                break;
                            }
                        }
                        t = this.data[tileY][tileX];
                        if (t == 1 || t > this.lastSlope || (t > 1 && this._checkTileDef(res, t, x, y, rvx, rvy, width, height, tileX, tileY))) {
                            if (t > 1 && t <= this.lastSlope && res.collision.slope) {
                                break;
                            }
                            res.collision.y = true;
                            res.tile.y = t;
                            res.pos.y = tileY * this.tilesize - pxOffsetY + tileOffsetY;
                            break;
                        }
                    }
                }
            }
        },
        _checkTileDef: function(res, t, x, y, vx, vy, width, height, tileX, tileY) {
            var def = this.tiledef[t];
            if (!def) {
                return false;
            }
            var lx = (tileX + def[0]) * this.tilesize
              , ly = (tileY + def[1]) * this.tilesize
              , lvx = (def[2] - def[0]) * this.tilesize
              , lvy = (def[3] - def[1]) * this.tilesize
              , solid = def[4];
            var tx = x + vx + (lvy < 0 ? width : 0) - lx
              , ty = y + vy + (lvx > 0 ? height : 0) - ly;
            if (lvx * ty - lvy * tx > 0) {
                if (vx * -lvy + vy * lvx < 0) {
                    return solid;
                }
                var length = Math.sqrt(lvx * lvx + lvy * lvy);
                var nx = lvy / length
                  , ny = -lvx / length;
                var proj = tx * nx + ty * ny;
                var px = nx * proj
                  , py = ny * proj;
                if (px * px + py * py >= vx * vx + vy * vy) {
                    return solid || (lvx * (ty - vy) - lvy * (tx - vx) < 0.5);
                }
                res.pos.x = x + vx - px;
                res.pos.y = y + vy - py;
                res.collision.slope = {
                    x: lvx,
                    y: lvy,
                    nx: nx,
                    ny: ny
                };
                return true;
            }
            return false;
        }
    });
    var H = 1 / 2
      , N = 1 / 3
      , M = 2 / 3
      , SOLID = true
      , NON_SOLID = false;
    ig.CollisionMap.defaultTileDef = {
        5: [0, 1, 1, M, SOLID],
        6: [0, M, 1, N, SOLID],
        7: [0, N, 1, 0, SOLID],
        3: [0, 1, 1, H, SOLID],
        4: [0, H, 1, 0, SOLID],
        2: [0, 1, 1, 0, SOLID],
        10: [H, 1, 1, 0, SOLID],
        21: [0, 1, H, 0, SOLID],
        32: [M, 1, 1, 0, SOLID],
        43: [N, 1, M, 0, SOLID],
        54: [0, 1, N, 0, SOLID],
        27: [0, 0, 1, N, SOLID],
        28: [0, N, 1, M, SOLID],
        29: [0, M, 1, 1, SOLID],
        25: [0, 0, 1, H, SOLID],
        26: [0, H, 1, 1, SOLID],
        24: [0, 0, 1, 1, SOLID],
        11: [0, 0, H, 1, SOLID],
        22: [H, 0, 1, 1, SOLID],
        33: [0, 0, N, 1, SOLID],
        44: [N, 0, M, 1, SOLID],
        55: [M, 0, 1, 1, SOLID],
        16: [1, N, 0, 0, SOLID],
        17: [1, M, 0, N, SOLID],
        18: [1, 1, 0, M, SOLID],
        14: [1, H, 0, 0, SOLID],
        15: [1, 1, 0, H, SOLID],
        13: [1, 1, 0, 0, SOLID],
        8: [H, 1, 0, 0, SOLID],
        19: [1, 1, H, 0, SOLID],
        30: [N, 1, 0, 0, SOLID],
        41: [M, 1, N, 0, SOLID],
        52: [1, 1, M, 0, SOLID],
        38: [1, M, 0, 1, SOLID],
        39: [1, N, 0, M, SOLID],
        40: [1, 0, 0, N, SOLID],
        36: [1, H, 0, 1, SOLID],
        37: [1, 0, 0, H, SOLID],
        35: [1, 0, 0, 1, SOLID],
        9: [1, 0, H, 1, SOLID],
        20: [H, 0, 0, 1, SOLID],
        31: [1, 0, M, 1, SOLID],
        42: [M, 0, N, 1, SOLID],
        53: [N, 0, 0, 1, SOLID],
        12: [0, 0, 1, 0, NON_SOLID],
        23: [1, 1, 0, 1, NON_SOLID],
        34: [1, 0, 1, 1, NON_SOLID],
        45: [0, 1, 0, 0, NON_SOLID]
    };
    ig.CollisionMap.staticNoCollision = {
        trace: function(x, y, vx, vy) {
            return {
                collision: {
                    x: false,
                    y: false,
                    slope: false
                },
                pos: {
                    x: x + vx,
                    y: y + vy
                },
                tile: {
                    x: 0,
                    y: 0
                }
            };
        }
    };
});

// lib/impact/background-map.js
ig.baked = true;
ig.module('impact.background-map').requires('impact.map', 'impact.image').defines(function() {
    "use strict";
    ig.BackgroundMap = ig.Map.extend({
        tiles: null ,
        scroll: {
            x: 0,
            y: 0
        },
        distance: 1,
        repeat: false,
        tilesetName: '',
        foreground: false,
        enabled: true,
        preRender: false,
        preRenderedChunks: null ,
        chunkSize: 512,
        debugChunks: false,
        anims: {},
        init: function(tilesize, data, tileset) {
            this.parent(tilesize, data);
            this.setTileset(tileset);
        },
        setTileset: function(tileset) {
            this.tilesetName = tileset instanceof ig.Image ? tileset.path : tileset;
            this.tiles = new ig.Image(this.tilesetName);
            this.preRenderedChunks = null ;
        },
        setScreenPos: function(x, y) {
            this.scroll.x = x / this.distance;
            this.scroll.y = y / this.distance;
        },
        preRenderMapToChunks: function() {
            var totalWidth = this.width * this.tilesize * ig.system.scale
              , totalHeight = this.height * this.tilesize * ig.system.scale;
            this.chunkSize = Math.min(Math.max(totalWidth, totalHeight), this.chunkSize);
            var chunkCols = Math.ceil(totalWidth / this.chunkSize)
              , chunkRows = Math.ceil(totalHeight / this.chunkSize);
            this.preRenderedChunks = [];
            for (var y = 0; y < chunkRows; y++) {
                this.preRenderedChunks[y] = [];
                for (var x = 0; x < chunkCols; x++) {
                    var chunkWidth = (x == chunkCols - 1) ? totalWidth - x * this.chunkSize : this.chunkSize;
                    var chunkHeight = (y == chunkRows - 1) ? totalHeight - y * this.chunkSize : this.chunkSize;
                    this.preRenderedChunks[y][x] = this.preRenderChunk(x, y, chunkWidth, chunkHeight);
                }
            }
        },
        preRenderChunk: function(cx, cy, w, h) {
            var tw = w / this.tilesize / ig.system.scale + 1
              , th = h / this.tilesize / ig.system.scale + 1;
            var nx = (cx * this.chunkSize / ig.system.scale) % this.tilesize
              , ny = (cy * this.chunkSize / ig.system.scale) % this.tilesize;
            var tx = Math.floor(cx * this.chunkSize / this.tilesize / ig.system.scale)
              , ty = Math.floor(cy * this.chunkSize / this.tilesize / ig.system.scale);
            var chunk = ig.$new('canvas');
            chunk.width = w;
            chunk.height = h;
            chunk.retinaResolutionEnabled = false;
            var chunkContext = chunk.getContext('2d');
            ig.System.scaleMode(chunk, chunkContext);
            var screenContext = ig.system.context;
            ig.system.context = chunkContext;
            for (var x = 0; x < tw; x++) {
                for (var y = 0; y < th; y++) {
                    if (x + tx < this.width && y + ty < this.height) {
                        var tile = this.data[y + ty][x + tx];
                        if (tile) {
                            this.tiles.drawTile(x * this.tilesize - nx, y * this.tilesize - ny, tile - 1, this.tilesize);
                        }
                    }
                }
            }
            ig.system.context = screenContext;
            return chunk;
        },
        draw: function() {
            if (!this.tiles.loaded || !this.enabled) {
                return;
            }
            if (this.preRender) {
                this.drawPreRendered();
            } 
            else {
                this.drawTiled();
            }
        },
        drawPreRendered: function() {
            if (!this.preRenderedChunks) {
                this.preRenderMapToChunks();
            }
            var dx = ig.system.getDrawPos(this.scroll.x)
              , dy = ig.system.getDrawPos(this.scroll.y);
            if (this.repeat) {
                var w = this.width * this.tilesize * ig.system.scale;
                dx = (dx % w + w) % w;
                var h = this.height * this.tilesize * ig.system.scale;
                dy = (dy % h + h) % h;
            }
            var minChunkX = Math.max(Math.floor(dx / this.chunkSize), 0)
              , minChunkY = Math.max(Math.floor(dy / this.chunkSize), 0)
              , maxChunkX = Math.ceil((dx + ig.system.realWidth) / this.chunkSize)
              , maxChunkY = Math.ceil((dy + ig.system.realHeight) / this.chunkSize)
              , maxRealChunkX = this.preRenderedChunks[0].length
              , maxRealChunkY = this.preRenderedChunks.length;
            if (!this.repeat) {
                maxChunkX = Math.min(maxChunkX, maxRealChunkX);
                maxChunkY = Math.min(maxChunkY, maxRealChunkY);
            }
            var nudgeY = 0;
            for (var cy = minChunkY; cy < maxChunkY; cy++) {
                var nudgeX = 0;
                for (var cx = minChunkX; cx < maxChunkX; cx++) {
                    var chunk = this.preRenderedChunks[cy % maxRealChunkY][cx % maxRealChunkX];
                    var x = -dx + cx * this.chunkSize - nudgeX;
                    var y = -dy + cy * this.chunkSize - nudgeY;
                    ig.system.context.drawImage(chunk, x, y);
                    ig.Image.drawCount++;
                    if (this.debugChunks) {
                        ig.system.context.strokeStyle = '#f0f';
                        ig.system.context.strokeRect(x, y, this.chunkSize, this.chunkSize);
                    }
                    if (this.repeat && chunk.width < this.chunkSize && x + chunk.width < ig.system.realWidth) {
                        nudgeX += this.chunkSize - chunk.width;
                        maxChunkX++;
                    }
                }
                if (this.repeat && chunk.height < this.chunkSize && y + chunk.height < ig.system.realHeight) {
                    nudgeY += this.chunkSize - chunk.height;
                    maxChunkY++;
                }
            }
        },
        drawTiled: function() {
            var tile = 0
              , anim = null 
              , tileOffsetX = (this.scroll.x / this.tilesize).toInt()
              , tileOffsetY = (this.scroll.y / this.tilesize).toInt()
              , pxOffsetX = this.scroll.x % this.tilesize
              , pxOffsetY = this.scroll.y % this.tilesize
              , pxMinX = -pxOffsetX - this.tilesize
              , pxMinY = -pxOffsetY - this.tilesize
              , pxMaxX = ig.system.width + this.tilesize - pxOffsetX
              , pxMaxY = ig.system.height + this.tilesize - pxOffsetY;
            for (var mapY = -1, pxY = pxMinY; pxY < pxMaxY; mapY++,
            pxY += this.tilesize) {
                var tileY = mapY + tileOffsetY;
                if (tileY >= this.height || tileY < 0) {
                    if (!this.repeat) {
                        continue;
                    }
                    tileY = (tileY % this.height + this.height) % this.height;
                }
                for (var mapX = -1, pxX = pxMinX; pxX < pxMaxX; mapX++,
                pxX += this.tilesize) {
                    var tileX = mapX + tileOffsetX;
                    if (tileX >= this.width || tileX < 0) {
                        if (!this.repeat) {
                            continue;
                        }
                        tileX = (tileX % this.width + this.width) % this.width;
                    }
                    if ((tile = this.data[tileY][tileX]) ) {
                        if ((anim = this.anims[tile - 1]) ) {
                            anim.draw(pxX, pxY);
                        } 
                        else {
                            this.tiles.drawTile(pxX, pxY, tile - 1, this.tilesize);
                        }
                    }
                }
            }
        }
    });
});

// lib/impact/game.js
ig.baked = true;
ig.module('impact.game').requires('impact.impact', 'impact.entity', 'impact.collision-map', 'impact.background-map').defines(function() {
    "use strict";
    ig.Game = ig.Class.extend({
        clearColor: '#000000',
        gravity: 0,
        screen: {
            x: 0,
            y: 0
        },
        _rscreen: {
            x: 0,
            y: 0
        },
        entities: [],
        namedEntities: {},
        collisionMap: ig.CollisionMap.staticNoCollision,
        backgroundMaps: [],
        backgroundAnims: {},
        autoSort: false,
        sortBy: null ,
        cellSize: 64,
        _deferredKill: [],
        _levelToLoad: null ,
        _doSortEntities: false,
        staticInstantiate: function() {
            this.sortBy = this.sortBy || ig.Game.SORT.Z_INDEX;
            ig.game = this;
            return null ;
        },
        loadLevel: function(data) {
            this.screen = {
                x: 0,
                y: 0
            };
            this.entities = [];
            this.namedEntities = {};
            for (var i = 0; i < data.entities.length; i++) {
                var ent = data.entities[i];
                this.spawnEntity(ent.type, ent.x, ent.y, ent.settings);
            }
            this.sortEntities();
            this.collisionMap = ig.CollisionMap.staticNoCollision;
            this.backgroundMaps = [];
            for (var i = 0; i < data.layer.length; i++) {
                var ld = data.layer[i];
                if (ld.name == 'collision') {
                    this.collisionMap = new ig.CollisionMap(ld.tilesize,ld.data);
                } 
                else {
                    var newMap = new ig.BackgroundMap(ld.tilesize,ld.data,ld.tilesetName);
                    newMap.anims = this.backgroundAnims[ld.tilesetName] || {};
                    newMap.repeat = ld.repeat;
                    newMap.distance = ld.distance;
                    newMap.foreground = !!ld.foreground;
                    newMap.preRender = !!ld.preRender;
                    newMap.name = ld.name;
                    this.backgroundMaps.push(newMap);
                }
            }
            for (var i = 0; i < this.entities.length; i++) {
                this.entities[i].ready();
            }
        },
        loadLevelDeferred: function(data) {
            this._levelToLoad = data;
        },
        getMapByName: function(name) {
            if (name == 'collision') {
                return this.collisionMap;
            }
            for (var i = 0; i < this.backgroundMaps.length; i++) {
                if (this.backgroundMaps[i].name == name) {
                    return this.backgroundMaps[i];
                }
            }
            return null ;
        },
        getEntityByName: function(name) {
            return this.namedEntities[name];
        },
        getEntitiesByType: function(type) {
            var entityClass = typeof (type) === 'string' ? ig.global[type] : type;
            var a = [];
            for (var i = 0; i < this.entities.length; i++) {
                var ent = this.entities[i];
                if (ent instanceof entityClass && !ent._killed) {
                    a.push(ent);
                }
            }
            return a;
        },
        spawnEntity: function(type, x, y, settings) {
            var entityClass = typeof (type) === 'string' ? ig.global[type] : type;
            if (!entityClass) {
                throw ("Can't spawn entity of type " + type);
            }
            var ent = new (entityClass)(x,y,settings || {});
            this.entities.push(ent);
            if (ent.name) {
                this.namedEntities[ent.name] = ent;
            }
            return ent;
        },
        sortEntities: function() {
            this.entities.sort(this.sortBy);
        },
        sortEntitiesDeferred: function() {
            this._doSortEntities = true;
        },
        removeEntity: function(ent) {
            if (ent.name) {
                delete this.namedEntities[ent.name];
            }
            ent._killed = true;
            ent.type = ig.Entity.TYPE.NONE;
            ent.checkAgainst = ig.Entity.TYPE.NONE;
            ent.collides = ig.Entity.COLLIDES.NEVER;
            this._deferredKill.push(ent);
        },
        run: function() {
            this.update();
            this.draw();
        },
        update: function() {
            if (this._levelToLoad) {
                this.loadLevel(this._levelToLoad);
                this._levelToLoad = null ;
            }
            this.updateEntities();
            this.checkEntities();
            for (var i = 0; i < this._deferredKill.length; i++) {
                this._deferredKill[i].erase();
                this.entities.erase(this._deferredKill[i]);
            }
            this._deferredKill = [];
            if (this._doSortEntities || this.autoSort) {
                this.sortEntities();
                this._doSortEntities = false;
            }
            for (var tileset in this.backgroundAnims) {
                var anims = this.backgroundAnims[tileset];
                for (var a in anims) {
                    anims[a].update();
                }
            }
        },
        updateEntities: function() {
            for (var i = 0; i < this.entities.length; i++) {
                var ent = this.entities[i];
                if (!ent._killed) {
                    ent.update();
                }
            }
        },
        draw: function() {
            if (this.clearColor) {
                ig.system.clear(this.clearColor);
            }
            this._rscreen.x = ig.system.getDrawPos(this.screen.x) / ig.system.scale;
            this._rscreen.y = ig.system.getDrawPos(this.screen.y) / ig.system.scale;
            var mapIndex;
            for (mapIndex = 0; mapIndex < this.backgroundMaps.length; mapIndex++) {
                var map = this.backgroundMaps[mapIndex];
                if (map.foreground) {
                    break;
                }
                map.setScreenPos(this.screen.x, this.screen.y);
                map.draw();
            }
            this.drawEntities();
            for (mapIndex; mapIndex < this.backgroundMaps.length; mapIndex++) {
                var map = this.backgroundMaps[mapIndex];
                map.setScreenPos(this.screen.x, this.screen.y);
                map.draw();
            }
        },
        drawEntities: function() {
            for (var i = 0; i < this.entities.length; i++) {
                this.entities[i].draw();
            }
        },
        checkEntities: function() {
            var hash = {};
            for (var e = 0; e < this.entities.length; e++) {
                var entity = this.entities[e];
                if (entity.type == ig.Entity.TYPE.NONE && entity.checkAgainst == ig.Entity.TYPE.NONE && entity.collides == ig.Entity.COLLIDES.NEVER) {
                    continue;
                }
                var checked = {}
                  , xmin = Math.floor(entity.pos.x / this.cellSize)
                  , ymin = Math.floor(entity.pos.y / this.cellSize)
                  , xmax = Math.floor((entity.pos.x + entity.size.x) / this.cellSize) + 1
                  , ymax = Math.floor((entity.pos.y + entity.size.y) / this.cellSize) + 1;
                for (var x = xmin; x < xmax; x++) {
                    for (var y = ymin; y < ymax; y++) {
                        if (!hash[x]) {
                            hash[x] = {};
                            hash[x][y] = [entity];
                        } 
                        else if (!hash[x][y]) {
                            hash[x][y] = [entity];
                        } 
                        else {
                            var cell = hash[x][y];
                            for (var c = 0; c < cell.length; c++) {
                                if (entity.touches(cell[c]) && !checked[cell[c].id]) {
                                    checked[cell[c].id] = true;
                                    ig.Entity.checkPair(entity, cell[c]);
                                }
                            }
                            cell.push(entity);
                        }
                    }
                }
            }
        }
    });
    ig.Game.SORT = {
        Z_INDEX: function(a, b) {
            return a.zIndex - b.zIndex;
        },
        POS_X: function(a, b) {
            return (a.pos.x + a.size.x) - (b.pos.x + b.size.x);
        },
        POS_Y: function(a, b) {
            return (a.pos.y + a.size.y) - (b.pos.y + b.size.y);
        }
    };
});

// lib/game/menus/base.js
ig.baked = true;
ig.module('game.menus.base').requires('impact.font').defines(function() {
    MenuItem = ig.Class.extend({
        getText: function() {
            return 'none'
        },
        left: function() {},
        right: function() {},
        ok: function() {},
        click: function() {
            this.ok();
            ig.system.canvas.style.cursor = 'auto';
        }
    });
    MenuItemBack = MenuItem.extend({
        getText: function() {
            return 'Back To Play';
        },
        ok: function() {
            ig.game.setTitle();
        }
    });
    Menu = ig.Class.extend({
        clearColor: null ,
        name: null ,
        font: new ig.Font('media/fonts/avenir-36-white.png'),
        fontSelected: new ig.Font('media/fonts/avenir-36-orange.png'),
        current: 0,
        itemClasses: [],
        items: [],
        scale: 1,
        alpha: 1,
        init: function() {
            this.width = ig.system.width;
            this.y = ig.system.height / 4 + 160;
            this.font.letterSpacing = -2;
            this.fontSelected.letterSpacing = -2;
            for (var i = 0; i < this.itemClasses.length; i++) {
                this.items.push(new this.itemClasses[i]());
            }
        },
        update: function() {
            if (ig.input.pressed('up')) {
                this.current--;
            }
            if (ig.input.pressed('down')) {
                this.current++;
            }
            this.current = this.current.limit(0, this.items.length - 1);
            if (ig.input.pressed('left')) {
                this.items[this.current].left();
            }
            if (ig.input.pressed('right')) {
                this.items[this.current].right();
            }
            if (ig.input.pressed('ok')) {
                this.items[this.current].ok();
            }
            var ys = this.y;
            var xs = ig.system.width / 2;
            var hoverItem = null ;
            for (var i = 0; i < this.items.length; i++) {
                var item = this.items[i];
                var w = (this.font.widthForString(item.getText()) / 2) * this.scale;
                if (item.y) {
                    ys = item.y;
                }
                if (ig.input.mouse.x > xs - w && ig.input.mouse.x < xs + w && ig.input.mouse.y > (ys - 10) * this.scale && ig.input.mouse.y < (ys + this.font.height * 1.2) * this.scale) {
                    hoverItem = item;
                    this.current = i;
                }
                ys += this.font.height * 1.5;
            }
            if (hoverItem) {
                ig.system.canvas.style.cursor = 'pointer';
                if (ig.input.pressed('click')) {
                    hoverItem.click();
                }
            } 
            else {
                ig.system.canvas.style.cursor = 'auto';
            }
        },
        draw: function() {
            ig.system.context.save();
            if (this.clearColor) {
                ig.system.context.fillStyle = this.clearColor;
                ig.system.context.fillRect(0, 0, ig.system.width, ig.system.height);
            }
            ig.system.context.globalAlpha = this.alpha;
            ig.system.context.scale(this.scale, this.scale);
            var xs = this.width / 2;
            var ys = this.y;
            if (this.name) {
                this.fontTitle.draw(this.name, xs, ys - 160, ig.Font.ALIGN.CENTER);
            }
            for (var i = 0; i < this.items.length; i++) {
                var item = this.items[i];
                var t = item.getText();
                if (item.y) {
                    ys = item.y;
                }
                if (item.alpha) {
                    ig.system.context.globalAlpha = item.alpha * this.alpha;
                }
                if (i == this.current) {
                    this.fontSelected.draw(t, xs, ys, ig.Font.ALIGN.CENTER);
                } 
                else {
                    this.font.draw(t, xs, ys, ig.Font.ALIGN.CENTER);
                }
                ys += this.font.height * 1.5;
                ig.system.context.globalAlpha = 1;
            }
            ig.system.context.restore();
        }
    });
});

// lib/game/menus/about.js
ig.baked = true;
ig.module('game.menus.about').requires('game.menus.base').defines(function() {
    MenuItemRestoreIAP = MenuItem.extend({
        getText: function() {
            return 'Restore In-App Purchases';
        },
        ok: function() {
            ig.game.restoreIAP();
        }
    });
    MenuAbout = Menu.extend({
        background: new ig.Image('media/background/stars.jpg'),
        credits: new ig.Image('media/title/credits.png'),
        scale: 1,
        scroll: 0,
        itemClasses: [MenuItemRestoreIAP, MenuItemBack],
        init: function() {
            if (!window.Ejecta) {
                this.itemClasses.shift();
            }
            this.parent();
            this.current = 1;
            this.width = ig.system.width / this.scale;
            this.items[0].y = (ig.system.height - 130) / this.scale;
            if (this.items.length > 1) {
                this.items[1].y = (ig.system.height - 80) / this.scale;
            }
        },
        draw: function() {
            var ctx = ig.system.context;
            ctx.save();
            ctx.scale(this.scale, this.scale);
            this.scroll += ig.system.tick * 10;
            this.scroll = this.scroll % this.background.height;
            this.background.draw(0, this.scroll - this.background.height);
            this.background.draw(0, this.scroll);
            ctx.restore();
            //this.credits.draw(32, 32);
            this.parent();
        }
    });
});

// lib/game/menus/interstitial.js
ig.baked = true;
ig.module('game.menus.interstitial').requires('game.menus.base').defines(function() {
    MenuItemSkip = MenuItem.extend({
        getText: function() {
            return 'skip';
        },
        ok: function() {
            ig.game.setTitle();
        }
    });
    MenuInterstitial = Menu.extend({
        itemClasses: [MenuItemSkip],
        scale: 0.75,
        banner: new ig.Image('media/nfn-phoboslab-works.png'),
        init: function() {
            this.parent();
            this.y = (ig.system.height - 130) / this.scale;
            this.width = ig.system.width / this.scale;
            this.timer = new ig.Timer();
            if (!MenuInterstitial.onclickInstalled) {
                MenuInterstitial.onclickInstalled = true;
                ig.system.canvas.addEventListener('click', function(event) {
                    if (ig.game && ig.game.menu && ig.game.menu.onclick) {
                        ig.game.menu.onclick(event);
                    }
                });
            }
        },
        update: function() {
            this.parent();
            var bx = ((ig.system.width - this.banner.width) / 2)
              , by = 64
              , bw = this.banner.width
              , bh = this.banner.height;
            if (ig.input.mouse.x > bx && ig.input.mouse.x < bx + bw && ig.input.mouse.y > by && ig.input.mouse.y < by + bh) {
                ig.system.canvas.style.cursor = 'pointer';
            }
        },
        onclick: function(event) {
            var internalWidth = parseInt(ig.system.canvas.offsetWidth) || ig.system.realWidth;
            var scale = ig.system.scale * (internalWidth / ig.system.realWidth);
            var pos = {
                left: 0,
                top: 0
            };
            if (ig.system.canvas.getBoundingClientRect) {
                pos = ig.system.canvas.getBoundingClientRect();
            }
            var ev = event.touches ? event.touches[0] : event;
            var x = (ev.clientX - pos.left) / scale;
            var y = (ev.clientY - pos.top) / scale;
            var bx = ((ig.system.width - this.banner.width) / 2)
              , by = 64
              , bw = this.banner.width
              , bh = this.banner.height;
            if (x > bx && x < bx + bw && y > by && y < by + bh) {
                window.open('#');
            }
        },
        draw: function() {
            this.parent();
            var x = (ig.system.width - this.banner.width) / 2
              , y = 64;
            this.banner.draw(x, y);
            var d = this.timer.delta();
            if (d < 0.3) {
                ig.system.context.globalAlpha = d.map(0, 0.3, 1, 0);
                ig.system.context.fillStyle = '#fff';
                ig.system.context.fillRect(x, y, this.banner.width, this.banner.height);
                ig.system.context.globalAlpha = 1;
            }
        }
    });
});

// lib/game/xhr.js
ig.baked = true;
ig.module('game.xhr').defines(function() {
    ig.xhr = function(url, data, callback) {
        var post = [];
        if (data) {
            for (var key in data) {
                post.push(key + '=' + encodeURIComponent(data[key]));
            }
        }
        var postString = post.join('&');
        var xhr = new XMLHttpRequest();
        if (callback) {
            xhr.onreadystatechange = function() {
                if (xhr.readyState == 4) {
                    //callback(JSON.parse(xhr.responseText));
                }
            }
            ;
        }
        xhr.open('POST', url);
        xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
        xhr.send(postString);
    }
    ;
});

// lib/game/menus/stats.js
ig.baked = true;
ig.module('game.menus.stats').requires('game.menus.base', 'game.xhr').defines(function() {
    StatsView = ig.Class.extend({
        data: null ,
        workingIndicator: ['', '.', '..', '...'],
        font: new ig.Font('media/fonts/avenir-36-white.png'),
        timer: null ,
        width: 432,
        height: 216,
        isExcerpt: false,
        totalGames: 0,
        init: function(width, height) {
            this.width = width;
            this.height = height;
            this.timer = new ig.Timer();
        },
        submit: function(data) {
            if (window.Cocoon || window.Ejecta) {
                var games = JSON.parse(localStorage.getItem('stats') || '[]');
                if (games.length > 30) {
                    games.shift();
                }
                data.created = (Date.now() / 1000) | 0;
                games.push(data);
                localStorage.setItem('stats', JSON.stringify(games));
                this.receivedData({
                    games: games
                });
            } 
            else {
                ig.xhr('backend.ashx', data, this.receivedData.bind(this));
            }
        },
        load: function() {
            if (window.Cocoon || window.Ejecta) {} 
            else {
                ig.xhr('backend/', {
                    load: true
                }, this.receivedData.bind(this));
            }
        },
        receivedData: function(response) {
            this.data = response.games;
            this.maxScore = 0;
            for (var i = 0; i < this.data.length; i++) {
                var d = this.data[i];
                d.created = new Date(d.created * 1000);
                this.maxScore = Math.max(this.maxScore, d.score);
            }
            this.isExcerpt = response.isExcerpt;
            this.totalGames = response.totalGames;
            this.scoreScale = this.roundToMagnitude(this.maxScore);
            this.timer.reset();
        },
        roundToMagnitude: function(n) {
            var mag = this.magnitude(n);
            return Math.ceil((n * 10) / mag) * mag / 10;
        },
        magnitude: function(n) {
            return Math.pow(10, Math.floor(Math.log(n) / Math.LN10));
        },
        draw: function(x, y) {
            if (!this.data) {
                this.drawLoading(x, y);
            } 
            else {
                this.drawGraph(x, y);
            }
        },
        drawLoading: function(x, y) {
            var scale = 0.5;
            var ctx = ig.system.context;
            ctx.save();
            ctx.scale(scale, scale);
            ctx.globalAlpha = 0.5;
            var indicator = this.workingIndicator[((this.timer.delta() * 4) | 0) % this.workingIndicator.length];
            this.font.draw('LOADING STATS  ' + indicator, x / scale, y / scale);
            ctx.restore();
        },
        drawGraph: function(x, y) {
            if (!this.data.length) {
                return;
            }
            var ctx = ig.system.context;
            var scale = 0.4;
            ctx.fillStyle = '#4dfed2';
            ctx.strokeStyle = 'rgba(255,255,255,0.1)';
            ctx.lineWidth = 1;
            if (this.isExcerpt) {
                ctx.save();
                ctx.scale(scale, scale);
                ctx.globalAlpha = 0.5;
                this.font.draw(('(trend over ' + this.totalGames + ' games)'), x / scale, (y + this.height) / scale, ig.Font.ALIGN.LEFT);
                ctx.restore();
            }
            var numLabels = 3;
            var rw = this.width - 32;
            for (var i = 0; i < numLabels; i++) {
                ctx.save();
                ctx.scale(scale, scale);
                ctx.globalAlpha = 0.8;
                var v = 1 / (numLabels - 1) * i;
                var label = Math.round(v * this.scoreScale);
                var xp = x + this.width
                  , yp = y + this.height - this.height * v;
                this.font.draw(label, xp / scale, (yp - this.font.height * scale / 2) / scale, ig.Font.ALIGN.RIGHT);
                ctx.restore();
                ctx.beginPath();
                ctx.moveTo(x, yp);
                ctx.lineTo(x + rw, yp);
                ctx.stroke();
            }
            ctx.strokeStyle = 'rgba(255,255,255,0.2)';
            ctx.lineWidth = 2;
            var lastp = null ;
            var max = Math.min((this.data.length * this.timer.delta()), this.data.length);
            for (var i = 0; i < max; i++) {
                var d = this.data[i];
                var v = 1 / (this.data.length - 1) * i || 0;
                var rx = x + (rw - 3) * v
                  , ry = y + this.height - (this.height - 3) * (d.score / this.scoreScale);
                ctx.fillRect(rx, ry, 3, 3);
                if (lastp) {
                    ctx.beginPath();
                    ctx.moveTo(lastp.x + 1.5, lastp.y + 1.5);
                    ctx.lineTo(rx + 1.5, ry + 1.5)
                    ctx.stroke();
                }
                lastp = {
                    x: rx,
                    y: ry
                };
            }
        }
    });
});

// lib/game/menus/game-over.js
ig.baked = true;
ig.module('game.menus.game-over').requires('game.menus.base', 'game.menus.interstitial', 'game.menus.stats').defines(function() {
    MenuItemInterstitial = MenuItem.extend({
        getText: function() {
            return 'back to title';
        },
        ok: function() {
            ig.game.menu = new MenuInterstitial();
        }
    });
    MenuGameOver = Menu.extend({
        itemClasses: [MenuItemBack],
        scale: 0.75,
        personalBestBadge: new ig.Image('media/ui/personal-best-badge.png'),
        fontTitle: new ig.Font('media/fonts/avenir-36-blue.png'),
        separatorBar: new ig.Image('media/ui/bar-blue.png'),
        init: function() {
            var lastBannerTime = parseInt(localStorage.getItem('bannerTime')) || 0;
            if (lastBannerTime < Date.now() / 1000 - 24 * 60 * 60) {
                localStorage.setItem('bannerTime', (Date.now() / 1000) | 0);
                this.itemClasses[0] = MenuItemInterstitial;
            }
            this.parent();
            this.y = (ig.system.height - 130) / this.scale;
            this.width = ig.system.width / this.scale;
            this.stats = new StatsView(432,ig.system.height - 500);
            this.stats.submit({
                score: ig.game.score,
                wave: ig.game.wave.wave,
                streak: ig.game.longestStreak,
                accuracy: ig.game.hits ? ig.game.hits / (ig.game.hits + ig.game.misses) * 100 : 0
            });
            this.timer = new ig.Timer();
        },
        update: function() {
            if (this.timer.delta() > 1.5) {
                this.parent();
            }
        },
        draw: function() {
            this.parent();
            var xs = ig.system.width / 2;
            var ys = 25;
            var acc = ig.game.hits ? ig.game.hits / (ig.game.hits + ig.game.misses) * 100 : 0;
            var ctx = ig.system.context;
            if (ig.game.isPersonalBest) {
                ctx.save();
                ctx.scale(0.5, 0.5);
                this.personalBestBadge.draw(24 / 0.5, 275 / 0.5);
                this.font.draw('NEW PERSONAL BEST', 60 / 0.5, 280 / 0.5, ig.Font.ALIGN.LEFT);
                ctx.restore();
            }
            var ss = 0.5;
            ctx.save();
            ctx.scale(ss, ss);
            ctx.globalAlpha = 0.5;
            this.font.draw('FINAL SCORE', 24 / ss, (ys + 0) / ss);
            this.font.draw('YOU REACHED', 252 / ss, (ys + 0) / ss);
            this.font.draw('ACCURACY', 24 / ss, (ys + 140) / ss);
            this.font.draw('LONGEST STREAK', 252 / ss, (ys + 140) / ss);
            ctx.restore();
            this.fontTitle.draw(ig.game.score.zeroFill(6), 24, ys + 25);
            this.fontTitle.draw('WAVE ' + ig.game.wave.wave.zeroFill(3), 252, ys + 25);
            ig.system.context.drawImage(this.separatorBar.data, 24, ys + 70, 432, 2);
            this.fontTitle.draw(acc.round(1) + '%', 24, ys + 165);
            this.fontTitle.draw(ig.game.longestStreak, 252, ys + 165);
            ig.system.context.drawImage(this.separatorBar.data, 24, ys + 210, 432, 2);
            if (this.stats) {
                this.stats.draw(24, ys + 300);
            }
        }
    });
});

// lib/game/menus/pause.js
ig.baked = true;
ig.module('game.menus.pause').requires('game.menus.base').defines(function() {
    MenuItemSoundVolume = MenuItem.extend({
        getText: function() {
            return ' -  ' + (ig.soundManager.volume * 100).round() + '%  + ';
        },
        left: function() {
            ig.soundManager.volume = (ig.soundManager.volume - 0.1).limit(0, 1);
        },
        right: function() {
            ig.soundManager.volume = (ig.soundManager.volume + 0.1).limit(0, 1);
        },
        click: function() {
            if (ig.input.mouse.x > 240) {
                this.right();
            } else {
                this.left();
            }
        }
    });
    MenuItemMusicVolume = MenuItem.extend({
        getText: function() {
            return ' -  ' + (ig.music.volume * 100).round() + '%  + ';
        },
        left: function() {
            ig.music.volume = (ig.music.volume - 0.1).limit(0, 1);
        },
        right: function() {
            ig.music.volume = (ig.music.volume + 0.1).limit(0, 1);
        },
        click: function() {
            if (ig.input.mouse.x > 240) {
                this.right();
            } else {
                this.left();
            }
        }
    });
    MenuItemResume = MenuItem.extend({
        getText: function() {
            return 'resume';
        },
        ok: function() {
            localStorage.setItem('soundVolume', ig.soundManager.volume);
            localStorage.setItem('musicVolume', ig.music.volume);
            ig.game.menu = null ;
        }
    });
    MenuPause = Menu.extend({
        scale: 0.8,
        clearColor: 'rgba(0,0,0,0.8)',
        init: function() {
            this.parent();
            this.y = ig.system.height / 3;
            this.items[0].y = 230 / this.scale;
            this.items[1].y = 360 / this.scale;
            this.items[2].y = 500 / this.scale;
            this.items[3].y = 560 / this.scale;
        },
        itemClasses: [MenuItemSoundVolume, MenuItemMusicVolume, MenuItemResume, MenuItemBack],
        draw: function() {
            this.width = ig.system.width / this.scale;
            var ctx = ig.system.context;
            this.parent();
            var s = 0.85;
            ctx.save();
            ctx.scale(s, s);
            ctx.globalAlpha = 0.5;
            this.font.draw('GAME PAUSED', ig.system.width / 2 / s, 60 / s, ig.Font.ALIGN.CENTER);
            ctx.restore();
            ctx.save();
            s = 0.5;
            ctx.scale(s, s);
            ctx.globalAlpha = 0.5;
            this.font.draw('SOUND', ig.system.width / 2 / s, 200 / s, ig.Font.ALIGN.CENTER);
            this.font.draw('MUSIC', ig.system.width / 2 / s, 330 / s, ig.Font.ALIGN.CENTER);
            ctx.restore();
        }
    });
});

// lib/game/menus/detailed-stats.js
ig.baked = true;
ig.module('game.menus.detailed-stats').requires('game.menus.base', 'game.xhr').defines(function() {
    DetailedStats = ig.Class.extend({
        container: null ,
        closeButton: null ,
        chart: null ,
        maxScore: 0,
        games: [],
        scoreScale: 1,
        init: function() {
            this.container = ig.$('#ztype-stats');
            this.closeButton = ig.$('#ztype-stats-close');
            this.content = ig.$('#ztype-stats-content');
            this.canvas = ig.$('#ztype-stats-canvas');
            this.context = this.canvas.getContext('2d');
        },
        show: function() {
            this.container.style.height = ig.system.canvas.clientHeight + 'px';
            this.container.style.display = 'block';
            this.content.style.display = 'none';
            var that = this;
            setTimeout(function() {
                that.container.className = 'expanded';
                setTimeout(function() {
                    that.content.style.display = 'block';
                    ig.xhr('backend/', {
                        loadAll: true
                    }, that._initChart.bind(that));
                }, 400);
            }, 16);
            this.closeBound = this.close.bind(this);
            this.drawBound = this.draw.bind(this);
            this.closeButton.addEventListener('click', this.closeBound);
            this.shareFacebookBound = this.shareFacebook.bind(this);
            ig.$('#ztype-stats-share-facebook').addEventListener('click', this.shareFacebookBound);
            this.shareTwitterBound = this.shareTwitter.bind(this);
            ig.$('#ztype-stats-share-twitter').addEventListener('click', this.shareTwitterBound);
        },
        _initChart: function(response) {
            this.resize();
            for (var i = 0; i < response.games.length; i++) {
                var g = response.games[i];
                g.created = new Date(g.created * 1000);
                this.maxScore = Math.max(this.maxScore, g.score);
            }
            this.scoreScale = this.roundToMagnitude(this.maxScore);
            this.timer = new ig.Timer();
            this.data = response.games;
            ig.system.stopRunLoop();
            this.draw();
            this.canvas.addEventListener('mousemove', this.drawBound);
        },
        close: function() {
            ig.system.startRunLoop();
            ig.$('#ztype-stats-share-facebook').removeEventListener('click', this.shareFacebookBound);
            ig.$('#ztype-stats-share-twitter').removeEventListener('click', this.shareTwitterBound);
            this.closeButton.removeEventListener('click', this.closeBound);
            this.canvas.removeEventListener('mousemove', this.drawBound);
            this.container.className = '';
            this.content.style.display = 'none';
            var that = this;
            setTimeout(function() {
                that.container.style.display = 'none';
            }, 300);
        },
        resize: function() {
            var nw = this.content.clientWidth
              , nh = this.content.clientHeight - 64;
            if (this.canvas.width !== nw || this.canvas.height !== nh) {
                this.canvas.width = nw;
                this.canvas.height = nh;
            }
        },
        roundToMagnitude: function(n) {
            var mag = this.magnitude(n);
            return Math.ceil((n * 10) / mag) * mag / 10;
        },
        magnitude: function(n) {
            return Math.pow(10, Math.floor(Math.log(n) / Math.LN10));
        },
        draw: function(ev) {
            this.resize();
            var mx = -100
              , my = -100;
            if (ev) {
                var pos = this.canvas.getBoundingClientRect();
                mx = ev.clientX - pos.left;
                my = ev.clientY - pos.top;
            }
            this.drawOnCanvas(this.canvas, mx, my);
        },
        drawOnCanvas: function(canvas, mx, my) {
            var width = canvas.width
              , height = canvas.height - 128;
            var ctx = canvas.getContext('2d');
            ctx.fillStyle = '#fff';
            ctx.fillRect(0, 0, canvas.width, canvas.height);
            if (!this.data.length) {
                ctx.fillStyle = '#555';
                ctx.font = '28px sans-serif';
                ctx.textAlign = 'center';
                ctx.fillText('NO STATS FOUND – PLAY SOME MORE', width / 2, height / 2);
                ctx.textAlign = 'left';
                return;
            }
            var x = 32
              , y = 64;
            var numLabels = 8;
            var rw = width - 84;
            ctx.fillStyle = '#555';
            ctx.font = '28px sans-serif';
            ctx.fillText('ZTYPE', 32, 38);
            ctx.fillStyle = '#ccc';
            ctx.fillText('SCORES OVER ' + this.data.length + ' GAMES', 140, 38);
            ctx.font = '10px sans-serif';
            ctx.textAlign = 'right';
            ctx.strokeStyle = '#f0f0f0';
            ctx.lineWidth = 1;
            for (var i = 0; i < numLabels; i++) {
                var v = 1 / (numLabels - 1) * i;
                var label = Math.round(v * this.scoreScale);
                var xp = x + rw + 32
                  , yp = y + height - height * v;
                ctx.fillStyle = '#555';
                ctx.fillText(label, xp, yp + 3)
                ctx.beginPath();
                ctx.moveTo(x, yp);
                ctx.lineTo(x + rw, yp);
                ctx.stroke();
            }
            ctx.textAlign = 'left';
            ctx.fillStyle = '#4dfed2';
            ctx.strokeStyle = 'rgba(0,0,0,0.2)';
            ctx.lineWidth = 1;
            var lastp = null ;
            var plot = null ;
            var md = 15;
            for (var i = 0; i < this.data.length; i++) {
                var g = this.data[i];
                var v = 1 / (this.data.length - 1) * i || 0;
                var rx = x + (rw - 3) * v
                  , ry = y + height - (height - 3) * (g.score / this.scoreScale);
                ctx.fillRect(rx, ry, 3, 3);
                if (lastp) {
                    ctx.beginPath();
                    ctx.moveTo(lastp.x + 1.5, lastp.y + 1.5);
                    ctx.lineTo(rx + 1.5, ry + 1.5)
                    ctx.stroke();
                }
                lastp = {
                    x: rx,
                    y: ry
                };
                if (mx < rx + md && mx > rx - md && my < ry + md && my > ry - md) {
                    var dx = Math.abs(mx - rx)
                      , dy = Math.abs(my - ry);
                    var dist = dx * dx + dy * dy;
                    if (!plot || plot.dist > dist) {
                        plot = {
                            x: rx,
                            y: ry,
                            game: g,
                            dist: dist
                        };
                    }
                }
            }
            if (plot) {
                var pw = 290;
                ctx.fillStyle = '#4dfed2';
                ctx.fillRect(plot.x - 2, plot.y - 2, 7, 7);
                var bx = plot.x > width - pw - 10 ? plot.x - pw - 7 : plot.x + 9
                  , by = plot.y - 32;
                ctx.fillStyle = 'rgba(255,255,255,0.8)';
                ctx.fillRect(bx, by, pw, 64);
                var date = plot.game.created.toLocaleDateString('en-US', {
                    year: 'numeric',
                    month: 'short',
                    day: 'numeric',
                    hour: '2-digit',
                    minute: '2-digit'
                });
                ctx.fillStyle = '#aaa';
                ctx.font = '10px sans-serif'
                ctx.fillText(date, bx + 16, by + 18);
                var scdesc = 'SCORE ' + plot.game.score;
                ctx.fillStyle = '#555';
                ctx.font = '12px sans-serif'
                ctx.fillText(scdesc, bx + 16, by + 36);
                var desc = 'WAVE ' + plot.game.wave + ' / ACCURACY ' + plot.game.accuracy.toFixed(2) + '%' + ' / STREAK ' + plot.game.streak;
                ctx.fillStyle = '#555';
                ctx.font = '12px sans-serif'
                ctx.fillText(desc, bx + 16, by + 52);
            }
        },
        saveScoresScreenshot: function(callback) {
            var offscreen = ig.$new('canvas');
            offscreen.width = 1280;
            offscreen.height = 720;
            this.drawOnCanvas(offscreen, -100, -100);
            var ctx = offscreen.getContext('2d');
            ctx.fillStyle = '#ccc';
            ctx.font = '28px sans-serif';
            ctx.fillText(' –  ', 515, 38);
            var png = offscreen.toDataURL('image/png');
            ig.xhr('backend/', {
                saveScreenshot: false,
                data: png
            }, callback);
        },
        shareFacebook: function() {
            
        },
        shareTwitter: function() {
           
        }
    });
});

// lib/game/ease.js
ig.baked = true;
ig.module('game.ease').defines(function() {
    ig.ease = {
        inOutQuad: function(time, start, c, duration) {
            if ((time /= duration / 2) < 1)
                return c / 2 * time * time + start;
            return -c / 2 * ((--time) * (time - 2) - 1) + start;
        },
        inOutBack: function(t, b, c, d, s) {
            if (s == undefined)
                s = 1.70158;
            if ((t /= d / 2) < 1)
                return c / 2 * (t * t * (((s *= (1.525)) + 1) * t - s)) + b;
            return c / 2 * ((t -= 2) * t * (((s *= (1.525)) + 1) * t + s) + 2) + b;
        },
        inBounce: function(t, b, c, d) {
            return c - ig.ease.outBounce(d - t, 0, c, d) + b;
        },
        outBounce: function(t, b, c, d) {
            if ((t /= d) < (1 / 2.75)) {
                return c * (7.5625 * t * t) + b;
            } else if (t < (2 / 2.75)) {
                return c * (7.5625 * (t -= (1.5 / 2.75)) * t + .75) + b;
            } else if (t < (2.5 / 2.75)) {
                return c * (7.5625 * (t -= (2.25 / 2.75)) * t + .9375) + b;
            } else {
                return c * (7.5625 * (t -= (2.625 / 2.75)) * t + .984375) + b;
            }
        },
        inOutBounce: function(t, b, c, d) {
            if (t < d / 2)
                return ig.ease.inBounce(t * 2, 0, c, d) * .5 + b;
            return ig.ease.outBounce(t * 2 - d, 0, c, d) * .5 + c * .5 + b;
        }
    };
});

// lib/game/menus/title.js
ig.baked = true;
ig.module('game.menus.title').requires('game.menus.base', 'game.menus.detailed-stats', 'game.ease').defines(function() {
    MenuItemNormalMode = MenuItem.extend({
        getText: function() {
            return " P L A Y ";
        },
        ok: function() {
            ig.game.setGame();
        },
    });

    MenuTitle = Menu.extend({
        itemClasses: [],
        scale: 0.75,
        y: 0,
        init: function() {
            this.itemClasses = [MenuItemNormalMode];
            
            this.parent();
            this.items[0].y = 740;
            this.items[0].alpha = 0.9;
            if (this.items.length > 1) {
                this.items[1].y = ig.system.height / this.scale - 40;
                this.items[1].alpha = 0.9;
                if (this.items.length > 2) {
                    this.items[2].y = ig.system.height / this.scale - 90;
                    this.items[2].alpha = 0.9;
                }
            }

            console.log(this.items);
            this.width = ig.system.width / this.scale;
            this.playerPos.x = (ig.system.width - this.ship.width) / 2;
            this.playerPos.y = 400;
        },
        scroll: 0,
        background: new ig.Image('media/background/stars.jpg'),
        ztype: new ig.Image('media/title/ztype.png'),
        phoboslab: new ig.Image('media/title/phoboslab.png'),
        ship: new ig.Image('media/title/ship.png'),
        exhaust: new ig.Image('media/title/exhaust.png'),
        infoIcon: new ig.Image('media/ui/information.png'),
        glitchStripe: new ig.Image('media/title/glitch-stripe.png'),
        glitchCode: new ig.Image('media/title/glitch-code.png'),
        glitchJPEG: new ig.Image('media/title/glitch-jpeg.png'),
        glitchGraph: new ig.Image('media/title/glitch-graph.png'),
        glitchLog: new ig.Image('media/title/glitch-log.png'),
        playerPos: {
            x: 200,
            y: 400
        },
        transition: 0,
        draw: function() {
            var ctx = ig.system.context;			
            ctx.globalAlpha = this.alpha + Math.sin(this.transition * Math.PI) * (1 - Math.random() * 0.5);
            ctx.save();
            ctx.scale(this.scale, this.scale);
            this.scroll += ig.system.tick * 10;
            this.scroll = this.scroll % this.background.height;
            this.background.draw(0, this.scroll - this.background.height);
            this.background.draw(0, this.scroll);
            this.phoboslab.draw((ig.system.width / this.scale - this.phoboslab.width) / 2, 80 - ig.ease.inOutQuad(this.transition, 0, 2800, 1));
            this.ztype.draw((ig.system.width / this.scale - this.ztype.width) / 2, 150 - ig.ease.inOutQuad(this.transition, 0, 1000, 1));
            ctx.globalAlpha = Math.sin(this.transition * Math.PI);
            ctx.globalCompositeOperation = 'lighter';
            this.glitchStripe.draw(0, 4000 - this.transition * 4200);
            ctx.restore();
            var i0y = Math.min(740, ig.system.height / this.scale - 210);
            this.items[0].y = ig.ease.inOutQuad(this.transition, i0y, 1600, 1) - 50;
            if (this.items.length > 1) {
                this.items[1].y = ig.ease.inOutQuad(this.transition, ig.system.height / this.scale - 160, 1900, 1) - 50;
            }
            if (this.items.length > 2) {
                this.items[2].y = ig.ease.inOutQuad(this.transition, ig.system.height / this.scale - 100, 2100, 1) - 50;
            }
            this.ship.draw(this.playerPos.x - 1, this.playerPos.y - 7);
            for (var i = 0; i < 10; i++) {
                var ey = (i * 139 + ig.Timer.time * 53 * (1 + i / 30)) % 400;
                ig.system.context.globalAlpha = (ey < 20 ? (ey / 20) * 0.7 : Math.max(0, 0.7 - (ey / 400))) * 0.3;
                this.exhaust.draw(this.playerPos.x - 1 + Math.sin(ig.Timer.time + ey / 30) * 0.7, this.playerPos.y + ey);
            }
            ig.system.context.globalAlpha = 1;
            this.parent();
            ctx.globalCompositeOperation = 'lighter';
            ctx.globalAlpha = Math.random();
            if (this.transition > 0.55 && this.transition < 0.62) {
                this.glitchGraph.draw(ig.system.width - this.glitchGraph.width - Math.random() * 10, 10);
            }
            if ((this.transition > 0.6 && this.transition < 0.7) || (this.transition > 0.3 && this.transition < 0.33)) {
                this.glitchCode.draw(0 + Math.random() * 20, 0);
            }
            if (this.transition > 0.65 && this.transition < 0.75) {
                this.glitchLog.draw(20 + Math.random() * 20, 400);
            }
            if (this.transition > 0.8 && this.transition < 0.85) {
                this.glitchJPEG.draw(ig.system.width - this.glitchJPEG.width, 300 + Math.random() * 100);
            }
            ctx.globalAlpha = Math.max(0.4 - this.transition, 0) + (this.infoIcon.hovered ? 0.3 : 0);
            //this.infoIcon.draw(ig.system.width - 48, ig.system.height - 48);
            ctx.globalAlpha = 1;
            ctx.globalCompositeOperation = 'source-over';
        },
        update: function() {
            if (ig.game.gameTransitionTimer) {
                return;
            }
            if (ig.input.mouse.y < 104 && ig.input.pressed('click')) {
                return;
            }
            this.parent();
            if (ig.input.mouse.x > ig.system.width - 64 && ig.input.mouse.y > ig.system.height - 64) {
                if (ig.input.pressed('click')) {
                    ig.game.menu = new MenuAbout();
                    ig.game.menu.scroll = this.scroll;
                    return;
                }
                this.infoIcon.hovered = true;
                ig.system.canvas.style.cursor = 'pointer';
            } 
            else {
                this.infoIcon.hovered = false;
            }
        }
    });
});

// lib/game/avenir-next.js
ig.baked = true;
ig.module('game.avenir-next').requires('impact.font').defines(function() {
    var METRICS = [0, 0, 1, 1, -29, -21, 5, 2, 0, 4, 15, 1, 2, 8, 7, 0, 6, 5, 3, 2, 11, 14, 0, 12, 15, 0, 2, 11, 27, 0, 11, 19, 0, 0, 11, 39, 0, 17, 17, 0, 1, 17, 57, 0, 14, 15, 1, 2, 15, 72, 0, 3, 5, 1, 2, 5, 76, 0, 6, 19, 0, 1, 5, 83, 0, 5, 19, 0, 1, 5, 89, 0, 10, 8, 0, 2, 9, 100, 0, 12, 11, 0, 5, 14, 113, 0, 5, 5, 0, 15, 5, 119, 0, 7, 2, 0, 11, 7, 127, 0, 4, 2, 1, 15, 5, 132, 0, 10, 17, -1, 1, 8, 143, 0, 11, 15, 0, 2, 11, 155, 0, 7, 15, 1, 2, 11, 163, 0, 11, 15, 0, 2, 11, 175, 0, 11, 15, 0, 2, 11, 187, 0, 11, 15, 0, 2, 11, 199, 0, 11, 15, 0, 2, 11, 211, 0, 11, 15, 0, 2, 11, 223, 0, 10, 15, 0, 2, 11, 234, 0, 11, 15, 0, 2, 11, 246, 0, 11, 15, 0, 2, 11, 258, 0, 4, 10, 1, 7, 5, 263, 0, 5, 13, 0, 7, 5, 269, 0, 12, 11, 0, 5, 14, 282, 0, 12, 4, 0, 9, 14, 295, 0, 12, 11, 0, 5, 14, 308, 0, 10, 15, 0, 2, 10, 319, 0, 17, 15, 0, 2, 17, 337, 0, 16, 15, -1, 2, 14, 354, 0, 12, 15, 1, 2, 13, 367, 0, 15, 15, 0, 2, 15, 383, 0, 15, 15, 0, 2, 16, 399, 0, 11, 15, 0, 2, 12, 411, 0, 11, 15, 0, 2, 12, 423, 0, 15, 15, 0, 2, 16, 439, 0, 13, 15, 1, 2, 15, 453, 0, 3, 15, 1, 2, 5, 457, 0, 10, 15, -1, 2, 10, 468, 0, 13, 15, 1, 2, 13, 482, 0, 10, 15, 1, 2, 11, 493, 0, 17, 15, 1, 2, 19, 0, 21, 14, 15, 0, 2, 16, 15, 21, 18, 15, 0, 2, 18, 34, 21, 11, 15, 0, 2, 12, 46, 21, 18, 15, 0, 2, 18, 65, 21, 12, 15, 0, 2, 12, 78, 21, 12, 15, 0, 2, 12, 91, 21, 13, 15, 0, 2, 12, 105, 21, 13, 15, 0, 2, 14, 119, 21, 15, 15, -1, 2, 12, 135, 21, 22, 15, -1, 2, 20, 158, 21, 15, 15, -1, 2, 13, 174, 21, 14, 15, -1, 2, 12, 189, 21, 12, 15, 0, 2, 12, 202, 21, 5, 18, 1, 2, 5, 208, 21, 10, 17, -1, 1, 8, 219, 21, 5, 18, 0, 2, 5, 225, 21, 12, 10, 0, 2, 14, 238, 21, 13, 1, -1, 19, 11, 252, 21, 6, 3, -1, 2, 5, 259, 21, 10, 10, 0, 7, 11, 270, 21, 12, 16, 1, 1, 13, 283, 21, 11, 10, 0, 7, 10, 295, 21, 13, 16, 0, 1, 13, 309, 21, 12, 10, 0, 7, 12, 322, 21, 8, 16, 0, 1, 6, 331, 21, 13, 15, 0, 7, 13, 345, 21, 10, 16, 0, 1, 12, 356, 21, 4, 15, 1, 2, 5, 361, 21, 6, 20, -1, 2, 5, 368, 21, 10, 16, 0, 1, 10, 379, 21, 3, 16, 1, 1, 5, 383, 21, 16, 10, 0, 7, 18, 400, 21, 10, 10, 0, 7, 12, 411, 21, 12, 10, 0, 7, 12, 424, 21, 12, 15, 1, 7, 13, 437, 21, 13, 15, 0, 7, 13, 451, 21, 7, 10, 1, 7, 7, 459, 21, 9, 10, 0, 7, 9, 469, 21, 8, 13, 0, 4, 7, 478, 21, 10, 10, 0, 7, 12, 489, 21, 11, 10, 0, 7, 10, 0, 43, 16, 10, 0, 7, 15, 17, 43, 12, 10, -1, 7, 10, 30, 43, 11, 15, 0, 7, 10, 42, 43, 9, 10, 0, 7, 9, 52, 43, 6, 18, 1, 2, 7, 59, 43, 3, 21, 1, 1, 5, 63, 43, 7, 18, 0, 2, 7, 71, 43, 12, 3, 0, 9, 14, 84, 43, 1, 1, -31, -21, 0, 86, 43, 1, 1, -31, -21, 0, 88, 43, 1, 1, -31, -21, 0, 90, 43, 1, 1, -31, -21, 0, 92, 43, 1, 1, -31, -21, 0, 94, 43, 1, 1, -31, -21, 0, 96, 43, 1, 1, -31, -21, 0, 98, 43, 1, 1, -31, -21, 0, 100, 43, 1, 1, -31, -21, 0, 102, 43, 1, 1, -31, -21, 0, 104, 43, 1, 1, -31, -21, 0, 106, 43, 1, 1, -31, -21, 0, 108, 43, 1, 1, -31, -21, 0, 110, 43, 1, 1, -31, -21, 0, 112, 43, 1, 1, -31, -21, 0, 114, 43, 1, 1, -31, -21, 0, 116, 43, 1, 1, -31, -21, 0, 118, 43, 1, 1, -31, -21, 0, 120, 43, 1, 1, -31, -21, 0, 122, 43, 1, 1, -31, -21, 0, 124, 43, 1, 1, -31, -21, 0, 126, 43, 1, 1, -31, -21, 0, 128, 43, 1, 1, -31, -21, 0, 130, 43, 1, 1, -31, -21, 0, 132, 43, 1, 1, -31, -21, 0, 134, 43, 1, 1, -31, -21, 0, 136, 43, 1, 1, -31, -21, 0, 138, 43, 1, 1, -31, -21, 0, 140, 43, 1, 1, -31, -21, 0, 142, 43, 1, 1, -31, -21, 0, 144, 43, 1, 1, -31, -21, 0, 146, 43, 1, 1, -31, -21, 0, 148, 43, 1, 1, -31, -21, 0, 150, 43, 1, 1, -29, -21, 5, 152, 43, 4, 15, 1, 7, 8, 157, 43, 11, 14, 0, 5, 11, 169, 43, 11, 15, 1, 2, 11, 181, 43, 13, 11, -1, 4, 11, 195, 43, 13, 15, -1, 2, 11, 209, 43, 3, 18, 1, 3, 5, 213, 43, 10, 18, 1, 2, 11, 224, 43, 8, 2, -1, 2, 5, 233, 43, 17, 15, 0, 2, 17, 251, 43, 8, 6, 0, 2, 7, 260, 43, 9, 9, 0, 8, 10, 270, 43, 12, 6, 0, 9, 14, 283, 43, 1, 1, -31, -21, 0, 285, 43, 17, 15, 0, 2, 17, 303, 43, 7, 1, -1, 3, 5, 311, 43, 8, 6, 0, 2, 8, 320, 43, 12, 11, 0, 5, 14, 333, 43, 8, 9, 0, 2, 7, 342, 43, 8, 9, 0, 2, 7, 351, 43, 5, 3, 1, 2, 5, 357, 43, 10, 15, 0, 7, 12, 368, 43, 12, 18, 0, 2, 13, 381, 43, 4, 2, 1, 9, 5, 386, 43, 6, 5, 0, 17, 5, 393, 43, 5, 9, 1, 2, 7, 399, 43, 8, 6, 0, 2, 8, 408, 43, 9, 9, 0, 8, 10, 418, 43, 17, 17, 0, 1, 16, 436, 43, 17, 17, 0, 1, 16, 454, 43, 17, 17, 0, 1, 16, 472, 43, 10, 15, 0, 7, 10, 483, 43, 16, 19, -1, -2, 14, 0, 66, 16, 19, -1, -2, 14, 17, 66, 16, 19, -1, -2, 14, 34, 66, 16, 19, -1, -2, 14, 51, 66, 16, 19, -1, -2, 14, 68, 66, 16, 20, -1, -3, 14, 85, 66, 20, 15, -1, 2, 19, 106, 66, 15, 20, 0, 2, 15, 122, 66, 11, 19, 0, -2, 12, 134, 66, 11, 19, 0, -2, 12, 146, 66, 11, 19, 0, -2, 12, 158, 66, 11, 19, 0, -2, 12, 170, 66, 6, 19, -1, -2, 5, 177, 66, 5, 19, 1, -2, 5, 183, 66, 8, 19, -1, -2, 5, 192, 66, 8, 19, -1, -2, 5, 201, 66, 17, 15, -1, 2, 16, 219, 66, 14, 19, 0, -2, 16, 234, 66, 18, 19, 0, -2, 18, 253, 66, 18, 19, 0, -2, 18, 272, 66, 18, 19, 0, -2, 18, 291, 66, 18, 19, 0, -2, 18, 310, 66, 18, 19, 0, -2, 18, 329, 66, 12, 10, 0, 6, 14, 342, 66, 18, 15, 0, 2, 18, 361, 66, 13, 19, 0, -2, 14, 375, 66, 13, 19, 0, -2, 14, 389, 66, 13, 19, 0, -2, 14, 403, 66, 13, 19, 0, -2, 14, 417, 66, 14, 19, -1, -2, 12, 432, 66, 11, 15, 0, 2, 12, 444, 66, 11, 16, 0, 1, 12, 456, 66, 10, 15, 0, 2, 11, 467, 66, 10, 15, 0, 2, 11, 478, 66, 10, 15, 0, 2, 11, 489, 66, 10, 15, 0, 2, 11, 500, 66, 10, 15, 0, 2, 11, 0, 88, 10, 16, 0, 1, 11, 11, 88, 18, 10, 0, 7, 18, 30, 88, 11, 15, 0, 7, 10, 42, 88, 12, 15, 0, 2, 12, 55, 88, 12, 15, 0, 2, 12, 68, 88, 12, 15, 0, 2, 12, 81, 88, 12, 15, 0, 2, 12, 94, 88, 6, 15, -1, 2, 5, 101, 88, 5, 15, 1, 2, 5, 107, 88, 8, 15, -1, 2, 5, 116, 88, 8, 15, -1, 2, 5, 125, 88, 12, 16, 0, 1, 12, 138, 88, 10, 15, 0, 2, 12, 149, 88, 12, 15, 0, 2, 12, 162, 88, 12, 15, 0, 2, 12, 175, 88, 12, 15, 0, 2, 12, 188, 88, 12, 15, 0, 2, 12, 201, 88, 12, 15, 0, 2, 12, 214, 88, 12, 9, 0, 6, 14, 227, 88, 13, 12, 0, 6, 12, 241, 88, 10, 15, 0, 2, 12, 252, 88, 10, 15, 0, 2, 12, 263, 88, 10, 15, 0, 2, 12, 274, 88, 10, 15, 0, 2, 12, 285, 88, 11, 20, 0, 2, 10, 297, 88, 12, 21, 1, 1, 13];
    var METRIC_OFFSETS = {
        SX: 0,
        SY: 1,
        SW: 2,
        SH: 3,
        OFFSET_X: 4,
        OFFSET_Y: 5,
        ADVANCE: 6,
        ENTRIES_PER_GLYPH: 7
    };
    ig.FontAvenirNext = ig.Font.extend({
        letterSpacing: 1,
        height: 33,
        firstChar: 32,
        lastChar: 255,
        _widthForLine: function(s) {
            var O = METRIC_OFFSETS;
            var width = 0;
            for (var i = 0; i < s.length; i++) {
                var advanceIdx = (s.charCodeAt(i) - this.firstChar) * O.ENTRIES_PER_GLYPH + O.ADVANCE;
                width += METRICS[advanceIdx];
            }
            return width + this.letterSpacing * s.length;
        },
        _drawChar: function(c, targetX, targetY) {
            c += this.firstChar;
            if (!this.loaded || c < this.firstChar || c >= this.lastChar) {
                return 0;
            }
            var O = METRIC_OFFSETS;
            var M = METRICS;
            var i = (c - this.firstChar) * O.ENTRIES_PER_GLYPH;
            var sx = M[i + O.SX]
              , sy = M[i + O.SY]
              , sw = M[i + O.SW]
              , sh = M[i + O.SH]
              , offsetX = M[i + O.OFFSET_X]
              , offsetY = M[i + O.OFFSET_Y]
              , advance = M[i + O.ADVANCE];
            ig.system.context.drawImage(this.data, sx, sy, sw, sh, ig.system.getDrawPos(targetX + offsetX), ig.system.getDrawPos(targetY + offsetY), sw, sh);
            return advance + this.letterSpacing;
        },
        _loadMetrics: function(image) {}
    });
});

// lib/impact/entity-pool.js
ig.baked = true;
ig.module('impact.entity-pool').requires('impact.game').defines(function() {
    "use strict";
    ig.EntityPool = {
        pools: {},
        mixin: {
            staticInstantiate: function(x, y, settings) {
                return ig.EntityPool.getFromPool(this.classId, x, y, settings);
            },
            erase: function() {
                ig.EntityPool.putInPool(this);
            }
        },
        enableFor: function(Class) {
            Class.inject(this.mixin);
        },
        getFromPool: function(classId, x, y, settings) {
            var pool = this.pools[classId];
            if (!pool || !pool.length) {
                return null ;
            }
            var instance = pool.pop();
            instance.reset(x, y, settings);
            return instance;
        },
        putInPool: function(instance) {
            if (!this.pools[instance.classId]) {
                this.pools[instance.classId] = [instance];
            } 
            else {
                this.pools[instance.classId].push(instance);
            }
        },
        drainPool: function(classId) {
            delete this.pools[classId];
        },
        drainAllPools: function() {
            this.pools = {};
        }
    };
    ig.Game.inject({
        loadLevel: function(data) {
            ig.EntityPool.drainAllPools();
            this.parent(data);
        }
    });
});

// lib/game/entities/particle.js
ig.baked = true;
ig.module('game.entities.particle').requires('impact.entity').defines(function() {
    EntityParticle = ig.Entity.extend({
        size: {
            x: 4,
            y: 4
        },
        offset: {
            x: 0,
            y: 0
        },
        maxVel: {
            x: 1000,
            y: 1000
        },
        minBounceVelocity: 0,
        type: ig.Entity.TYPE.NONE,
        checkAgainst: ig.Entity.TYPE.NONE,
        collides: ig.Entity.COLLIDES.LITE,
        lifetime: 5,
        fadetime: 1,
        bounciness: 0.6,
        friction: {
            x: 20,
            y: 0
        },
        init: function(x, y, settings) {
            this.parent(x, y, settings);
            this.vel.x = (Math.random() * 2 - 1) * this.vel.x;
            this.vel.y = (Math.random() * 2 - 1) * this.vel.y;
            this.currentAnim.flip.x = (Math.random() > 0.5);
            this.currentAnim.flip.y = (Math.random() > 0.5);
            this.currentAnim.gotoRandomFrame();
            this.idleTimer = new ig.Timer();
        },
        update: function() {
            if (this.idleTimer.delta() > this.lifetime) {
                this.kill();
                return;
            }
            this.currentAnim.alpha = this.idleTimer.delta().map(this.lifetime - this.fadetime, this.lifetime, 1, 0);
            this.parent();
        },
        reset: function(x, y, settings) {
            this.parent(x, y, settings);
            this.vel.x = (Math.random() * 2 - 1) * this.vel.x;
            this.vel.y = (Math.random() * 2 - 1) * this.vel.y;
            this.currentAnim.flip.x = (Math.random() > 0.5);
            this.currentAnim.flip.y = (Math.random() > 0.5);
            this.currentAnim.gotoRandomFrame();
            this.idleTimer.reset();
        }
    });
});

// lib/game/entities/explosion.js
ig.baked = true;
ig.module('game.entities.explosion').requires('impact.entity', 'impact.entity-pool', 'game.entities.particle').defines(function() {
    window.EntityExplosionParticle = EntityParticle.extend({
        lifetime: 0.5,
        fadetime: 0.5,
        vel: {
            x: 60,
            y: 60
        },
        animSheet: new ig.AnimationSheet('media/sprites/explosion.png',32,32),
        init: function(x, y, settings) {
            this.addAnim('idle', 5, [0, 1, 2]);
            this.parent(x, y, settings);
        },
        update: function() {
            this.currentAnim.angle += 0.1 * ig.system.tick;
            this.parent();
        }
    });
    ig.EntityPool.enableFor(EntityExplosionParticle);
    window.EntityExplosionSpark = EntityParticle.extend({
        lifetime: 0.5,
        fadetime: 0.5,
        vel: {
            x: 60,
            y: 60
        },
        animSheet: new ig.AnimationSheet('media/sprites/spark.png',48,48),
        init: function(x, y, settings) {
            this.addAnim('idle', 1, [0]);
            this.parent(x, y, settings);
        },
        update: function() {
            this.currentAnim.angle += 0.1 * ig.system.tick;
            this.parent();
        }
    });
    ig.EntityPool.enableFor(EntityExplosionSpark);
    window.EntityExplosionHuge = ig.Entity.extend({
        lifetime: 1,
        fadetime: 1,
        alpha: 0,
        img: new ig.Image('media/sprites/explosion-huge.jpg',512,512),
        init: function(x, y, settings) {
            this.parent(x, y, settings);
            this.idleTimer = new ig.Timer();
        },
        update: function() {
            if (this.idleTimer.delta() > this.lifetime) {
                this.kill();
                return;
            }
            this.alpha = this.idleTimer.delta().map(this.lifetime - this.fadetime, this.lifetime, 1, 0);
        },
        draw: function() {
            var ctx = ig.system.context;
            ctx.save();
            var scale = this.alpha.map(0, 1, 10, 0);
            ctx.translate(this.pos.x - ig.game._rscreen.x, this.pos.y - ig.game._rscreen.y);
            ctx.scale(scale, scale);
            ctx.globalAlpha = this.alpha;
            this.img.draw(-256, -256);
            ctx.globalAlpha = 1;
            ig.system.context.restore();
        }
    });
    window.EntityExplosionHugeGlitch = EntityExplosionHuge.extend({
        img: new ig.Image('media/sprites/explosion-huge-glitch.jpg',512,512),
        glitchJPEG: new ig.Image('media/title/glitch-jpeg.png'),
        glitchStripe: new ig.Image('media/title/glitch-stripe.png'),
        draw: function() {
            this.parent();
            var d = this.idleTimer.delta();
            if (d < 0.1 || (d > 0.2 && d < 0.3)) {
                this.glitchJPEG.draw(this.pos.x - 240 + (200 - Math.random() * 400), Math.random() * ig.system.height);
            }
            if (d < 0.15 || (d > 0.25 && d < 0.35)) {
                this.glitchStripe.draw(0, Math.random() * ig.system.height);
            }
        },
        kill: function() {
            this.parent();
            ig.game.showGameOverScreen();
        }
    });
});

// lib/game/entities/enemy.js
ig.baked = true;
ig.module('game.entities.enemy').requires('impact.entity', 'impact.font', 'game.avenir-next', 'game.entities.explosion').defines(function() {
    EntityEnemy = ig.Entity.extend({
        word: 'none',
        remainingWord: 'none',
        health: 8,
        currentLetter: 0,
        targeted: false,
        font: new ig.FontAvenirNext('media/fonts/avenir-22-white-v2.png'),
        fontActive: new ig.FontAvenirNext('media/fonts/avenir-22-orange-v2.png'),
        speed: 10,
        friction: {
            x: 100,
            y: 100
        },
        hitTimer: null ,
        dead: false,
        angle: 0,
        wordLength: {
            min: 8,
            max: 8
        },
        numKillParticles: 20,
        numHitParticles: 5,
        soundHit: new ig.Sound('media/sounds/hit.ogg'),
        reticleTimer: null ,
        reticle: new ig.Image('media/ui/reticle.png'),
        type: ig.Entity.TYPE.B,
        checkAgainst: ig.Entity.TYPE.A,
        init: function(x, y, settings) {
            this.parent(x, y, settings);
            this.font.letterSpacing = 0;
            this.fontActive.letterSpacing = 0;
            var length = Math.random().map(0, 1, this.wordLength.min, this.wordLength.max).round();
            this.word = settings.word || this.getWordWithLength(length);
            this.health = this.word.length;
            this.remainingWord = this.word;
            this.hitTimer = new ig.Timer(0);
            this.dieTimer = new ig.Timer(0);
            ig.game.registerTarget(this.word.charAt(0), this);
            this.angle = this.angleTo(ig.game.player);
            if (!ig.ua.mobile) {
                this.speed *= ig.game.speedFactor;
            }
        },
        getWordWithLength: function(l) {
            var w = 'wtf';
            for (var i = 0; i < 20; i++) {
                if (l >= 2 && l <= 12) {
                    w = ig.game.wordlist[l].random();
                } 
                else {
                    w = String.fromCharCode('a'.charCodeAt(0) + (Math.random() * 26).floor());
                }
                if (!ig.game.targets[w.charAt(0).toLowerCase()].length) {
                    return w;
                }
            }
            return w;
        },
        target: function() {
            this.targeted = true;
            ig.game.currentTarget = this;
            ig.game.unregisterTarget(this.remainingWord.charAt(0), this);
            ig.game.entities.erase(this);
            ig.game.entities.push(this);
            this.reticleTimer = new ig.Timer(0.5);
        },
        draw: function() {
            this.parent();
            if (this.reticleTimer) {
                var d = this.reticleTimer.delta().map(-0.5, 0, 0, 1);
                var ctx = ig.system.context;
                ctx.save();
                var scale = (1 - d) * (1 - d) * 4;
                ctx.translate(this.pos.x + this.size.x / 2 - ig.game._rscreen.x, this.pos.y + this.size.y / 2 - ig.game._rscreen.y);
                ctx.scale(scale, scale);
                ctx.globalAlpha = Math.sqrt(d);
                ctx.rotate(d * Math.PI * 1);
                this.reticle.draw(-128, -128);
                ctx.globalAlpha = 1;
                ig.system.context.restore();
            }
        },
        drawLabel: function() {
            if (!this.remainingWord.length) {
                return;
            }
            var w = this.font.widthForString(this.word);
            var x = (this.pos.x - 6).limit(w + 2, ig.system.width - 1) + ig.game._rscreen.x;
            var y = (this.pos.y + this.size.y - 10).limit(2, ig.system.height - 19) + ig.game._rscreen.y;
            var bx = ig.system.getDrawPos(x - w - 2);
            var by = ig.system.getDrawPos(y - 1);
            ig.system.context.fillStyle = 'rgba(0,0,0,0.75)';
            ig.system.context.fillRect(bx, by + 5, w + 8, 24);
            if (this.targeted) {
                this.fontActive.draw(this.remainingWord, x + 2, y + 6, ig.Font.ALIGN.RIGHT);
            } 
            else {
                this.font.draw(this.remainingWord, x + 2, y + 6, ig.Font.ALIGN.RIGHT);
            }
        },
        kill: function(silent) {
            if (this.remainingWord.length) {
                ig.game.unregisterTarget(this.remainingWord.charAt(0), this);
            }
            if (ig.game.currentTarget == this) {
                ig.game.currentTarget = null ;
            }
            this.parent();
            if (this.explosionSound && !silent) {
                this.explosionSound.play();
            }
            if (!silent) {
                ig.game.lastKillTimer.set(0.3);
                var px = this.pos.x - this.size.x / 2
                  , py = this.pos.y - this.size.y / 2;
                for (var i = 0; i < this.wordLength.max; i++) {
                    ig.game.spawnEntity(EntityExplosionSpark, px, py, {
                        vel: {
                            x: 360,
                            y: 360
                        }
                    });
                }
            }
        },
        cancel: function() {
            ig.game.currentTarget = null ;
            this.targeted = false;
            ig.game.registerTarget(this.remainingWord.charAt(0), this);
        },
        update: function() {
            if (this.hitTimer.delta() > 0) {
                this.vel.x = Math.cos(this.angle) * this.speed;
                this.vel.y = Math.sin(this.angle) * this.speed;
            }
            this.parent();
            if (this.pos.x < -this.animSheet.width || this.pos.x > ig.system.width + 10 || this.pos.y > ig.system.height + 10 || this.pos.y < -this.animSheet.height - 30) {
                this.kill(true);
            }
            if (this.reticleTimer && this.reticleTimer.delta() > 0) {
                this.reticleTimer = null ;
            }
        },
        hit: function(other) {
            this.spawnExplosionParticles((this.health <= 1));
            this.vel.x = -Math.cos(this.angle) * 20 + Math.random() * 20;
            this.vel.y = -Math.sin(this.angle) * 20 + Math.random() * 20;
            this.hitTimer.set(0.3);
            ig.game.lastKillTimer.set(0.2);
            this.receiveDamage(1);
            this.soundHit.play();
        },
        spawnExplosionParticles: function(killed) {
            var numParticles = killed ? this.numKillParticles : this.numHitParticles;
            for (var i = 0; i < numParticles; i++) {
                ig.game.spawnEntity(EntityExplosionParticle, this.pos.x, this.pos.y);
            }
            var numSparks = numParticles / 2;
            for (var i = 0; i < numSparks; i++) {
                ig.game.spawnEntity(EntityExplosionSpark, this.pos.x, this.pos.y);
            }
        },
        isHitBy: function(letter) {
            var expected = ig.game.translateUmlaut(this.remainingWord.charAt(0).toLowerCase());
            if (expected == letter.toLowerCase()) {
                this.remainingWord = this.remainingWord.substr(1);
                if (this.remainingWord.length == 0) {
                    ig.game.currentTarget = null ;
                    this.dead = true;
                }
                return true;
            } 
            else {
                return false;
            }
        },
        check: function(other) {
            other.kill();
            this.kill();
        }
    });
});

// lib/game/entities/enemy-missle.js
ig.baked = true;
ig.module('game.entities.enemy-missle').requires('game.entities.enemy').defines(function() {
    EntityEnemyMissle = EntityEnemy.extend({
        size: {
            x: 8,
            y: 15
        },
        offset: {
            x: 6,
            y: 7
        },
        animSheet: new ig.AnimationSheet('media/sprites/missle.png',20,26),
        explosionSound: new ig.Sound('media/sounds/explosion-small.ogg'),
        health: 4,
        speed: 35,
        targetTimer: null ,
        wordLength: {
            min: 2,
            max: 5
        },
        numKillParticles: 10,
        numHitParticles: 2,
        init: function(x, y, settings) {
            if (ig.doc) {
                this.wordLength.min = 1;
                this.wordLength.max = 1;
            }
            this.parent(x, y, settings);
            this.addAnim('idle', 1, [0]);
            this.angle = settings.angle;
            this.currentAnim.angle = this.angle - Math.PI / 2;
            this.targetTimer = new ig.Timer(1);
        },
        update: function() {
            var d = this.targetTimer.delta();
            if (d > 0 && d < 0.7) {
                var ad = this.angle - this.angleTo(ig.game.player);
                this.angle -= ad * ig.system.tick * 2;
                this.currentAnim.angle = this.angle - Math.PI / 2;
            }
            this.parent();
        }
    });
});

// lib/game/entities/enemy-mine.js
ig.baked = true;
ig.module('game.entities.enemy-mine').requires('game.entities.enemy').defines(function() {
    EntityEnemyMine = EntityEnemy.extend({
        size: {
            x: 12,
            y: 12
        },
        offset: {
            x: 10,
            y: 10
        },
        animSheet: new ig.AnimationSheet('media/sprites/mine.png',32,32),
        explosionSound: new ig.Sound('media/sounds/explosion.ogg'),
        speed: 30,
        health: 6,
        wordLength: {
            min: 3,
            max: 6
        },
        init: function(x, y, settings) {
            this.parent(x, y, settings);
            this.addAnim('idle', 1, [0]);
        },
        kill: function(silent) {
            this.parent(silent);
            ig.game.screenShake(5);
        },
        update: function() {
            this.angle = this.angleTo(ig.game.player);
            this.parent();
            this.currentAnim.angle += 2 * ig.system.tick;
        }
    });
});

// lib/game/entities/enemy-destroyer.js
ig.baked = true;
ig.module('game.entities.enemy-destroyer').requires('game.entities.enemy', 'game.entities.explosion').defines(function() {
    EntityEnemyDestroyer = EntityEnemy.extend({
        size: {
            x: 24,
            y: 34
        },
        offset: {
            x: 10,
            y: 8
        },
        animSheet: new ig.AnimationSheet('media/sprites/destroyer.png',43,58),
        explosionSound: new ig.Sound('media/sounds/explosion-large.ogg'),
        health: 8,
        speed: 20,
        shootTimer: null ,
        wordLength: {
            min: 7,
            max: 10
        },
        numKillParticles: 40,
        numHitParticles: 10,
        init: function(x, y, settings) {
            this.parent(x, y, settings);
            this.addAnim('idle', 1, [0]);
            this.shootTimer = new ig.Timer(ig.doc ? 12 : 5);
            this.angle = (Math.random().map(0, 1, 67, 90) 
            + (this.pos.x > ig.system.width / 2 ? 22.5 : 0)) * Math.PI / 180;
            this.currentAnim.angle = this.angle - Math.PI / 2;
        },
        kill: function(silent) {
            this.parent(silent);
            if (!silent) {
                ig.game.spawnEntity(EntityExplosionHuge, this.pos.x + 12, this.pos.y + 22);
                ig.game.screenShake(10);
            }
        },
        update: function() {
            this.parent();
            if (this.shootTimer.delta() > 0) {
                this.shootTimer.reset();
                if (this.distanceTo(ig.game.player) > 100) {
                    ig.game.spawnEntity(EntityEnemyMissle, this.pos.x + 12, this.pos.y + 22, {
                        angle: this.angle
                    });
                }
            }
        }
    });
});

// lib/game/entities/enemy-oppressor.js
ig.baked = true;
ig.module('game.entities.enemy-oppressor').requires('game.entities.enemy').defines(function() {
    EntityEnemyOppressor = EntityEnemy.extend({
        size: {
            x: 36,
            y: 58
        },
        offset: {
            x: 16,
            y: 10
        },
        animSheet: new ig.AnimationSheet('media/sprites/oppressor.png',68,88),
        explosionSound: new ig.Sound('media/sounds/explosion-large.ogg'),
        health: 10,
        speed: 15,
        shootTimer: null ,
        bullets: 8,
        wordLength: {
            min: 9,
            max: 12
        },
        numKillParticles: 40,
        numHitParticles: 10,
        init: function(x, y, settings) {
            this.parent(x, y - 18, settings);
            this.addAnim('idle', 1, [0]);
            this.shootTimer = new ig.Timer(ig.doc ? 14 : 7);
            this.angle = Math.PI / 2;
        },
        kill: function(silent) {
            this.parent(silent);
            if (!silent) {
                ig.game.spawnEntity(EntityExplosionHuge, this.pos.x + 12, this.pos.y + 22);
                ig.game.screenShake(20);
            }
        },
        update: function() {
            this.parent();
            if (this.shootTimer.delta() > 0) {
                if (this.distanceTo(ig.game.player) > 100) {
                    var inc = 100 / (this.bullets - 1);
                    var a = 40;
                    var radius = 21;
                    for (var i = 0; i < this.bullets; i++) {
                        var angle = a * Math.PI / 180;
                        var x = this.pos.x + 18 + Math.cos(angle) * radius;
                        var y = this.pos.y + 48 + Math.sin(angle) * radius;
                        ig.game.spawnEntity(EntityEnemyBullet, x, y, {
                            angle: angle
                        });
                        a += inc;
                    }
                }
                this.shootTimer.reset();
            }
        }
    });
    EntityEnemyBullet = EntityEnemy.extend({
        size: {
            x: 2,
            y: 2
        },
        offset: {
            x: 8,
            y: 11
        },
        animSheet: new ig.AnimationSheet('media/sprites/bullet.png',20,24),
        health: 1,
        speed: 50,
        wordLength: {
            min: 1,
            max: 1
        },
        init: function(x, y, settings) {
            this.parent(x, y, settings);
            this.addAnim('idle', 1, [0]);
            this.angle = settings.angle;
            this.currentAnim.angle = this.angle - Math.PI / 2;
        }
    });
});

// lib/game/entities/emp.js
ig.baked = true;
ig.module('game.entities.emp').requires('impact.entity').defines(function() {
    "use strict";
    window.EntityEMP = ig.Entity.extend({
        lifetime: 1,
        fadetime: 1,
        alpha: 0,
        killedProjectiles: false,
        img: new ig.Image('media/sprites/emp.png'),
        sound: new ig.Sound('media/sounds/emp.ogg',false),
        init: function(x, y, settings) {
            this.parent(x, y, settings);
            this.idleTimer = new ig.Timer();
            this.sound.play();
            ig.game.screenShake(30);
        },
        update: function() {
            this.alpha = this.idleTimer.delta().map(this.lifetime - this.fadetime, this.lifetime, 1, 0);
            var scale = this.alpha.map(0, 1, 1.7, 0);
            scale = Math.pow(scale, 0.5) * 200;
            var ents = ig.game.entities;
            for (var i = 0; i < ents.length; i++) {
                var ent = ents[i];
                if (ent instanceof EntityEnemy) {
                    if (this.distanceTo(ent) < scale) {
                        ent.spawnExplosionParticles(true);
                        ent.kill();
                    }
                }
            }
            if (this.idleTimer.delta() > this.lifetime) {
                this.kill();
                return;
            }
        },
        draw: function() {
            var scale = Math.pow(this.alpha.map(0, 1, 1.7, 0), 0.5);
            var ctx = ig.system.context;
            ctx.save();
            ctx.translate(this.pos.x - ig.game._rscreen.x, this.pos.y - ig.game._rscreen.y);
            ctx.scale(scale * 2, scale * 2);
            ctx.rotate(Math.pow(scale, 0.5) * Math.PI * 2);
            ctx.globalAlpha = this.alpha;
            this.img.draw(-128, -128);
            ig.system.context.restore();
        }
    });
});

// lib/game/entities/player.js
ig.baked = true;
ig.module('game.entities.player').requires('impact.entity', 'impact.entity-pool', 'game.entities.emp', 'game.entities.particle').defines(function() {
    EntityPlayer = ig.Entity.extend({
        animSheet: new ig.AnimationSheet('media/sprites/ship.png',24,24),
        targetAngle: 0,
        size: {
            x: 8,
            y: 8
        },
        offset: {
            x: 8,
            y: 8
        },
        angle: 0,
        targetAngle: 0,
        soundShoot: new ig.Sound('media/sounds/plasma.ogg'),
        soundMiss: new ig.Sound('media/sounds/click.ogg'),
        soundExplode: new ig.Sound('media/sounds/explosion-player.ogg'),
        type: ig.Entity.TYPE.A,
        init: function(x, y, settings) {
            this.parent(x, y, settings);
            this.addAnim('idle', 60, [0]);
            this.addAnim('shoot', 0.05, [3, 2, 1, 0], true);
            this.addAnim('miss', 0.05, [4, 5, 6], true);
        },
        update: function() {
            if (this.currentAnim.loopCount > 0) {
                this.currentAnim = this.anims.idle;
            }
            var ad = this.angle - this.targetAngle;
            if (Math.abs(ad) < 0.02) {
                this.angle = this.targetAngle;
            } 
            else {
                this.angle -= ad * ig.system.tick * 10;
            }
            this.currentAnim.angle = this.angle;
            this.parent();
        },
        spawnEMP: function() {
            if (ig.game.emps) {
                ig.game.spawnEntity(EntityEMP, this.pos.x + this.size.x / 2, this.pos.y + this.size.y / 2);
                ig.game.emps--;
            }
        },
        kill: function() {
            ig.game.setGameOver();
            this.soundExplode.play();
            ig.game.spawnEntity(EntityExplosionHugeGlitch, this.pos.x + 12, this.pos.y + 22);
            ig.game.screenShake(10);
            this.pos.y = ig.system.height + 300;
            this.parent();
        },
        shoot: function(target) {
            this.currentAnim = this.anims.shoot.rewind();
            var ent = ig.game.spawnEntity(EntityPlasma, this.pos.x + 6, this.pos.y + 4);
            ent.target = target;
            var angle = this.angleTo(target);
            this.targetAngle = angle + Math.PI / 2;
            this.soundShoot.play();
        },
        miss: function() {
            this.currentAnim = this.anims.miss.rewind();
            this.soundMiss.play();
        }
    });
    EntityPlasma = ig.Entity.extend({
        speed: 800,
        maxVel: {
            x: 10000,
            y: 10000
        },
        animSheet: new ig.AnimationSheet('media/sprites/plasma.png',96,96),
        size: {
            x: 4,
            y: 4
        },
        offset: {
            x: 46,
            y: 46
        },
        distance: 100000,
        init: function(x, y, settings) {
            this.parent(x, y, settings);
            this.addAnim('idle', 1, [0]);
        },
        reset: function(x, y, settings) {
            this.parent(x, y, settings);
            this.distance = 100000;
        },
        update: function() {
            if (this.target) {
                var currentDistance = this.distanceTo(this.target);
                if (currentDistance > this.distance || currentDistance < this.target.size.y) {
                    this.target.hit(this);
                    this.kill();
                    return;
                } 
                else {
                    var angle = this.angleTo(this.target);
                    this.currentAnim.angle = angle + Math.PI / 2;
                    this.vel.x = Math.cos(angle) * this.speed;
                    this.vel.y = Math.sin(angle) * this.speed;
                }
                this.distance = currentDistance;
                this.parent();
            } 
            else {
                this.kill();
            }
        }
    });
    ig.EntityPool.enableFor(EntityPlasma);
});

// lib/game/keyboard.js
ig.baked = true;
ig.module('game.keyboard').requires('impact.font').defines(function() {
    ig.Keyboard = ig.Class.extend({
        background: new ig.Image('media/ui/keyboard.png'),
        empButton: new ig.Image('media/ui/emp-buttons.png'),
        pauseButton: new ig.Image('media/ui/pause.png'),
        font: new ig.Font('media/fonts/avenir-36-blue.png'),
        hoverImages: {
            leftEdge: new ig.Image('media/ui/key-edge-left.png'),
            rightEdge: new ig.Image('media/ui/key-edge-right.png'),
            normal: new ig.Image('media/ui/key.png')
        },
        multiBar: new ig.Image('media/ui/bar-blue.png'),
        multiIndicator: new ig.Image('media/ui/multi-indicator.png'),
        multiSounds: {
            2: new ig.Sound('media/sounds/multi-2.ogg'),
            3: new ig.Sound('media/sounds/multi-3.ogg')
        },
        width: 0,
        height: 0,
        hoverKey: null ,
        expectedKeys: null ,
        keys: [['q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p'], ['a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l'], ['z', 'x', 'c', 'v', 'b', 'n', 'm']],
        layout: {
            key: {
                width: 64,
                height: 106
            }
        },
        init: function(callback) {
            ig.system.canvas.addEventListener('touchmove', this.touchmove.bind(this), false);
            ig.system.canvas.addEventListener('touchend', this.touchend.bind(this), false);
            ig.system.canvas.addEventListener('touchstart', this.touchstart.bind(this), false);
            this.callback = callback;
            this.width = this.background.width;
            this.height = (ig.ua.mobile ? this.background.height : 64);
            this.drawScale = ig.system.width / this.width;
            this.x = 0;
            this.y = ig.system.height / this.drawScale - this.height;
        },
        getCurrentKey: function(touches) {
            var touch = touches[touches.length - 1];
            var documentScale = (parseInt(ig.system.canvas.offsetWidth) || ig.system.realWidth) / ig.system.width;
            var touchX = (touch.clientX / documentScale) / this.drawScale
              , touchY = (touch.clientY / documentScale) / this.drawScale;
            if (touchX < 96 && touchY < 96) {
                return 'ESC';
            } 
            else if (touchY < this.y - 20) {
                return 'BACKSPACE';
            } 
            else if (touchY > this.y + 214 && touchX > this.x + 560) {
                return 'ENTER';
            }
            var row = ((touchY - this.y) / this.layout.key.height) | 0;
            if (row < 0 || !this.keys[row]) {
                return;
            }
            var offsetX = (this.background.width - this.keys[row].length * this.layout.key.width) / 2;
            var col = ((touchX - this.x - offsetX) / this.layout.key.width) | 0;
            var key = this.keys[row][col];
            if (this.expectedKeys) {
                var closest = Infinity;
                for (var i = 0; i < this.expectedKeys.length; i++) {
                    var kpos = ig.Keyboard.Map[this.expectedKeys[i]];
                    var keyX = kpos[0] * this.layout.key.width + this.layout.key.width / 2
                      , keyY = kpos[1] * this.layout.key.height + this.layout.key.height / 2;
                    var dx = touchX - this.x - keyX
                      , dy = touchY - this.y - keyY;
                    var distance = Math.sqrt(dx * dx + dy * dy);
                    if (distance < this.layout.key.width && distance < closest) {
                        closest = distance;
                        key = this.expectedKeys[i];
                    }
                }
            }
            return key;
        },
        touchstart: function(ev) {
            var key = this.getCurrentKey(ev.touches);
            if (key !== 'ESC' && key !== 'ENTER' && key !== 'BACKSPACE') {
                this.hoverKey = key;
            }
        },
        touchmove: function(ev) {
            var key = this.getCurrentKey(ev.touches);
            if (key !== 'ESC' && key !== 'ENTER' && key !== 'BACKSPACE') {
                this.hoverKey = key;
            }
        },
        touchend: function(ev) {
            var key = this.getCurrentKey(ev.changedTouches);
            if (key) {
                this.callback(key);
            }
            this.hoverKey = null ;
        },
        showMultiplier: function(m) {
            this.multiplierTimer = new ig.Timer(2);
            this.multiplierIndex = m - 2;
            this.multiSounds[m].play();
        },
        drawFull: function() {
            var ctx = ig.system.context;
            ctx.save();
            ctx.globalAlpha = 1;
            ctx.scale(this.drawScale, this.drawScale);
            this.background.draw(this.x, this.y);
            this.drawMultiplierBar(this.x, this.y);
            if (this.hoverKey) {
                var offsetX = 0;
                var hoverImage = this.hoverImages.normal;
                if (this.hoverKey == 'q') {
                    hoverImage = this.hoverImages.leftEdge;
                    offsetX = 26;
                } 
                else if (this.hoverKey == 'p') {
                    hoverImage = this.hoverImages.rightEdge;
                    offsetX = -26;
                }
                var kpos = ig.Keyboard.Map[this.hoverKey];
                var x = (kpos[0] * this.layout.key.width - 19 + offsetX)
                  , y = this.y + kpos[1] * this.layout.key.height - 112;
                ctx.globalAlpha = 0.9;
                hoverImage.draw(x, y);
                ig.system.context.globalAlpha = 1;
                this.font.draw(this.hoverKey.toUpperCase(), x + 51, y + 24, ig.Font.ALIGN.CENTER);
            }
            if (!ig.game.emps) {
                ctx.globalAlpha = 0.7;
            }
            this.empButton.drawTile(this.x + 582, this.y + 250, ig.game.emps, 34, 40);
            ctx.globalAlpha = 0.15;
            this.pauseButton.draw(28, 28);
            ctx.restore();
        },
        drawMultiplierBar: function(x, y) {
            if (ig.game.streak > 0) {
                var multiLength = 1 - (75 / (75 + ig.game.streak * 2));
                this.multiBar.draw(x, y, 0, 0, this.width * multiLength, 2);
            }
            var showMultiplierTime = this.multiplierTimer ? this.multiplierTimer.delta() : 0;
            if (showMultiplierTime < 0) {
                ig.system.context.globalAlpha = showMultiplierTime.map(-2, 0, 2, 0).limit(0, 1);
                this.multiIndicator.drawTile(x + this.width * multiLength - 30, y - 22, this.multiplierIndex, 32, 18);
                ig.system.context.globalAlpha = 1;
            }
        },
        drawMinimal: function() {
            var ctx = ig.system.context;
            ctx.save();
            ctx.globalAlpha = 1;
            ctx.scale(this.drawScale, this.drawScale);
            this.drawMultiplierBar(this.x, this.y + 62);
            if (!ig.game.emps) {
                ctx.globalAlpha = 0.7;
            }
            this.empButton.drawTile(this.x + 582, this.y, ig.game.emps, 34, 40);
            ctx.globalAlpha = 0.1;
            this.pauseButton.draw(28, 28);
            ctx.restore();
        },
        draw: function() {
            if (ig.ua.mobile) {
                this.drawFull();
            } 
            else {
                this.drawMinimal();
            }
        }
    });
    ig.Keyboard.Map = {
        q: [0, 0],
        w: [1, 0],
        e: [2, 0],
        r: [3, 0],
        t: [4, 0],
        y: [5, 0],
        u: [6, 0],
        i: [7, 0],
        o: [8, 0],
        p: [9, 0],
        a: [0.5, 1],
        s: [1.5, 1],
        d: [2.5, 1],
        f: [3.5, 1],
        g: [4.5, 1],
        h: [5.5, 1],
        j: [6.5, 1],
        k: [7.5, 1],
        l: [8.5, 1],
        z: [1.5, 2],
        x: [2.5, 2],
        c: [3.5, 2],
        v: [4.5, 2],
        b: [5.5, 2],
        n: [6.5, 2],
        m: [7.5, 2]
    };
});

// lib/plugins/silent-loader.js
ig.baked = true;
ig.module('plugins.silent-loader').requires('impact.loader').defines(function() {
    ig.SilentLoader = ig.Loader.extend({
        endTimer: 0,
        fadeToGameTime: 300,
        end: function() {
            this.parent();
            this.endTime = Date.now();
            ig.system.setDelegate(this);
        },
        run: function() {
            var t = Date.now() - this.endTime;
            var alpha = 1;
            if (t < this.fadeToGameTime) {
                ig.game.run();
                alpha = t.map(0, this.fadeToGameTime, 1, 0);
            } 
            else {
                ig.system.setDelegate(ig.game);
                return;
            }
            ig.system.context.fillStyle = 'rgba(0,0,0,' + alpha + ')';
            ig.system.context.fillRect(0, 0, ig.system.realWidth, ig.system.realHeight);
        },
        draw: function() {
            return;
        }
    });
});

// lib/plugins/rise-loader.js
ig.baked = true;
ig.module('plugins.rise-loader').requires('impact.loader').defines(function() {
    ig.RiseLoader = ig.Loader.extend({
        endTimer: 0,
        fadeToGameTime: 300,
        end: function() {
            this.parent();
            this.endTime = Date.now();
            ig.system.setDelegate(this);
        },
        run: function() {
            var t = Date.now() - this.endTime;
            var alpha = 1;
            if (t < this.fadeToGameTime) {
                ig.game.run();
                alpha = t.map(0, this.fadeToGameTime, 1, 0);
            } 
            else {
                ig.system.setDelegate(ig.game);
                return;
            }
            ig.system.context.fillStyle = 'rgba(255,255,255,' + alpha + ')';
            ig.system.context.fillRect(0, 0, ig.system.realWidth, ig.system.realHeight);
        },
        draw: function() {
            this._drawStatus += (this.status - this._drawStatus) / 5;
            ig.system.context.fillStyle = '#000';
            ig.system.context.fillRect(0, 0, ig.system.realWidth, ig.system.realHeight);
            var h = this._drawStatus * ig.system.realHeight;
            ig.system.context.fillStyle = '#fff';
            ig.system.context.fillRect(0, ig.system.realHeight - h, ig.system.realWidth, h);
        }
    });
});

// lib/game/document-scanner.js
ig.baked = true;
ig.module('game.document-scanner').defines(function() {
    "use strict";
    ig.DocumentScanner = ig.Class.extend({
        _reSkipNode: null ,
        _reSkipClass: null ,
        _reTextNode: null ,
        _reHasWords: null ,
        _reSplitSentences: null ,
        _currentFragment: null ,
        _animationDoneCallback: null ,
        _hurry: false,
        _scrollAnimationId: 0,
        looksLikeEnglish: false,
        fragments: [],
        highlightMarginTop: 32,
        highlightClass: 'ztype-current-text-fragment',
        init: function(rootElement) {
            this._reSkipNode = /^(script|style|head|link|meta|button|input|nav|label|img|svg|canvas|ins)$/i;
            this._reSkipClass = /head|foot|links|menu|nav|pagetop|subtext|side|notice|overlay|siteSub|infobox|hatnote|abstract|caption|disclaimer|byline/i;
            this._reTextNode = /^(em|strong|b|i|mark|cite|dfn|small|del|ins|sub|sup|a|#text|span|code)$/i;
            this._reHasWords = /\b[^\d\W]{2,}\b/g;
            this._reSplitSentences = /([.?!:]+[\s\[\]]+)(?=\w)/g;
            this._reHasEnglishWords = /\b(the|be|to|of|and|that|have|it|for|not|on|with|he|as|you|do|at)\b/i;
            this.$scanOverlay = ig.$('#ztype-overlay');
            this.$scanProgress = ig.$('#ztype-scan-progress');
            this.$scanOverlay.className = 'ztype-scanning';
            var elements = this.traverse(rootElement);
            this.fragments = [];
            for (var i = 0; i < elements.length; i++) {
                this.splitIntoFragments(elements[i], this.fragments);
            }
        },
        traverse: function(element, elements) {
            if (!elements) {
                elements = [];
            }
            var style = element instanceof HTMLElement ? window.getComputedStyle(element) : null ;
            if (element.nodeName.match(this._reSkipNode) || (element.id && element.id.match(this._reSkipClass)) || (element.className && element.className.match(this._reSkipClass)) || (style && (style.display == 'hidden' || style.opacity === "0"))) {
                if (element.textContent.length < 3000) {
                    return elements;
                }
            }
            var allChildrenAreLinks = element.childNodes.length > 0;
            for (var i = 0; i < element.childNodes.length; i++) {
                var child = element.childNodes[i]
                if (child.nodeName !== 'A' && (!child.nodeName.match(this._reTextNode) || child.textContent.match(this._reHasWords))) {
                    allChildrenAreLinks = false;
                }
            }
            if (allChildrenAreLinks) {
                return elements;
            }
            if (this.childrenAreText(element)) {
                var text = element.textContent;
                var tm = text.match(this._reHasWords);
                if (element.offsetParent !== null  && (element.nodeName === '#text' || element.offsetHeight > 0) && tm && tm.length > 0) {
                    elements.push(element);
                    return elements;
                }
            } 
            else {
                for (var i = 0; i < element.childNodes.length; i++) {
                    this.traverse(element.childNodes[i], elements);
                }
            }
            return elements;
        },
        splitIntoFragments: function(element, fragments) {
            var parent = element.parentNode;
            var text = element.textContent;
            var sentences = text.replace(this._reSplitSentences, '$1%%ZT%%').split('%%ZT%%');
            var spans = [];
            for (var i = 0; i < sentences.length; i++) {
                var span = document.createElement('span');
                span.textContent = sentences[i];
                spans.push(span);
                fragments.push({
                    text: sentences[i],
                    element: span
                });
            }
            if (element.nodeName === '#text') {
                for (var i = 0; i < spans.length; i++) {
                    if (i > 0) {
                        parent.insertBefore(spans[i], spans[i - 1].nextSibling);
                    } 
                    else {
                        parent.replaceChild(spans[i], element);
                    }
                }
            } 
            else {
                element.innerHTML = '';
                for (var i = 0; i < spans.length; i++) {
                    element.appendChild(spans[i]);
                }
            }
        },
        childrenAreText: function(element) {
            for (var i = 0; i < element.childNodes.length; i++) {
                if (!element.childNodes[i].nodeName.match(this._reTextNode) || (element.id && element.id.match(this._reSkipClass)) || (element.className && element.className.match(this._reSkipClass)) || !this.childrenAreText(element.childNodes[i])) {
                    return false;
                }
            }
            return true;
        },
        _detectEnglishText: function() {
            var count = 0;
            for (var i = 0; i < this.fragments.length; i++) {
                if (this.fragments[i].text.match(this._reHasEnglishWords)) {
                    count++;
                }
            }
            this.looksLikeEnglish = count > (this.fragments.length / 4);
            this._lleCount = count;
        },
        highlightFragment: function(fragment, margin, dontDeHighlight) {
            if (this._currentFragment && !dontDeHighlight) {
                this._currentFragment.className = '';
            }
            this._currentFragment = fragment.element;
            if (this._currentFragment) {
                this._currentFragment.className = this.highlightClass;
                var target = (document.documentElement.scrollTop || document.body.scrollTop) 
                + this._currentFragment.getBoundingClientRect().top - (margin || this.highlightMarginTop);
                this.scrollTo(target);
            }
        },
        scrollTo: function(y) {
            this._scrollTarget = y;
            if (!this._scrollAnimationId) {
                this._scrollAnimationId = setInterval(this._animateScroll.bind(this), 5);
            }
        },
        _animateScroll: function() {
            var current = document.documentElement.scrollTop = document.body.scrollTop;
            var diff = (this._scrollTarget - current);
            var pos = current + diff / 10;
            if (Math.abs(diff) > 1024) {
                pos = this._scrollTarget;
            }
            document.documentElement.scrollTop = document.body.scrollTop = pos;
            if (Math.abs(diff) < 10) {
                clearInterval(this._scrollAnimationId);
                this._scrollAnimationId = 0;
            }
        },
        playScanAnimation: function(doneCallback) {
            setTimeout(this.nextAnimationStep.bind(this, 0), 300);
            this._animationDoneCallback = doneCallback;
        },
        fastForwardScanAnimation: function() {
            this._hurry = true;
        },
        nextAnimationStep: function(current) {
            if (this.fragments[current]) {
                this.highlightFragment(this.fragments[current], window.innerHeight / 3, true);
                var cc = current + 1;
                if (this._hurry) {
                    cc = cc * cc;
                }
                if (this._hurry && current > 75) {
                    this.scanComplete();
                    return;
                }
                var t = Math.max((500 / (Math.pow(cc, 0.7))), 16);
                setTimeout(this.nextAnimationStep.bind(this, current + 1), t);
            } 
            else {
                this.scanComplete();
            }
            this.$scanProgress.style.width = Math.min(1, current / this.fragments.length) * 100 + '%';
        },
        scanComplete: function() {
            this._currentFragment = null ;
            this.$scanProgress.style.width = '100%';
            this.$scanOverlay.className = 'ztype-scan-done';
            this._detectEnglishText();
            setTimeout((function() {
                this.scrollTo(0);
                for (var i = 0; i < this.fragments.length; i++) {
                    this.fragments[i].element.className = '';
                }
                this.$scanOverlay.className = 'ztype-playing';
                if (this._animationDoneCallback) {
                    this._animationDoneCallback();
                }
            }
            ).bind(this), 300);
        }
    });
});

// lib/game/words/en.js
ig.baked = true;
ig.module('game.words.en').defines(function() {
    ig.WORDS = ig.WORDS || {};
    ig.WORDS.EN = {
        2: ['am', 'an', 'as', 'at', 'be', 'by', 'cs', 'do', 'go', 'he', 'if', 'in', 'is', 'it', 'me', 'my', 'no', 'of', 'oh', 'on', 'or', 'pi', 're', 'so', 'to', 'up', 'us', 'we'],
        3: ['act', 'add', 'age', 'ago', 'aid', 'aim', 'air', 'all', 'and', 'any', 'are', 'arm', 'art', 'ask', 'ate', 'bad', 'ban', 'bar', 'bed', 'bet', 'bid', 'big', 'bit', 'box', 'boy', 'bug', 'bus', 'but', 'buy', 'can', 'car', 'cat', 'cry', 'cup', 'cut', 'day', 'did', 'die', 'doe', 'dog', 'dry', 'due', 'eat', 'end', 'err', 'eye', 'fan', 'far', 'fed', 'few', 'fit', 'fix', 'fly', 'for', 'fry', 'fun', 'gap', 'gas', 'get', 'got', 'gun', 'guy', 'had', 'has', 'hat', 'her', 'hid', 'him', 'his', 'hit', 'hot', 'how', 'ice', 'ill', 'its', 'job', 'joy', 'key', 'kid', 'law', 'lay', 'led', 'leg', 'let', 'lie', 'log', 'lot', 'low', 'mad', 'man', 'map', 'may', 'men', 'met', 'mix', 'mod', 'net', 'new', 'nor', 'not', 'now', 'odd', 'off', 'oil', 'old', 'one', 'our', 'out', 'owe', 'own', 'pay', 'pen', 'per', 'pop', 'put', 'ran', 'raw', 'red', 'rid', 'row', 'run', 'sad', 'sat', 'saw', 'say', 'see', 'set', 'she', 'sit', 'six', 'son', 'sum', 'sun', 'tax', 'tea', 'ten', 'the', 'tie', 'tin', 'too', 'top', 'try', 'two', 'use', 'van', 'via', 'war', 'was', 'way', 'who', 'why', 'win', 'won', 'yes', 'yet', 'you'],
        4: ['able', 'acts', 'adds', 'ages', 'aims', 'also', 'area', 'army', 'asks', 'away', 'back', 'ball', 'band', 'bank', 'bars', 'base', 'bear', 'been', 'best', 'bets', 'bids', 'bill', 'bind', 'bite', 'bits', 'blow', 'blue', 'boat', 'body', 'book', 'boot', 'bore', 'both', 'bugs', 'bulk', 'bury', 'busy', 'buys', 'byte', 'call', 'came', 'card', 'care', 'case', 'cell', 'cent', 'char', 'chip', 'city', 'club', 'clue', 'code', 'cold', 'come', 'cope', 'copy', 'core', 'cost', 'cure', 'cuts', 'dare', 'dark', 'data', 'date', 'days', 'dead', 'deal', 'dear', 'deem', 'deep', 'desk', 'died', 'dies', 'disc', 'disk', 'does', 'done', 'door', 'down', 'draw', 'drew', 'drop', 'dumb', 'dump', 'duty', 'each', 'ease', 'east', 'easy', 'eats', 'edge', 'edit', 'else', 'ends', 'even', 'ever', 'eyes', 'face', 'fact', 'fail', 'fair', 'fall', 'farm', 'fast', 'fate', 'fear', 'feed', 'feel', 'feet', 'fell', 'felt', 'file', 'fill', 'film', 'find', 'fine', 'fire', 'firm', 'fish', 'fits', 'five', 'flag', 'flat', 'flew', 'flow', 'folk', 'food', 'foot', 'form', 'four', 'free', 'from', 'full', 'fund', 'gain', 'game', 'gave', 'gets', 'girl', 'give', 'glad', 'goes', 'gone', 'good', 'grew', 'grow', 'hack', 'hair', 'half', 'hall', 'hand', 'hang', 'hard', 'harm', 'hate', 'have', 'head', 'hear', 'heat', 'held', 'hell', 'help', 'here', 'hide', 'high', 'hill', 'hint', 'hits', 'hold', 'hole', 'home', 'hope', 'host', 'hour', 'huge', 'hung', 'hunt', 'idea', 'inch', 'info', 'into', 'item', 'jobs', 'join', 'joke', 'jump', 'junk', 'just', 'keen', 'keep', 'kept', 'keys', 'kill', 'kind', 'king', 'knew', 'know', 'lack', 'lady', 'lain', 'land', 'last', 'late', 'laws', 'lazy', 'lead', 'leaf', 'left', 'legs', 'lend', 'less', 'lets', 'lied', 'lies', 'life', 'lift', 'like', 'line', 'link', 'list', 'live', 'load', 'loan', 'lock', 'logs', 'long', 'look', 'loop', 'lose', 'loss', 'lost', 'lots', 'love', 'luck', 'made', 'mail', 'main', 'make', 'many', 'mark', 'mass', 'mean', 'meet', 'mere', 'mess', 'mile', 'mind', 'mine', 'miss', 'mode', 'more', 'most', 'move', 'much', 'must', 'name', 'near', 'neck', 'need', 'news', 'next', 'nice', 'nine', 'none', 'note', 'numb', 'obey', 'odds', 'omit', 'once', 'ones', 'only', 'onto', 'open', 'ours', 'over', 'owed', 'owes', 'pack', 'page', 'paid', 'pain', 'pair', 'park', 'part', 'pass', 'past', 'path', 'pays', 'peak', 'pick', 'pile', 'pint', 'pipe', 'plan', 'play', 'plea', 'plot', 'plug', 'plus', 'poem', 'poet', 'poll', 'pool', 'poor', 'port', 'post', 'pull', 'pure', 'push', 'puts', 'putt', 'quit', 'race', 'rain', 'rare', 'rate', 'read', 'real', 'rely', 'rest', 'ride', 'rids', 'ring', 'rise', 'risk', 'road', 'role', 'roll', 'room', 'root', 'rule', 'runs', 'rush', 'safe', 'said', 'sake', 'sale', 'same', 'save', 'says', 'scan', 'seek', 'seem', 'seen', 'sees', 'self', 'sell', 'send', 'sent', 'sets', 'ship', 'shop', 'show', 'shut', 'side', 'sign', 'site', 'sits', 'size', 'slip', 'slow', 'soft', 'sold', 'some', 'soon', 'sort', 'spot', 'stay', 'step', 'stop', 'such', 'suit', 'sure', 'take', 'talk', 'tank', 'tape', 'task', 'team', 'tell', 'tend', 'term', 'test', 'text', 'than', 'that', 'them', 'then', 'they', 'thin', 'this', 'thus', 'tied', 'ties', 'till', 'time', 'told', 'took', 'town', 'trap', 'tree', 'trip', 'true', 'tune', 'turn', 'type', 'ugly', 'unit', 'upon', 'used', 'user', 'uses', 'vary', 'vast', 'very', 'vice', 'view', 'vote', 'wait', 'walk', 'wall', 'want', 'warm', 'warn', 'wash', 'ways', 'wear', 'week', 'well', 'went', 'were', 'west', 'what', 'when', 'whom', 'wide', 'wife', 'wild', 'will', 'wind', 'wine', 'wins', 'wire', 'wise', 'wish', 'with', 'word', 'wore', 'work', 'worn', 'year', 'your', 'zero'],
        5: ['about', 'above', 'abuse', 'acted', 'added', 'admit', 'adopt', 'after', 'again', 'agree', 'ahead', 'aimed', 'alarm', 'album', 'alias', 'alive', 'allow', 'alone', 'along', 'alter', 'among', 'amuse', 'angle', 'angry', 'annoy', 'apart', 'apple', 'apply', 'areas', 'argue', 'arise', 'aside', 'asked', 'avoid', 'awake', 'award', 'aware', 'awful', 'backs', 'badly', 'based', 'bases', 'basic', 'basis', 'bears', 'began', 'begin', 'begun', 'being', 'below', 'binds', 'bites', 'black', 'blame', 'blank', 'block', 'board', 'books', 'borne', 'bound', 'boxes', 'brand', 'break', 'brief', 'bring', 'broke', 'brown', 'build', 'built', 'bytes', 'calls', 'cards', 'cares', 'carry', 'cases', 'catch', 'cause', 'cease', 'chain', 'chair', 'chaos', 'chars', 'cheap', 'child', 'chips', 'chose', 'claim', 'class', 'clean', 'clear', 'clock', 'close', 'coded', 'codes', 'comes', 'costs', 'could', 'count', 'court', 'cover', 'crash', 'crazy', 'crisp', 'cross', 'cycle', 'daily', 'dated', 'dates', 'datum', 'deals', 'dealt', 'death', 'deems', 'delay', 'depth', 'digit', 'dirty', 'discs', 'ditto', 'doing', 'doors', 'doubt', 'dozen', 'drawn', 'draws', 'dream', 'drink', 'drive', 'drops', 'drove', 'dying', 'early', 'earth', 'eaten', 'edits', 'eight', 'elect', 'empty', 'ended', 'enemy', 'enjoy', 'enter', 'entry', 'equal', 'error', 'evens', 'event', 'every', 'exact', 'exist', 'extra', 'facts', 'fails', 'faith', 'falls', 'false', 'fancy', 'fatal', 'fault', 'feeds', 'feels', 'fewer', 'field', 'fight', 'filed', 'files', 'fills', 'final', 'finds', 'first', 'fixed', 'fixes', 'flash', 'flied', 'flies', 'float', 'floor', 'flown', 'folks', 'force', 'forms', 'forth', 'found', 'frame', 'fresh', 'fries', 'front', 'fully', 'funds', 'funny', 'gains', 'games', 'given', 'gives', 'glass', 'going', 'goods', 'grand', 'grant', 'graph', 'grave', 'great', 'green', 'grind', 'gross', 'group', 'grown', 'grows', 'guard', 'guess', 'guide', 'habit', 'hands', 'handy', 'hangs', 'happy', 'heads', 'heard', 'hears', 'heart', 'heavy', 'hello', 'helps', 'hence', 'hides', 'hints', 'holds', 'holes', 'hoped', 'hopes', 'horse', 'hotel', 'hours', 'house', 'human', 'hurry', 'ideal', 'ideas', 'image', 'imply', 'index', 'inner', 'input', 'issue', 'items', 'joins', 'joint', 'judge', 'jumps', 'keeps', 'kills', 'kinds', 'knock', 'known', 'knows', 'label', 'lacks', 'lands', 'large', 'lasts', 'later', 'leach', 'leads', 'learn', 'least', 'leave', 'legal', 'level', 'light', 'liked', 'likes', 'limit', 'lines', 'links', 'lists', 'lived', 'lives', 'loads', 'local', 'locks', 'logic', 'looks', 'loose', 'lorry', 'loses', 'lower', 'lucky', 'lunch', 'lying', 'magic', 'major', 'makes', 'march', 'marks', 'marry', 'match', 'maybe', 'means', 'meant', 'media', 'meets', 'merit', 'messy', 'metal', 'might', 'miles', 'minds', 'minor', 'mixed', 'mixes', 'model', 'money', 'month', 'moral', 'mouth', 'moved', 'moves', 'movie', 'music', 'naive', 'named', 'names', 'nasty', 'needs', 'never', 'nicer', 'night', 'noise', 'noisy', 'north', 'noted', 'notes', 'novel', 'occur', 'offer', 'often', 'older', 'omits', 'opens', 'order', 'other', 'ought', 'outer', 'owing', 'owner', 'pages', 'pairs', 'paper', 'parts', 'party', 'patch', 'pause', 'payed', 'peace', 'phase', 'phone', 'picks', 'piece', 'place', 'plain', 'plane', 'plans', 'plant', 'plays', 'plots', 'point', 'posts', 'pound', 'power', 'press', 'price', 'prime', 'print', 'prior', 'prone', 'proof', 'prove', 'pulls', 'putts', 'queue', 'quick', 'quiet', 'quite', 'quits', 'quote', 'radio', 'raise', 'range', 'rapid', 'rates', 'reach', 'react', 'reads', 'ready', 'refer', 'reply', 'right', 'river', 'rooms', 'rough', 'round', 'route', 'rules', 'sadly', 'safer', 'saint', 'sales', 'saved', 'saves', 'scale', 'scene', 'score', 'scrap', 'seeks', 'seems', 'sells', 'sends', 'sense', 'serve', 'seven', 'shall', 'shame', 'shape', 'share', 'sharp', 'sheet', 'shelf', 'shell', 'shift', 'shoot', 'shops', 'short', 'shown', 'shows', 'shuts', 'sides', 'sight', 'signs', 'silly', 'since', 'sites', 'sizes', 'skill', 'sleep', 'small', 'smile', 'solid', 'solve', 'sorry', 'sorts', 'sound', 'south', 'space', 'spare', 'speak', 'speed', 'spell', 'spend', 'spent', 'spite', 'split', 'spoke', 'spots', 'staff', 'stage', 'stand', 'start', 'state', 'stays', 'steal', 'stick', 'still', 'stock', 'stone', 'stood', 'stops', 'store', 'stuck', 'study', 'stuff', 'style', 'sugar', 'suits', 'table', 'taken', 'takes', 'talks', 'tanks', 'tapes', 'tasks', 'taste', 'teach', 'teeth', 'tells', 'tends', 'terms', 'tests', 'thank', 'their', 'there', 'these', 'thing', 'think', 'third', 'those', 'three', 'threw', 'throw', 'tight', 'timed', 'times', 'title', 'today', 'token', 'tooth', 'topic', 'total', 'touch', 'trace', 'track', 'train', 'traps', 'trash', 'treat', 'trees', 'trial', 'trick', 'tried', 'tries', 'truck', 'truly', 'trunk', 'trust', 'truth', 'turns', 'twice', 'tying', 'typed', 'types', 'under', 'unite', 'units', 'until', 'upper', 'upset', 'usage', 'users', 'using', 'usual', 'vague', 'valid', 'value', 'video', 'views', 'visit', 'vital', 'voice', 'votes', 'waits', 'walks', 'walls', 'wants', 'warns', 'waste', 'watch', 'water', 'wears', 'weeks', 'weird', 'wheel', 'where', 'which', 'while', 'white', 'whole', 'whose', 'wider', 'wills', 'woman', 'women', 'words', 'works', 'world', 'worry', 'worse', 'worst', 'worth', 'would', 'write', 'wrong', 'wrote', 'years', 'young', 'yours'],
        6: ['accept', 'access', 'accord', 'across', 'acting', 'action', 'active', 'actual', 'adding', 'adjust', 'admits', 'adopts', 'advice', 'advise', 'affair', 'affect', 'afford', 'afraid', 'agency', 'agreed', 'agrees', 'aiming', 'allows', 'almost', 'alters', 'always', 'amount', 'amused', 'amuses', 'animal', 'annoys', 'annual', 'answer', 'anyone', 'anyway', 'appeal', 'appear', 'argued', 'argues', 'arises', 'around', 'arrive', 'artist', 'asking', 'asleep', 'aspect', 'assume', 'assure', 'attach', 'attack', 'attend', 'author', 'autumn', 'avoids', 'backed', 'basing', 'became', 'become', 'before', 'begins', 'behalf', 'behave', 'behind', 'belong', 'better', 'beyond', 'bigger', 'binary', 'biting', 'bitten', 'boards', 'bodies', 'borrow', 'bother', 'bottle', 'bottom', 'bought', 'branch', 'breach', 'breaks', 'bridge', 'bright', 'brings', 'broken', 'bucket', 'budget', 'buffer', 'builds', 'buried', 'buries', 'button', 'buying', 'called', 'cannot', 'caught', 'caused', 'causes', 'chance', 'change', 'charge', 'choice', 'choose', 'chosen', 'church', 'circle', 'claims', 'clears', 'clever', 'closed', 'closer', 'closes', 'coding', 'coffee', 'column', 'coming', 'common', 'copied', 'copies', 'corner', 'counts', 'county', 'couple', 'course', 'covers', 'create', 'credit', 'crisis', 'crisps', 'cursor', 'cycles', 'damage', 'danger', 'dating', 'debate', 'decade', 'decent', 'decide', 'deduce', 'deemed', 'deeply', 'define', 'degree', 'delete', 'demand', 'depend', 'derive', 'design', 'desire', 'detail', 'detect', 'device', 'devote', 'differ', 'digits', 'dinner', 'direct', 'divide', 'dollar', 'domain', 'double', 'dozens', 'driven', 'driver', 'drives', 'during', 'easier', 'easily', 'eating', 'edited', 'editor', 'effect', 'effort', 'either', 'elects', 'enable', 'ending', 'enough', 'ensure', 'enters', 'entire', 'entity', 'eraser', 'errors', 'escape', 'evened', 'events', 'except', 'excess', 'excuse', 'exists', 'expand', 'expect', 'expert', 'extend', 'extent', 'factor', 'failed', 'fairly', 'fallen', 'family', 'famous', 'faster', 'father', 'faults', 'fewest', 'fields', 'figure', 'filing', 'filled', 'finger', 'finish', 'finite', 'firmly', 'fiscal', 'fishes', 'fitted', 'fixing', 'flight', 'floats', 'flying', 'follow', 'forced', 'forces', 'forget', 'forgot', 'formal', 'format', 'formed', 'former', 'fourth', 'freely', 'french', 'friend', 'future', 'gained', 'garden', 'gather', 'giving', 'global', 'gotten', 'grands', 'grants', 'grinds', 'ground', 'groups', 'growth', 'habits', 'handed', 'handle', 'hanged', 'happen', 'harder', 'hardly', 'having', 'headed', 'header', 'health', 'helped', 'hereby', 'hidden', 'hiding', 'higher', 'highly', 'honest', 'hoping', 'horses', 'ignore', 'images', 'impact', 'impose', 'inches', 'income', 'indeed', 'inform', 'inputs', 'insert', 'inside', 'insist', 'intend', 'invent', 'invite', 'issued', 'issues', 'itself', 'joined', 'killed', 'kindly', 'knocks', 'labels', 'lacked', 'ladies', 'landed', 'larger', 'latest', 'latter', 'layout', 'leaded', 'leader', 'learns', 'leaved', 'leaves', 'length', 'lesser', 'lesson', 'letter', 'levels', 'liable', 'lights', 'likely', 'liking', 'limits', 'linear', 'linked', 'listed', 'listen', 'little', 'living', 'loaded', 'locked', 'logged', 'longer', 'looked', 'losing', 'loudly', 'lowest', 'mainly', 'making', 'manage', 'manner', 'manual', 'marked', 'market', 'master', 'matter', 'medium', 'member', 'memory', 'merely', 'merits', 'method', 'middle', 'minded', 'minute', 'misled', 'missed', 'misses', 'misuse', 'mixing', 'models', 'modern', 'modify', 'moment', 'months', 'mostly', 'mother', 'motion', 'moving', 'myself', 'namely', 'naming', 'nation', 'nature', 'nearby', 'nearer', 'nearly', 'needed', 'nicest', 'nobody', 'normal', 'notice', 'notify', 'noting', 'number', 'object', 'obtain', 'occupy', 'occurs', 'offers', 'office', 'oldest', 'opened', 'oppose', 'option', 'orders', 'origin', 'others', 'output', 'owners', 'packet', 'papers', 'parent', 'partly', 'passed', 'passes', 'paying', 'people', 'period', 'permit', 'person', 'petrol', 'phrase', 'picked', 'pieces', 'placed', 'places', 'planet', 'played', 'please', 'plenty', 'pocket', 'points', 'police', 'policy', 'posted', 'pounds', 'powers', 'prefer', 'pretty', 'prices', 'prints', 'prompt', 'proper', 'proved', 'proves', 'public', 'pulled', 'purely', 'pushed', 'pushes', 'putted', 'quoted', 'quotes', 'raised', 'raises', 'random', 'rarely', 'rather', 'reader', 'really', 'reason', 'recall', 'recent', 'record', 'reduce', 'refers', 'refuse', 'regard', 'region', 'regret', 'reject', 'relate', 'remain', 'remark', 'remind', 'remote', 'remove', 'repair', 'repeat', 'report', 'resort', 'result', 'retain', 'return', 'reveal', 'review', 'rights', 'rubber', 'safely', 'safest', 'safety', 'sample', 'saving', 'saying', 'scheme', 'school', 'scores', 'screen', 'script', 'search', 'season', 'second', 'secret', 'secure', 'seeing', 'seemed', 'select', 'senior', 'serial', 'series', 'served', 'server', 'serves', 'settle', 'severe', 'shared', 'shares', 'should', 'showed', 'signal', 'signed', 'simple', 'simply', 'single', 'skills', 'slight', 'slower', 'slowly', 'smooth', 'social', 'solely', 'solved', 'solves', 'sooner', 'sorted', 'sought', 'sounds', 'source', 'spaces', 'speaks', 'speech', 'spells', 'spends', 'spirit', 'splits', 'spoken', 'spread', 'spring', 'square', 'stable', 'stages', 'stands', 'starts', 'stated', 'states', 'status', 'stayed', 'sticks', 'stones', 'stored', 'stores', 'stream', 'street', 'strict', 'strike', 'string', 'strong', 'struck', 'stupid', 'submit', 'subset', 'subtle', 'sudden', 'suffer', 'suited', 'summer', 'supply', 'surely', 'survey', 'switch', 'symbol', 'syntax', 'system', 'tables', 'taking', 'talked', 'target', 'taught', 'tested', 'thanks', 'theory', 'things', 'thinks', 'though', 'threat', 'thrown', 'throws', 'ticket', 'timing', 'titles', 'topics', 'toward', 'tracks', 'trains', 'travel', 'treats', 'trusts', 'trying', 'turned', 'twelve', 'twenty', 'typing', 'unable', 'unique', 'unless', 'unlike', 'update', 'upsets', 'useful', 'values', 'varied', 'varies', 'vastly', 'vector', 'virtue', 'vision', 'volume', 'waited', 'walked', 'wanted', 'warned', 'wasted', 'wastes', 'weapon', 'weight', 'wheels', 'whilst', 'widely', 'widest', 'willed', 'window', 'winter', 'wished', 'wishes', 'within', 'wonder', 'wooden', 'worded', 'worked', 'worker', 'worthy', 'writer', 'writes', 'yellow'],
        7: ['ability', 'absence', 'accepts', 'accords', 'account', 'achieve', 'acquire', 'actions', 'address', 'adopted', 'advance', 'advised', 'advises', 'affairs', 'affects', 'against', 'allowed', 'already', 'altered', 'amongst', 'amounts', 'amusing', 'ancient', 'annoyed', 'another', 'answers', 'anybody', 'apology', 'appears', 'applied', 'applies', 'approve', 'arguing', 'arrange', 'arrived', 'arrives', 'article', 'aspects', 'assumed', 'assumes', 'assured', 'assures', 'attempt', 'attends', 'attract', 'authors', 'average', 'avoided', 'awkward', 'backing', 'balance', 'battery', 'bearing', 'because', 'becomes', 'believe', 'belongs', 'benefit', 'besides', 'betting', 'between', 'bidding', 'biggest', 'binding', 'biology', 'bizarre', 'borrows', 'bothers', 'bracket', 'briefly', 'brother', 'brought', 'burying', 'calling', 'capable', 'capital', 'captain', 'careful', 'carried', 'carries', 'catches', 'causing', 'central', 'century', 'certain', 'chances', 'changed', 'changes', 'channel', 'chapter', 'charged', 'charges', 'cheaper', 'checked', 'chooses', 'circuit', 'citizen', 'claimed', 'clarify', 'classes', 'cleared', 'clearer', 'clearly', 'closely', 'closest', 'closing', 'collect', 'college', 'combine', 'command', 'comment', 'company', 'compare', 'complex', 'compose', 'compute', 'concept', 'concern', 'confirm', 'confuse', 'connect', 'consist', 'contact', 'contain', 'content', 'context', 'control', 'convert', 'copying', 'corners', 'correct', 'corrupt', 'costing', 'council', 'counted', 'counter', 'country', 'courses', 'covered', 'crashed', 'crashes', 'created', 'creates', 'culture', 'cumming', 'curious', 'current', 'cutting', 'damaged', 'damages', 'dealing', 'decided', 'decides', 'declare', 'deeming', 'default', 'defined', 'defines', 'degrees', 'deleted', 'deletes', 'deliver', 'demands', 'depends', 'derived', 'derives', 'designs', 'desired', 'desires', 'despite', 'destroy', 'details', 'detects', 'develop', 'devices', 'devoted', 'devotes', 'digital', 'directs', 'discuss', 'dislike', 'display', 'distant', 'disturb', 'divided', 'divides', 'drastic', 'drawing', 'drivers', 'driving', 'dropped', 'dubious', 'earlier', 'easiest', 'economy', 'editing', 'edition', 'editors', 'effects', 'efforts', 'elected', 'element', 'enables', 'ensured', 'ensures', 'entered', 'entitle', 'entries', 'equally', 'evening', 'exactly', 'examine', 'example', 'exclude', 'execute', 'existed', 'expands', 'expects', 'expense', 'experts', 'explain', 'express', 'extends', 'extract', 'extreme', 'factors', 'failing', 'failure', 'falling', 'farther', 'fashion', 'fastest', 'feature', 'federal', 'feeding', 'feeling', 'figures', 'filling', 'finally', 'finding', 'fingers', 'firstly', 'fitting', 'flashed', 'flashes', 'floated', 'follows', 'forcing', 'foreign', 'forever', 'forgets', 'forming', 'fortune', 'forward', 'freedom', 'friends', 'further', 'gaining', 'garbage', 'general', 'genuine', 'getting', 'granted', 'graphic', 'greater', 'greatly', 'grosses', 'grounds', 'growing', 'guessed', 'guesses', 'handing', 'handled', 'handles', 'hanging', 'happens', 'happily', 'hardest', 'harmful', 'heading', 'healthy', 'hearing', 'heavily', 'helpful', 'helping', 'herself', 'highest', 'himself', 'history', 'hitting', 'holding', 'holiday', 'however', 'hundred', 'husband', 'ignored', 'ignores', 'illegal', 'imagine', 'implied', 'implies', 'imposed', 'imposes', 'improve', 'incline', 'include', 'informs', 'initial', 'inserts', 'insists', 'install', 'instant', 'instead', 'integer', 'intends', 'invalid', 'invents', 'invited', 'invites', 'involve', 'isolate', 'issuing', 'joining', 'justify', 'keeping', 'killing', 'knocked', 'knowing', 'lacking', 'landing', 'largely', 'largest', 'leading', 'leaving', 'lecture', 'legally', 'lessons', 'letters', 'letting', 'library', 'limited', 'linking', 'listing', 'loading', 'locking', 'logging', 'logical', 'longest', 'looking', 'machine', 'managed', 'manager', 'manages', 'manuals', 'marking', 'massive', 'matches', 'matters', 'maximum', 'meaning', 'measure', 'medical', 'mediums', 'meeting', 'members', 'mention', 'message', 'methods', 'million', 'minding', 'minimal', 'minimum', 'minutes', 'mislead', 'missing', 'mistake', 'mistook', 'monitor', 'morning', 'natural', 'naughty', 'nearest', 'needing', 'neither', 'nervous', 'network', 'nothing', 'noticed', 'notices', 'nowhere', 'numbers', 'numbest', 'objects', 'obscure', 'observe', 'obtains', 'obvious', 'offered', 'officer', 'offices', 'omitted', 'opening', 'operate', 'opinion', 'opposed', 'opposes', 'options', 'ordered', 'outside', 'overall', 'package', 'painful', 'partial', 'parties', 'passing', 'patient', 'pattern', 'perfect', 'perform', 'perhaps', 'permits', 'persons', 'phrases', 'picking', 'picture', 'placing', 'planned', 'plastic', 'playing', 'pleased', 'pleases', 'pointed', 'popular', 'posting', 'precise', 'prefers', 'prepare', 'present', 'pressed', 'presses', 'presume', 'prevent', 'primary', 'printed', 'printer', 'private', 'problem', 'process', 'produce', 'product', 'project', 'promise', 'propose', 'protect', 'protest', 'provide', 'proving', 'publish', 'pulling', 'purpose', 'pushing', 'putting', 'qualify', 'quality', 'quarter', 'quicker', 'quickly', 'quietly', 'quoting', 'raising', 'rapidly', 'reached', 'reaches', 'readers', 'readily', 'reading', 'reality', 'reasons', 'receive', 'records', 'recover', 'reduced', 'reduces', 'reflect', 'refused', 'refuses', 'regards', 'regular', 'rejects', 'related', 'relates', 'release', 'remains', 'remarks', 'reminds', 'removal', 'removed', 'removes', 'repeats', 'replace', 'replied', 'replies', 'reports', 'request', 'require', 'reserve', 'respect', 'respond', 'restart', 'restore', 'results', 'returns', 'reveals', 'reverse', 'rewrite', 'ridding', 'roughly', 'routine', 'rubbish', 'running', 'satisfy', 'schools', 'science', 'scratch', 'screens', 'seconds', 'section', 'seeking', 'seeming', 'selects', 'selling', 'seminar', 'sending', 'serious', 'service', 'serving', 'session', 'setting', 'settled', 'settles', 'several', 'sharing', 'shopped', 'shorter', 'shortly', 'showing', 'signals', 'signing', 'similar', 'simpler', 'sitting', 'slowest', 'smaller', 'society', 'solving', 'somehow', 'someone', 'soonest', 'sorting', 'sounded', 'sources', 'speaker', 'special', 'specify', 'spotted', 'spreads', 'started', 'stating', 'station', 'staying', 'stopped', 'storage', 'storing', 'strange', 'strikes', 'strings', 'student', 'studied', 'studies', 'subject', 'submits', 'succeed', 'success', 'suffers', 'suffice', 'suggest', 'suiting', 'summary', 'support', 'suppose', 'surface', 'survive', 'suspect', 'suspend', 'symbols', 'systems', 'talking', 'teacher', 'teaches', 'tedious', 'telling', 'testing', 'thereby', 'thought', 'through', 'tickets', 'tonight', 'totally', 'touched', 'touches', 'towards', 'traffic', 'trained', 'trapped', 'treated', 'trivial', 'trouble', 'trusted', 'turning', 'typical', 'unaware', 'unclear', 'unhappy', 'uniform', 'unknown', 'unusual', 'updated', 'updates', 'upwards', 'useless', 'usually', 'utility', 'utterly', 'vaguely', 'variety', 'various', 'varying', 'version', 'visible', 'waiting', 'walking', 'wanting', 'warning', 'wasting', 'watched', 'watches', 'wearing', 'weather', 'weekend', 'welcome', 'western', 'whereas', 'whereby', 'whether', 'whoever', 'willing', 'windows', 'winning', 'wishing', 'without', 'wonders', 'wording', 'workers', 'working', 'worried', 'worries', 'writing', 'written'],
        8: ['absolute', 'academic', 'accepted', 'accident', 'accorded', 'accounts', 'accuracy', 'accurate', 'achieved', 'achieves', 'acquired', 'acquires', 'activity', 'actually', 'addition', 'adequate', 'admitted', 'adopting', 'advanced', 'advances', 'advising', 'affected', 'agreeing', 'allowing', 'altering', 'although', 'analogue', 'analysis', 'announce', 'annoying', 'answered', 'anyplace', 'anything', 'anywhere', 'apparent', 'appeared', 'applying', 'approach', 'approval', 'approved', 'approves', 'argument', 'arranged', 'arranges', 'arriving', 'articles', 'assembly', 'assuming', 'assuring', 'attached', 'attaches', 'attempts', 'attended', 'attitude', 'audience', 'avoiding', 'becoming', 'believed', 'believes', 'benefits', 'borrowed', 'bothered', 'brackets', 'branches', 'breaking', 'bringing', 'building', 'bulletin', 'business', 'campaign', 'capacity', 'carrying', 'cassette', 'catching', 'category', 'chairman', 'changing', 'channels', 'charging', 'cheapest', 'checking', 'chemical', 'children', 'choosing', 'claiming', 'clearest', 'clearing', 'collapse', 'collects', 'colleges', 'combined', 'combines', 'commands', 'comments', 'commonly', 'compared', 'compares', 'compiler', 'complain', 'complete', 'composed', 'composes', 'computed', 'computer', 'computes', 'concerns', 'concrete', 'confirms', 'confused', 'confuses', 'connects', 'consider', 'consists', 'constant', 'contains', 'contents', 'continue', 'contract', 'contrary', 'contrast', 'controls', 'convince', 'corrects', 'corrupts', 'counting', 'covering', 'crashing', 'creating', 'creation', 'creature', 'critical', 'customer', 'damaging', 'database', 'deciding', 'decision', 'declared', 'declares', 'decrease', 'dedicate', 'defining', 'definite', 'deleting', 'delivers', 'delivery', 'depended', 'deriving', 'describe', 'designed', 'desiring', 'destroys', 'detailed', 'detected', 'develops', 'devoting', 'directed', 'directly', 'director', 'disagree', 'disaster', 'discount', 'discover', 'displays', 'distance', 'distinct', 'district', 'disturbs', 'dividing', 'division', 'document', 'doubtful', 'dropping', 'earliest', 'economic', 'electing', 'election', 'electric', 'elements', 'elevator', 'emphasis', 'employee', 'engineer', 'enormous', 'ensuring', 'entering', 'entirely', 'entitled', 'entitles', 'entrance', 'estimate', 'evenings', 'everyone', 'evidence', 'examined', 'examines', 'examples', 'exchange', 'excluded', 'excludes', 'executed', 'executes', 'exercise', 'existing', 'expanded', 'expected', 'explains', 'explicit', 'extended', 'external', 'facility', 'familiar', 'farthest', 'feasible', 'features', 'feedback', 'finished', 'finishes', 'flashing', 'flexible', 'floating', 'followed', 'fraction', 'frequent', 'friendly', 'function', 'furthest', 'gasoline', 'generate', 'governor', 'graduate', 'granting', 'graphics', 'grateful', 'greatest', 'grinding', 'guessing', 'handling', 'happened', 'hardware', 'harmless', 'holidays', 'horrible', 'hospital', 'hundreds', 'identify', 'identity', 'ignoring', 'implying', 'imposing', 'improved', 'improves', 'incident', 'inclined', 'inclines', 'included', 'includes', 'increase', 'indicate', 'industry', 'inferior', 'infinite', 'informed', 'initials', 'innocent', 'inputted', 'inserted', 'insisted', 'installs', 'instance', 'integers', 'integral', 'intended', 'interact', 'interest', 'internal', 'interval', 'invented', 'inviting', 'involved', 'involves', 'irritate', 'isolated', 'isolates', 'keyboard', 'knocking', 'language', 'learning', 'lectures', 'lifetime', 'likewise', 'limiting', 'location', 'machines', 'magnetic', 'maintain', 'majority', 'managing', 'marriage', 'material', 'meanings', 'measured', 'measures', 'mechanic', 'meetings', 'mentions', 'messages', 'midnight', 'military', 'millions', 'minority', 'misleads', 'mistaken', 'mistakes', 'modified', 'modifies', 'mornings', 'movement', 'multiple', 'national', 'negative', 'networks', 'nonsense', 'normally', 'noticing', 'nowadays', 'numerous', 'objected', 'observed', 'observes', 'obtained', 'occasion', 'occupied', 'occupies', 'occurred', 'offering', 'official', 'omitting', 'operated', 'operates', 'operator', 'opinions', 'opposing', 'opposite', 'optional', 'ordering', 'ordinary', 'original', 'packages', 'parallel', 'patterns', 'peculiar', 'performs', 'personal', 'persuade', 'physical', 'pictures', 'planning', 'pleasant', 'pleasing', 'pointing', 'policies', 'position', 'positive', 'possible', 'possibly', 'powerful', 'prepared', 'prepares', 'presence', 'presents', 'preserve', 'pressing', 'pressure', 'prevents', 'previous', 'printers', 'printing', 'printout', 'probably', 'problems', 'produced', 'produces', 'products', 'progress', 'projects', 'promised', 'promises', 'promptly', 'properly', 'property', 'proposal', 'proposed', 'proposes', 'prospect', 'protects', 'provided', 'provides', 'publicly', 'puncture', 'purchase', 'purposes', 'quantity', 'question', 'quickest', 'quitting', 'randomly', 'reaching', 'reaction', 'readable', 'received', 'receives', 'recently', 'recorded', 'recovers', 'reducing', 'referred', 'reflects', 'refusing', 'regarded', 'register', 'rejected', 'relating', 'relation', 'relative', 'released', 'releases', 'relevant', 'reliable', 'religion', 'remained', 'remember', 'reminded', 'remotely', 'removing', 'repeated', 'replaced', 'replaces', 'replying', 'reported', 'requests', 'required', 'requires', 'research', 'reserved', 'reserves', 'resident', 'resource', 'respects', 'response', 'restored', 'restores', 'restrict', 'resulted', 'returned', 'revealed', 'sciences', 'searched', 'searches', 'secondly', 'sections', 'security', 'selected', 'sensible', 'sensibly', 'sentence', 'separate', 'sequence', 'services', 'sessions', 'settling', 'severely', 'shopping', 'shortage', 'shortest', 'shutting', 'simplest', 'slightly', 'smallest', 'software', 'solution', 'somebody', 'sometime', 'somewhat', 'sounding', 'southern', 'speakers', 'speaking', 'specific', 'spelling', 'spending', 'spotting', 'standard', 'standing', 'starting', 'stations', 'sticking', 'stopping', 'straight', 'strategy', 'strength', 'strictly', 'striking', 'strongly', 'students', 'studying', 'subjects', 'suddenly', 'suffered', 'suggests', 'suitable', 'suitably', 'superior', 'supplied', 'supplies', 'supports', 'supposed', 'supposes', 'surprise', 'survived', 'survives', 'suspects', 'suspends', 'switched', 'switches', 'teaching', 'tendency', 'terminal', 'terribly', 'thinking', 'thoughts', 'thousand', 'throwing', 'together', 'tomorrow', 'touching', 'training', 'transfer', 'trapping', 'treating', 'trusting', 'ultimate', 'unlikely', 'unwanted', 'updating', 'vacation', 'validity', 'valuable', 'variable', 'versions', 'watching', 'welcomed', 'welcomes', 'whatever', 'whenever', 'wherever', 'withdraw', 'wondered', 'worrying', 'yourself'],
        9: ['abilities', 'accepting', 'according', 'achieving', 'acquiring', 'addressed', 'addresses', 'admitting', 'advancing', 'advantage', 'advertise', 'affecting', 'afternoon', 'agreement', 'algorithm', 'alternate', 'ambiguous', 'anonymous', 'answering', 'apologies', 'appearing', 'approving', 'arbitrary', 'arguments', 'arranging', 'assembler', 'assistant', 'associate', 'attaching', 'attempted', 'attending', 'attention', 'authority', 'automatic', 'available', 'backwards', 'basically', 'beautiful', 'beginning', 'believing', 'borrowing', 'bothering', 'broadcast', 'buildings', 'calculate', 'candidate', 'cardboard', 'carefully', 'certainly', 'character', 'collected', 'combining', 'commented', 'committee', 'community', 'comparing', 'complains', 'complaint', 'completed', 'completes', 'component', 'composing', 'computers', 'computing', 'concerned', 'condition', 'confident', 'confirmed', 'confusing', 'confusion', 'connected', 'considers', 'construct', 'contained', 'continued', 'continues', 'convinced', 'convinces', 'corrected', 'correctly', 'corrupted', 'criticism', 'currently', 'dangerous', 'decisions', 'declaring', 'dedicated', 'dedicates', 'delivered', 'depending', 'described', 'describes', 'designing', 'desirable', 'desperate', 'destroyed', 'detailing', 'detecting', 'determine', 'developed', 'different', 'difficult', 'directing', 'direction', 'directory', 'disappear', 'discovers', 'discussed', 'discusses', 'displayed', 'disturbed', 'documents', 'education', 'effective', 'efficient', 'elsewhere', 'embarrass', 'emergency', 'encounter', 'encourage', 'engineers', 'entitling', 'equipment', 'essential', 'establish', 'everybody', 'examining', 'excellent', 'exception', 'excessive', 'excluding', 'exclusive', 'executing', 'existence', 'expanding', 'expansion', 'expecting', 'expensive', 'explained', 'expressed', 'expresses', 'extending', 'extension', 'extensive', 'extremely', 'financial', 'finishing', 'following', 'forgotten', 'functions', 'generally', 'generated', 'generates', 'gradually', 'guarantee', 'happening', 'hopefully', 'identical', 'immediate', 'implement', 'important', 'improving', 'inability', 'inclining', 'including', 'incorrect', 'increased', 'increases', 'indicates', 'influence', 'informing', 'initially', 'inputting', 'inserting', 'insisting', 'installed', 'instantly', 'insurance', 'intending', 'intention', 'interests', 'interface', 'interpret', 'intervals', 'introduce', 'inventing', 'invisible', 'involving', 'irritated', 'irritates', 'isolating', 'justified', 'justifies', 'knowledge', 'languages', 'libraries', 'literally', 'locations', 'magnitude', 'maintains', 'materials', 'measuring', 'mechanics', 'mechanism', 'mentioned', 'mistaking', 'modifying', 'movements', 'naturally', 'necessary', 'necessity', 'numerical', 'objecting', 'objection', 'observing', 'obtaining', 'obviously', 'occasions', 'occupying', 'occurring', 'operating', 'operation', 'operators', 'otherwise', 'ourselves', 'paragraph', 'partially', 'perfectly', 'performed', 'permanent', 'permitted', 'persuaded', 'persuades', 'pointless', 'political', 'positions', 'potential', 'practical', 'precisely', 'preferred', 'preparing', 'presented', 'president', 'prevented', 'primitive', 'principle', 'procedure', 'processed', 'processes', 'processor', 'producing', 'promising', 'proposing', 'protected', 'providing', 'publicity', 'published', 'publishes', 'qualified', 'qualifies', 'questions', 'receiving', 'reception', 'recommend', 'recording', 'recovered', 'reduction', 'redundant', 'reference', 'referring', 'reflected', 'regarding', 'registers', 'regularly', 'rejecting', 'releasing', 'relevance', 'religious', 'reluctant', 'remaining', 'remembers', 'reminding', 'repeating', 'replacing', 'reporting', 'represent', 'reproduce', 'requested', 'requiring', 'reserving', 'resourced', 'resources', 'responses', 'restoring', 'restricts', 'resulting', 'returning', 'revealing', 'satisfied', 'satisfies', 'searching', 'secondary', 'secretary', 'selecting', 'selection', 'sensitive', 'sentences', 'sequences', 'seriously', 'similarly', 'sincerely', 'situation', 'solutions', 'someplace', 'something', 'sometimes', 'somewhere', 'specially', 'specified', 'specifies', 'splitting', 'spreading', 'standards', 'statement', 'statistic', 'structure', 'submitted', 'suffering', 'suggested', 'supplying', 'supported', 'supposing', 'surprised', 'surprises', 'surviving', 'suspected', 'suspended', 'suspicion', 'switching', 'technical', 'technique', 'telephone', 'temporary', 'terminals', 'therefore', 'thousands', 'transfers', 'translate', 'transport', 'treatment', 'uncertain', 'universal', 'unlimited', 'upsetting', 'vacations', 'variables', 'variation', 'virtually', 'welcoming', 'wonderful', 'wondering', 'yesterday'],
        10: ['absolutely', 'acceptable', 'accessible', 'accidental', 'activities', 'additional', 'addressing', 'admittedly', 'advantages', 'advertised', 'advertises', 'algorithms', 'altogether', 'apparently', 'appearance', 'appreciate', 'arithmetic', 'artificial', 'associated', 'associates', 'assumption', 'atmosphere', 'attempting', 'attractive', 'automobile', 'background', 'beforehand', 'broadcasts', 'categories', 'characters', 'collecting', 'collection', 'commenting', 'commercial', 'commission', 'commitment', 'comparable', 'comparison', 'compatible', 'complained', 'complaints', 'completely', 'completing', 'complexity', 'complicate', 'components', 'compromise', 'compulsory', 'concerning', 'conclusion', 'conditions', 'conference', 'confirming', 'connecting', 'connection', 'considered', 'consistent', 'constraint', 'containing', 'continuing', 'continuous', 'contribute', 'controlled', 'convenient', 'convention', 'convincing', 'correcting', 'correction', 'corrupting', 'dedicating', 'definitely', 'definition', 'definitive', 'deliberate', 'delivering', 'democratic', 'department', 'describing', 'destroying', 'determined', 'determines', 'developing', 'dictionary', 'difference', 'difficulty', 'directions', 'disappears', 'discipline', 'discourage', 'discovered', 'discussing', 'discussion', 'displaying', 'distinctly', 'distribute', 'disturbing', 'documented', 'electronic', 'encounters', 'encouraged', 'encourages', 'engineered', 'equivalent', 'especially', 'eventually', 'everything', 'everywhere', 'exceptions', 'experience', 'experiment', 'explaining', 'expressing', 'expression', 'facilities', 'forgetting', 'frequently', 'generating', 'generation', 'government', 'guaranteed', 'guarantees', 'historical', 'implements', 'importance', 'impossible', 'impression', 'inadequate', 'incomplete', 'increasing', 'indication', 'individual', 'industrial', 'inevitably', 'installing', 'interested', 'interprets', 'introduced', 'introduces', 'invariably', 'invitation', 'irrelevant', 'irritating', 'justifying', 'laboratory', 'literature', 'maintained', 'meaningful', 'membership', 'mentioning', 'misleading', 'mysterious', 'objections', 'occasional', 'operations', 'opposition', 'originally', 'particular', 'performing', 'permission', 'permitting', 'personally', 'persuading', 'phenomenon', 'philosophy', 'population', 'postmaster', 'preferable', 'preferably', 'preference', 'preferring', 'presenting', 'presumably', 'preventing', 'previously', 'principles', 'processing', 'processors', 'production', 'programmer', 'properties', 'proportion', 'protecting', 'protection', 'publishing', 'qualifying', 'quantities', 'reasonable', 'reasonably', 'recommends', 'recovering', 'references', 'reflecting', 'reflection', 'regardless', 'registered', 'regulation', 'relatively', 'remembered', 'repeatedly', 'represents', 'requesting', 'resolution', 'resourcing', 'restricted', 'ridiculous', 'satisfying', 'scientific', 'separately', 'situations', 'specifying', 'statements', 'statistics', 'structures', 'submitting', 'subsequent', 'substitute', 'successful', 'sufficient', 'suggesting', 'suggestion', 'supervisor', 'supporting', 'supposedly', 'surprising', 'suspecting', 'suspending', 'techniques', 'technology', 'television', 'themselves', 'thoroughly', 'throughout', 'translated', 'translates', 'ultimately', 'underneath', 'understand', 'understood', 'university', 'unpleasant', 'unsuitable', 'whatsoever', 'widespread', 'worthwhile'],
        11: ['accordingly', 'advertising', 'alternative', 'application', 'appreciated', 'appreciates', 'appropriate', 'arrangement', 'associating', 'association', 'authorities', 'calculation', 'circulation', 'combination', 'communicate', 'competition', 'complaining', 'complicated', 'complicates', 'composition', 'connections', 'consequence', 'considering', 'consistency', 'constraints', 'consumption', 'continually', 'controlling', 'conventions', 'definitions', 'demonstrate', 'description', 'determining', 'development', 'differences', 'differently', 'disappeared', 'discouraged', 'discourages', 'discovering', 'discussions', 'distinction', 'distinguish', 'distributed', 'distributes', 'documenting', 'educational', 'effectively', 'electronics', 'embarrassed', 'embarrasses', 'encountered', 'encouraging', 'engineering', 'environment', 'essentially', 'established', 'establishes', 'experienced', 'experiences', 'experiments', 'explanation', 'forthcoming', 'fortunately', 'fundamental', 'imagination', 'immediately', 'implemented', 'implication', 'importantly', 'improvement', 'independent', 'individuals', 'information', 'institution', 'instruction', 'intelligent', 'interesting', 'interpreted', 'introducing', 'investigate', 'maintaining', 'mathematics', 'meaningless', 'necessarily', 'observation', 'opportunity', 'performance', 'permanently', 'possibility', 'potentially', 'practically', 'preparation', 'programmers', 'publication', 'punctuation', 'recognition', 'recommended', 'registering', 'regulations', 'remembering', 'replacement', 'represented', 'requirement', 'responsible', 'restricting', 'significant', 'statistical', 'substantial', 'suggestions', 'temperature', 'temporarily', 'terminology', 'theoretical', 'traditional', 'transferred', 'translating', 'translation', 'understands', 'unfortunate', 'unnecessary'],
        12: ['accidentally', 'alternatives', 'announcement', 'applications', 'appreciating', 'arrangements', 'broadcasting', 'calculations', 'circumstance', 'combinations', 'complicating', 'consequences', 'consequently', 'considerable', 'considerably', 'continuation', 'continuously', 'contribution', 'conventional', 'conversation', 'deliberately', 'descriptions', 'difficulties', 'disadvantage', 'disappearing', 'discouraging', 'distributing', 'distribution', 'embarrassing', 'encountering', 'establishing', 'experiencing', 'experimental', 'guaranteeing', 'implementing', 'implications', 'improvements', 'incidentally', 'incompatible', 'inconsistent', 'individually', 'institutions', 'instructions', 'intelligence', 'interpreting', 'intervention', 'introduction', 'manipulation', 'mathematical', 'nevertheless', 'occasionally', 'particularly', 'professional', 'recommending', 'relationship', 'representing', 'requirements', 'respectively', 'significance', 'simultaneous', 'sophisticate', 'specifically', 'successfully', 'sufficiently', 'transferring', 'unacceptable', 'universities', 'unreasonable']
    };
});

// lib/game/main.js
ig.baked = true;
ig.module('game.main').requires('impact.game', 'impact.font', 'game.menus.about', 'game.menus.game-over', 'game.menus.pause', 'game.menus.title', 'game.entities.enemy-missle', 'game.entities.enemy-mine', 'game.entities.enemy-destroyer', 'game.entities.enemy-oppressor', 'game.entities.player', 'game.keyboard', 'game.xhr', 'game.ease', 'plugins.silent-loader', 'plugins.rise-loader', 'game.document-scanner', 'game.words.en').defines(function() {
    Number.zeroes = '000000000000';
    Number.prototype.zeroFill = function(d) {
        var s = this.toString();
        return Number.zeroes.substr(0, d - s.length) + s;
    }
    ;
    ZType = ig.Game.extend({
        font: new ig.Font('media/fonts/avenir-18-white.png'),
        fontTitle: new ig.Font('media/fonts/avenir-36-blue.png'),
        separatorBar: new ig.Image('media/ui/bar-blue.png'),
        idleTimer: null ,
        spawnTimer: null ,
        targets: {},
        currentTarget: null ,
        yScroll: 0,
        yScroll2: 0,
        gradient: new ig.Image('media/background/gradient.png'),
        stars: new ig.Image('media/background/stars.jpg'),
        grid: new ig.Image('media/background/grid.png'),
        music1: new ig.Sound('media/music/endure.ogg',false),
        music2: new ig.Sound('media/music/orientation.ogg',false),
        cancelSound: new ig.Sound('media/sounds/cancel.ogg'),
        spawnSound: new ig.Sound('media/sounds/spawn.ogg'),
        menu: null ,
        mode: 0,
        score: 0,
        streak: 0,
        hits: 0,
        misses: 0,
        multiplier: 1,
        wave: {},
        gameTime: 0,
        kills: 0,
        emps: 0,
        personalBest: 0,
        isPersonalBest: false,
        waitingForItunes: false,
        adPage: null ,
        difficulty: (ig.ua.mobile ? 'MOBILE' : 'DESKTOP'),
        keyboard: null ,
        _screenShake: 0,
        wordlist: null ,
        init: function() {
            if (ig.doc && ig.doc.fragments.length < 2) {
                ig.doc = null ;
            }
            this.fontTitle.letterSpacing = -2;
            this.font.letterSpacing = -1;
            var bgmap = new ig.BackgroundMap(620,[[1]],this.grid);
            bgmap.repeat = true;
            this.backgroundMaps.push(bgmap);
            ig.music.add(this.music1);
            ig.music.add(this.music2);
            ig.music.loop = true;
            ig.music.random = true;
            var soundVolume = localStorage.getItem('soundVolume')/2;
            var musicVolume = localStorage.getItem('musicVolume');
            if (soundVolume !== null  && musicVolume !== null ) {
                ig.soundManager.volume = parseFloat(soundVolume);
                ig.music.volume = parseFloat(musicVolume);
            }
            window.addEventListener('keypress', this.keypress.bind(this), false);
            window.addEventListener('keydown', this.keydown.bind(this), false);
            this.keyboard = new ig.Keyboard(this.virtualKeydown.bind(this));
            ig.input.bind(ig.KEY.ENTER, 'ok');
            ig.input.bind(ig.KEY.SPACE, 'ok');
            ig.input.bind(ig.KEY.MOUSE1, 'click');
            ig.input.bind(ig.KEY.ESC, 'menu');
            ig.input.bind(ig.KEY.UP_ARROW, 'up');
            ig.input.bind(ig.KEY.DOWN_ARROW, 'down');
            ig.input.bind(ig.KEY.LEFT_ARROW, 'left');
            ig.input.bind(ig.KEY.RIGHT_ARROW, 'right');
            ig.system.canvas.onclick = function() {
                window.focus();
            }
            ;
            this.personalBest = parseInt(localStorage.getItem('highscore')) | 0;
            if (window.Ejecta) {
                this.gameCenter = new Ejecta.GameCenter();
                this.gameCenter.authenticate();
                if (!localStorage.getItem('removeAds')) {
                    this.adPage = new Ejecta.AdMobPage("ca-app-pub-8533552145182353/1344920700");
                }
            }
            if (window.Cocoon && window.Cocoon.Ad) {
                Cocoon.Ad.configure({
                    android: {
                        interstitial: 'ca-app-pub-8533552145182353/1042008307'
                    }
                });
                this.cocoonInterstitial = Cocoon.Ad.createInterstitial();
            }
            this.setTitle();
            this.wordlist = ig.WORDS.EN;
            if (ig.doc) {
                this.reAllWordCharacter = /^[a-zßàáâãäåæçèéêëìíîïðñòóôõöøùúûüý]+$/i;
                this.reSplitNonWord = /[^0-9a-zßàáâãäåæçèéêëìíîïðñòóôõöøùúûüý]/i;
                ig.doc.fastForwardScanAnimation();
            }
        },
        reset: function() {
            this.entities = [];
            this.currentTarget = null ;
            this.wave = ig.copy(ZType.WAVES[this.difficulty]);
            var first = 'a'.charCodeAt(0)
              , last = 'z'.charCodeAt(0);
            for (var i = first; i <= last; i++) {
                this.targets[String.fromCharCode(i)] = [];
            }
            for (var c in this._umlautTable) {
                this.targets[c] = [];
            }
            this.score = 0;
            this.rs = 0;
            this.streak = 0;
            this.longestStreak = 0;
            this.hits = 0;
            this.misses = 0;
            this.kills = 0;
            this.multiplier = 1;
            this.gameTime = 0;
            this.isPersonalBest = false;
            this.speedFactor = 1;
            this.lastKillTimer = new ig.Timer();
            this.spawnTimer = new ig.Timer();
            this.idleTimer = new ig.Timer();
            this.waveEndTimer = null ;
        },
        nextWave: function() {
            this.wave.wave++;
            this.wave.spawnWait = (this.wave.spawnWait * 0.97).limit(0.2, 1);
            this.wave.currentSpawnWait = this.wave.spawnWait;
            this.wave.spawn = [];
            this.speedFactor *= this.wave.speedIncrease;
            if (ig.doc) {
                for (var i = 0; i < 10 && this.wave.spawn.length < 2; i++) {
                    this.nextDocFragment();
                }
                this.wave.spawn.reverse();
            } 
            else {
                var dec = 0;
                for (var t = 0; t < this.wave.types.length; t++) {
                    var type = this.wave.types[t];
                    type.count -= dec;
                    if (this.wave.wave % type.incEvery == 0) {
                        type.count++;
                        dec++;
                    }
                    for (var s = 0; s < type.count; s++) {
                        this.wave.spawn.push(type);
                    }
                }
                this.wave.spawn.sort(function() {
                    return Math.random() - 0.5;
                });
            }
        },
        nextDocFragment: function() {
            this.wave.fragment++;
            var fragment = ig.doc.fragments[(this.wave.fragment - 1) % ig.doc.fragments.length];
            if (!ig.ua.mobile) {
                ig.doc.highlightFragment(fragment);
            }
            for (var t = 0; t < this.wave.types.length; t++) {
                var type = this.wave.types[t];
                if (this.wave.wave % type.incEvery == 0) {
                    type.count++;
                }
            }
            var words = fragment.text.replace(/['’‘’’]/g, '').split(this.reSplitNonWord);
            var filteredWords = [];
            for (var i = 0; i < words.length; i++) {
                var w = words[i].trim();
                if (w.match(this.reAllWordCharacter)) {
                    filteredWords.push(w);
                }
            }
            var wordsByLength = filteredWords.slice().sort(function(a, b) {
                return b.length - a.length;
            });
            var bigShipChance = (this.wave.types[0].count + this.wave.types[1].count) / this.wave.types[2].count;
            var wordLengthForBigShip = wordsByLength[Math.floor(wordsByLength.length * bigShipChance * 0.75)].length;
            var longSentenceFactor = (filteredWords.length / 8).limit(1, 1.5);
            for (var i = 0; i < filteredWords.length; i++) {
                var w = filteredWords[i];
                var wait = (w.length / 5).limit(0.7, 3) * 1.2 * longSentenceFactor;
                var type = (w.length > wordLengthForBigShip) ? (Math.random() > 0.75 ? EntityEnemyOppressor : EntityEnemyDestroyer) : EntityEnemyMine;
                this.wave.spawn.push({
                    type: type,
                    word: w,
                    wait: wait
                });
            }
        },
        spawnCurrentWave: function() {
            if (!this.wave.spawn.length) {
                if (this.entities.length <= 1 && !this.waveEndTimer) {
                    this.waveEndTimer = new ig.Timer(2);
                } 
                else if (this.waveEndTimer && this.waveEndTimer.delta() > 0) {
                    this.waveEndTimer = null ;
                    this.nextWave();
                }
            } 
            else if (this.spawnTimer.delta() > this.wave.currentSpawnWait) {
                this.spawnTimer.reset();
                var spawn = this.wave.spawn.pop();
                var x = Math.random().map(0, 1, 10, ig.system.width - 10);
                var y = -30;
                this.spawnEntity(spawn.type, x, y, {
                    healthBoost: this.wave.healthBoost,
                    word: spawn.word
                }, true);
                this.wave.currentSpawnWait = spawn.wait ? this.wave.spawnWait * spawn.wait : this.wave.spawnWait;
            }
        },
        spawnEntity: function(type, x, y, settings, atBeginning) {
            var ent = new (type)(x,y,settings || {});
            if (atBeginning) {
                this.entities.unshift(ent);
            } 
            else {
                this.entities.push(ent);
            }
            if (ent.name) {
                this.namedEntities[ent.name] = ent;
            }
            return ent;
        },
        registerTarget: function(letter, ent) {
            var c = this.translateUmlaut(letter.toLowerCase());
            this.targets[c].push(ent);
            if (!this.currentTarget) {
                this.setExpectedKeys();
            }
        },
        unregisterTarget: function(letter, ent) {
            var c = this.translateUmlaut(letter.toLowerCase());
            this.targets[c].erase(ent);
            if (!this.currentTarget) {
                this.setExpectedKeys();
            }
        },
        setExpectedKeys: function() {
            this.keyboard.expectedKeys = [];
            for (var k in this.targets) {
                if (this.targets[k].length) {
                    this.keyboard.expectedKeys.push(k);
                }
            }
        },
        _umlautTable: {
            'ß': 's',
            'à': 'a',
            'á': 'a',
            'â': 'a',
            'ã': 'a',
            'ä': 'a',
            'å': 'a',
            'æ': 'a',
            'ç': 'c',
            'è': 'e',
            'é': 'e',
            'ê': 'e',
            'ë': 'e',
            'ì': 'i',
            'í': 'i',
            'î': 'i',
            'ï': 'i',
            'ð': 'd',
            'ñ': 'n',
            'ò': 'o',
            'ó': 'o',
            'ô': 'o',
            'õ': 'o',
            'ö': 'o',
            'ø': 'o',
            'ù': 'u',
            'ú': 'u',
            'û': 'u',
            'ü': 'u',
            'ý': 'y'
        },
        translateUmlaut: function(k) {
            if (ig.ua.mobile || (ig.doc && ig.doc.looksLikeEnglish)) {
                return this._umlautTable[k] || k;
            } 
            else {
                return k;
            }
        },
        keypress: function(ev) {
            if (ev.target.tagName == 'INPUT' || ev.ctrlKey || ev.altKey || this.mode != ZType.MODE.GAME || this.menu) {
                return true;
            }
            var c = ev.charCode;
            if (c < 64) {
                return true;
            }
            ev.stopPropagation();
            ev.preventDefault();
            var letter = String.fromCharCode(c).toLowerCase();
            this.shoot(letter);
            return false;
        },
        keydown: function(ev) {
            if (ev.target.tagName == 'INPUT' || ev.ctrlKey || ev.altKey || this.mode != ZType.MODE.GAME || this.menu) {
                return true;
            }
            var c = ev.which;
            if (c === ig.KEY.ENTER) {
                this.player.spawnEMP();
                return false;
            }
            if (c == ig.KEY.BACKSPACE) {
                if (this.currentTarget) {
                    this.currentTarget.cancel();
                    this.cancelSound.play();
                }
                ev.preventDefault();
                return false;
            }
            return true;
        },
        virtualKeydown: function(letter) {
            if (this.mode != ZType.MODE.GAME || this.menu) {
                return true;
            }
            if (letter == 'ENTER') {
                this.player.spawnEMP();
                return true;
            }
            if (letter == 'ESC') {
                this.menu = new MenuPause();
                return true;
            }
            if (letter == 'BACKSPACE') {
                if (this.currentTarget) {
                    this.currentTarget.cancel();
                    this.cancelSound.play();
                }
                return true;
            }
            this.shoot(letter);
        },
        shoot: function(letter) {
            this.idleTimer.reset();
            if (!this.currentTarget) {
                var potentialTargets = this.targets[letter];
                var nearestDistance = -1;
                var nearestTarget = null ;
                for (var i = 0; i < potentialTargets.length; i++) {
                    var distance = this.player.distanceTo(potentialTargets[i]);
                    if (distance < nearestDistance || !nearestTarget) {
                        nearestDistance = distance;
                        nearestTarget = potentialTargets[i];
                    }
                }
                if (nearestTarget) {
                    nearestTarget.target();
                } 
                else {
                    this.player.miss();
                    this.multiplier = 1;
                    this.streak = 0;
                    this.misses++;
                }
            }
            if (this.currentTarget) {
                var target = this.currentTarget;
                var hit = this.currentTarget.isHitBy(letter);
                if (hit) {
                    this.player.shoot(target);
                    this.score += this.multiplier;
                    this.hits++;
                    this.streak++;
                    this.longestStreak = Math.max(this.streak, this.longestStreak);
                    if (ZType.MULTIPLIER_TIERS[this.streak]) {
                        this.multiplier = ZType.MULTIPLIER_TIERS[this.streak];
                        this.keyboard.showMultiplier(this.multiplier);
                    }
                    if (target.dead) {
                        this.kills++;
                        this.setExpectedKeys();
                    } 
                    else {
                        var translated = this.translateUmlaut(target.remainingWord.charAt(0).toLowerCase());
                        if (this.keyboard) {
                            this.keyboard.expectedKeys = [translated];
                        }
                    }
                } 
                else {
                    this.player.miss();
                    this.multiplier = 1;
                    this.streak = 0;
                    this.misses++;
                }
            }
        },
        setGame: function() {
            this.reset();
            this.gameTransitionTimer = new ig.Timer(2);
            var sx = ig.system.width / 2 - 6
              , sy = ig.system.height - this.keyboard.height * this.keyboard.drawScale - 30;
            this.player = this.spawnEntity(EntityPlayer, sx, sy);
            this.mode = ZType.MODE.GAME;
            this.nextWave();
            ig.music.next();
            this.spawnSound.play();
            this.emps = 3;
        },
        setGameOver: function() {
            if (this.score > this.personalBest) {
                this.isPersonalBest = true;
                this.personalBest = this.score;
                localStorage.setItem('highscore', this.personalBest);
            }
            if (this.gameCenter && this.score > 5) {
                this.gameCenter.reportScore('score', this.score);
            }
            this.mode = ZType.MODE.GAME_OVER;
            ig.music.fadeOut(1);
        },
        showGameOverScreen: function() {
            this.menu = new MenuGameOver();
            if (this.adPage && !localStorage.getItem('removeAds')) {
                this.adPage.show();
            }
            if (this.cocoonInterstitial) {
                this.cocoonInterstitial.show();
            }
        },
        setTitle: function() {
            if (this.cocoonInterstitial) {
                this.cocoonInterstitial.load();
            }
            this.reset();
            this.mode = ZType.MODE.TITLE;
            this.menu = new MenuTitle();
            this.emps = 0;
        },
        update: function() {
            if (ig.input.pressed('menu')) {
                if (this.menu && this.menu instanceof MenuPause) {
                    this.menu = null ;
                } 
                else if (!this.menu) {
                    this.menu = new MenuPause();
                }
            }
            if (this.menu) {
                this.backgroundMaps[0].scroll.y -= 100 * ig.system.tick;
                if (this.waitingForItunes) {
                    return;
                }
                this.menu.update();
                if (!(this.menu instanceof MenuGameOver)) {
                    return;
                }
            }
            this.parent();
            if (this.mode === ZType.MODE.GAME) {
                this.spawnCurrentWave();
                if (!this.menu && !ig.ua.mobile && ig.input.pressed('click') && ig.input.mouse.x < 64 && ig.input.mouse.y < 64) {
                    this.menu = new MenuPause();
                }
            } 
            else if (ig.input.pressed('ok')) {
                if (this.mode === ZType.MODE.TITLE) {
                    this.setGame();
                } 
                else if ((this.mode === ZType.MODE.GAME_OVER && this.menu && this.menu.timer.delta() > 1.5) || this.mode !== ZType.MODE.GAME_OVER) {
                    this.setTitle();
                }
            }
            var scrollSpeed = 100;
            if (this.waveEndTimer) {
                this.player.targetAngle = 0;
                var dt = Math.sin((this.waveEndTimer.delta() * -0.5) * Math.PI);
                scrollSpeed = 100 + dt * dt * 300;
                this.idleTimer.reset();
            }
            this.yScroll2 += ig.system.tick * scrollSpeed * 0.1;
            this.yScroll2 = this.yScroll2 % this.stars.height;
            this.yScroll -= scrollSpeed * ig.system.tick;
            this.backgroundMaps[0].scroll.y = this.yScroll;
            if (this.entities.length > 1 && this.mode == ZType.MODE.GAME) {
                this.gameTime += ig.system.tick;
            }
            if (this.score - this.rs > 100 || ig.Timer.timeScale != 1) {
                this.score = 0;
            }
            this.rs = this.score;
            this._screenShake /= 1.1;
            if (this._screenShake < 0.5) {
                this._screenShake = 0;
            }
            this._rscreen.x = Math.random() * this._screenShake;
            this._rscreen.y = Math.random() * this._screenShake;
        },
        screenShake: function(strength) {
            this._screenShake = Math.max(strength, this._screenShake);
        },
        draw: function() {
            if (this.mode == ZType.MODE.GAME || this.mode === ZType.MODE.GAME_OVER) {
                this.drawGame();
            }
            if (this.menu) {
                this.menu.draw();
                if (typeof (this.menu.scroll) != 'undefined') {
                    this.yScroll2 = this.menu.scroll;
                }
                if (this.gameTransitionTimer) {
                    var dt = 2 - (this.gameTransitionTimer.delta() * -1);
                    this.menu.transition = (dt / 2);
                    var sy = ig.system.height - this.keyboard.height * this.keyboard.drawScale - 30;
                    var move = sy - MenuTitle.prototype.playerPos.y;
                    this.menu.playerPos.y = ig.ease.inOutBack(dt, MenuTitle.prototype.playerPos.y, move, 2);
                    this.menu.alpha = 1 - (dt / 2);
                    this.player.pos.y = this.menu.playerPos.y;
                    if (dt > 2) {
                        this.gameTransitionTimer = null ;
                        this.menu = null ;
                    }
                }
            }
            if (this.waitingForItunes) {
                this.drawSpinner();
            }
        },
        drawSpinner: function() {
            ig.system.context.fillStyle = 'rgba(0,0,0,0.7)';
            ig.system.context.fillRect(0, 0, ig.system.width, ig.system.height);
            var spinner = ['', '.', '..', '...'];
            var tt = ((ig.Timer.time * 5) % spinner.length) | 0;
            this.fontTitle.draw(spinner[tt], ig.system.width / 2 - 16, ig.system.height / 2);
        },
        drawGame: function() {
            var ctx = ig.system.context;
            ctx.save();
            ctx.scale(0.75, 0.75);
            this.stars.draw(0, this.yScroll2 - this.stars.height);
            this.stars.draw(0, this.yScroll2);
            ctx.restore();
            ig.system.context.globalAlpha = 0.2;
            ig.system.context.drawImage(this.gradient.data, 0, 0, ig.system.width, ig.system.height);
            var d = this.lastKillTimer.delta();
            ig.system.context.globalAlpha = d < 0 ? d * -1 + 0.1 : 0.1;
            this.backgroundMaps[0].draw();
            ig.system.context.globalAlpha = 1;
            ig.system.context.globalCompositeOperation = 'lighter';
            for (var i = 0; i < this.entities.length; i++) {
                this.entities[i].draw();
            }
            ig.system.context.globalCompositeOperation = 'source-over';
            for (var i = 0; i < this.entities.length; i++) {
                this.entities[i].drawLabel && this.entities[i].drawLabel();
            }
            if (this.mode == ZType.MODE.GAME) {
                this.drawUI();
            }
        },
        drawUI: function() {
            if (this.waveEndTimer) {
                var d = -this.waveEndTimer.delta();
                var a = d > 1.7 ? d.map(2, 1.7, 0, 1) : d < 1 ? d.map(1, 0, 1, 0) : 1;
                var ys = 276 + (d < 1 ? Math.cos(1 - d).map(1, 0, 0, 250) : 0);
                var w = this.wave.wave.zeroFill(3);
                ig.system.context.globalAlpha = a;
                this.fontTitle.draw('WAVE ' + w + ' CLEAR', 32, ys, ig.Font.ALIGN.LEFT);
                ig.system.context.drawImage(this.separatorBar.data, 32, ys + 48, 276, 2);
                this.font.draw('SCORE: ' + this.score.zeroFill(6), 32, (ys * 1.2) + 10, ig.Font.ALIGN.LEFT);
                ig.system.context.globalAlpha = 1;
            }
            if (!ig.ua.mobile && this.idleTimer.delta() > 8) {
                var aa = this.idleTimer.delta().map(8, 9, 0, 1).limit(0, 1);
                ig.system.context.globalAlpha = (Math.sin(this.idleTimer.delta() * 4) * 0.25 + 0.75) * aa;
                this.font.draw('ENTER for EMP', ig.system.width / 2, ig.system.height - 30, ig.Font.ALIGN.CENTER);
                ig.system.context.globalAlpha = 1;
            }
            this.keyboard.draw();
        },
        purchaseRemoveAds: function() {
            this.iap = this.iap || new Ejecta.IAPManager();
            ig.game.waitingForItunes = true;
            this.iap.getProducts(['removeAds'], function(error, products) {
                if (error) {
                    ig.game.waitingForItunes = false;
                    ig.game.setTitle();
                } 
                else if (products.length) {
                    products[0].purchase(1, function(error, transaction) {
                        ig.game.waitingForItunes = false;
                        if (error) {
                            console.log(error);
                        } 
                        else {
                            localStorage.setItem('removeAds', true);
                        }
                        ig.game.setTitle();
                    });
                }
            });
        },
        restoreIAP: function() {
            this.iap = this.iap || new Ejecta.IAPManager();
            ig.game.waitingForItunes = true;
            this.iap.restoreTransactions(function(error, transactions) {
                ig.game.waitingForItunes = false;
                if (error) {
                    console.log(error);
                } 
                else {
                    for (var i = 0; i < transactions.length; i++) {
                        if (transactions[i].productId == 'removeAds') {
                            localStorage.setItem('removeAds', true);
                            ig.game.setTitle();
                            return;
                        }
                    }
                }
                ig.game.setTitle();
            });
        }
    });
    ZType.MODE = {
        TITLE: 0,
        GAME: 1,
        GAME_OVER: 2
    };
    ZType.MULTIPLIER_TIERS = {
        20: 2,
        50: 3
    };
    ZType.WAVES = {
        MOBILE: {
            fragment: 0,
            wave: 0,
            spawn: [],
            spawnWait: 1,
            healthBoost: 0,
            speedIncrease: 1.01,
            types: [{
                type: EntityEnemyOppressor,
                count: 0,
                incEvery: 9
            }, {
                type: EntityEnemyDestroyer,
                count: 0,
                incEvery: 4
            }, {
                type: EntityEnemyMine,
                count: 3,
                incEvery: 1
            }]
        },
        DESKTOP: {
            fragment: 0,
            wave: 0,
            spawn: [],
            spawnWait: 0.7,
            healthBoost: 0,
            speedIncrease: 1.01,
            types: [{
                type: EntityEnemyOppressor,
                count: 0,
                incEvery: 7
            }, {
                type: EntityEnemyDestroyer,
                count: 0,
                incEvery: 3
            }, {
                type: EntityEnemyMine,
                count: 3,
                incEvery: 1
            }]
        }
    };
    var canvas = document.getElementById('ztype-game-canvas');
    var width =600;
    var height = 720;
    if (ig.ua.mobile) {
        ig.$('#ztype-gsense').style.display = 'none';
        ig.$('#ztype-byline').style.display = 'none';
        var resize = function() {
            height = Math.min((window.innerHeight / (window.innerWidth)) * width, 852);
            canvas.style.position = 'absolute';
            canvas.style.display = 'block';
            canvas.style.width = window.innerWidth + 'px';
            canvas.style.height = (window.innerWidth / width) * height + 'px';
            canvas.style.bottom = 'auto';
            canvas.style.right = 'auto';
            if (ig.game && ig.system) {
                ig.system.resize(width, height);
            }
        }
        window.addEventListener('resize', function() {
            setTimeout(resize, 500);
        });
        resize();
    }
    ig.System.drawMode = ig.System.DRAW.SUBPIXEL;
    ig.Sound.use = [ig.Sound.FORMAT.OGG, ig.Sound.FORMAT.MP3];
    if (window !== window.top || window.location.href.match(/\?gp=1/)) {
        var ad = ig.$('#ztype-gsense');
        if (ad) {
            ad.className = 'ztype-gsense-full';
        }
    }
    if (window.ZTypeDocumentMode) {
        ig.doc = new ig.DocumentScanner(document.body);
    }
    ig.main('#ztype-game-canvas', ZType, 60, width, height, 1, ig.RiseLoader);
});
