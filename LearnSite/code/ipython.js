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



var hjson = document.getElementById("hidenjson").value;
var jsonstr = hjson;

var hnid = document.getElementById("hidennid").value;  
var hcid = document.getElementById("hidencid").value; 
var hlid = document.getElementById("hidenlid").value;         
var docurl = document.URL;
var ipurl = docurl.substring(0, docurl.lastIndexOf("/"));
var returnurl=ipurl+"/console.aspx?lid="+hlid;
if(returnurl.indexOf("Teacher")!=-1){
    returnurl="#";
}
$("#btnreturn").attr('href',returnurl);
$("#btnreturn").hide(); 

var obj = JSON.parse(jsonstr);
var len = obj.length;
console.log("题目数："+len);
var problems = new Array();
var answers = new Array();
var results = new Array();
var pids=new Array();
var scores=new Array();


for (key in obj) {    
	problems[key] = obj[key].Ptitle;
	answers[key] = obj[key].Pcode;
	results[key] = obj[key].Pouput; 	
	
    pids[key]=obj[key].Pid;
    scores[key]=obj[key].Pscore;
	
}





console.log("预设结果：");
for (r in results){
	console.log(results[r]);
}


function notice(n) {
    var audio = document.createElement("audio");
	switch(n){
		case 1:
			audio.src = '../code/right.ogg';
			break;
		case 2:
			audio.src = '../code/success.ogg'; 
			break;
		default:
			audio.src = '../code/bug.ogg';
			break;
	}
    audio.play();
}



/*
$("#player").click(function(){
	var evt = $.Event('keydown', {keyCode: 13, ctrlKey: true});
	$(document).trigger(evt); 
	alert("");
});
*/

(function () {
    var 
        importre = new RegExp("\\s*import"),
        
        defre = new RegExp("def.*|class.*"),
        
        comment = new RegExp("^#.*"),
        
        
        
        
        assignment = /^((\s*\(\s*(\s*((\s*((\s*[_a-zA-Z]\w*\s*)|(\s*\(\s*(\s*[_a-zA-Z]\w*\s*,)*\s*[_a-zA-Z]\w*\s*\)\s*))\s*)|(\s*\(\s*(\s*((\s*[_a-zA-Z]\w*\s*)|(\s*\(\s*(\s*[_a-zA-Z]\w*\s*,)*\s*[_a-zA-Z]\w*\s*\)\s*))\s*,)*\s*((\s*[_a-zA-Z]\w*\s*)|(\s*\(\s*(\s*[_a-zA-Z]\w*\s*,)*\s*[_a-zA-Z]\w*\s*\)\s*))\s*\)\s*))\s*,)*\s*((\s*((\s*[_a-zA-Z]\w*\s*)|(\s*\(\s*(\s*[_a-zA-Z]\w*\s*,)*\s*[_a-zA-Z]\w*\s*\)\s*))\s*)|(\s*\(\s*(\s*((\s*[_a-zA-Z]\w*\s*)|(\s*\(\s*(\s*[_a-zA-Z]\w*\s*,)*\s*[_a-zA-Z]\w*\s*\)\s*))\s*,)*\s*((\s*[_a-zA-Z]\w*\s*)|(\s*\(\s*(\s*[_a-zA-Z]\w*\s*,)*\s*[_a-zA-Z]\w*\s*\)\s*))\s*\)\s*))\s*\)\s*)|(\s*\s*(\s*((\s*((\s*[_a-zA-Z]\w*\s*)|(\s*\(\s*(\s*[_a-zA-Z]\w*\s*,)*\s*[_a-zA-Z]\w*\s*\)\s*))\s*)|(\s*\(\s*(\s*((\s*[_a-zA-Z]\w*\s*)|(\s*\(\s*(\s*[_a-zA-Z]\w*\s*,)*\s*[_a-zA-Z]\w*\s*\)\s*))\s*,)*\s*((\s*[_a-zA-Z]\w*\s*)|(\s*\(\s*(\s*[_a-zA-Z]\w*\s*,)*\s*[_a-zA-Z]\w*\s*\)\s*))\s*\)\s*))\s*,)*\s*((\s*((\s*[_a-zA-Z]\w*\s*)|(\s*\(\s*(\s*[_a-zA-Z]\w*\s*,)*\s*[_a-zA-Z]\w*\s*\)\s*))\s*)|(\s*\(\s*(\s*((\s*[_a-zA-Z]\w*\s*)|(\s*\(\s*(\s*[_a-zA-Z]\w*\s*,)*\s*[_a-zA-Z]\w*\s*\)\s*))\s*,)*\s*((\s*[_a-zA-Z]\w*\s*)|(\s*\(\s*(\s*[_a-zA-Z]\w*\s*,)*\s*[_a-zA-Z]\w*\s*\)\s*))\s*\)\s*))\s*\s*))([+-/*%&\^\|]?|[/<>*]{2})=/;

    Sk.globals = {
        _ih: new Sk.builtin.list(),
        _oh: new Sk.builtin.dict(),
        _: Sk.builtin.str.$empty,
        __: Sk.builtin.str.$empty,
        ___: Sk.builtin.str.$empty,
        _i: Sk.builtin.str.$empty,
        _ii: Sk.builtin.str.$empty,
        _iii: Sk.builtin.str.$empty,
        __name__: new Sk.builtin.str("__main__"),
    };
    Sk.globals.In = Sk.globals._ih;
    Sk.globals.Out = Sk.globals._oh;

    var stopExecution = false;

    
    var info = "# Python 3.7             ( 运行程序请按 Ctrl+回车键 ) \n" ;

	var prognum=0;         
	var progpass=false;    
	var statpass="✔";
	var curcode="";
	var curoutput="";
    var passcount=0;
    var errorcount=0;
	var catcherror="";
	var dateBegin = new Date();
	var mycell;	
	var succession=0;
	var outContent="";
	
    ipythonExample = function (dom) {
        if (ace === undefined) {
            throw Error("No ace");
        }
        this.editor = dom;



        
        var keyboardInterrupt = (e) => {
            if (e.ctrlKey && e.key === "c") {
                
                stopExecution = true;
            }
        };
        this.editor.addEventListener("keydown", keyboardInterrupt);

        
        var infoElement = document.createElement("DIV");
        infoElement.innerText = info;
        infoElement.style.margin = "5px";
        this.editor.appendChild(infoElement);

		this.initprogram();
        
        this.inputs = [];
        this.idx = 0;
        this.newCells();
        this.outf = this.outf.bind(this);
        this.lineHeight = this.inCell.renderer.lineHeight || 24;
        this.pad = 15;		
		
		
        
        Sk.configure({
            output: this.outf,
            __future__: Sk.python3,
            yieldLimit: 1000,
            execLimit: 300000,
            retainGlobals: true,
            inputfunTakesPrompt: false,
            inputfun: (args) => {
                let res;
                this.outf(args + "");
				res = prompt(args)				
				if(res=="")
					res=1
				if(res==null)
					res=2				
                this.outf((res));	
				
                return res;
            },
        });
    };
	

    ipythonExample.prototype.savepass=function(){
		var pid=pids[prognum];
		var score=scores[prognum];
		if(ipurl.indexOf("Teacher")!=-1){
			console.log("Teacher");
		}
		else{
			var saveurl = ipurl + "/SaveIdle.ashx";
			var formData = new FormData();
			formData.append('pid', pid);
			formData.append('score', score);
			formData.append('cid', hcid);
			formData.append('nid', hnid);
			formData.append('lid', hlid);
			formData.append('answer', curcode);

			$.ajax({
				url: saveurl,
				type: 'POST',
				cache: false,
				data: formData,
				processData: false,
				contentType: false
			}).done(function (res) {
				
				console.log(res)
			});
		}
    }

	ipythonExample.prototype.problem=function(num){
		pnum=num+1;
		text="第"+pnum+"题："+problems[num];
		
		return text;
	}
	
	var setcode="s=0";
		
	ipythonExample.prototype.initprogram=function(){
		var key="prognum";
		prognum=0;
		msg=this.problem(prognum);
		var cname="divprog";
		this.addCell(msg,cname);	
	}
	
	ipythonExample.prototype.saveprognum=function(value) {
		var key="prognum";
		sessionStorage.setItem(key,value);
		var log=key+"="+value;
		
	}
	
	
	ipythonExample.prototype.compareassign=function(cur,ans){
		if(prognum>-1){
			var ans=answers[prognum];
			var cur=curcode;	     
			if(ans.indexOf("=")<0 && cur.indexOf("=")>0)
			{
				return false;
			}
			else{
				return true;
			}
		}
		else{
			return true;			
		}

	}
	
	ipythonExample.prototype.comma=function(str){
		
		let res = str.match(/,/g);
		let count = !res ? 0 : res.length;
		return count;
	}	
	
	
	ipythonExample.prototype.compareresult=function(one,two){
		one=one.replace(/\s+/g,"");
		two=two.replace(/\s+/g,"");		
		console.log("结果比较");	
		console.log(one);		
		console.log(two);
		return (one==two);
	}	
	
	ipythonExample.prototype.compare=function(one,two){
		one=one.replace(/\s+/g,"");
		two=two.replace(/\s+/g,"");
		
		return (one==two);
	}
	
	ipythonExample.prototype.comparecomma=function(one,two){
		let commaone=this.comma(one);
		let commatwo=this.comma(two);
		
		
		return (commaone==commatwo);
	}
	
	
	ipythonExample.prototype.comparesimi=function(one,two){
		var simi=this.Similarity(one,two)*100;		
		return (simi>70);
	}
		
	
	ipythonExample.prototype.comparehighsimi=function(one,two){
		var simi=this.Similarity(one,two)*100;		
		return (simi>95);
	}
	
	
	ipythonExample.prototype.comparefirst=function(one,two){
		one=one.replace(/\s+/g,"");
		two=two.replace(/\s+/g,"");
		var onefirst="";
		var twofirst="";
		
		if(two.indexOf("=")>0){
			onefirst=one.split("=")[0];
			twofirst=two.split("=")[0];
		}
		else if(two.indexOf("[")>0){
			onefirst=one.split("[")[0];
			twofirst=two.split("[")[0];			
		}
		else if(two.indexOf("(")>0){
			onefirst=one.split("(")[0];
			twofirst=two.split("(")[0];			
		}
		
		console.log("输入变量名："+onefirst);
		console.log("答案变量名："+twofirst);		

		return (onefirst==twofirst);
		
	}
	
	
	ipythonExample.prototype.quotcontent=function(str){
		var pattern =/[\"|'](.*?)[\"|']/g;
        
        var tr= str.match(pattern);      
	
		return tr;
	}
	
	ipythonExample.prototype.comparecontent=function(cur,ans){
		console.log("引号内容匹配开始：");		
		var curArray=this.quotcontent(cur);
		var ansArray=this.quotcontent(ans);	
		
		
		
		if (curArray==null||ansArray==null) return true;
		
		var n=curArray.length;
		var m=ansArray.length;
		if(n>0 && m>0){
			for(c in curArray){
				if(curArray[c]!=ansArray[c]){
					console.log(curArray[c]+" 不等于 "+ansArray[c]);
					return false;				
				}
			}			
		}
		console.log("------匹配结束-----");				
		return true;
	}

	
	ipythonExample.prototype.comparespecial=function(one,two){
		if(one.indexOf("=")>0 && two.indexOf("=")>0)
		{
			one=one.replace(/\s+/g,"");
			two=two.replace(/\s+/g,"");
			
			var onefirst=one.split("=")[1];
			var twofirst=two.split("=")[1];
			var myspec=["+","-","*","/","//","%","**",":"];
			for(m in myspec){
				var ch=myspec[m]
				if(two.indexOf(ch)>0){
					
					if(one.indexOf(ch)<0){					
						console.log("运算符："+ch);
						return false;
					}					
				}				
			}
			return true;
		}
		else
		{
			return true;
		}
	}
	
	
	ipythonExample.prototype.comparetwo=function(one,two){
		if(one.indexOf("=")>0 && two.indexOf("=")>0)
		{
			one=one.replace(/\s+/g,"");
			two=two.replace(/\s+/g,"");
			
			var onefirst=one.split("=")[1];
			var twofirst=two.split("=")[1];

			onefirst=onefirst.charAt(0);
			twofirst=twofirst.charAt(0);
			oneasc=onefirst.charCodeAt();
			twoasc=twofirst.charCodeAt();
			
			
			
			if(oneasc==34 || oneasc==39)
			{
			if(twoasc==34 || twoasc==39)
				{
					return true;
				}
				else{
					return false;
				}
			}
			else{
				return (onefirst==twofirst);
			}
		}
		else
		{
			return true;
		}
	}
	
	
	ipythonExample.prototype.comparenum=function(one,two){
		one=one.replace(/\s+/g,"");
		two=two.replace(/\s+/g,"");
		
		var onenum=one.match(/\(([^)]*)\)/);
		var twonum=two.match(/\(([^)]*)\)/);
		if(onenum&&twonum)		{
			
			
			var reg = /^[A-Za-z]+$/;
			if(reg.test(onenum[1])&&reg.test(twonum[1])){
				console.log("参数为字母");
				return true;
			}
			else{	
				var reg = /^["|'](.*)["|']$/g;
				var  onm=onenum[1].replace(reg,"$1");
				var  tnm=onenum[1].replace(reg,"$1");				
				return (onm==tnm);
			}
		}
		else{
			return true;
		}
	}
	
		
	ipythonExample.prototype.comparecolon =function(one,two){
		one=one.replace(/\s+/g,"");
		two=two.replace(/\s+/g,"");
		if(two.indexOf(":")>0 )
		{
			if(one.indexOf(":")>0)
				return true;
			else
				return false;
		}
		else
		{
			if(one.indexOf(":")<0)
				return true;
			else
				return false;			
		}
	}
	
	ipythonExample.prototype.compareerror=function(){	
		if(catcherror!=""){
			catcherror="";
			return true;
		}
		else{
			return false;
		}
	}
	
	ipythonExample.prototype.compareall=function(cur,ans,res,out){
		cur=this.Clearquot(cur);
		ans=this.Clearquot(ans);
		var codecheck=this.compare(cur,ans);
		var codechecksimi=this.comparesimi(cur,ans);
		var codecheckhighsimi=this.comparehighsimi(cur,ans);
		var resultcheck=this.compareresult(res,out);
		
		var firstcheck=this.comparefirst(cur,ans);
		var twocheck=this.comparetwo(cur,ans);
		var numcheck=this.comparenum(cur,ans);
		var errorcheck=this.compareerror();
		var commacheck=this.comparecomma(cur,ans);
		var noneoutcheck=(res=="");
		var quotcontentcheck=this.comparecontent(cur,ans);
		var specialcheck=this.comparespecial(cur,ans);
		var coloncheck=this.comparecolon(cur,ans);
		
		
		console.log("代码相似"+codechecksimi);
		console.log("代码相等"+codecheck);
		console.log("结果相等"+resultcheck);
		console.log("无输出"+noneoutcheck);
		console.log("输入变量"+firstcheck);
		console.log("赋值内容首字母"+twocheck);
		console.log("输入参数"+numcheck);
		console.log("运算符检测"+specialcheck);
		console.log("冒号检测"+coloncheck);
		
		
		console.log("题目预设结果");
		console.log(res);
		console.log("当前输出结果：");
		console.log(out);
		
		
		
		if(errorcheck) return false;
		if(codecheck) return true;
		if(noneoutcheck){
			if(codecheckhighsimi&&firstcheck&&numcheck&&twocheck&&commacheck&&quotcontentcheck&&specialcheck&&coloncheck){
				return true;
				
			}
		}
		else{
			if(resultcheck)	return true; 
		}
					
		return false;
	}
	
	
	ipythonExample.prototype.checkprogram=function(){	
		if(prognum>-1){
			var cname ="";
			var ptitle=this.problem(prognum);
			var answercode=answers[prognum];
			var resultset=results[prognum];			
			var simi=this.Similarity(answercode,curcode)*100;
			var simimsg=simi.toFixed(2).toString()+"%";
			var simiint=simi.toFixed(0)
			console.log("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
			console.log(ptitle);
			console.log("代码相似度 "+simi);		
						
			if (this.compareall(curcode,answercode,resultset,curoutput))
			{
				succession=succession+1;
				progpass=true;	
				statpass="✔ 代码正确！";

				cname = "divpass";	
                notice(1);	
                this.savepass(); 
                passcount=passcount+1; 
				prognum=prognum+1; ;
				
			}
			else{
				succession=0;
				progpass=false;	
				statpass="✖ 代码有误，请重新输入！";
				cname = "divwrong";	
				this.saveprognum(prognum);				
				console.log("当前代码长度"+curcode.length);
				console.log("答案代码长度"+answercode.length);	
                errorcount=errorcount+1;
			}
			this.addCell(statpass,cname);
		}
	}
	
	ipythonExample.prototype.addprogram=function(){		
		this.checkprogram();		
		if(prognum<len){
			if(progpass){
				msg=this.problem(prognum);
				var cname="divprog";
				this.addCell(msg,cname);	
			}
				this.newCells();	
		}
		else{		
            var countmsg= "\n完成"+passcount+"题";	
			if(errorcount>0){
				countmsg= countmsg+",有"+errorcount+"题查过错误!";	
			}
			var dateEnd = new Date();
			var dateDiff = dateEnd.getTime() - dateBegin.getTime();
			var m = parseInt(dateDiff%3600000/60000,10);
			var s = dateDiff%60000/1000;
			var passtime=" 用时"+m + "分" + s + "秒";	
			if (m==0){
				passtime=" 用时" + s + "秒";	
			}
			
			msg="测评完毕，祝贺你顺利通过！"+passtime;
			var cname="divprog";
			this.addCell(msg,cname);
			notice(2);            
            $("#btnreturn").show();
		}
	}
	
	
	var oldwrongdiv=null;	
	
	ipythonExample.prototype.addCellMsg=function(msg,classname){
		const progElement = document.createElement("DIV");
		progElement.innerText = msg;
		progElement.className = classname;
		progElement.id="Msg"+prognum;
		this.editor.appendChild(progElement);	
		oldwrongdiv=progElement;
	}
	
	ipythonExample.prototype.addCell=function(msg,classname){
		const progElement = document.createElement("DIV");
		progElement.innerHTML = msg;  //innerText
		progElement.className = classname;
		this.editor.appendChild(progElement);
	}
	
    ipythonExample.prototype.newCells = function () {
        this.idx = this.inputs.length;
        this.inCell = this.newIn();
        this.printCell = this.newPrint();
        this.outCell = this.newOut();
        this.inCell.focus();
        this.inCell.container.scrollIntoView();
    };
	

    ipythonExample.prototype.execute = function () {
        stopExecution = false;
        const code = this.inCell.getValue();
		curcode=code.trimEnd();  
	
		const codeAsPyStr = Sk.ffi.remapToPy(code);
		Sk.globals["_i" + this.inputs.length] = codeAsPyStr; 
		Sk.globals.In.v.push(codeAsPyStr);
		this.inputs[this.inputs.length - 1] = code;

		let compile_code = code.trimEnd() || "None"; 
	
		const lines = compile_code.split("\n");
		let last_line = lines[lines.length - 1];
		if (
			!assignment.test(last_line) &&
			!defre.test(last_line) &&
			!importre.test(last_line) &&
			!comment.test(last_line) &&
			last_line.length > 0 &&
			last_line[0] !== " "
		) {
			lines[lines.length - 1] = "_" + this.inputs.length + " = " + last_line;
		}
		compile_code = lines.join("\n");
		
		this.inCell.setReadOnly(true);
		this.inCell.renderer.$cursorLayer.element.style.opacity = 0;

		
		const executionPromise = Sk.misceval.asyncToPromise(() => Sk.importMainWithBody("ipython", false, compile_code, true), {
			"*": () => {
				if (stopExecution) {
					throw "\nKeyboard interrupt";
				}
			},
		});
		executionPromise
			.then((res) => {
				const printContent = this.printCell.getValue();
				if (printContent.substring(printContent.length - 1) === "\n") {
					this.printCell.setValue(printContent.substring(0, printContent.length - 1), -1);				
				}
				this.printCell.container.style.height = this.printCell.session.getLength() * this.lineHeight + this.pad;
				this.printCell.resize();

				curoutput=printContent; 
				
									
				const last_input = Sk.globals["_" + this.inputs.length];
				if (Sk.builtin.checkNone(last_input) || last_input === undefined) {
					delete Sk.globals["_" + this.inputs.length]; 
				} else {
					this.outCell.setValue(Sk.ffi.remapToJs(Sk.misceval.objectRepr(last_input)), -1);
					if (last_input !== Sk.globals.Out) {
						Sk.abstr.objectSetItem(Sk.globals._oh, Sk.ffi.remapToPy(this.inputs.length), last_input);
						Sk.globals["___"] = Sk.globals["__"];
						Sk.globals["__"] = Sk.globals["_"];
						Sk.globals["_"] = last_input;
					}
				}
			})
			.catch((e) => {
				catcherror=e.toString();
				outContent = catcherror;
				this.outf(catcherror);
			})
			.finally(() => {
				Sk.globals["_iii"] = Sk.globals["_ii"];
				Sk.globals["_ii"] = Sk.globals["_i"];
				Sk.globals["_i"] = Sk.globals["_i" + this.inputs.length];
				if (this.printCell.isVisible || this.outCell.isVisible) {
					this.inCell.container.style.height = this.inCell.session.getLength() * 40;
					this.inCell.resize();					
				}				
				
				
				this.addprogram();
			});
					
		if(oldwrongdiv!=null){
			console.log(oldwrongdiv);
			document.getElementById("editor").removeChild(oldwrongdiv);
			oldwrongdiv=null;
			
		}
			

    };

    ipythonExample.prototype.outf = function (text) {
        const curr_val = this.printCell.getValue();
        this.printCell.setValue(curr_val + text, 1);
    };

    ipythonExample.prototype.newIn = function () {
        let cell = document.createElement("DIV");
        this.editor.appendChild(cell);
        cell = ace.edit(cell);
        this.inputs.push("");
        cell.setOptions({
            showLineNumbers: false,
            firstLineNumber: this.inputs.length,
            printMargin: false,
            highlightGutterLine: false,
            highlightActiveLine: false,
			enableLiveAutocompletion: true, 
			enableSnippets: true, 
            showFoldWidgets: false,
            theme: "ace/theme/gruvbox",
            mode: "ace/mode/python",
			fontSize:24,
        });
        cell.session.addGutterDecoration(0, "in-gutter");
        cell.container.style.height = 40;
        cell.resize();
        cell.on("blur", () => {
            cell.selection.setSelectionRange(new ace.Range(0, 0, 0, 0), false);
        });
        cell.on("change", () => {
            cell.container.style.height = cell.session.getLength() * this.lineHeight + this.pad;
            cell.resize();
            cell.container.scrollIntoView();
        });
        cell.renderer.onResize(() => {});

        cell.commands.addCommands([
            {
                name: "upHistory",
                bindKey: {
                    mac: "Up",
                    win: "Up",
                },
                exec: (e, t) => {
                    if (cell.getCursorPosition().row === 0) {
                        if (this.idx > 0) {
                            cell.setValue(this.inputs[--this.idx], 1);
                            cell.container.scrollIntoView();
                        }
                    } else {
                        cell.commands.commands.golineup.exec(e, t);
                    }
                },
            },
            {
                name: "downHistory",
                bindKey: {
                    mac: "Down",
                    win: "Down",
                },
                exec: (e, t) => {
                    if (cell.getCursorPosition().row === cell.session.getLength() - 1) {
                        if (this.idx < this.inputs.length - 1) {
                            cell.setValue(this.inputs[++this.idx], -1);
                            cell.container.scrollIntoView();
                        }
                    } else {
                        cell.commands.commands.golinedown.exec(e, t);
                    }
                },
            },
            {
                name: "execute",
                bindKey: {
                    win: "ctrl-enter",
                    mac: "command-enter",
                },
                exec: (e, t) => {
                    this.execute();
                },
            },
            {
                name: "exeauto",
                bindKey: {
                    win: "enter",
                },
                exec: (e, t) => {
					var codestr=cell.getValue();
					if(codestr!=""){
						var cursorPosition = cell.getCursorPosition();
						var alllen=cell.session.getLength();
						//console.log(cell.selection.getCursor());
						var nowline=cell.selection.getCursor()["row"]+1;
						var nowcol=cell.selection.getCursor()["column"];
						//console.log("总行数",alllen,"当前行",nowline,"当前列",nowcol);
										
						if(alllen!=nowline){						
							cell.session.insert(cursorPosition, "\n");
						}
						else{
							if(nowcol>0){
								cell.session.insert(cursorPosition, "\n");
							}
							else{
								this.execute();
							}
						}

					}
					else{
						console.log("无代码");
					}						
                },
            },
            {
                name: "keyboardInterrupt",
                bindKey: {
                    mac: "ctrl-C",
                    win: "ctrl-C",
                },
                exec: (e, t) => {
                    stopExecution = true;
                },
                readOnly: true,
            },
        ]);
		mycell=cell;
		
		
        return cell;
    };

    ipythonExample.prototype.newPrint = function () {
        let cell = document.createElement("DIV");
        this.editor.appendChild(cell);
        cell = ace.edit(cell);
        cell.setOptions({
            readOnly: true,
            printMargin: false,
            showGutter: false,
            showLineNumbers: false,
            highlightActiveLine: false,
            highlightGutterLine: false,
            highlightSelectedWord: false,
            theme: "ace/theme/gruvbox",
			fontSize:24,
        });
        cell.renderer.$cursorLayer.element.style.opacity = 0;
        cell.on("blur", () => {
            cell.selection.setSelectionRange(new ace.Range(0, 0, 0, 0), false);
            cell.renderer.$cursorLayer.element.style.opacity = 0;
        });
        cell.on("change", () => {
            if (!cell.isVisible) {
                cell.container.style.display = "block";
                cell.isVisible = true;
            }
            cell.container.style.height = cell.session.getLength() * this.lineHeight + this.pad;
            cell.resize();
            cell.focus();
            cell.renderer.$cursorLayer.element.style.opacity = "";
            cell.container.scrollIntoView();
        });
        cell.container.style.display = "none";
        cell.isVisible = false;
        cell.commands.addCommand({
            name: "keyboardInterrupt",
            bindKey: {
                mac: "ctrl-C",
                win: "ctrl-C",
            },
            exec: (e, t) => {
                stopExecution = true;
            },
            readOnly: true,
        });
        return cell;
    };

    ipythonExample.prototype.newOut = function () {
        let cell = document.createElement("DIV");
        this.editor.appendChild(cell);
        cell = ace.edit(cell);
        cell.setOptions({
            showLineNumbers: true,
            firstLineNumber: this.inputs.length,
            highlightActiveLine: false,
            highlightGutterLine: false,
            highlightSelectedWord: false,
            readOnly: true,
            printMargin: false,
            theme: "ace/theme/gruvbox",
			fontSize:24,
        });
        cell.session.addGutterDecoration(0, "out-gutter");
        cell.renderer.$cursorLayer.element.style.opacity = 0;
        cell.on("blur", () => {
            cell.selection.setSelectionRange(new ace.Range(0, 0, 0, 0), false);
        });
        cell.on("change", () => {
            if (!cell.isVisible) {
                cell.container.style.display = "block";
                this.printCell.container.style.height = this.printCell.session.getLength() * this.lineHeight;
                this.printCell.resize();
                cell.isVisible = true;
            }
            cell.container.style.height = cell.session.getLength() * this.lineHeight + this.pad;
            cell.resize();
            cell.container.scrollIntoView();
        });
        cell.container.style.display = "none";
        cell.isVisible = false;
        return cell;
    };
	
	ipythonExample.prototype.Clearquot = function(s) {
		r=s.replace(/\"/g,"'");	
		if (r!=s) {
			console.log("双引号替换为单引号");
		}
		return r;
	 }	
	
	ipythonExample.prototype.Similarity = function(s, t) {
		if(s===t) return 1;
		s=new difflib.SequenceMatcher(s,t);		
		return s.ratio();
	 }	 
	 
	 
	 
	 
})();
