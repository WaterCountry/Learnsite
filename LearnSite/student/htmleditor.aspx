﻿<%@ page language="C#" autoeventwireup="true" inherits="student_htmleditor, LearnSite" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head >  
  <style>
		body {
			margin: 0;
			overflow: hidden;
		}
		.banner {
			height: 30px;
			line-height:30px;
			vertical-align: middle;
			padding-left: 20px;
			font-weight: bold;
			background-color: #eee;
			color: #666;
			user-select: none;
			box-shadow: 2px 2px 2px Gainsboro;
		}
		#main {
			padding-top:2px;
			minwidth:600px;
			display: flex;
		}
		#left {
			width: calc(50% - 6px);
		}
		#resize {
			width: 6px;
			height: 100vh;
			cursor: ew-resize;
			background-color:WhiteSmoke;
			box-shadow: 2px 2px 2px #999;
		}
		#resize:hover {
			background-color: Gainsboro;
		}
		#right {
			width: 50%;
			/* height: 100vh; */
			margin:10px;
		}
		#tooltip{
			position: fixed;
			top:0px;
			height:30px;
			line-height:30px;
			left:200px;
			user-select: none;
			width:100%;	
			display:none;		
		}
		.keyword{	
			margin:2px;
			padding:2px;
			color:#999;	
			cursor: hand;	
		}
		.keyword:hover{
			background-color: #252d30;
			cursor: hand;
			color:#ccc;
		}
		#sideby{
	        position:absolute;
	        right:50px;
	        top:0px;	
            z-index: 999;
            color: #333;
			line-height:30px;	
        }
		.sp{
		  width: 30px;
		  display:inline-block;
		}
	</style>
<link href="../code/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
<script src="../code/jquery.min.js" type="text/javascript"></script>
<script src="../code/build/src/ace.js" type="text/javascript"></script>
<script src="../code/build/src/ext-language_tools.js" type="text/javascript"></script>
</head>
<body>
    <div>
    <div class="banner">
	     <i class="fa fa-edge" ></i> <a id="title" >在线网页设计</a>
    </div>
        <div id="main">
        <div id="left"></div>
        <div id="resize"></div>
        <div id="right"></div>
        </div>
	<div id="tooltip">
	<span class="keyword" title="超文本标记语言">html</span>
	<span class="keyword" title="页面标题">title</span>
	<span class="keyword" title="样式">style</span>
	<span class="keyword" title="网页的主体">body</span>
	<span class="keyword" title="文字标题">h1</span>
	<span class="keyword" title="超链接">a</span>	
	<span class="keyword" title="超链接目标">href</span>
	<span class="keyword" title="段落">p</span>
	<span class="keyword" title="层">div</span>
	<span class="keyword" title="图像">img</span>
	<span class="keyword" title="资源链接">src</span>
	<span class="keyword" title="视频">video</span>
	<span class="keyword" title="资源">source</span>
	<span class="keyword" title="音频">audio</span>
	<span class="keyword" title="文字颜色">color</span>
	<span class="keyword" title="网页脚本">script</span>
</div>
<div  id="sideby">
<button  onclick="example()" class="button"  title="网页模板">
<i class="fa fa-file-code-o" aria-hidden="true"></i> 模板</button>
<span class="sp"></span>
<button  onclick="savehtml()" class="button"  >
<i class="fa fa-save" aria-hidden="true"></i> 保存</button>
<span class="sp"></span>
<button  onclick="showhtml()" class="button" >
<i class="fa fa-edge" aria-hidden="true" title="请先保存，然后使用浏览器"></i> 浏览器</button>
<span class="sp"></span>
<button  onclick="returnurl()" class="button">
<i class="fa fa-reply" aria-hidden="true"></i> 返回</button>
</div>
	<script type="text/javascript" >
	    var editor = ace.edit("left", {
	        theme: "ace/theme/chrome",
	        mode: "ace/mode/html"
	    });
	    editor.setFontSize(16);
	    editor.focus();
	    editor.setOptions({
	        wrap: true,
	        enableLiveAutocompletion: true
	    });

	    var preview = document.getElementById("right");
	    var snum = "<%=Snum %>";
	    var id = "<%=Id %>";
	    var cf = "<%=codefile %>";
	    var fpage = "<%=Fpage %>";
        var mypage= "<%=Mypage %>";
	    var codefile = decodeURIComponent(window.atob(cf)); //定义字典

	    var sessionkey = "htmlcode" + snum + "-" + id;

	    function savesession() {
	        var codestr = editor.getValue();
	        if (codestr != null && codestr != "") {
	            sessionStorage.setItem(sessionkey, codestr);
	            preview.innerHTML = codestr;

	        }
	    }
	    function getsession() {
	        var codestr = sessionStorage.getItem(sessionkey);
	        if (codestr != null && codestr != "") {
	            editor.setValue(codestr, 1);
	            //sessionStorage.clear();//读取后清除，防止污染
	            preview.innerHTML = codestr;
	        }
	    }
	    document.onkeyup = keyUp;
	    function keyUp() {
	        savesession();
	    }

	    if (codefile != '' && codefile != null) {
	        setcode(codefile);
	        preview.innerHTML = codefile;
	        getsession();
	        console.log("读取数据库储存程序");
	    }
	    function setcode() {
	        editor.setValue(codefile, 1);
	    }

	    function savehtml() { 
        	var htmlcode = editor.getValue();
        	if (htmlcode != null && htmlcode != "") {
        	    sessionStorage.setItem(sessionkey, htmlcode); //保存时更新临时数据
        	    var urls = 'uploadhtml.ashx?id=' + id;
        	    var formData = new FormData();
        	    var encodehtml = window.btoa(encodeURIComponent(htmlcode));
        	    formData.append('codefile', encodehtml);

        	    $.ajax({
        	        url: urls,
        	        type: 'POST',
        	        cache: false,
        	        data: formData,
        	        processData: false,
        	        contentType: false
        	    }).done(function (res) {
        	        alert("保存成功！");
        	        console.log(res);
        	    }).fail(function (res) {
        	        console.log(res)
        	    }); 			

        	}
        }

        function showhtml(){
            window.open(mypage,"blank");  
           // window.location.href=mypage;
        }

		function downhtml(){
			var filename="mypage";
			var type="text/html";
			var content= editor.getValue();
			var ele = document.createElement('a');// 创建下载链接
			ele.download = filename;//设置下载的名称
			ele.style.display = 'none';// 隐藏的可下载链接
			// 字符内容转变成blob地址
			var blob = new Blob([content],{type});
			ele.href = URL.createObjectURL(blob);
			// 绑定点击时间
			document.body.appendChild(ele);
			ele.click();
			// 然后移除
			document.body.removeChild(ele);
		}

        function returnurl() {
            if (confirm('确定要返回吗？记得先保存。') == true) {
                sessionStorage.clear(); //返回后清除，防止污染
                window.location.href = fpage;
            }
        }
        
        function example() {
            var examplecode = "JTNDIURPQ1RZUEUlMjBodG1sJTNFJTBEJTBBJTNDaHRtbCUzRSUwRCUwQSUyMCUyMCUyMCUyMCUzQ2hlYWQlM0UlMEQlMEElMDklMDklM0NtZXRhJTIwY2hhcnNldCUzRCUyMlVURi04JTIyJTNFJTBEJTBBJTIwJTIwJTIwJTIwJTIwJTIwJTIwJTIwJTNDdGl0bGUlM0UlRTklQTElQjUlRTklOUQlQTIlRTYlQTAlODclRTklQTIlOTglM0MlMkZ0aXRsZSUzRSUwRCUwQSUyMCUyMCUyMCUyMCUzQyUyRmhlYWQlM0UlMEQlMEElMjAlMjAlMjAlMjAlM0Nib2R5JTNFJTBEJTBBJTIwJTIwJTIwJTIwJTIwJTIwJTIwJTIwJTNDaDIlM0UlRTYlOTYlODclRTclQUIlQTAlRTYlQTAlODclRTklQTIlOTglM0MlMkZoMiUzRSUwRCUwQSUyMCUyMCUyMCUyMCUyMCUyMCUyMCUyMCUzQ3AlM0UlMEQlMEElMjAlMjAlMjAlMjAlMjAlMjAlMjAlMjAlMjAlMjAlMjAlMjAlRTYlQUQlQTMlRTYlOTYlODclMEQlMEElMjAlMjAlMjAlMjAlMjAlMjAlMjAlMjAlM0MlMkZwJTNFJTBEJTBBJTIwJTIwJTIwJTIwJTNDJTJGYm9keSUzRSUwRCUwQSUzQyUyRmh0bWwlM0U=";
            var exampledecode = decodeURIComponent(window.atob(examplecode)); //网页模板
            preview.innerHTML = exampledecode;
            editor.setValue(exampledecode, 1);
        }

        $("#title").click(function () {
            $("#tooltip").toggle();
        });

	</script>

    <script type="text/javascript" >
        window.onload = function () {
            var resize = document.getElementById('resize');
            var left = document.getElementById('left');
            var right = document.getElementById('right');
            var main = document.getElementById('main');
            var tooltip = document.getElementById('tooltip');
            var minwidth = 400;
            resize.onmousedown = function (e) {
                // 记录鼠标按下时的x轴坐标
                var preX = e.clientX;
                resize.left = resize.offsetLeft;
                document.onmousemove = function (e) {
                    var curX = e.clientX;
                    var deltaX = curX - preX;
                    var leftWidth = resize.left + deltaX;
                    // 左边区域的最小宽度限制为64px
                    if (leftWidth < minwidth) leftWidth = minwidth;
                    // 右边区域最小宽度限制为64px
                    if (leftWidth > main.clientWidth - minwidth) leftWidth = main.clientWidth - minwidth;
                    // 设置左边区域的宽度
                    left.style.width = leftWidth + 'px';
                    // 设备分栏竖条的left位置
                    resize.style.left = leftWidth;
                    // 设置右边区域的宽度
                    right.style.width = (main.clientWidth - leftWidth - 6) + 'px';
                    console.log("左侧", leftWidth);
                    tooltip.style.left = leftWidth;
                }
                document.onmouseup = function (e) {
                    document.onmousemove = null;
                    document.onmouseup = null;
                }
            }
        };
	</script>
    </div>
</body>
</html>