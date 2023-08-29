<%@ page language="C#" autoeventwireup="true" inherits="Teacher_mygraph, LearnSite" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>流程图</title>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
	<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <link rel="stylesheet" type="text/css" href="../mxgraph/styles/grapheditor.css">
	<script type="text/javascript">
	    // Parses URL parameters. Supported parameters are:
	    // - lang=xy: Specifies the language of the user interface.
	    // - touch=1: Enables a touch-style user interface.
	    // - storage=local: Enables HTML5 local storage.
	    // - chrome=0: Chromeless mode.
	    var urlParams = (function (url) {
	        var result = new Object();
	        var idx = url.lastIndexOf('?');

	        if (idx > 0) {
	            var params = url.substring(idx + 1).split('&');

	            for (var i = 0; i < params.length; i++) {
	                idx = params[i].indexOf('=');

	                if (idx > 0) {
	                    result[params[i].substring(0, idx)] = params[i].substring(idx + 1);
	                }
	            }
	        }

	        return result;
	    })(window.location.href);

	    // Default resources are included in grapheditor resources
	    mxLoadResources = false;
	</script>
	<script type="text/javascript" src="../mxgraph/js/Init.js"></script>
	<script type="text/javascript" src="../mxgraph/deflate/pako.min.js"></script>
	<script type="text/javascript" src="../mxgraph/deflate/base64.js"></script>
	<script type="text/javascript" src="../mxgraph/jscolor/jscolor.js"></script>
	<script type="text/javascript" src="../mxgraph/sanitizer/sanitizer.min.js"></script>
	<script type="text/javascript" src="../mxgraph/mxClient.js"></script>
	<script type="text/javascript" src="../mxgraph/js/EditorUi.js"></script>
	<script type="text/javascript" src="../mxgraph/js/Editor.js"></script>
	<script type="text/javascript" src="../mxgraph/js/Sidebar.js"></script>
	<script type="text/javascript" src="../mxgraph/js/Graph.js"></script>
	<script type="text/javascript" src="../mxgraph/js/Format.js"></script>
	<script type="text/javascript" src="../mxgraph/js/Shapes.js"></script>
	<script type="text/javascript" src="../mxgraph/js/Actions.js"></script>
	<script type="text/javascript" src="../mxgraph/js/Menus.js"></script>
	<script type="text/javascript" src="../mxgraph/js/Toolbar.js"></script>
	<script type="text/javascript" src="../mxgraph/js/Dialogs.js"></script>
    <script src="../code/jquery.min.js" type="text/javascript"></script>
	<style>		
		.savetext{
			position: fixed;
			top: 2px;
			left: 50%;
			z-index: 888;
			border-radius: 10px;   
			width:100px;
		}
		.savetext:hover{
			background:#99cc99;
		}
		.returnbtn{
			position: fixed;
			top: 2px;
			right: 60px;
			z-index: 888;
			border-radius: 10px;   
			width:100px;
		}
		.returnbtn:hover{
			background:#99cc99;
		}

	</style>
</head>
<body class="geEditor">
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>

	<script type="text/javascript">
	    var editor;

	    mxResources.loadDefaultBundle = false;
	    var bundle = mxResources.getDefaultBundle(RESOURCE_BASE, mxLanguage) ||
				mxResources.getSpecialBundle(RESOURCE_BASE, mxLanguage);

	    // Fixes possible asynchronous requests
	    mxUtils.getAll([bundle, STYLE_PATH + '/default.xml'], function (xhr) {
	        // Adds bundle text to resources
	        mxResources.parse(xhr[0].getText());

	        // Configures the default graph theme
	        var themes = new Object();
	        themes[Graph.prototype.defaultThemeName] = xhr[1].getDocumentElement();

	        // Main
	        editor = new Editor(urlParams['chrome'] == '0', themes);
	        new EditorUi(editor);
	        readfromnet();
	    }, function () {
	        document.body.innerHTML = '<center style="margin-top:10%;">Error loading resource files. Please check browser console.</center>';
	    });
	    //})();
        
	    function readfromnet() {

	        var codefile = "<%=codefile %>";
	        //console.log(codefile);
	        codefile = decodeURIComponent(codefile);
	        console.log("读取作品：");
	        //console.log(codefile);
	        var viewxml;
	        if (codefile != null) viewxml = codefile;

	        if (viewxml != null) {
	            var doc = mxUtils.parseXml(viewxml);
	            var codec = new mxCodec(doc);
	            var root = doc.documentElement;
	            var graph = editor.graph;
	            codec.decode(root, graph.getModel());
	        }
	    }

	    function downFile(content, filename) {
	        var ele = document.createElement('a'); // 创建下载链接
	        ele.download = filename; //设置下载的名称
	        ele.style.display = 'none'; // 隐藏的可下载链接
	        // 字符内容转变成blob地址
	        var blob = new Blob([content]);
	        ele.href = URL.createObjectURL(blob);
	        // 绑定点击时间
	        document.body.appendChild(ele);
	        ele.click();
	        // 然后移除
	        document.body.removeChild(ele);
	    };
	</script>
</body>
</html>