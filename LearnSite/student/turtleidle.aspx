﻿<%@ page language="C#" autoeventwireup="true" inherits="Student_turtleidle, LearnSite" %>

<html xmlns="http://www.w3.org/1999/xhtml"> 
<head id="Head1" runat="server">
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Python绘图编程</title>
</head>

<link href="../code/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
<script src="../code/jquery.min.js" type="text/javascript"></script>
<script src="../code/html2canvas.min.js" type="text/javascript"></script>
<script src="../code/skulpt.min.js" type="text/javascript"></script>
<script src="../code/skulpt-stdlib.js" type="text/javascript"></script>
<script src="../code/build/src/ace.js" type="text/javascript"></script>
<script src="../code/build/src/ext-language_tools.js" type="text/javascript"></script>
<link rel="stylesheet" type="text/css" href="../code/idleturtle.css"/>
<script src="../kindeditor/plugins/code/prettify.js" type="text/javascript"></script>
<link href="../kindeditor/plugins/code/prettify.css?ver=621" rel="stylesheet" type="text/css" />

<body  onload="prettyPrint(); ">

<div class="main" >
	<div id="done"><img src="../images/sucessed.png"></img></div>
	<div class="description">
		<h2 ><%=Titles%></h2>
		<div id="content" >
		<div class="map"><img class="mapimg" src="<%=Midurl %>" alt=""/></div>
				<%=Mcontents %>
				<br /><br />   
		</div>	
	</div>
	<div id="editor" class="ace-gruvbox ace_editor" > </div>
	<div class="right">
		<div id="cv" class="cvdiv"></div>
	</div>
</div>

<div class="tooltip"></div>
<div  id="big" onclick="fontbig()" title="放大代码"> </div>
<div  id="small" onclick="fontsmall()" title="缩小代码"> </div>
<div id="colorbox"></div>
<div id="codexample">
	<div id="codeplace"></div>
	<div id="codebutton">
	<button class="btncode" id="prev">上一页</button>&nbsp;&nbsp;&nbsp;&nbsp;<button class="btncode"  id="next">下一页</button>
	</div>
</div>

<form id="form1" runat="server"> 

<span id="btnclear"  onclick="clearcv()" >
<i class="fa fa-eraser" aria-hidden="true"></i> 整理</span>

<span id="btnrun"  onclick="run()" >
<i class="fa fa-play-circle" aria-hidden="true"></i> 运行</span>

<span id="btnsave"  onclick="savecode()"  >
<i class="fa fa-save" aria-hidden="true"></i> 保存</span>  
<span id="btnreturn" onclick="returnurl()" >
<i class="fa fa-reply" aria-hidden="true"></i> 返回</span>
</form>

<div id="savemsg"></div>

<script src="../code/idle.js"  type="text/javascript"></script>
<script src="../code/colorbox.js" type="text/javascript"></script>
<script src="../code/example.js" type="text/javascript"></script>

<script type="text/javascript">

    var snum = "<%=Snum %>";
	var id = "<%=Id %>";
	var tout="";
	var cf = "<%=Codefile %>";
		
    var argimg = "<%=argimg %>";
    var fpage = "<%=Fpage %>";
    var mhelp = "<%=mhelp %>";
    
	var codefile = decodeURIComponent(window.atob(cf));  
	var codes=new Array();
	var arrange=false;
    var keyopen=true;
    var keybox=false;
    var sessionkey=snum+"-"+id+"auto";

    var fontsize=24;
	var ide;	
	var editor = document.getElementById("editor");
	ide = new ipythonExample(editor);	
		
	if(id==null||id==''){
		console.log("新建画布");
	}
	else{		 
		ide.inCell.setValue(codefile,1);
        getsession();
	}
    function returnurl() {
      if(confirm('确定要返回吗？记得先保存。')==true){
          window.location.href=fpage;
        }
    }
	
    function run(){
		ide.execute();
	}

	function clearcv(){
		if(!arrange){
			var ln=codes.length;
			console.log(ln);
			if(ln==0){
				var codestr = ide.inCell.getValue();
				$("#cv").empty(); 
				$("#editor").empty(); 
				ide = new ipythonExample(editor);	 
				ide.inCell.setValue(codestr,1);			
			}		
			else{
				$("#cv").empty(); 
				$("#editor").empty(); 
				ide = new ipythonExample(editor);	 
				ide.inCell.setValue(ArrayToString(codes),1);	 
			}
			arrange=true;
		}
    }

	function fontbig(){
		fontsize+=2;
		if (fontsize>36){
			fontsize=36;
		}
		$(".ace_editor").css({"font-size":fontsize});
	}

	function fontsmall(){
		fontsize-=2;
		if (fontsize<24){
			fontsize=24;
		}
		$(".ace_editor").css({"font-size":fontsize});
	}
     
    function savesession(){
        var ln=codes.length;
		if(ln==0){
			var codestr = ide.inCell.getValue();
            if(codestr!=null && codestr!=""){
                sessionStorage.setItem(sessionkey,codestr);
            }
		}		
		else{	 
			sessionStorage.setItem(sessionkey,ArrayToString(codes)); 
		}    
    }
    function getsession(){
        var codestr=sessionStorage.getItem(sessionkey);
        if(codestr!=null && codestr!=""){	 
			ide.inCell.setValue(codestr,1);
            sessionStorage.clear();//读取后清除，防止污染
        }    
    }
 
var lockimg=false;
var codemodel=false;   //0编程 1绘图
if(mhelp=="1"){
    codemodel=true;
}

var helpcode=[
	'<span class="keymodel" title="界面切换">❖</span>',
	'<span class="keyword" title="输出">print</span>',
	'<span class="keyword" title="输入">input</span>',
	'<span class="keyword" title="如果">if</span>',
	'<span class="keyword" title="否则">else</span>',
	'<span class="keyword" title="或者">elif</span>',
	'<span class="keyword" title="当条件成立时">while</span>',
	'<span class="keyword" title="中断">break</span>',
	'<span class="keyword" title="继续">continue</span>',
	'<span class="keyword" title="遍历">for</span>',
	'<span class="keyword" title="整数列表">range</span>',
	'<span class="keyword" title="转换整数">int</span>',
	'<span class="keyword" title="转换数字">float</span>',
	'<span class="keyword" title="获取长度">len</span>',
	'<span class="keyexample" title="参考示例">✿</span>'
	].join('');
var helpturtle=[
	'<span class="keymodel" title="界面切换">❖</span>',
	'<span class="keyword" title="前进">forward</span>',
	'<span class="keyword" title="后退">backward</span>',
	'<span class="keyword" title="左转">left</span>',
	'<span class="keyword" title="右转">right</span>',
	'<span class="keyword" title="画圆">circle</span>',
	'<span class="keyword" title="抬笔">penup</span>',
	'<span class="keyword" title="落笔">pendown</span>',
	'<span class="keyword" title="画笔颜色">pencolor</span>',
	'<span class="keyword" title="画笔粗细">pensize</span>',
	'<span class="keyword" title="填充颜色">fillcolor</span>',
	'<span class="keyword" title="开始填充">begin_fill</span>',
	'<span class="keyword" title="结束填充">end_fill</span>',
	'<span class="keyword" title="回家">home</span>',
	'<span class="keybox" title="颜色选择">✿</span>'
	].join('');
	
	$(document).ready(function(){
		if(codemodel){
			$(".tooltip").html(helpturtle);
		}
		else{
			$(".tooltip").html(helpcode);
		}
				
		$(".keymodel").click(function(){
			var url= window.location.href;
			url= url.replace("turtleidle","python");
            savesession();
            window.location.href=url;
		});	
        
	    $(".keybox").click(function(){
		    keybox=!keybox;
		    if(keybox) {
			    $("#colorbox").show();
		    }
		    else{
			    $("#colorbox").hide();
		    }
	    });	
		
		$(".keyexample").click(function(){
			keybox=!keybox;
			if(keybox) {
				$("#codexample").show();
				showexample();
			}
			else{
				$("#codexample").hide();
			}
		});	

		$(".keyopen").click(function(){
			keyopen=!keyopen;
			if(keyopen) {
				$(this).css("color","#88c166");
				$(".keyword").css("color","#88c166");
			}
			else{
				$(this).css("color","#56676b");
				$(".keyword").css("color","#56676b");
			}
		});	
			
		$(".keyword").click(function(){
			if(keyopen){
				var keytxt=$(this).text();	
				var cmdstr="";
				switch(keytxt)
				{
					case "print":
						cmdstr="print()";
						break;
					case "input":
						cmdstr="n=input()";
						break;
					case "if":
						cmdstr="if";
						break;
					case "else":
						cmdstr="else:";
						break;
					case "elif":
						cmdstr="elif";
						break;
					case "while":
						cmdstr="while";
						break;
					case "break":
						cmdstr="break";
						break;
					case "continue":
						cmdstr="continue";
						break;
					case "for":
						cmdstr="for";
						break;
					case "range":
						cmdstr="range()";
						break;
					case "int":
						cmdstr="int()";
						break;
					case "float":
						cmdstr="float()";
						break;
					case "len":
						cmdstr="len()";
						break;
					case "forward":
						cmdstr="forward(50)";
						break;
					case "backward":
						cmdstr="backward(50)";
						break;
					case "left":
						cmdstr="left(90)";
						break;
					case "right":
						cmdstr="right(90)";
						break;
					case "circle":
						cmdstr="circle(50)";
						break;
					case "pencolor":
						cmdstr="pencolor('red')";
						break;
					case "penup":
						cmdstr="penup()";
						break;
					case "pendown":
						cmdstr="pendown()";
						break;
					case "pensize":
						cmdstr="pensize(5)";
						break;
					case "setheading":
						cmdstr="setheading(30)";
						break;
					case "home":
						cmdstr="home()";
						break;
					case "fillcolor":
						cmdstr="fillcolor('red')";
						break;
					case "begin_fill":
						cmdstr="begin_fill()";
						break;
					case "end_fill":
						cmdstr="end_fill()";
						break;
					case "stamp":
						cmdstr="stamp()";
						break;				
					default:
						break;			
				}
				var oldstr =ide.inCell.getValue();
				if(oldstr!=""){
					oldstr=oldstr+"\n";
				}
				var newstr=oldstr+cmdstr;
				ide.inCell.setValue(newstr,1);
				ide.inCell.focus();
			}
		});
	});

	function toHex(r,g,b) {
		return ("00000" + (r << 16 | g << 8 | b).toString(16)).slice(-6);
	}

	function blob(dataURI) {
		var mimeString = dataURI.split(',')[0].split(':')[1].split(';')[0];  
		var byteString = atob(dataURI.split(',')[1]);  
		var arrayBuffer = new ArrayBuffer(byteString.length);  
		var intArray = new Uint8Array(arrayBuffer);  

		for (var i = 0; i < byteString.length; i++) {
			intArray[i] = byteString.charCodeAt(i);
		}
		return new Blob([intArray], {type: mimeString});
	}
  

 
function savecode(){
    var pass=0;
	var drawcheck=false;
	if(codes.length>0){
		var res=document.querySelector("#cv");
		var obj=$("#cv").find("canvas")[1];
		 
		if(!obj) {
			console.log("无绘图");
			obj=res; 
		}
		else{
			console.log("有绘图");
		}
		console.log('裁剪开始');		
		
		var opts = {
	      backgroundColor: "transparent", 
	       
	       
	    };

		html2canvas(obj,opts ).then(pic => {
		   var dataURL=pic.toDataURL();

		   if(dataURL=="data:,"){
				dataURL="data:image/gif;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVQImWNgYGBgAAAABQABh6FO1AAAAABJRU5ErkJggg==";
		   }

		   var img = new Image();  
			img.src = dataURL;  
			img.onload = function(){
				var c = document.createElement('canvas'); 
				var ctx = c.getContext('2d');
				c.width = img.width;
				c.height = img.height;
				ctx.drawImage(img,0,0); 
				var imgData = ctx.getImageData(0, 0, c.width, c.height).data; 
				var lOffset = c.width, rOffset = 0,tOffset = c.height, bOffset = 0;	
				
				for (var i = 0; i < c.width; i++) {
					for (var j = 0; j < c.height; j++) {
						var pos = (i + c.width * j) * 4;
						var p0= imgData[pos];
						var p1= imgData[pos+1];
						var p2= imgData[pos+2];
						var p3= imgData[pos+3];	
						/*
						if (p0 == 255 || p1 == 255 || p2 == 255 || p3 == 255) {
							bOffset = Math.max(j, bOffset);  
							rOffset = Math.max(i, rOffset);  
							tOffset = Math.min(j, tOffset);  
							lOffset = Math.min(i, lOffset);  
						}
						*/
						if (p0 >0 || p1 >0  || p2 >0  || p3 >0 ) {
							bOffset = Math.max(j, bOffset);  
							rOffset = Math.max(i, rOffset);  
							tOffset = Math.min(j, tOffset);  
							lOffset = Math.min(i, lOffset);  
						}
					}
				}
					
				var x = document.createElement("canvas"); 
				x.width = rOffset-lOffset;
				x.height = bOffset-tOffset;
				var xx = x.getContext("2d");
				xx.drawImage(img, lOffset, tOffset, x.width, x.height, 0, 0, x.width, x.height); 
				 
				var newimgData = xx.getImageData(0, 0, x.width, x.height).data; 
				var arrsetl=new Array();
				var arrsetr=new Array();
				
				var xwidth = x.width;
				var xheght = x.height;
				var lcount=0;
				var rcount=0;
				
				var colors=new Array();
				
				for (var m=0;m<xwidth;m++){
					for (var n=0;n<xheght;n++){
						var newpos = (m + xwidth * n) * 4;
						var np0= newimgData[newpos];
						var np1= newimgData[newpos+1];
						var np2= newimgData[newpos+2];
						var np3= newimgData[newpos+3];	
						
						if (np0 >0 || np1 >0  || np2 >0  || np3 >0 ) {	
							var npall=toHex(np0,np1,np2); 
							if(m==n){
								arrsetl.push(m); 
								lcount++;
								colors.push(npall);
							}
							if(m==xwidth-n){
								arrsetr.push(m); 
								rcount++;
								colors.push(npall);
							}
							if(n==xheght-1){								
								colors.push(npall); 
							}
							var half=parseInt(xwidth/2);
							if(m==half){
								colors.push(npall); 
								 
								 
							}
						}
					}					
				}	

				let setcolors=new Set(colors);
				let newcolors=Array.from(setcolors);
                    newcolors=colorselect(newcolors);
				let colorsize=newcolors.length;
				
				console.log("颜色：",newcolors);
				 
				var minNl=0;
				var maxNl=0;
				if(arrsetl.length>0){
					minNl = Math.min.apply(null,arrsetl);	
					maxNl = Math.max.apply(null,arrsetl);	
				}
				mi_mxl=(maxNl-minNl)*Math.sqrt(2).toFixed(0);
					
				var minNr=0;
				var maxNr=0;
				if(arrsetr.length>0){					
					minNr = Math.min.apply(null,arrsetr);	
					maxNr = Math.max.apply(null,arrsetr);	
				}
                    	
				mi_mxr=(maxNr-minNr)*Math.sqrt(2).toFixed(0);

				var milr=mi_mxl+"x"+mi_mxr+"x"+lcount+"x"+rcount+"x"+colorsize;	
				
				console.log("自动裁剪");			
				
				dataURL = x.toDataURL();
				 
				var scale=x.width/x.height;//宽高比例
				scale=scale.toFixed(2);
									
				var outcv=x.width+"x"+scale+"x"+milr;				 
				 
				if(argimg!=""){
					if(sizechecknew(argimg,outcv)){
						console.log("绘图检测正确：\n长x比例x斜x斜x点x点x颜");
						console.log(argimg);	
						console.log(outcv);	
						pass=1;
					}
					else{
						console.log("绘图检测失败：\n长x比例x斜x斜x点x点x颜");
						console.log(argimg);	
						console.log(outcv);		
					}	
					drawcheck=true;
				}
				
			   var Cover=blob(dataURL); 
			    
			   console.log('保存快照');
			    
			   
			    
			   var codestr=ArrayToString(codes);
			   
			   console.log("保存代码");
			    
			   
				var urls = 'uploadpython.ashx?id=' + id;
				var formData = new FormData();
				formData.append('cover', Cover);
				formData.append('codefile', window.btoa(encodeURIComponent(codestr)));
				formData.append('codedict', '');
				formData.append('pass',pass);

				$.ajax({
					url: urls,
					type: 'POST',
					cache: false,
					data: formData,
					processData: false,
					contentType: false
				}).done(function (rest) { 
					console.log(rest);
					if(drawcheck){
						if(pass==1){	
							savemsg.innerHTML= "恭喜！绘图正确，"+rest;
							$('#done').show();
						}
						else{
							savemsg.innerHTML= "绘图有误，请继续努力！"+rest;
							$('#done').hide();
						}
					}
					else{
						savemsg.innerHTML= "自由创作，"+rest;
						$('#done').hide();
					}
					
				}).fail(function (rest) {
					savemsg.innerHTML= "抱谦！保存失败！";
					console.log(rest);
					$('#done').hide();
				}); 			
			
			}			

		});
	}
	else{
		savemsg.innerHTML= "当前没有代码，保存失败！";
	}
}
const  hexdatacolors="#FFB6C1,#FFC0CB,#FFF0F5,#DB7093,#FF69B4,#FF1493,#C71585,#DA70D6,#D8BFD8,#DDA0DD,#EE82EE,#FF00FF,#800080,#BA55D3,#9400D3,#9932CC,#4B0082,#8A2BE2,#9370DB,#7B68EE,#6A5ACD,#483D8B,#E6E6FA,#0000FF,#0000CD,#00008B,#000080,#4169E1,#6495ED,#B0C4DE,#708090,#1E90FF,#F0F8FF,#4682B4,#87CEFA,#87CEEB,#00BFFF,#ADD8E6,#B0E0E6,#5F9EA0,#F0FFFF,#E1FFFF,#AFEEEE,#00FFFF,#00CED1,#2F4F4F,#008B8B,#20B2AA,#40E0D0,#7FFFAA,#00FA9A,#00FF7F,#3CB371,#2E8B57,#F0FFF0,#90EE90,#98FB98,#8FBC8F,#32CD32,#00FF00,#228B22,#008000,#006400,#7CFC00,#ADFF2F,#556B2F,#6B8E23,#FFFFF0,#FFFFE0,#FFFF00,#808000,#BDB76B,#FFFACD,#F0E68C,#FFD700,#DAA520,#FFFAF0,#FDF5E6,#F5DEB3,#FFE4B5,#FFA500,#FFEFD5,#FFEBCD,#FFDEAD,#FAEBD7,#D2B48C,#FFE4C4,#FF8C00,#FAF0E6,#CD853F,#FFDAB9,#F4A460,#D2691E,#FFF5EE,#A0522D,#FFA07A,#FF7F50,#FF4500,#FF6347,#FFE4E1,#FA8072,#FFFAFA,#F08080,#BC8F8F,#CD5C5C,#FF0000,#DC143C,#A52A2A,#B22222,#8B0000,#FFFFFF,#F5F5F5,#DCDCDC,#D3D3D3,#C0C0C0,#808080,#A9A9A9,#696969,#000000";

function colorselect(colors){
	console.log("颜色开始列表：",colors);
	var n=colors.length;
	var m=0;
	for(i=0;i<n;i++){
		var ncolor=colors[i];
		ncolor=ncolor.toUpperCase();
		if(hexdatacolors.indexOf(ncolor)<0){
			colors.splice(i,1);
			console.log("清除颜色：",ncolor);
			i--;
			n--;
			m++;
		}		
	}
	colors=colorcomp(colors);
	console.log("颜色结果列表：",colors);
	return colors;
}
function colorcomp(colors){
	var n=colors.length;

	for(i=0;i<n;i++){
		for(j=i+1;j<n;j++){	
			var fcolor=colors[i];		
			var ncolor=colors[j];
			
			//console.log(ncolor);
			var fr=fcolor.substring(0,2)
			var fg=fcolor.substring(2,4)
			var fb=fcolor.substring(4,6)
			//console.log("rgb:",fr,fg,fb);
			var nr=ncolor.substring(0,2)
			var ng=ncolor.substring(2,4)
			var nb=ncolor.substring(4,6)
			//console.log("rgb:",nr,ng,nb);

			var absR=hex2int(fr)-hex2int(nr);
			var absG=hex2int(fg)-hex2int(ng);
			var absB=hex2int(fb)-hex2int(nb);
			//console.log("abs:",absR,absG,absB);
			var dis=Math.sqrt(absR*absR+absG*absG+absB*absB);
			//console.log(fcolor,"-",j,"-",ncolor," dis:",dis);
			if(dis<72){
				colors.splice(j,1);
				n--;
				j--;
			}

		}		
	}
	//console.log("颜色：",colors);
	return  colors;
}

function hex2int(hex) {
    var len = hex.length, a = new Array(len), code;
    for (var i = 0; i < len; i++) {
        code = hex.charCodeAt(i);
        if (48<=code && code < 58) {
            code -= 48;
        } else {
            code = (code & 0xdf) - 65 + 10;
        }
        a[i] = code;
    }
    
    return a.reduce(function(acc, c) {
        acc = 16 * acc + c;
        return acc;
    }, 0);
}
 
function ArrayToString(c){
	var s="";
	var len=c.length;
	for (i=0;i<len;i++){
		s=s+c[i];
		if(i<len-1) s=s+"\n";
	}
	return s;
}

function sizechecknew(a,b){
	var arra=a.split("x");
	var arrb=b.split("x");
	if(arra.length==7 && arrb.length==7)
	{		
		var a1=parseInt(arra[0]);
		var a2=parseInt(arra[1]);
		var a3=parseInt(arra[2]);
		var a4=parseInt(arra[3]);
		var a5=parseInt(arra[4]);
		var a6=parseInt(arra[5]);
		var a7=parseInt(arra[6]);;//颜色
		var b1=parseInt(arrb[0]);
		var b2=parseInt(arrb[1]);
		var b3=parseInt(arrb[2]);
		var b4=parseInt(arrb[3]);
		var b5=parseInt(arrb[4]);
		var b6=parseInt(arrb[5]);
		var b7=parseInt(arrb[6]);//颜色
		
		if(!isNaN(a1) &&!isNaN(a2) &&!isNaN(b1)&&!isNaN(b2)){					
			
			var ab1=Math.abs(a1-b1);
			var ab2=Math.abs(a2-b2);
			var ab3=Math.abs(a3-b3);
			var ab4=Math.abs(a4-b4);
			var ab5=Math.abs(a5-b5);
			var ab6=Math.abs(a6-b6);
			var ab7=Math.abs(a7-b7);
			
			//长x宽x斜x斜x点x点x颜
			var p1=mistake(a1,2);
			var p2=mistake(a2,1);
			var p3=mistake(a3,2);
			var p4=mistake(a4,2);
			var p5=mistake(a5,3);
			var p6=mistake(a6,3);
			var p7=mistake(a7,5);
			
			console.log("标准尺寸 ",a1,a2,a3,a4,a5,a6,a7);
			console.log("当前尺寸 ",b1,b2,b3,b4,b5,b6,b7);
			console.log("存在误差 ",ab1,ab2,ab3,ab4,ab5,ab6,ab7);
			console.log("允许误差 ",p1,p2,p3,p4,p5,p6,p7);
			if((ab1<=p1) &&( ab2<=p2) && (ab3<=p3) && (ab4<=p4)&& (ab5<=p5) && (ab6<=p6) &&  (ab7<=p7) ){
				console.log("自动批改通过！");
				return true;
			}
		}	
	}

	return false;
}

function samestr(a,b){
	if(a==b) return true;
	else return false;
}

function mistake(a,n){
	var p=a*n/10;
	if(p==0) {
		p=1;
	}
	return Math.round(p);	
}
	
</script>
</body>
</html>