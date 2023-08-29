<%@ page language="C#" autoeventwireup="true" inherits="student_pythonblock, LearnSite" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"> 
  <title></title>
<link href="../code/block.css" rel="stylesheet" type="text/css" />
<link href="../code/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../js/jquery-ui-1.8.24.custom.min.js" type="text/javascript"></script>
<script src="../code/build/src/ace.js" type="text/javascript"></script>
<script src="../code/build/src/ext-language_tools.js" type="text/javascript"></script>
<script src="../code/skulpt.min.js?ver=20211202" type="text/javascript"></script>
<script src="../code/skulpt-stdlib.js" type="text/javascript"></script>
<script src="../code/html2canvas.min.js" type="text/javascript"></script>
</head>
<body>
<div>
<div class="banner">
	 <i class="fa fa-codepen" ></i> <a id="title" >Python拼图编程：<%=Titles%></a>
</div>
<div id="main">
<div id="left">
&nbsp;请将第一块积木拖放到这里！
</div>
<div id="right"></div>
</div>

<div id="content" >
	<%=Mcontents %>
	<br /><br />
</div>
<div id="result">
<div id="savemsg"></div>
<pre id="output" ></pre>
</div>
<div id="cv" ></div>
<div  id="big" onclick="fontbig()" title="放大代码"> </div>
<div  id="small" onclick="fontsmall()" title="缩小代码"> </div>
<div class="map"><img class="mapimg" src="<%=Midurl %>" alt=""/></div>
<div id="done"><img src="../images/sucessed.png"></img></div>
<audio id="myaudio" src="../code/adsorb.ogg" controls="controls"  hidden="true" ></audio>
<div  id="sideby">
<button  onclick="helper()" class="button"  >
<i class="fa fa-book" aria-hidden="true"></i>学案</button>&nbsp;&nbsp;
<span class="sp"></span>
<button  onclick="runit()" class="button"  >
<i class="fa fa-play-circle" aria-hidden="true"></i> 运行</button>&nbsp;&nbsp;
<span class="sp"></span>
<button  onclick="savecode()" class="button"  >
<i class="fa fa-save" aria-hidden="true"></i> 保存</button>&nbsp;&nbsp;
<span class="sp"></span>
<button  onclick="returnurl()" class="button">
<i class="fa fa-reply" aria-hidden="true"></i> 返回</button>
</div>
	<script type="text/javascript" >  
		var fontsize=25;
	    var editor = ace.edit("right", {
	        theme: "ace/theme/chrome",
	        mode: "ace/mode/python"
	    });
	    editor.setFontSize(fontsize);
	    editor.focus();
		editor.setReadOnly(true);

	    var snum = "<%=Snum %>";
	    var id = "<%=Id %>";
	    var fpage = "<%=Fpage %>";
        var argcodestr = "<%=argcode %>";
        var arginstr = "<%=argin %>";
        var argin = arginstr.split("#");

        var argout0 = "<%=argout0 %>";
        var argout1 = "<%=argout1 %>";
        var argout2 = "<%=argout2 %>";
        
        var mpass = "<%=mpass %>";

        var codearg = decodeURIComponent(window.atob(argcodestr)); //定义字典 
        var example =codetoarray(codearg,true);


	    var sessionkey = "htmlcode" + snum + "-" + id;

	    function savesession() {
	        var codestr = editor.getValue();
	        if (codestr != null && codestr != "") {
	            sessionStorage.setItem(sessionkey, codestr);

	        }
	    }
	    function getsession() {
	        var codestr = sessionStorage.getItem(sessionkey);
	        if (codestr != null && codestr != "") {
	            editor.setValue(codestr, 1);
	        }
	    }
	    document.onkeyup = keyUp;
	    function keyUp() {
	        savesession();
	    }

function fontbig(){
	fontsize+=1;
	editor.setFontSize(fontsize);	
}

function fontsmall(){
	fontsize-=1;
	if (fontsize<25){
		fontsize=25;
	}
	editor.setFontSize(fontsize);	
}

        function returnurl() {
            if (confirm('确定要返回吗？记得先保存。') == true) {
                sessionStorage.clear(); //返回后清除，防止污染
                window.location.href = fpage;
            }
        }

		var minleft=360;
		var mintop=42; 
		var toplist=[];
		var leftlist=[];
		var curtop;

        $(function () {
			if(mpass=="1"){
				rightblock();
				$('#done').show();
			}
			else{
				arrayblock();
				$('#done').hide();
			}
            init();
			
            function init() {
				
                $('.brick').draggable({ opacity: 0.3, helper: 'original', grid: [14, 14],
                    containment: "#left", snap: true, snapTolerance: 14,
					start:function(){
						curtop = $(this).offset().top;//获取拖放前的位置					
					},					
                    drag: function (event, ui) {
						var tpos = $(this).offset().top;
						//console.log("设置上边距",tpos);
						if(isoverlap(tpos)){							
							$(this).draggable({revert: true});//重叠
						}
						else{								
							$(this).draggable({revert: false});//空位
						}	
                    },
					stop: function (event, ui) {
                        var x = $(this).offset().left;
                        var y = $(this).offset().top;	
						var lpos=4+60*(Math.floor(x/30)-Math.floor(x/60));//缩进4个空格宽度为60像素
						//var ttpos=28*(Math.floor(y/14)-Math.floor(y/28))-14;//行距28像素
						
						$(this).offset({left:lpos});
						//console.log("设置左边距",lpos);
						if(y<70){
							y=mintop;
						}
						$(this).offset({top:y});
						//console.log(y,"左",lpos,"上",y,ttpos);
						
						if(lpos<minleft){
							minleft=lpos;
						}						
						blocktocode();
						voiceplay();
					}

                });
                $(".brick").css("cursor", "move");
			
            }
			
			
        });
		
		function isoverlap(top){
			var isres=false;
			var str="空位";
			//console.clear();
			for(var i=0;i<toplist.length;i++){
				var item=toplist[i];
				var min=item-2;
				var max=item+2;
				
				//console.log(top,"当前位置",item);
				if(top>=min && top<max && top!=curtop){
					console.log("重叠",min,top,max,curtop);
					return true;
				}
				else{
					//console.log("空位",min,top,max);
				}
			
			}				
			
			return isres;
		}
			
		function blocktocode(){
			//console.log("blocktocode生成代码：");
			var $block=$('.brick');		
			var codestr="";
			var dict={};
			var posi={};
			//console.clear();
			$block.each(function(i){
				var lpos=$(this).offset().left;		
				var tpos=$(this).offset().top;
				dict[tpos]=$(this).text();//记录排序前代码文本位置
				if($(this).text()=="　　　　　　"){
					dict[tpos]="";
				}
				//console.log("左",lpos,"上",tpos," ",dict[tpos]);
				posi[tpos]=lpos;
				toplist[i]=tpos;
				leftlist[i]=lpos;
			})
			
			//console.log(dict);
			var res = Object.keys(dict).sort((a,b)=>a-b);
			for (var key in res){
				var newlpos=posi[res[key]];
				var returnstr="";
				if(key<res.length-1){
					returnstr="\r\n";
				}
				var linecode=tabnum(newlpos)+dict[res[key]]+returnstr;//获取排序后代码文本
				codestr=codestr+linecode;
				//console.log(key,linecode);
			}
			//console.log(posi);
			//console.log(codestr);	
			editor.setValue(codestr, 1);
			$('#result').hide();
		}
		
		function tabnum(newlpos){
			var n=parseInt((newlpos-minleft)/50);
			console.log("左边距",newlpos,"最左边",minleft,n);
			var str="";
			for(var i=0;i<n;i++){
				str=str+"    ";
			}
			return str;
		}
		
		function arrayblock(){
			console.log("arrayblock");
			if(example.length>0){
				var blockplace=document.getElementById("left");
				example.sort(function () {
					return Math.random() - 0.5
				})
				
				for(var i=0;i<example.length;i++){
					var span=document.createElement("span");
					span.innerText=example[i];
					span.className="brick";
					//console.log(i,'上:',mytop,mytop+44)
					span.style.backgroundColor="#76A9DC";	
					if(example[i].indexOf("print")!=-1){
						span.style.backgroundColor="#95CA97";				
					}
					//默认方块为蓝色#76A9DC，如果为输出设置为绿#95CA97，如果为输入设置为红#DDA3C1 紫#A78BD3 淡黄#EFEFDE
					if(example[i].indexOf("input")!=-1){
						span.style.backgroundColor="#DDA3C1";				
					}
					if(example[i].indexOf("from")!=-1){
						span.style.backgroundColor="#A78BD3";				
					}
					if(example[i].indexOf("for ")!=-1){
						span.style.backgroundColor="#D8C07A";				
					}				
					if(example[i].indexOf("while")!=-1){
						span.style.backgroundColor="#D8C07A";				
					}				
					if(example[i].indexOf("if")!=-1){
						span.style.backgroundColor="#B5BB85";				
					}				
					if(example[i].indexOf("else")!=-1){
						span.style.backgroundColor="#B5BB85";				
					}				
					if(example[i].indexOf("elif")!=-1){
						span.style.backgroundColor="#B5BB85";				
					}
					if(example[i]==""){	
						span.style.backgroundColor="#E1E1CD";
						span.innerText="　　　　　　";
					}					
					blockplace.appendChild(span);			
				}
			
				var $block=$('.brick');		
				var dict={};
				var myleft=120;
				var mytop=166;
				$block.each(function(i){				
					mytop=mytop+38;//横幅高40像素，4像素间隙，积木行距38
					
					$(this).offset({left:myleft,top:mytop});
					
					var lpos=$(this).offset().left;
					var tpos=$(this).offset().top;
					
					dict[tpos]=example[i];
					//console.log(i,"左",lpos,"上",tpos,dict[tpos]);
					toplist[i]=tpos;					
					leftlist[i]=lpos;
				})
				
				var footer=document.createElement("span");
				footer.innerText=".";
				footer.className="blank";
				blockplace.appendChild(footer);
				$(".blank").offset({top:mytop+38});
				
				$('#result').hide();//隐藏输出控制台
			}
			else{
				savemsg.innerHTML= "未设定批改程序，请咨询老师！";
			}
		}
			
		function countblank(str){
			var count=0;
			for(var i=0;i<str.length;i++){
				var c=str[i]
				if(c==' '){
					count++;
				}
				else{
					break;
				}
			}
			return count;
		}
		
		function rightblock(){
			var blockplace=document.getElementById("left");
			var codeargarray=codetoarray(codearg,false);
			console.log("已保存的正确拼图");
			//console.log(codeargarray);
			for(var i=0;i<example.length;i++){
				var span=document.createElement("span");
				//console.log(i,"空格数：",blankcount);
				span.innerText=example[i].trim();
				span.className="brick";
				var mytop=4+i*28;
				span.style.top=mytop+"px";
				//console.log(i+1,mytop,example[i]);
								
				span.style.backgroundColor="#76A9DC";	
				if(example[i].indexOf("print")!=-1){
					span.style.backgroundColor="#95CA97";				
				}
				//默认方块为蓝色#76A9DC，如果为输出设置为绿#95CA97，如果为输入设置为红#DDA3C1 紫#A78BD3 淡黄#EFEFDE
				if(example[i].indexOf("input")!=-1){
					span.style.backgroundColor="#DDA3C1";				
				}
				if(example[i].indexOf("from")!=-1){
					span.style.backgroundColor="#A78BD3";				
				}
				if(example[i].indexOf("for ")!=-1){
					span.style.backgroundColor="#D8C07A";				
				}				
				if(example[i].indexOf("while")!=-1){
					span.style.backgroundColor="#D8C07A";				
				}				
				if(example[i].indexOf("if")!=-1){
					span.style.backgroundColor="#B5BB85";				
				}				
				if(example[i].indexOf("else")!=-1){
					span.style.backgroundColor="#B5BB85";				
				}				
				if(example[i].indexOf("elif")!=-1){
					span.style.backgroundColor="#B5BB85";				
				}
				if(example[i]==""){	
						span.style.backgroundColor="#E1E1CD";
						span.innerText="　　　　　　";
					}					
				
				blockplace.appendChild(span);			
			}		
		
			var $block=$('.brick');	
			$block.each(function(i){				
				var codeline=codeargarray[i];
				var blankcount=countblank(codeline);
				
				var myleft=4+15*blankcount;
				var mytop=44+i*28;//横幅高40像素，4像素间隙
				$(this).offset({left:myleft,top:mytop});
				
				var lpos=$(this).offset().left;
				var tpos=$(this).offset().top;
				
				if(lpos<minleft){
					minleft=lpos;
				}
				//console.log(i+1,"空",blankcount,"上",tpos,codeline);
			})
			$('#result').hide();//隐藏输出控制台
			
			editor.setValue(codearg, 1);
		}
		
    	$("#left").click(function(){
		  $("#result").slideUp();
		});
		$("#title").click(function(){
		  $("#result").slideDown();
		  $("#cv").slideDown();
		});

        $("#right").click(function(){
		  $("#content").slideUp();
		  $("#cv").slideUp();
		});

        function  helper(){
	        $("#content").slideToggle();	
        }

		function randomsort(a, b) {
		  return Math.random()>.5 ? -1 : 1; //通过随机产生0到1的数，然后判断是否大于0.5从而影响排序，产生随机性的效果。
		}
	

$("#content").slideDown();

var mypre = document.getElementById("output");
var result = document.getElementById("result");
var savemsg = document.getElementById("savemsg");
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
var isrun=false;
var isright=true;
 
function savecode(){
	var prog = editor.getValue();
	if(prog!=null&&prog!=""){		
		if(isrun){
			if(comparecode(prog,codearg)){
			   $('#done').show();
			   alert("恭喜！ 拼图成功！");
			}
			else{
				$('#done').hide();
				alert("拼图失败，请继续努力！");
			}
			uploadblock(prog);			
		}
		else{		
			alert("程序运行正常后，再点击保存！");		
		}				
	}
	else{		
		alert("根据学案代码拼图后再运行程序！");			
	}
	isrun=false;
}

function comparecode(prog,codearg){
	//prog.replace(/\r/g,"");
	var parray=prog.split("\n");
	//console.log("编辑器:\n",parray);
	//codearg.replace(/\r/g,"");
	var carray=codearg.split("\n");
	//console.log("原程序:\n",carray);	
	console.log("代码比对开始");	
	isright=true;
	for(var i=0;i<parray.length;i++){
		var pstr=parray[i].replace("\r","");
		var cstr=carray[i].replace("\r","");
		if(pstr.trim()!=cstr.trim()){
			console.log(i,"错误",pstr,cstr);		
			isright=false;
		}
		else{
			//console.log(i,"正确",pstr,cstr);		
		}
		
	}	
	return isright;
}

function uploadblock(prog){
    var res=document.querySelector("#result");
	var obj=$("#cv").find("canvas")[1];			
	if(!obj) {
		$("#cv").hide();
		console.log("无绘图");
		obj=res;
	}
	var opts = {
		backgroundColor: "transparent", 
    };
	html2canvas(obj).then(pic => {		   
	   var dataURL=pic.toDataURL();
	   //console.log(dataURL);

	   if(dataURL=="data:,"){
			dataURL="data:image/gif;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVQImWNgYGBgAAAABQABh6FO1AAAAABJRU5ErkJggg==";
	   }
       		
	   var Cover=blob(dataURL);	   
	   console.log('保存快照'); 
	   var pass=-1;
	   if(isright){
		   pass=3;
	   }
	   
		var urls = 'uploadblock.ashx?id=' + id;
		var formData = new FormData();
		formData.append('cover', Cover);
		formData.append('codefile', window.btoa(encodeURIComponent(prog)));
		formData.append('codedict', '');
		formData.append('pass',pass);

		$.ajax({
			url: urls,
			type: 'POST',
			cache: false,
			data: formData,
			processData: false,
			contentType: false
		}).done(function (res) { 
			if(argcodestr!=""&& argcodestr!=null){			
				if(isright){  //如果代码拼图正确，标志为3那么										
					savemsg.innerHTML= "恭喜！ 代码拼图正确，保存成功。";
				}
				else{					
					savemsg.innerHTML= "拼图失败，请继续努力！保存成功。";
				}
			}
		}).fail(function (res) {
			savemsg.innerHTML= "保存失败！";
			console.log(res)
		}); 			

		});	

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
	
function runit() {
    var prog = editor.getValue();
	if(prog!=null&&prog!=""){
		$("#content").slideUp();
		$("#cv").slideDown();
		$('#result').show();
		mypre.innerHTML = '';
		output.innerHTML = '';
		savemsg.innerHTML='';
		Sk.pre = "output";		
		Sk.configure({ output: outf, read: builtinRead,execLimit: 600000, __future__: Sk.python3, inputfun: myfun});

		(Sk.TurtleGraphics || (Sk.TurtleGraphics = {})).target = 'cv';
		Sk.TurtleGraphics.width=cv.clientWidth;
		Sk.TurtleGraphics.height=cv.clientHeight;
		var dateBegin = new Date();

		var myPromise = Sk.misceval.asyncToPromise(function() {
			return Sk.importMainWithBody("<stdin>", false, prog, true);
		});
	 
		myPromise.then(function(mod) {
			var dateEnd = new Date();
			var dateDiff = (dateEnd.getTime() - dateBegin.getTime())/1000;
			var spendtime='耗时'+dateDiff+"秒"
			console.log(spendtime);			
			
			msgerror="";
			console.log('运行成功!');
			savemsg.innerHTML = spendtime;
            isrun=true;
		},
		function(err) {	
			pass=-1;//如果异常，标志为-1
			var msg=err.toString();
			console.log(msg);
			if(msg.indexOf("TimeLimitError")>0){
				msg="运行超时！";
			}
			savemsg.innerHTML = msg;
			msgerror=msg;
		});
	}	
	else{
		alert("根据学案代码拼图后再运行程序！");
		savemsg.innerHTML= "当前没有代码，无法运行！";
	}
	
}
	
function voiceplay(){
	document.getElementById("myaudio").play();
}
		
function codetoarray(str,istrim){
	if(str !=null&& str.length != 0){
		//str.replace(/\r/g,"");
		var codearray=str.split("\n");		
		//console.log(codearray);
		if(istrim){
			for(c in codearray){
				codearray[c]=codearray[c].trim();
			}
		}
		return codearray;
	}
	else{
		return "";
	}
}

	</script>

    </div>
</body>
</html>