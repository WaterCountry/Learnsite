<%@ page language="C#" autoeventwireup="true" validaterequest="false" enableviewstatemac="false" inherits="Student_mxgraph, LearnSite" %>

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
			right: 350px;
			z-index: 888;  
			width:100px;
		}
		.savetext:hover{
			background:#a8e083;
			border:1px solid;
		}
		.contentbtn{
			position: fixed;
			top: 2px;
			right: 200px;
			z-index: 888;  
			width:100px;
		}
		.contentbtn:hover{
			background:#a8e083;
			border:1px solid;
		}
		.returnbtn{
			position: fixed;
			top: 2px;
			right: 50px;
			z-index: 888;  
			width:100px;
		}
		.returnbtn:hover{
			background:#a8e083;
			border:1px solid;
		}
		::-webkit-scrollbar {  display: none; /* Chrome Safari */  }
	</style>
</head>
<body class="geEditor">
<button class="savetext"  onclick="savetoxml()" type="button"  >保存流程图</button>
<button  onclick="showcontent()" type="button" class="contentbtn" > 查看学案</button>
<button  onclick="returnurl()" type="button" class="returnbtn" > 返回</button>
    <form id="form1" runat="server">
       <div id="mcontext" style="display: none; background: #fffdea; overflow-y: auto; overflow-x: hidden;
            position: absolute;  width: 500px; height: 50%; z-index: 999;opacity:0.9; font-size: 16px;
            right: 0px; bottom: 0px; padding: 2px;">
            <div style="margin:10px; ">
            <h4><%=Titles%></h4>
            <%=Mcontents %>
            </div>
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

	    var snum = "<%=Snum+Id %>";
	    var savekey = "wzsxgraph" + snum;
	    var savemsg = document.getElementById("savemsg");

	    function savetoxml() {
	        var format = "png";
	        var bg = '#ffffff';
	        var scale = 1;
	        var b = 1;

	        var graph = editor.graph;
	        var xml = mxUtils.getXml(new mxCodec().encode(graph.getModel()));
	        console.log(xml);
	        console.log("保存xml成功")
	        if (xml.indexOf("mxGeometry") != -1) {
	            console.log("有内容");
	            sessionStorage.setItem(savekey, xml); //sessionStorage  localStorage
	            //var filename='mxgraph'+ parseInt(Math.random()*100)+'.xml';
	            //downFile(xml,filename)

	            // New image export
	            var imgExport = new mxImageExport();
	            var bounds = graph.getGraphBounds();
	            var vs = graph.view.scale;
	            var xmlDoc = mxUtils.createXmlDocument();
	            var root = xmlDoc.createElement('output');
	            xmlDoc.appendChild(root);
	            // Renders graph. Offset will be multiplied with state's scale when painting state.
	            var xmlCanvas = new mxXmlCanvas2D(root);
	            xmlCanvas.translate(Math.floor((b / scale - bounds.x) / vs), Math.floor((b / scale - bounds.y) / vs));
	            xmlCanvas.scale(scale / vs);
	            imgExport.drawState(graph.getView().getState(graph.model.root), xmlCanvas);
	            // Puts request data together
	            var w = Math.ceil(bounds.width * scale / vs + 2 * b);
	            var h = Math.ceil(bounds.height * scale / vs + 2 * b);
	            var exml = mxUtils.getXml(root);

	            if (bg != null) {
	                bg = '&bg=' + bg;
	            }

	            var id = "<%=Id %>";
	            var urls = 'uploadgraph.ashx?id=' + id;
	            var formData = new FormData();
                xml=encodeURIComponent(xml);
                exml=encodeURIComponent(exml);//编码

	            formData.append('xml', xml);
	            formData.append('exml', exml);
	            formData.append('w', w);
	            formData.append('h', h);
	            formData.append('bg', bg);

	            $.ajax({
	                url: urls,
	                type: 'POST',
	                cache: false,
	                data: formData,
	                processData: false,
	                contentType: false
	            }).done(function (res) {
	                alert("保存成功！");
	                console.log(res)
	            }).fail(function (res) {
	                alert("保存失败！");
	                console.log(res)
	            });
	        } else {
	            console.log("无内容");
	        }
	    }

	    function readfromxml() {
	        var valuexml = sessionStorage.getItem(savekey);
	        if (valuexml != null) {
	            //console.log(valuexml);
	            var doc = mxUtils.parseXml(valuexml);
	            var codec = new mxCodec(doc);
	            var root = doc.documentElement;
	            var graph = editor.graph;
	            codec.decode(root, graph.getModel());
	            console.log("读取xml成功")
	        }
	    }
	    function readfromnet() {
	        var sessionxml = sessionStorage.getItem(savekey);
	        console.log("本地存储：");
	        //console.log(sessionxml);
	        var codefile = "<%=codefile %>";
	        //console.log(codefile);
	        codefile = decodeURIComponent(codefile);
	        codefile = decodeURIComponent(codefile);//二次加密，所以这里要二次解密
	        console.log("读取作品：");
	        //console.log(codefile);
	        var viewxml;
	        if (codefile != null) viewxml = codefile;
	        if (sessionxml != null) viewxml = sessionxml;

	        if (viewxml != null) {
	            var doc = mxUtils.parseXml(viewxml);
	            var codec = new mxCodec(doc);
	            var root = doc.documentElement;
	            var graph = editor.graph;
	            codec.decode(root, graph.getModel());
	        }
	    }
	    function returnurl() {
	        if (confirm('确定要返回吗，记得先保存。') == true) {
	            window.location.href = "<%=Fpage %>"
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

	    function showcontent() {
	        $("#mcontext").slideToggle();
	    }
	</script>

</body>
</html>