<%@ page language="C#" autoeventwireup="true" inherits="Student_python, LearnSite" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
  <link href="../code/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
  <link href="../code/turtle.css" rel="stylesheet" type="text/css" />
    <script src="../kindeditor/plugins/code/prettify.js" type="text/javascript"></script>
    <link href="../kindeditor/plugins/code/prettify.css?ver=621" rel="stylesheet" type="text/css" />
</head>

<body  onload="prettyPrint(); ">

<div class="banner">
	<i class="fa fa-codepen" ></i> <a id="title" ><%=Titles%></a>
</div>
<div class="main" >
	<div id="done"><img src="../images/sucessed.png"></img></div>
	<div class="description">
        <div class="map"><img class="mapimg" src="<%=Midurl %>" alt=""/></div> 
		<div id="content" >
		  <%=Mcontents %>
		  <br /><br /> <br><br />
		</div>	
	</div>
	<div class="left" id="editor"></div>
	<div class="right">
		<div id="result">
		<pre id="output" ></pre>
		 <div class="fullimg"><img src="<%=MidurlFull %>" /></div> 
		<div id="cv" ></div>
		</div>
	</div>
</div>

<div  id="big" onclick="fontbig()" title="放大代码"> </div>
<div  id="small" onclick="fontsmall()" title="缩小代码"> </div>
<div id="colorbox"></div>
<div id="codexample">
	<div id="codeplace"></div>
	<div id="codebutton">
	<button class="btncode" id="prev">上一页</button>&nbsp;&nbsp;&nbsp;&nbsp;<button class="btncode"  id="next">下一页</button>
	</div>
</div>
<div class="tooltip"></div>

<div id="savemsg"></div>

<div  id="sideby">
<button  onclick="remember()" class="button"  >
<i class="fa fa-video-camera" aria-hidden="true"></i>回忆</button>&nbsp;&nbsp;
<span class="sp"></span>

<button  onclick="runit()" class="button"  >
<i class="fa fa-play-circle" aria-hidden="true"></i>运行</button>&nbsp;&nbsp;
<span class="sp"></span>

<button  onclick="checkright()" class="button"  >
<i class="fa fa-save" aria-hidden="true"></i>保存</button>&nbsp;&nbsp;
<span class="sp"></span>
<button  onclick="returnurl()" class="button">
<i class="fa fa-reply" aria-hidden="true"></i>返回</button>
</div>

</div>
</div>

<script src="../code/skulpt.min.js?ver=20211202" type="text/javascript"></script>
<script src="../code/skulpt-stdlib.js" type="text/javascript"></script>
<script src="../code/html2canvas.min.js" type="text/javascript"></script>
<script src="../code/jquery.min.js" type="text/javascript"></script>

<script src="../code/build/src/ace.js" type="text/javascript"></script>
<script src="../code/build/src/ext-language_tools.js" type="text/javascript"></script>
<script src="../code/build/src/ext-beautify.js" type="text/javascript"></script>

<script type="text/javascript">

    var snum = "<%=Snum %>";
    var cvbg = "<%=mback %>";
    var cf = "<%=codefile %>";
    var id = "<%=Id %>";

    var testn = 0;
    var testg = 0;
    var argck = [0, 0, 0];
    var argcodestr = "<%=argcode %>";

    var arginstr = "<%=argin %>";
    var argin = arginstr.split("#");

    var argout0 = "<%=argout0 %>";
    var argout1 = "<%=argout1 %>";
    var argout2 = "<%=argout2 %>";
    var argout = new Array();
    argout[0] = decodeURIComponent(window.atob(argout0));
    argout[1] = decodeURIComponent(window.atob(argout1));
    argout[2] = decodeURIComponent(window.atob(argout2));
    var argimg = "<%=argimg %>";
    var mhelp = "<%=mhelp %>";

    var fpage = "<%=Fpage %>";

    var mypre = document.getElementById("output");
    var result = document.getElementById("result");
    var savemsg = document.getElementById("savemsg");
    var cv = document.getElementById("cv");
    var dictvalue = new Array(); //定义新字典
    var codedict = new Array(); //定义新字典
    var codefile = decodeURIComponent(window.atob(cf)); //定义字典 
    var codearg = decodeURIComponent(window.atob(argcodestr)); //定义字典 

</script>
<script src="../code/turtle.js" type="text/javascript"></script>
<script src="../code/colorbox.js" type="text/javascript"></script>
<script src="../code/example.js" type="text/javascript"></script>
</body>
</html>