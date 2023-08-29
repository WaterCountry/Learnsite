<%@ page language="C#" autoeventwireup="true" inherits="Python_turtle, LearnSite" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Python绘画编程</title>
  <link href="../code/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
  <link href="../code/site.css" rel="stylesheet" type="text/css" />
    <script src="../kindeditor/plugins/code/prettify.js" type="text/javascript"></script>
    <link href="../kindeditor/plugins/code/prettify.css?ver=621" rel="stylesheet" type="text/css" />
</head>

<body  onload="prettyPrint(); ">

<div class="banner">
	<i class="fa fa-codepen" ></i> <input id="title"  type="text"  value="Python 绘画编程"/>
</div>
<div class="main" >
	<div class="left" id="editor"></div>
	<div class="right">
		<div id="result">
		<pre id="output" ></pre>
		<div id="cv" ></div>
		</div>
	</div>
</div>

<div  id="memory" onclick="remember()" title="回放代码" > </div>
<div  id="big" onclick="fontbig()" title="放大代码"> </div>
<div  id="small" onclick="fontsmall()" title="缩小代码"> </div>
<div id="colorbox"></div>

<div class="tooltip">
	<span class="keymodel" title="界面切换">❖</span>
	<span class="keyword" title="前进">forward</span>
	<span class="keyword" title="后退">backward</span>
	<span class="keyword" title="左转">left</span>
	<span class="keyword" title="右转">right</span>
	<span class="keyword" title="画圆">circle</span>
	<span class="keyword" title="抬笔">penup</span>
	<span class="keyword" title="落笔">pendown</span>
	<span class="keyword" title="画笔颜色">pencolor</span>
	<span class="keyword" title="画笔粗细">pensize</span>
	<span class="keyword" title="填充颜色">fillcolor</span>
	<span class="keyword" title="开始填充">begin_fill</span>
	<span class="keyword" title="结束填充">end_fill</span>
	<span class="keyword" title="回家">home</span>
	<span class="keybox" title="颜色选择">✿</span>
</div>

<div id="savemsg"></div>

<div  id="sideby">
<button  onclick="runit()" class="button"  >
<i class="fa fa-caret-right" aria-hidden="true"></i>运行</button>&nbsp;&nbsp;
<span class="sp"></span>
<button  onclick="savecode()" class="button">
<i class="fa fa-save" aria-hidden="true"></i>保存</button>
<span class="sp"></span>
<button  onclick="returnurl()" class="button">
<i class="fa fa-reply" aria-hidden="true"></i>返回</button>
</div>
</div>

<script src="../code/colorbox.js" type="text/javascript"></script>
<script src="../code/skulpt.min.js?ver=20211202" type="text/javascript"></script>
<script src="../code/skulpt-stdlib.js" type="text/javascript"></script>
<script src="../code/html2canvas.min.js" type="text/javascript"></script>
<script src="../code/jquery.min.js" type="text/javascript"></script>

<script src="../code/build/src/ace.js" type="text/javascript"></script>
<script src="../code/build/src/ext-language_tools.js" type="text/javascript"></script>
<script src="../code/build/src/ext-beautify.js" type="text/javascript"></script>
<script type="text/javascript">
    var fontsize = 24;
    var editor = ace.edit("editor");
    editor.setOptions({
        wrap: true,
        enableLiveAutocompletion: true,
        enableSnippets: true
    });
    editor.setTheme("ace/theme/gruvbox");
    editor.getSession().setMode("ace/mode/python");
    editor.setFontSize(fontsize);
    editor.setReadOnly(false)
    editor.getSession().setTabSize(4);
    editor.commands.addCommand({
	    name: 'execute',
	    bindKey: {win: 'Ctrl-enter', mac: 'Command-enter'},
	    exec: function(editor) {
		    runit();
	    },
    });

    function returnurl() {
        if (confirm('确定要返回吗？记得先保存。') == true) {
            window.location.href = "index.aspx";
        }
    }

</script>
<script type="text/javascript">

var snum = "<%=Snum %>";
var id = "<%=Id %>";
var tout="";
var cf = "<%=Codefile %>";
var title="<%=title %>";
var codefile = decodeURIComponent(window.atob(cf)); 

var codedict = new Array();
var keyopen=true;
var keybox=false;
var sessionkey=snum+"-"+id+"auto";

var mypre = document.getElementById("output");
var result = document.getElementById("result");
var savemsg = document.getElementById("savemsg");
var cv = document.getElementById("cv");

function outf(text) {
    mypre.innerHTML = mypre.innerHTML + text;
}
function builtinRead(x) {
    if (Sk.builtinFiles === undefined || Sk.builtinFiles["files"][x] === undefined)
            throw "File not found: '" + x + "'";
    return Sk.builtinFiles["files"][x];
}

function myfun() {
	return new Promise(function(resolve,reject){		
		var myinput=document.createElement("input");
		myinput.setAttribute("type","text");
		myinput.setAttribute("class","input");
		mypre.appendChild(myinput);
		myinput.focus();
        result.onclick=function(){
            myinput.focus();
        }

		myinput.onkeypress =function() {
             if (event.keyCode == 13)
             {
				var argv=myinput.value;	
				console.log(argv);
				mypre.removeChild(myinput);
                mypre.innerHTML= mypre.innerHTML+argv+" \n";				
                resolve(argv);
             }
         }
      })
}


function runit() {
    var prog = editor.getValue();
    if (prog != null && prog != "") {
        mypre.innerHTML = '';
        output.innerHTML = '';
        savemsg.innerHTML = '';
        Sk.pre = "output";
        
        Sk.configure({ output: outf, read: builtinRead, execLimit: 360000, __future__: Sk.python3, inputfun: myfun });

        (Sk.TurtleGraphics || (Sk.TurtleGraphics = {})).target = 'cv';
        Sk.TurtleGraphics.width = cv.clientWidth;
        Sk.TurtleGraphics.height = cv.clientHeight;
        var dateBegin = new Date();

        var myPromise = Sk.misceval.asyncToPromise(function () {
            return Sk.importMainWithBody("<stdin>", false, prog, true);
        });

        myPromise.then(function (mod) {
            var dateEnd = new Date(); 
            var dateDiff = (dateEnd.getTime() - dateBegin.getTime()) / 1000; 
            var spendtime = '耗时' + dateDiff + "秒"
            console.log(spendtime);
            msgerror = ""; 
            console.log('运行成功!');
            savemsg.innerHTML = spendtime;
            tout=output.innerHTML;
        },
	function (err) {
		
		
		var msg = err.toString();
		console.log(msg);
		if (msg.indexOf("TimeLimitError") > 0) {
		    msg = "运行超时！";
		}
		savemsg.innerHTML = msg;
		msgerror = msg;
	});
    }
    else {
        savemsg.innerHTML = "当前没有代码，无法运行！";
    }
}


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

var grid=true;
$(".right").css("background-image","url(../code/grid.png)");
$(".right").css("background-color","#454e4e");

$(".right").click(function(){
	if(grid){
		$(".right").css("background-image","none");
		grid=false;	
	}
	else{	
		$(".right").css("background-image","url(../code/grid.png)");
		grid=true;
	}
});


function savecode(){
	var pass=0;
	var draw=false;
	var prog = editor.getValue();
    title=$("#title").val();

	if(prog!=null&&prog!=""){
		savecoding();
		var res=document.querySelector("#result");
		var obj=$("#cv").find("canvas")[1];
		
		if(!obj) {
			$("#cv").hide();
			console.log("无绘图");
			draw=false;
			obj=res;
		}
		else{
			console.log("有绘图");
			draw=true;
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
				
				
				
				var minNl = Math.min.apply(null,arrsetl);	
				var maxNl = Math.max.apply(null,arrsetl);	
				var mi_mxl=(maxNl-minNl)*Math.sqrt(2).toFixed(0);				
				
				var minNr = Math.min.apply(null,arrsetr);	
				var maxNr = Math.max.apply(null,arrsetr);	
				var mi_mxr=(maxNr-minNr)*Math.sqrt(2).toFixed(0);
				var milr=mi_mxl+"x"+mi_mxr+"x"+lcount+"x"+rcount+"x"+colorsize;
				
				console.log("自动裁剪");	
				dataURL = x.toDataURL();				
                
				var scale=x.width/x.height;//宽高比例
				scale=scale.toFixed(2);
									
				var outcv=x.width+"x"+scale+"x"+milr;				
			   var Cover=blob(dataURL);
			   
			   console.log('保存快照');			   
			   
				var urls = 'uploadturtle.ashx?id=' + id;
				var formData = new FormData();
				formData.append('cover', Cover);
				formData.append('tcode', window.btoa(encodeURIComponent(prog)));
				formData.append('timg', outcv);
				formData.append('tout', tout);
				formData.append('title',title);
				formData.append('study',"0");

				$.ajax({
					url: urls,
					type: 'POST',
					cache: false,
					data: formData,
					processData: false,
					contentType: false
				}).done(function (res) { 
					console.log(res);
                    if(res>0){
					    savemsg.innerHTML= "运行正常,自动保存成功！"+savemsg.innerText;
                        if(id!=res){
							id=res;
							var urlnew="../Python/turtle.aspx?id="+res;                            
                        }
                    }
                    else{
                        savemsg.innerHTML= "运行正常,自动保存失败！"+res;
                    }
					
				}).fail(function (res) {
					savemsg.innerHTML= "自动保存失败！";
					console.log(res)
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
	console.log("颜色：",colors);
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

function history(key,value) {
	localStorage.setItem(key,value);
	codedict[key]=value;
	
}
var keycount=0;
var oldlen=0;
var oldcode='';

var missionkey="turtle"+"-"+id+"-";
if(id==null||id==''){
	missionkey=missionkey+Date.now();
}
else{
	$("#title").val(title);
}

console.log("旧记录key"+missionkey);
var countkey=missionkey+"count";
var oldcount=localStorage.getItem(countkey);
if(oldcount!=null){
	keycount=parseInt(oldcount);
}
console.log("旧记录数"+keycount);

document.onkeyup = keyUp;

function keyUp() {
	savecoding();
}

function savecoding(){	
	var p = editor.getValue();
	var temp=$.trim(p)
	
	if(temp!=oldcode){
		keycount=parseInt(keycount)+1;
		var hiskey=missionkey+keycount;
		history(hiskey,p);
		
		oldcode=temp;
		history(countkey,keycount);
	}	
}

function setcode() {
  editor.setValue(codefile,1);
}

function getcoding(){
	oldcount=localStorage.getItem(countkey);
	if(oldcount!=null){
		var backkey=missionkey+oldcount;
		var coding=localStorage.getItem(backkey);
		console.log("--------------\n\r"+coding+"\n\r--------------")
		editor.setValue(coding,1);		
		console.log("读取临时记录程序");
	}
	else{		
		console.log("空白程序");
	}
}

if(codefile!=''&& codefile!=null){
	setcode(codefile);
    getsession();
	console.log("读取数据库储存程序");
}
else{
	getcoding();
}

function selectTheme(){
	if(theme){
		editor.setTheme("ace/theme/gruvbox");
		theme=false;
	}
	else{
		editor.setTheme("ace/theme/textmate");
		theme=true;
	}
}

function fontbig(){
	fontsize+=2;
	editor.setFontSize(fontsize);	
}

function fontsmall(){
	fontsize-=2;
	if (fontsize<18){
		fontsize=18;
	}
	editor.setFontSize(fontsize);	
}

function screenclear(){    
	mypre.innerHTML = '';
	output.innerHTML = '';
	savemsg.innerHTML='';
	cv.innerHTML='';
}

var key=1;
var kcount=1;
var remauto=false;

function remember() {
	remauto=!remauto;
	recoding();
}	

function recoding(){
	if(remauto)
	{
		if(key==1) console.log('回放代码开始---');
		var oldcount=localStorage.getItem(countkey);
		if(oldcount!=null){
			console.log("旧记录"+key+"/"+oldcount);
			kcount=parseInt(oldcount);
			
			var backkey=missionkey+key;
			var coding=localStorage.getItem(backkey);
			
			if(key<kcount+1){
				editor.setValue(coding,1);
				key=key+1;
				setTimeout("recoding()",200);
			}else{
				key=1;
				remauto=false;
				console.log('回放代码结束-----');
			}
		 }
		 else{
			remauto=false;
			console.log("旧记录数为空\n\r回放代码结束-----");		 
		 }
	}
}
    var keyopen=false;
	var sessionkey=snum+"-"+id+"auto";

    function savesession(){
	    var codestr = editor.getValue();
        if(codestr!=null && codestr!=""){
            sessionStorage.setItem(sessionkey,codestr);
        }  
    }
    function getsession(){
        var codestr=sessionStorage.getItem(sessionkey);
        if(codestr!=null && codestr!=""){	 
		    editor.setValue(codestr,1);
            sessionStorage.clear();//读取后清除，防止污染
        }    
    }	
    	    	
	$(document).ready(function(){
		$(".keymodel").click(function(){
				var url= window.location.href;
				url= url.replace("turtle","idle");
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

		$(".keyopen").click(function(){
			keyopen=!keyopen;
			if(keyopen) {
				$(this).css("color","#86cf5f");
				$(".keyword").css("color","#86cf5f");
			}
			else{
				$(this).css("color","#73878e");
				$(".keyword").css("color","#73878e");
			}
		});	
			
		$(".keyword").click(function(){
			if(keyopen){
				var keytxt=$(this).text();	
				var cmdstr="";
				switch(keytxt)
				{
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
				var oldstr =editor.getValue();
				if(oldstr!=""){
					oldstr=oldstr+" ";
				}
				var newstr=oldstr+cmdstr;
				editor.setValue(newstr,1);
				editor.focus();
			}
		});
	});
</script>
</body>
</html>