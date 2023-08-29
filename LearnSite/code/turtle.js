var editor = ace.edit("editor");
var fontsize=22;
editor.setOptions({  
  wrap: true,   
  enableLiveAutocompletion: true, 
  enableSnippets: true, 
  
});

editor.setTheme("ace/theme/gruvbox");

editor.getSession().setMode("ace/mode/python");
editor.setFontSize(fontsize);
editor.setReadOnly(false)
editor.getSession().setTabSize(4);
/*
editor.commands.addCommand({
	name: 'execute',
	bindKey: {win: 'Ctrl-enter', mac: 'Command-enter'},
	exec: function(editor) {
		runit();
	},
});
*/

function fontbig(){
	fontsize+=1;
	editor.setFontSize(fontsize);	
}

function fontsmall(){
	fontsize-=1;
	if (fontsize<24){
		fontsize=24;
	}
	editor.setFontSize(fontsize);	
}

function screenclear(){    
	mypre.innerHTML = '';
	output.innerHTML = '';
	savemsg.innerHTML='';
	cv.innerHTML='';
}

var keyopen=true;
var keybox=false;
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

var lockimg=false;
var codemodel=false;   //0编程 1绘图
if(mhelp=="1"){
    codemodel=true;
}


var cssface=true; //界面颜色切换
var desface=true; //学案显示隐藏

$(".fa-codepen").click(function(){
	if(desface){
		$(".description").hide();
		desface=false;
	}
	else{
		$(".description").show();
		desface=true;
	}
});

$("#title").click(function(){
	if(cssface){
		editor.setTheme("ace/theme/chrome");
		$(document.body).css({"background":"#eee"});
		$(".banner").css({"background":"#eee","color":"#666"});
		$("#title").css({"background":"#eee","color":"#666"});
		$(".button").css({"color":"#666"});
		$("#editor").css({"background":"#F9F9F5"});
		$("#content").css({"background":"transparent","color":"#333"});
		$("#output").css({"color":"#333"});
		$("#codexample").css({"background":"#eee","border":"#333"});
		if(codemodel){
			$(".right").css("background","url(../code/gridwhite.png)");
			$("#big").css("filter","invert(80%)");
			$("#small").css("filter","invert(80%)");
		}
		cssface=false;
	}
	else{
		editor.setTheme("ace/theme/gruvbox");
		$(document.body).css({"background":"#454e4e"});
		$(".banner").css({"background":"#454e4e","color":"#ccc"});
		$("#title").css({"background":"#454e4e","color":"#ccc"});
		$(".button").css({"color":"#ccc"});
		$("#editor").css({"background":"#252d30"});
		$("#content").css({"background":"transparent","color":"#ccc"});
		$("#output").css({"color":"#ccc"});
		$("#codexample").css({"background":"#454e4e","border":"#ccc"});
		if(codemodel){
			$(".right").css("background","url(../code/grid.png)");
			$("#big").css("filter","");
			$("#small").css("filter","");
		}
		cssface=true;
		
	}
});

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
	'<span class="keyword" title="大于"> > </span>',
	'<span class="keyword" title="等于"> == </span>',
	'<span class="keyword" title="小于"> < </span>',
	'<span class="keyword" title="冒号"> : </span>',
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
		$(".right").css("background","url(../code/grid.png)");
	}
	else{
		$(".tooltip").html(helpcode);
	}
	
	
	$(".keymodel").click(function(){
			var url= window.location.href;
			url= url.replace("python","turtleidle");
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
				case " > ":
					cmdstr=" > ";
					break;
				case " == ":
					cmdstr=" == ";
					break;
				case " < ":
					cmdstr=" < ";
					break;
				case " : ":
					cmdstr=" : ";
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
			
			var cursorPosition = editor.getCursorPosition(); 
			editor.session.insert(cursorPosition, "\n"+cmdstr);
			editor.focus();
		}
	});
});


if(cvbg!=''){
  cv.style.background="url("+cvbg+")";
}
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

var msgerror="";
var pass=0; 
var runing=false;

function runit() {
	runing=true;
	console.log(runing);
	$("#colorbox").hide();
    var prog = editor.getValue();
	if(prog!=null&&prog!=""){
		mypre.innerHTML = '';
		output.innerHTML = '';
		savemsg.innerHTML='';
		Sk.pre = "output";
		
		Sk.configure({ output: outf, read: builtinRead,execLimit: 3000000, __future__: Sk.python3, inputfun: myfun});

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
			pass=0;//正常运动，标志为0
			//checkright();	
		
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
		savemsg.innerHTML= "当前没有代码，无法运行！";
	}
	
}

function toHex(r,g,b) {
	return ("00000" + (r << 16 | g << 8 | b).toString(16)).slice(-6);
}


function checkright(){
	if(runing){
		var successedmsg = "恭喜自动批改通过！";
		var errormsg = "批改失败，请仔细思考！";
		var draw=false;
		var prog = editor.getValue();
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
					
					console.log("颜色：",newcolors);
					newcolors=colorselect(newcolors);

					let colorsize=newcolors.length;
					
					
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
					
					var simiop=0;
					var simicd=0;

					if(draw){					
						console.log("绘图检测：长x比例x斜x斜x点x点x颜");
						if(sizechecknew(argimg,outcv)){
							console.log(argimg);	
							console.log(outcv);	
							pass=2;
						}
						else{
							console.log(argimg);	
							console.log(outcv);		
						}			
					}
					else{
						simiop=SimiOutput();
						simicd=SimiCodein(prog);
						if(simicd>50 && simiop>80){
							pass=1;							
							console.log("结果相似");
						}
						else{
							if(simicd>92 && simiop>70) {
								pass=1;
								console.log("代码相似");
							}
							else{
								console.log("完全错误");
							}
						}					
					}
					
				   var Cover=blob(dataURL);
				   
				   console.log('保存快照');
				   
				   
					var urls = 'uploadpython.ashx?id=' + id;
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
							if(pass>-1){  //如果运行正常，标志为0那么
								if(draw){
									if (pass==2){						
										savemsg.innerHTML= "恭喜！绘图正确，保存成功！";
										$('#done').show();
									}					
									else{
										savemsg.innerHTML= "绘图有误，请继续努力！";
										$('#done').hide();
									}
								}
								else{
									var mysmg=" 结果相似度"+simiop+"% 代码相似度"+simicd+"%";
									if(pass==1){						
										savemsg.innerHTML= "恭喜！代码正确，保存成功！"+mysmg;
										$('#done').show();
									}					
									else{
										savemsg.innerHTML= "代码有误，请继续努力！"+mysmg;
										$('#done').hide();
									}							
								}	
							}						
							
						}
						else{
							savemsg.innerHTML= "程序运行正常！已保存！";
							$('#done').hide();						
						}
						
					}).fail(function (res) {
						savemsg.innerHTML= "保存失败！";
						console.log(res)
					}); 			
				
				}			

			});
		}
		else{
			savemsg.innerHTML= "当前没有代码，保存失败！";
		}
	}
	else{
		savemsg.innerHTML= "请先运行程序，然后再点保存！";
	}
	
	runing=false;
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
				colors.splice(j,1);//删除一个颜色
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


function SimiCodein(prog) {
	
	s=new difflib.SequenceMatcher(prog,codearg);	
	var ro= s.ratio()*100;
	var sc=ro.toFixed(0);
	console.log("检测代码相似度：",sc);
	return sc;
}


function SimiOutput(){
	var nowpre=output.innerText.trim();	
	var ism=false;
	var res=0;
	if(nowpre!=""){
		for(var i=0;i<3;i++){
			var setpre=argout[i].trim();
			sim= Simiout(nowpre,setpre);
			console.log("检测结果相似度：",sim);
			console.log(nowpre,"长度",nowpre.length);
			console.log(setpre,"长度",setpre.length);
			if(sim>res){
				res=sim;
			}
			if(sim>70){
				ism= true;
				break;
			}		
		}	
	}
	return res;
}

function Simiout(a,b) {
	
	s=new difflib.SequenceMatcher(a,b);	
	var ro= s.ratio()*100;
	return ro.toFixed(0);
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
	
function history(key,value) {
	localStorage.setItem(key,value);
	codedict[key]=value;
	
}
var keycount=0;
var oldlen=0;
var oldcode='';
console.log("学号"+snum);

var missionkey=snum+"-"+id+"-";

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
		savemsg.innerHTML= "";
	}
    voice();	
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
		var codestr=sessionStorage.getItem(sessionkey);
        if(codestr!=null && codestr!=""){	 
			editor.setValue(codestr,1);
            sessionStorage.clear();//读取后清除，防止污染
        }  
		else{
			console.log("空白程序");
		}
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

/*
document.onkeydown=function (e) {
  var theEvent = window.event || e;
  var code = theEvent.keyCode || theEvent.which || theEvent.charCode;
  savemsg.innerHTML='';
  if (code == 13) {
      var p = editor.getValue();
      if (p.indexOf('input')<0){
          
        }
      console.log('回车');
  }
}
*/
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


function stepcode(n) {
  if(key==0) console.log('步进代码开始');
  var tp=localStorage.getItem('kcount');
  if(tp!=null){
  	console.log('记录总数：'+tp);
    kcount=parseInt(tp);
    key=key+n;
    if(key>-1&&key<kcount){
      console.log('key：'+key);
      var value=localStorage.getItem(key);
      
      editor.setValue(value,1);
      voice();
    }else{
      if (key>kcount) key=0;
      if (key<0) key=kcount;
      console.log('当前记录：'+key);
    }
   }else{
   	stepdict(n);
   }

} 

function voice() {
    var audio = document.createElement("audio");
    audio.src = '../code/code.ogg';
    
}

function random(lower, upper) {
	return Math.floor(Math.random() * (upper - lower+1)) + lower;
} 

function code(n) {
  clearit();
  switch(n){
  case 1:
      editor.setValue(example5);      
      break;
  case 2:
      editor.setValue(example4);
      break;
  default:
  	  var num=random(1,6);
  	  var examplename='example'+num;
  	  var gifname='gif'+num;
      editor.setValue(eval(examplename)); 
      
  }
}
function setcode() {
  editor.setValue(codefile,1);
}
function focus() {
  editor.focus();
}
focus();
function clearit(){   
    localStorage.clear(); 
    mypre.innerHTML = '';
    output.innerHTML = '';
}



if(localStorage.getItem(missionkey)){
   console.log("页面被刷新");
   console.log("代码记录："+codedict);
}else{
	console.log("首次被加载");
	localStorage.setItem(missionkey, true)
}

function playdict() {
	if(key==0) console.log('播放代码开始');
	var tp=dictvalue['kcount'];

  	console.log(codedict);
  	console.log('记录总数：'+tp);	
	if(tp!=null){
		kcount=parseInt(tp);
		var value=dictvalue[key];
		if(key<kcount){
			editor.setValue(value,1);
			key=key+1;
      		voice();
		 	setTimeout("playcode()",300);
		}else{
			key=0;
			console.log('播放代码结束');
		}
	 }
}	

function stepdict(n) {
  if(key==0) console.log('步进代码开始');
  var tp=dictvalue['kcount'];
  if(tp!=null){
  	console.log('记录总数：'+tp);
    kcount=parseInt(tp);
    key=key+n;
    if(key>-1&&key<kcount){
      console.log('key：'+key);
      var value=dictvalue[key];
      
      editor.setValue(value,1);
      voice();
    }else{
      if (key>kcount) key=0;
      if (key<0) key=kcount;
      console.log('当前记录：'+key);
    }
   }else{
   	console.log('没有代码记录');
   }

} 

var winHeight = $(window).height()-20; 
$("#content").css("height",winHeight);

$(window).resize(function() { 
    winHeight = $(window).height()-20; 
    $("#content").css("height",winHeight); 
    
});

function returnurl() {
  if(confirm('确定要返回吗？记得先保存。')==true){
      window.location.href=fpage;
    }
}


function sleep(ms) {
    return new Promise(function(resolve, reject) {
		
        setTimeout(resolve,ms);
    })
}


/* Author: Chas Emerick <cemerick@snowtide.com> */
var __whitespace = {" ":true, "\t":true, "\n":true, "\f":true, "\r":true};

var difflib = {
	defaultJunkFunction: function (c) {
		return __whitespace.hasOwnProperty(c);
	},
	
	stripLinebreaks: function (str) { return str.replace(/^[\n\r]*|[\n\r]*$/g, ""); },
	
	stringAsLines: function (str) {
		var lfpos = str.indexOf("\n");
		var crpos = str.indexOf("\r");
		var linebreak = ((lfpos > -1 && crpos > -1) || crpos < 0) ? "\n" : "\r";
		
		var lines = str.split(linebreak);
		for (var i = 0; i < lines.length; i++) {
			lines[i] = difflib.stripLinebreaks(lines[i]);
		}
		
		return lines;
	},
	
	
	__reduce: function (func, list, initial) {
		if (initial != null) {
			var value = initial;
			var idx = 0;
		} else if (list) {
			var value = list[0];
			var idx = 1;
		} else {
			return null;
		}
		
		for (; idx < list.length; idx++) {
			value = func(value, list[idx]);
		}
		
		return value;
	},
	
	
	__ntuplecomp: function (a, b) {
		var mlen = Math.max(a.length, b.length);
		for (var i = 0; i < mlen; i++) {
			if (a[i] < b[i]) return -1;
			if (a[i] > b[i]) return 1;
		}
		
		return a.length == b.length ? 0 : (a.length < b.length ? -1 : 1);
	},
	
	__calculate_ratio: function (matches, length) {
		return length ? 2.0 * matches / length : 1.0;
	},
	
	
	
	
	__isindict: function (dict) {
		return function (key) { return dict.hasOwnProperty(key); };
	},
	
	
	__dictget: function (dict, key, defaultValue) {
		return dict.hasOwnProperty(key) ? dict[key] : defaultValue;
	},	
	
	SequenceMatcher: function (a, b, isjunk) {
		this.set_seqs = function (a, b) {
			this.set_seq1(a);
			this.set_seq2(b);
		}
		
		this.set_seq1 = function (a) {
			if (a == this.a) return;
			this.a = a;
			this.matching_blocks = this.opcodes = null;
		}
		
		this.set_seq2 = function (b) {
			if (b == this.b) return;
			this.b = b;
			this.matching_blocks = this.opcodes = this.fullbcount = null;
			this.__chain_b();
		}
		
		this.__chain_b = function () {
			var b = this.b;
			var n = b.length;
			var b2j = this.b2j = {};
			var populardict = {};
			for (var i = 0; i < b.length; i++) {
				var elt = b[i];
				if (b2j.hasOwnProperty(elt)) {
					var indices = b2j[elt];
					if (n >= 200 && indices.length * 100 > n) {
						populardict[elt] = 1;
						delete b2j[elt];
					} else {
						indices.push(i);
					}
				} else {
					b2j[elt] = [i];
				}
			}
	
			for (var elt in populardict) {
				if (populardict.hasOwnProperty(elt)) {
					delete b2j[elt];
				}
			}
			
			var isjunk = this.isjunk;
			var junkdict = {};
			if (isjunk) {
				for (var elt in populardict) {
					if (populardict.hasOwnProperty(elt) && isjunk(elt)) {
						junkdict[elt] = 1;
						delete populardict[elt];
					}
				}
				for (var elt in b2j) {
					if (b2j.hasOwnProperty(elt) && isjunk(elt)) {
						junkdict[elt] = 1;
						delete b2j[elt];
					}
				}
			}
	
			this.isbjunk = difflib.__isindict(junkdict);
			this.isbpopular = difflib.__isindict(populardict);
		}
		
		this.find_longest_match = function (alo, ahi, blo, bhi) {
			var a = this.a;
			var b = this.b;
			var b2j = this.b2j;
			var isbjunk = this.isbjunk;
			var besti = alo;
			var bestj = blo;
			var bestsize = 0;
			var j = null;
			var k;
	
			var j2len = {};
			var nothing = [];
			for (var i = alo; i < ahi; i++) {
				var newj2len = {};
				var jdict = difflib.__dictget(b2j, a[i], nothing);
				for (var jkey in jdict) {
					if (jdict.hasOwnProperty(jkey)) {
						j = jdict[jkey];
						if (j < blo) continue;
						if (j >= bhi) break;
						newj2len[j] = k = difflib.__dictget(j2len, j - 1, 0) + 1;
						if (k > bestsize) {
							besti = i - k + 1;
							bestj = j - k + 1;
							bestsize = k;
						}
					}
				}
				j2len = newj2len;
			}
	
			while (besti > alo && bestj > blo && !isbjunk(b[bestj - 1]) && a[besti - 1] == b[bestj - 1]) {
				besti--;
				bestj--;
				bestsize++;
			}
				
			while (besti + bestsize < ahi && bestj + bestsize < bhi &&
					!isbjunk(b[bestj + bestsize]) &&
					a[besti + bestsize] == b[bestj + bestsize]) {
				bestsize++;
			}
	
			while (besti > alo && bestj > blo && isbjunk(b[bestj - 1]) && a[besti - 1] == b[bestj - 1]) {
				besti--;
				bestj--;
				bestsize++;
			}
			
			while (besti + bestsize < ahi && bestj + bestsize < bhi && isbjunk(b[bestj + bestsize]) &&
					a[besti + bestsize] == b[bestj + bestsize]) {
				bestsize++;
			}
	
			return [besti, bestj, bestsize];
		}
		
		this.get_matching_blocks = function () {
			if (this.matching_blocks != null) return this.matching_blocks;
			var la = this.a.length;
			var lb = this.b.length;
	
			var queue = [[0, la, 0, lb]];
			var matching_blocks = [];
			var alo, ahi, blo, bhi, qi, i, j, k, x;
			while (queue.length) {
				qi = queue.pop();
				alo = qi[0];
				ahi = qi[1];
				blo = qi[2];
				bhi = qi[3];
				x = this.find_longest_match(alo, ahi, blo, bhi);
				i = x[0];
				j = x[1];
				k = x[2];
	
				if (k) {
					matching_blocks.push(x);
					if (alo < i && blo < j)
						queue.push([alo, i, blo, j]);
					if (i+k < ahi && j+k < bhi)
						queue.push([i + k, ahi, j + k, bhi]);
				}
			}
			
			matching_blocks.sort(difflib.__ntuplecomp);
	
			var i1 = 0, j1 = 0, k1 = 0, block = 0;
			var i2, j2, k2;
			var non_adjacent = [];
			for (var idx in matching_blocks) {
				if (matching_blocks.hasOwnProperty(idx)) {
					block = matching_blocks[idx];
					i2 = block[0];
					j2 = block[1];
					k2 = block[2];
					if (i1 + k1 == i2 && j1 + k1 == j2) {
						k1 += k2;
					} else {
						if (k1) non_adjacent.push([i1, j1, k1]);
						i1 = i2;
						j1 = j2;
						k1 = k2;
					}
				}
			}
			
			if (k1) non_adjacent.push([i1, j1, k1]);
	
			non_adjacent.push([la, lb, 0]);
			this.matching_blocks = non_adjacent;
			return this.matching_blocks;
		}
		
		this.get_opcodes = function () {
			if (this.opcodes != null) return this.opcodes;
			var i = 0;
			var j = 0;
			var answer = [];
			this.opcodes = answer;
			var block, ai, bj, size, tag;
			var blocks = this.get_matching_blocks();
			for (var idx in blocks) {
				if (blocks.hasOwnProperty(idx)) {
					block = blocks[idx];
					ai = block[0];
					bj = block[1];
					size = block[2];
					tag = '';
					if (i < ai && j < bj) {
						tag = 'replace';
					} else if (i < ai) {
						tag = 'delete';
					} else if (j < bj) {
						tag = 'insert';
					}
					if (tag) answer.push([tag, i, ai, j, bj]);
					i = ai + size;
					j = bj + size;
					
					if (size) answer.push(['equal', ai, i, bj, j]);
				}
			}
			
			return answer;
		}
		
		
		
		this.get_grouped_opcodes = function (n) {
			if (!n) n = 3;
			var codes = this.get_opcodes();
			if (!codes) codes = [["equal", 0, 1, 0, 1]];
			var code, tag, i1, i2, j1, j2;
			if (codes[0][0] == 'equal') {
				code = codes[0];
				tag = code[0];
				i1 = code[1];
				i2 = code[2];
				j1 = code[3];
				j2 = code[4];
				codes[0] = [tag, Math.max(i1, i2 - n), i2, Math.max(j1, j2 - n), j2];
			}
			if (codes[codes.length - 1][0] == 'equal') {
				code = codes[codes.length - 1];
				tag = code[0];
				i1 = code[1];
				i2 = code[2];
				j1 = code[3];
				j2 = code[4];
				codes[codes.length - 1] = [tag, i1, Math.min(i2, i1 + n), j1, Math.min(j2, j1 + n)];
			}
	
			var nn = n + n;
			var group = [];
			var groups = [];
			for (var idx in codes) {
				if (codes.hasOwnProperty(idx)) {
					code = codes[idx];
					tag = code[0];
					i1 = code[1];
					i2 = code[2];
					j1 = code[3];
					j2 = code[4];
					if (tag == 'equal' && i2 - i1 > nn) {
						group.push([tag, i1, Math.min(i2, i1 + n), j1, Math.min(j2, j1 + n)]);
						groups.push(group);
						group = [];
						i1 = Math.max(i1, i2-n);
						j1 = Math.max(j1, j2-n);
					}
					
					group.push([tag, i1, i2, j1, j2]);
				}
			}
			
			if (group && !(group.length == 1 && group[0][0] == 'equal')) groups.push(group)
			
			return groups;
		}
		
		this.ratio = function () {
			matches = difflib.__reduce(
							function (sum, triple) { return sum + triple[triple.length - 1]; },
							this.get_matching_blocks(), 0);
			return difflib.__calculate_ratio(matches, this.a.length + this.b.length);
		}
		
		this.quick_ratio = function () {
			var fullbcount, elt;
			if (this.fullbcount == null) {
				this.fullbcount = fullbcount = {};
				for (var i = 0; i < this.b.length; i++) {
					elt = this.b[i];
					fullbcount[elt] = difflib.__dictget(fullbcount, elt, 0) + 1;
				}
			}
			fullbcount = this.fullbcount;
	
			var avail = {};
			var availhas = difflib.__isindict(avail);
			var matches = numb = 0;
			for (var i = 0; i < this.a.length; i++) {
				elt = this.a[i];
				if (availhas(elt)) {
					numb = avail[elt];
				} else {
					numb = difflib.__dictget(fullbcount, elt, 0);
				}
				avail[elt] = numb - 1;
				if (numb > 0) matches++;
			}
			
			return difflib.__calculate_ratio(matches, this.a.length + this.b.length);
		}
		
		this.real_quick_ratio = function () {
			var la = this.a.length;
			var lb = this.b.length;
			return _calculate_ratio(Math.min(la, lb), la + lb);
		}
		
		this.isjunk = isjunk ? isjunk : difflib.defaultJunkFunction;
		this.a = this.b = null;
		this.set_seqs(a, b);
	}
};

function csimilar(s, t) {
	if (!s || !t) {
		return 0
	}
	var l = s.length > t.length ? s.length : t.length
	var n = s.length
	var m = t.length
	var d = []
	var min = function(a, b, c) {
		return a < b ? (a < c ? a : c) : (b < c ? b : c)
	}
	var i, j, si, tj, cost
	if (n === 0) return m
	if (m === 0) return n
	for (i = 0; i <= n; i++) {
		d[i] = []
		d[i][0] = i
	}
	for (j = 0; j <= m; j++) {
		d[0][j] = j
	}
	for (i = 1; i <= n; i++) {
		si = s.charAt(i - 1)
		for (j = 1; j <= m; j++) {
			tj = t.charAt(j - 1)
			if (si === tj) {
				cost = 0
			} else {
				cost = 1
			}
			d[i][j] = min(d[i - 1][j] + 1, d[i][j - 1] + 1, d[i - 1][j - 1] + cost)
		}
	}
	let res = (1 - d[n][m] / l)*100
	return res.toFixed(0)
}