<%@ page title="" language="C#" masterpagefile="~/teacher/Teach.master" autoeventwireup="true" inherits="Teacher_judgeedit, LearnSite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<link href="../code/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
<style type="text/css">
#editor{
  position: absolute;
  left: 0px;
  height: 96vh;
  right: 0px;
  background-color: #fef8e4;
  min-width:600px;
  
}
#divmain{
  padding: 0px;
}
#divright{
  float: left;
  color: #333;
  top:  0px; 
  padding:10px;
  width: 602px;
  overflow-x: hidden; 
  text-align:left;
}

#divleft{
    position: absolute;
    top:  0px;
    left: 720px;
    bottom: 0px;
    right: 0px;
}
#savemsg
{
  position: absolute;      
  bottom: 10px;
  right: 10px;
  margin: 10px;
  color:#ed7d31; 
  font-size:22px;  
}

#title
{
  text-align:center;  
}
#output{
  padding: 10px;  
  font-size: 24px;
  color: #333;
  text-align:left;
}

.input{
	border:1px solid #ccc;
	width:400px;
}

#cv{
    width:600px;
    height:600px;
}

</style>
<div id="divmain">

<div id="divleft">
	<div id="editor"></div>
</div>


<div id="divright">
<div id="title">
<h2 style="text-align:center;"> <asp:Label ID="LabelTitle" runat="server" Text="标题"></asp:Label></h2>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<input id="BtnRun" type="button" value="运行" style=" width:100px;" onclick="passcheck()" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<input id="BtnSave" type="button" value="保存" style=" width:100px;" onclick="save()" title="有代码保存会实现自动批改，无代码保存则删除自动批改" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<input id="BtnReturn" type="button" value="返回" style=" width:100px;" onclick="back()" />
</div>
<p>
输入：<input id="input1" type="text" class="input"  value=""/>
<pre id="output1" class="output" ></pre>
</p>

<p>
输入：<input id="input2" type="text"  class="input"  value=""/>
<pre id="output2" class="output" ></pre>
</p>

<p>
输入：<input id="input3" type="text"  class="input"  value=""/>
<pre id="output3" class="output" ></pre>
</p>
<p><strong>特别注意</strong>：<br>如果是while循环，记得只放第一个跳出循环的条件输入参数，将其它两个参数要留空，否则检验无效。检验只判断跳出循环的条件情况。</p>


</div>

<div id="savemsg"></div>
<div id="cv" ></div>

</div>

  <script src="../code/build/src/ace.js" type="text/javascript"></script>
  <script src="../code/build/src/ext-language_tools.js" type="text/javascript"></script>
  <script src="../code/build/src/ext-beautify.js" type="text/javascript"></script>
  <script type="text/javascript">
      var editor = ace.edit("editor");
      editor.setOptions({
          wrap: true,
          enableLiveAutocompletion: true,
          enableSnippets: true
      });
      editor.setTheme("ace/theme/textmate");
      editor.getSession().setMode("ace/mode/python");
      editor.setFontSize(24);
      editor.setReadOnly(false)
      editor.getSession().setTabSize(4);

  </script>

<script src="../code/skulpt.min.js" type="text/javascript"></script>
<script src="../code/skulpt-stdlib.js" type="text/javascript"></script>
<script src="../code/html2canvas.min.js" type="text/javascript"></script>
<script src="../code/jquery.min.js" type="text/javascript"></script>
<script type="text/javascript">
    var id = "<%=Id %>";
    var cid="<%=Cid %>";
    var mid="<%=Mid %>";
    var num = 1;
    var key = "ls2022"+ id;    
    document.getElementById("BtnSave").setAttribute("disabled", true);
    var in1 = document.getElementById("input1");
    var in2 = document.getElementById("input2");
    var in3 = document.getElementById("input3");
    var out1 = document.getElementById("output1");
    var out2 = document.getElementById("output2");
    var out3 = document.getElementById("output3");

    function outf(text) {
        var outid = "output" + num;
        var mypre = document.getElementById(outid);
        mypre.innerHTML = mypre.innerHTML + text;
    }
    function builtinRead(x) {
        if (Sk.builtinFiles === undefined || Sk.builtinFiles["files"][x] === undefined)
            throw "File not found: '" + x + "'";
        return Sk.builtinFiles["files"][x];
    }

function myfun() {
        return new Promise(function (resolve, reject) {
            var inid = "input" + num;
            var myinput = document.getElementById(inid);
            args = myinput.value;
            console.log(args);
            resolve(args);
            var outid = "output" + num;
            var mypre = document.getElementById(outid);
            temp = mypre.innerText;
            temp = temp + args;
            mypre.innerHTML = temp + "\n";
        })
    }

    function sleep(ms) {
        return new Promise(function(resolve, reject) {
		    
            setTimeout(resolve,ms);
        })
    }

    async function passcheck(){
		savemsg.innerHTML= "";
		var arg1=in1.value;
		var arg2=in2.value;
		var arg3=in3.value;
        num=1;
	    runit();		
		
		if(arg2!=""){		
			await sleep(200);
			num = num + 1;
			runit();
			if(arg3!=""){	
				await sleep(200);
				num = num + 1;
				await sleep(200);
				runit();
			}
		}
		console.log("输入数据：",arg1,arg2,arg3)
		document.getElementById("BtnSave").removeAttribute("disabled");
    }

    function clear() {
        var outid = "output" + num;
        var mypre = document.getElementById(outid);
        mypre.innerHTML ="";
    }
    function runit() {
        var prog = editor.getValue();
        sessionStorage.setItem(key, prog);
        clear();
        Sk.pre = "output";
        Sk.configure({ output: outf, read: builtinRead, execLimit: 3000000, __future__: Sk.python3, inputfun: myfun });

        (Sk.TurtleGraphics || (Sk.TurtleGraphics = {})).target = 'cv';
        var myPromise = Sk.misceval.asyncToPromise(function () {
            return Sk.importMainWithBody("<stdin>", false, prog, true);
        });

        myPromise.then(function (mod) {
            console.log('succeed!');
        },
    function (err) {
        var msg = err.toString();
		savemsg.innerHTML= msg;
        console.log(msg);
    });
}

function codeshow() {
    var value = sessionStorage.getItem(key);   
	if(value!=null)  editor.setValue(value,1);
}

function saveshow(){
    var scode="<%=code %>";
    var sarg1="<%=arg1 %>";
    var sarg2="<%=arg2 %>";
    var sarg3="<%=arg3 %>";
    if(scode!=""){
        scode=decodeURIComponent(window.atob(scode));
        editor.setValue(scode,1);
        in1.value=sarg1;
        in2.value=sarg2;
        in3.value=sarg3;          
    }
}

codeshow();
saveshow();

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

function save(){
    var codevalue= editor.getValue().trim();
    var Cover=""; 
    var fullCover="";
	var resimg="../images/none.gif ";
	if(codevalue.length > 0 ){	
		var wi=0;
		var he=0;
		var mi_mxl=0;
		var mi_mxr=0;
		var res=document.querySelector("#result");
		var obj=$("#cv").find("canvas")[1];
		console.log('裁剪开始');		
		
		var opts = {
	      backgroundColor: "transparent",  
	    };
		console.log(obj);
		if (obj!=null){			
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
							var pos = (i + c.width * j) * 4
							if (imgData[pos] == 255 || imgData[pos + 1] == 255 || imgData[pos + 2] == 255 || imgData[pos + 3] == 255) {
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
					var alphas=new Array();
					
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
									
									alphas.push(np3);
								}
							}
						}					
					}	

					let setcolors=new Set(colors);
					let newcolors=Array.from(setcolors);

					console.log("颜色：",newcolors);
                    newcolors=colorselect(newcolors);

					let colorsize=newcolors.length;
					
					//console.log("透明度：",alphas);
					
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
										
					var scale=x.width/x.height;//宽高比例
					scale=scale.toFixed(2);
					
					resimg=x.width+"x"+scale+"x"+milr;					

					console.log("自动裁剪");

                    fullCover=blob(dataURL);
					dataURL = x.toDataURL();
                    
			        Cover=blob(dataURL);
					
					console.log("绘图尺寸：\n长x比例x斜x斜x点x点x颜\n",resimg);					
					upload(codevalue,resimg,Cover,fullCover);
					
				}			

			});	
		}
		else{
			console.log("没有绘图");
			upload(codevalue,resimg,Cover,fullCover);
		}

	}
	else{
		console.log("没有代码");
		upload(codevalue,resimg,Cover,fullCover);
	}
}

$("#cv").dblclick(function (){
    var codevalue= editor.getValue().trim(); 
	if(codevalue!=null&&codevalue!=""){	
        var Cover="";
		var wi=0;
		var he=0;
		var mi_mxl=0;
		var mi_mxr=0;
		var resimg="../images/none.gif ";
		var res=document.querySelector("#result");
		var obj=$("#cv").find("canvas")[1];
		console.log('裁剪开始',obj);		
		
		var opts = {
	      backgroundColor: "transparent",  
	    };
		console.log(obj);
		if (obj!=null){			
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
							var pos = (i + c.width * j) * 4
							if (imgData[pos] == 255 || imgData[pos + 1] == 255 || imgData[pos + 2] == 255 || imgData[pos + 3] == 255) {
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
					var alphas=new Array();
					
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
									
									alphas.push(np3);
								}
							}
						}					
					}	

					let setcolors=new Set(colors);
					let newcolors=Array.from(setcolors);

					console.log("颜色：",newcolors);
                    newcolors=colorselect(newcolors);

					let colorsize=newcolors.length;
					
					//console.log("透明度：",alphas);
					
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
					
					var scale=x.width/x.height;//宽高比例
					scale=scale.toFixed(2);
					
					resimg=x.width+"x"+scale+"x"+milr;				

					console.log("自动裁剪");
					dataURL = x.toDataURL();
                    
			        Cover=blob(dataURL);
					const a= document.createElement("a");
					a.href = URL.createObjectURL(Cover);
					a.download = "turtle.png" ;// 这里填保存成的文件名
					a.click();
					URL.revokeObjectURL(a.href);
			　　　		a.remove();
			
			
					console.log("绘图尺寸：\n长x比例x斜x斜x点x点x颜\n",resimg);
								
				}			

			});	
		}
		else{
			console.log("没有绘图");
		}

	}
});

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

function upload(codevalue,resimg,Cover,fullCover){
	var arg1=in1.value;
	var arg2=in2.value;
	var arg3=in3.value;		

	var res1=out1.innerText.trim();
	var res2=out2.innerText.trim();
	var res3=out3.innerText.trim();

	var encode=window.btoa(encodeURIComponent(codevalue));
	console.log("代码长度：",encode.length);

	var urls = 'judgesave.ashx?id=' + id;
	var formData = new FormData();
	formData.append('fullcover', fullCover);
	formData.append('cover', Cover);
	formData.append('code', encode);

	formData.append('arg1', arg1);
	formData.append('arg2', arg2);
	formData.append('arg3', arg3);
	formData.append('res1', res1);
	formData.append('res2', res2);
	formData.append('res3', res3);
	formData.append('resimg', resimg);
	formData.append('id', id);
	formData.append('cid', cid);
	formData.append('mid', mid);

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
			savemsg.innerHTML= "保存成功！";
		}
		if(res==0) 	savemsg.innerHTML= "保存失败！";
		if(res<0) 	savemsg.innerHTML= "请先登录！";
	}).fail(function (res) {
		savemsg.innerHTML= "保存异常！";
		console.log(res)
	}); 
}

function back(){
    window.location.href="<%=Fpage %>";
}

</script>

</asp:Content>