<%@ page language="C#" autoeventwireup="true" inherits="student_pythonblockly, LearnSite" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">  
  <title>Python Blockly 积木编程</title>
  <meta charset="UTF-8">
	<link href="../code/blockpy/blockpy.css" rel="stylesheet" type="text/css" />
	<link href="../code/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
	<script src="../js/jquery-1.8.2.min.js" type="text/javascript"></script>
	<script src="../js/jquery-ui-1.8.24.custom.min.js" type="text/javascript"></script>
	<script src="../code/build/src/ace.js" type="text/javascript"></script>
	<script src="../code/build/src/ext-language_tools.js" type="text/javascript"></script>
	<script src="../code/skulpt.min.js?ver=20211202" type="text/javascript"></script>
	<script src="../code/skulpt-stdlib.js" type="text/javascript"></script>
    <script src="../code/blockpy/blockly_compressed.js"></script>
    <script src="../code/blockpy/blocks_compressed.js"></script>
    <script src="../code/blockpy/python_compressed.js"></script>
    <script src="../code/blockpy/msg/en.js"></script>
  <script src="../code/blockpy/storage.js"></script>
<script src="../code/html2canvas.min.js" type="text/javascript"></script>
</head>
<body>
<div>
<div class="banner">
	 <i class="fa fa-codepen" ></i> <a id="title" >Python Blockly 积木编程</a> &nbsp;  
</div>
<div id="main">
<div id="left">
<div id="blocklyDiv" style="height: 95vh; width: 100wh;"></div>
</div>
<div id="right">
	<div id="editor"></div>
</div>

<div id="done"><img src="../images/sucessed.png"></img></div>
<div id="content" >
    <div class="map"><img class="mapimg" src="<%=Midurl %>" alt=""/></div>
	<h2 ><%=Titles%></h2>  
	<%=Mcontents %>
	<br /><br />
</div>

</div>


<div id="result">
<div id="savemsg"></div>
<pre id="output" ></pre>
</div>
<div id="cv" ></div>
<audio id="audio" controls="controls"  hidden="true" ></audio>
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

<xml  id="toolbox" style="display: none">
  <category name="编程" colour="#5b80a5">
	<block type="py_start"></block> 
	<block type="py_input"></block>
	<block type="py_print"></block>
	<block type="py_assign"></block> 
	<block type="py_text"></block> 
    <block type="math_number">
		<field name="NUM">0</field>
    </block>
	<block type="py_data"></block> 
	<block type="py_range"></block> 
	<block type="py_for"></block>
    <block type="controls_if"></block>    
    <block type="py_while"></block> 
    <block type="py_break"></block> 
  </category> 
  <category name="绘图" colour="#5ba55b">
    <block type="py_import"></block>  
    <block type="turtle_move"></block> 
    <block type="turtle_turn"></block>	
    <block type="turtle_circle"></block> 
    <block type="turtle_pensize"></block> 
    <block type="turtle_pen"></block> 
    <block type="turtle_fill"></block> 
    <block type="turtle_color"></block>
    <block type="turtle_goto"></block>
    <block type="turtle_fun"></block>
    <block type="turtle_write"></block> 
    <block type="turtle_sleep"></block> 
  </category> 
</xml>


<script>

    var snum = "<%=Snum %>";
    var cvbg = "<%=mback %>";
    var cf = "<%=codefile %>";
    var id = "<%=Id %>";
    var fpage = "<%=Fpage %>";
    var pass = 0;


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
        
    var codearg = decodeURIComponent(window.atob(argcodestr)); //自动批改代码 
	
    var options = {
        toolbox: document.getElementById('toolbox'),
        media: '../code/blockpy/media/',
        trashcan: true,
        grid: { spacing: 20, length: 4, colour: '#ddd', snap: true },
        zoom: {
            controls: true, startScale: 1.0, maxScale: 3, minScale: 1, scaleSpeed: 1.2
        }
    };

    this.Workspace = Blockly.inject('blocklyDiv', options);
    //this.workspace.toolbox.flyout.autoClose = false;

    function myUpdateFunction(event) {
        var codestr = Blockly.Python.workspaceToCode(this.Workspace);    // 将工作区代码块生成代码
		codestr=codestr.substring(0,codestr.length-1);
        editor.session.setValue(codestr);
        //console.log(codestr);    // 控制台显示生成的代码
    }
    this.Workspace.addChangeListener(myUpdateFunction);   // 监听工作区改变事件	

    $(document).ready(function () {
        BlocklyStorage.restoreBlocks(this.Workspace); //从本地存储中恢复
    });


	var Cover="";
    function savecode() {
        var res=document.querySelector("#result");
		var obj=$("#cv").find("canvas")[1];		
		var opts = {
	      backgroundColor: "transparent", 
	    };	
		var draw=false;
        var pass = 0;
		
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
		
		var txt=BlocklyStorage.backupBlocks_(this.Workspace);
		//console.log("代码:\n",txt);
		
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
				newcolors=colorcomp(newcolors);
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

			var outcv=x.width+"x"+x.height+"x"+milr;
			
			var simiop=0;
			var simicd=0;

			if(draw){					
				console.log("绘图检测正确：\n长x宽x斜x斜x点x点x颜");
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
				var prog = editor.getValue();
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
		   

			var urls = 'uploadblockpy.ashx?id=' + id;
			var formData = new FormData();
			
			txt=encodeURIComponent(txt);//必须编码，否则在linux下数据上传异常导致可能会无法生成缩略图

			formData.append('xml', txt);
			formData.append('cover', Cover);
			formData.append('pass', pass);

			$.ajax({
				url: urls,
				type: 'POST',
				cache: false,
				data: formData,
				processData: false,
				contentType: false
			}).done(function (res) {
				//alert("保存成功！");					
						   
				if(argcodestr!=""&& argcodestr!=null){			
					if(pass>-1){  //如果运行正常，标志为0那么
						$('#done').hide();
						if(draw){
							if (pass==2){						
								savemsg.innerHTML= "恭喜！绘图正确，保存成功！";
								 $('#done').show();
							}					
							else{
								savemsg.innerHTML= "绘图有误，请继续努力！";
							}
						}
						else{
							var mysmg=" 结果相似度"+simiop+"% 代码相似度"+simicd+"%";
							if(pass==1){						
								savemsg.innerHTML= "恭喜！代码正确, 保存成功！"+mysmg;
								 $('#done').show();
							}					
							else{
								savemsg.innerHTML= "代码有误, 请继续努力！"+mysmg;
							}							
						}
						alert(savemsg.innerHTML);	
					}											
					
				}
				else{
					savemsg.innerHTML= "程序运行正常！已保存！";						
				}	
                var msg=savemsg.innerHTML;
                alert(msg);				
				
			}).fail(function (res) {
				alert("保存失败！");
				console.log(res);
			});
	
		}			

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

</script>


	<script type="text/javascript" >

	    var editor = ace.edit("editor", {
	        theme: "ace/theme/chrome",
	        mode: "ace/mode/python"
	    });
	    editor.setFontSize(24);
	    //editor.focus();
	    editor.setReadOnly(true);
	    //editor.blur();

	    var codefile = ""; //定义字典

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


	    function returnurl() {
	        if (confirm('确定要返回吗？记得先保存。') == true) {
	            sessionStorage.clear(); //返回后清除，防止污染
	            window.location.href = fpage;
	        }
	    }

	    $("#result").hide();
	    $("#left").click(function () {
	        $("#result").slideUp();
	    });
	    $("#title").click(function () {
	        $("#result").slideToggle();
	    });

	    function helper() {
	        $("#content").slideToggle();
	    }

	    function randomsort(a, b) {
	        return Math.random() > .5 ? -1 : 1; //通过随机产生0到1的数，然后判断是否大于0.5从而影响排序，产生随机性的效果。
	    }


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
	        return new Promise(function (resolve, reject) {
	            var myinput = document.createElement("input");
	            myinput.setAttribute("type", "text");
	            myinput.setAttribute("class", "input");
	            mypre.appendChild(myinput);
	            myinput.focus();
	            result.onclick = function () {
	                myinput.focus();
	            }

	            myinput.onkeypress = function () {
	                if (event.keyCode == 13) {
	                    var argv = myinput.value;
	                    console.log(argv);
	                    mypre.removeChild(myinput);
	                    mypre.innerHTML = mypre.innerHTML + argv + " \n";
	                    resolve(argv);
	                }
	            }
	        })
	    }


	    function runit() {
	        $('#result').show();
	        var prog = editor.getValue();
	        if (prog != null && prog != "") {
	            mypre.innerHTML = '';
	            output.innerHTML = '';
	            savemsg.innerHTML = '';
	            Sk.pre = "output";
	            Sk.configure({ output: outf, read: builtinRead, execLimit: 600000, __future__: Sk.python3, inputfun: myfun });

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

	            },
		function (err) {
		    pass = -1; //如果异常，标志为-1
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

	    function voiceplay() {
	        var audio = document.getElementById("audio");
	        audio.src = '../code/adsorb.ogg';
	        audio.play();
	    }
		

	</script>

	<script type="text/javascript" >

	    function sizechecknew(a, b) {
	        var arra = a.split("x");
	        var arrb = b.split("x");
	        if (arra.length == 7 && arrb.length == 7) {
	            var a1 = parseInt(arra[0]);
	            var a2 = parseInt(arra[1]);
	            var a3 = parseInt(arra[2]);
	            var a4 = parseInt(arra[3]);
	            var a5 = parseInt(arra[4]);
	            var a6 = parseInt(arra[5]);
	            var a7 = parseInt(arra[6]);
	            var b1 = parseInt(arrb[0]);
	            var b2 = parseInt(arrb[1]);
	            var b3 = parseInt(arrb[2]);
	            var b4 = parseInt(arrb[3]);
	            var b5 = parseInt(arrb[4]);
	            var b6 = parseInt(arrb[5]);
	            var b7 = parseInt(arrb[6]);

	            if (!isNaN(a1) && !isNaN(a2) && !isNaN(b1) && !isNaN(b2)) {
	                var sp = 10;
	                var ct = 3;
	                var cd = 10;
	                var cr = 5;

	                var ab1 = Math.abs(a1 - b1);
	                var ab2 = Math.abs(a2 - b2);
	                var ab3 = Math.abs(a3 - b3);
	                var ab4 = Math.abs(a4 - b4);
	                var ab5 = Math.abs(a5 - b5);
	                var ab6 = Math.abs(a6 - b6);
	                var ab7 = Math.abs(a7 - b7);
	                //长x宽x斜x斜x点x点x颜
	                var p1 = mistake(a1, 1);
	                var p2 = mistake(a2, 1);
	                var p3 = mistake(a3, 2);
	                var p4 = mistake(a4, 2);
	                var p5 = mistake(a5, 3);
	                var p6 = mistake(a6, 3);
	                var p7 = mistake(a7, 5);

	                console.log("实际尺寸 ", a1, a2, a3, a4, a5, a6, a7);
	                console.log("实际误差 ", ab1, ab2, ab3, ab4, ab5, ab6, ab7);
	                console.log("允许误差 ", p1, p2, p3, p4, p5, p6, p7);
	                if ((ab1 <= p1) && (ab2 <= p2) && (ab3 <= p3) && (ab4 <= p4) && (ab5 <= p5) && (ab6 <= p6) && (ab7 <= p7)) {
	                    console.log("自动批改通过！");
	                    return true;
	                }
	            }
	        }

	        return false;
	    }

	    function mistake(a, n) {
	        var p = a * n / 10;
	        if (a < 10) {
	            p = a * n % 10;
	        }
	        return Math.round(p);
	    }

	    function SimiCodein(prog) {

	        s = new difflib.SequenceMatcher(prog, codearg);
	        var ro = s.ratio() * 100;
	        var sc = ro.toFixed(0);
	        console.log("检测代码相似度：", sc);
	        return sc;
	    }


	    function SimiOutput() {
	        var nowpre = output.innerText.trim();
	        var ism = false;
	        var res = 0;
	        if (nowpre != "") {
	            for (var i = 0; i < 3; i++) {
	                var setpre = argout[i].trim();
	                sim = Simiout(nowpre, setpre);
	                console.log("检测结果相似度：", sim);
	                console.log(nowpre, "长度", nowpre.length);
	                console.log(setpre, "长度", setpre.length);
	                if (sim > res) {
	                    res = sim;
	                }
	                if (sim > 70) {
	                    ism = true;
	                    break;
	                }
	            }
	        }
	        return res;
	    }

	    function Simiout(a, b) {

	        s = new difflib.SequenceMatcher(a, b);
	        var ro = s.ratio() * 100;
	        return ro.toFixed(0);
	    }

	    function toHex(r, g, b) {
	        return ("00000" + (r << 16 | g << 8 | b).toString(16)).slice(-6);
	    }

	    function colorcomp(colors) {
	        var n = colors.length;

	        for (i = 0; i < n; i++) {
	            for (j = i + 1; j < n; j++) {
	                var fcolor = colors[i];
	                var ncolor = colors[j];

	                //console.log(ncolor);
	                var fr = fcolor.substring(0, 2)
	                var fg = fcolor.substring(2, 4)
	                var fb = fcolor.substring(4, 6)
	                //console.log("rgb:",fr,fg,fb);
	                var nr = ncolor.substring(0, 2)
	                var ng = ncolor.substring(2, 4)
	                var nb = ncolor.substring(4, 6)
	                //console.log("rgb:",nr,ng,nb);

	                var absR = hex2int(fr) - hex2int(nr);
	                var absG = hex2int(fg) - hex2int(ng);
	                var absB = hex2int(fb) - hex2int(nb);
	                //console.log("abs:",absR,absG,absB);
	                var dis = Math.sqrt(absR * absR + absG * absG + absB * absB);
	                //console.log(fcolor,"-",j,"-",ncolor," dis:",dis);
	                if (dis < 72) {
	                    colors.splice(j, 1);
	                    n--;
	                    j--;
	                }

	            }
	        }
	        console.log("颜色：", colors);
	        return colors;
	    }

	    function hex2int(hex) {
	        var len = hex.length, a = new Array(len), code;
	        for (var i = 0; i < len; i++) {
	            code = hex.charCodeAt(i);
	            if (48 <= code && code < 58) {
	                code -= 48;
	            } else {
	                code = (code & 0xdf) - 65 + 10;
	            }
	            a[i] = code;
	        }

	        return a.reduce(function (acc, c) {
	            acc = 16 * acc + c;
	            return acc;
	        }, 0);
	    }


	    /* Author: Chas Emerick <cemerick@snowtide.com> */
	    var __whitespace = { " ": true, "\t": true, "\n": true, "\f": true, "\r": true };

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
	                        if (i + k < ahi && j + k < bhi)
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
	                            i1 = Math.max(i1, i2 - n);
	                            j1 = Math.max(j1, j2 - n);
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

	
	</script>
	
    </div>
</body>
</html>
