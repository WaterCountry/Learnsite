<%@ page language="C#" autoeventwireup="true" inherits="sokoban_index, LearnSite" %>
<!doctype html>
<html>
	<head>
		<meta charset="UTF-8">
		<meta name="Keywords" content="关键词">
		<meta name="Description" content="描述">
		<title>推箱子</title>
		<style>
		*{
			margin:0px;
			padding:0px;
		}
		body{
			overflow:hidden;
			background-color:#333;
			color:#ddd;
		}
		.banner{
			
		}
		#msg{
			padding:10px;
			position:absolute;
			left:60px;
			max-width:608px;
			color:#999;
		}
		.game{
			top:50px;
			width:640px;
			margin:10px auto;
			text-align:center;
			position:absolute;
			left:50px;
		}
		.move{
			top:50px;
			width:220px;
			margin:10px auto;
			position:absolute;
			left:750px;
			background-color:#333;
			color:#999;
			padding:10px;
			height:590px;
			overflow-x:hidden;
		}
		.move:hover{
			box-shadow:0px 0px 4px #ddd;			
		}
						
		.rank{
			top:50px;
			width:220px;
			margin:10px auto;
			position:absolute;
			left:750px;
			background-color:#333;
			color:#999;
			padding:10px;
			height:590px;
			overflow-x:hidden;
			display:none;
			z-index:999;
		}
		.rank:hover{
			box-shadow:0px 0px 4px #FFB046;			
		}
		
		#btntool{
			position:absolute;
			left:740px;	
		}

		.button {
			background-color: #565b55;
			color: #999;
			padding: 4px 4px;
			margin:10px;
			text-align: center;
			text-decoration: none;
			border: 1px #ddd;
		}
		.button:hover {
			background-color: #768b75; /* Green */
			color: #fff;
			box-shadow:0px 0px 4px #ddd;
		}
		
		/*定义滚动条高宽及背景 高宽分别对应横竖滚动条的尺寸*/
		::-webkit-scrollbar
		{
			width: 10px;
			height: 10px;
			background-color: #585858;
		}
		 
		/*定义滚动条轨道 内阴影+圆角*/
		::-webkit-scrollbar-track
		{
			background-color: #333;
		}
		 
		/*定义滑块 内阴影+圆角*/
		::-webkit-scrollbar-thumb
		{
			-webkit-box-shadow: inset 0 0 6px rgba(0,0,0,.3);
			background-color: #222;
		}
		
		</style>
	</head>
	<body onkeydown="doKeyDown(event)"><!--身体-->
	<div id="main" >
		<div class="banner">
			<div id="msg"></div>
			<div id="btntool">
				<input id="btnmove" type="button" class="button" value="历史记录" onclick="automove()">
				<input type="button" class="button" value="重新开始" onclick="NextLevel(0)">
				<input type="button" class="button"  value="撤消一步" onclick="showBack()">		
			</div>
		</div>
		<div  class="game" >
			<canvas id="canvas" class="canvas" width="608" height="608"></canvas>
		</div>
		<div id="move" class="move"> 
		移动记录：
		</div>
        <div id="rank" class="move"> 
		英雄榜：
		</div>
		<audio id="myaudio" src="../sokoban/sound/music.ogg" controls="controls" autoplay loop="true" hidden="true" ></audio>		
		<audio id="audio" controls="controls"  hidden="true" ></audio>
	</div>	
	</body>
	<script src="../code/jquery.min.js" type="text/javascript"></script>
	<script src="js/mapdata100.js"></script>
	<script>
		var tip=document.getElementById("move");
		var can = document.getElementById("canvas");
		var msg = document.getElementById("msg");
		var cxt = can.getContext("2d");
		var w = 38,h = 38;
		var curMap;//当前的地图
		var curLevel;//当前等级的地图
		var curMan;//初始化小人
		var direction;//当前方向
		var step=0;//推箱步数
		var iCurlevel = 0;//关卡数
		var moveTimes = 0;//移动了多少次
		var moves="第1关 移动记录：";//移动记录
		var begin=false;
        var levelsave = "<%=gsave %>";
        var movenote = "<%=gnote %>";
        var gamestart= "<%=gstart %>";
        var timepass= "<%=gpass %>";
        var grank= "<%=grank %>";
		var maparray=[];
		var movearray=[];
		
		//预加载所有图片
		var oImgs = {
			"block" : "images/block.png",
			"wall" : "images/wall.png",
			"box" : "images/box.png",
			"ball" : "images/ball.png",
			"up" : "images/up.png",
			"down" : "images/down.png",
			"left" : "images/left.png",
			"right" : "images/right.png",
		}
		function imgPreload(srcs,callback){
			var count = 0,imgNum = 0,images = {};

			for(src in srcs){
				imgNum++;
			}
			for(src in srcs ){
				images[src] = new Image();
				images[src].onload = function(){
					//判断是否所有的图片都预加载完成
					if (++count >= imgNum)
					{
						callback(images);
					}
				}
				images[src].src = srcs[src];
			}
		}
		var block,wall,box,ball,up,down,left,right;
		imgPreload(oImgs,function(images){
			//console.log(images.block);
			block = images.block;
			wall = images.wall;
			box = images.box;
			ball = images.ball;
			up = images.up;
			down = images.down;
			left = images.left;
			right = images.right;
			init();
		});
		//初始化游戏
		function init(){  
            var gpass=parseInt(timepass);  
			$("#rank").html("英雄榜：<br><br>"+grank);	        
            if (gpass>40){
			    initLevel();//初始化对应等级的游戏
			    if(levelsave!="0"){
				    console.log(parseInt(levelsave));
				    NextLevel(parseInt(levelsave)+1);
				    console.log("读取数据库进度");
			    }
			    showMoveInfo();//初始化对应等级的游戏数据
				
				//console.log(curMap);
            }
            else{
                alert("上课35分钟之后才开放推箱游戏，谢谢！");
                window.close();
            }
		}
		
		//绘制地板
		function InitMap(){
			for (var i=0;i<18 ;i++ )
			{
				for (var j=0;j<18 ;j++ )
				{
					cxt.drawImage(block,w*j,h*i,w,h);
				}
			}
		}
		//小人位置坐标
		function Point(x,y){
			this.x = x;
			this.y = y;
		}
		var perPosition = new Point(5,5);//小人的初始标值
		//绘制每个游戏关卡地图
		function DrawMap(level){
			for (var i=0;i<level.length ;i++ )
			{
				for (var j=0;j<level[i].length ;j++ )
				{
					var pic = block;//初始图片
					switch (level[i][j])
					{
					case 1://绘制墙壁
						pic = wall;
						break;
					case 2://绘制陷井
						pic = ball;
						break;
					case 3://绘制箱子
						pic = box;
						break;
					case 4://绘制小人
						pic = curMan;//小人有四个方向 具体显示哪个图片需要和上下左右方位值关联
						//获取小人的坐标位置
						perPosition.x = i;
						perPosition.y = j;
						break;
					case 5://绘制箱子及陷井位置
						pic = box;
						break;
					}
					//每个图片不一样宽 需要在对应地板的中心绘制地图
					cxt.drawImage(pic,w*j-(pic.width-w)/2,h*i-(pic.height-h),pic.width,pic.height)
				}
			}
		}
		//初始化游戏等级
        //        var levelsave = "<%=gsave %>";        var movenote = "<%=gnote %>";

		function initLevel(){		
			// 加载游戏进度		
			curMap = copyArray(levels[iCurlevel]);//当前移动过的游戏地图
			curLevel = levels[iCurlevel];//当前等级的初始地图
			curMan = down;//初始化小人
			InitMap();//初始化地板
			DrawMap(curMap);//绘制出当前等级的地图
		}
			
		//下一关
		function NextLevel(i){
			var way="save"+iCurlevel;			
			localStorage.setItem(way, moves);
			//iCurlevel当前的地图关数
			iCurlevel = iCurlevel + i;
			if (iCurlevel<0)
			{
				iCurlevel = 0;
				return;
			}
			var len = levels.length;
			if (iCurlevel > len-1)
			{
				iCurlevel = len-1;
			}
			initLevel();//初始当前等级关卡
			moveTimes = 0;//游戏关卡移动步数清零
			showMoveInfo();//初始化当前关卡数据			
			step=0;//推箱步数
			movearray=[];
			showTable();			
							
			maparray.push(JSON.stringify(curMap));//当前地图进栈
			
			var tempmap="tempmap"+iCurlevel;
			var tempstr=JSON.stringify(maparray);
			sessionStorage.setItem(tempmap, tempstr);
		}
		
		var Backset=false;
		//显示记录面板		
		function showTable(){
			Backset=false;
			var process=iCurlevel+1;
			var msg="第"+process+"关 移动记录：";
			for(var i=0;i< movearray.length;i++){
				var stepnow=i+1;
				msg=msg+"</br>"+stepnow+"："+movearray[i]
			}			
			tip.innerHTML=msg;
			tip.scrollTop = tip.scrollHeight;
			//console.log(movearray);
		}
		
		function notice(n) {
			var audio = document.getElementById("audio");
			switch(n){
				case 'tui':
					audio.src = '../sokoban/sound/tui.ogg';
					break;
				case 'pop':
					audio.src = '../sokoban/sound/pop.ogg';
					break;
				case 'pass':
					audio.src = '../sokoban/sound/pass.ogg';
					break;
				case 'zou':
					audio.src = '../sokoban/sound/zou.ogg'; 
					break;
				case 'ok':
					audio.src = '../sokoban/sound/ok.ogg'; 
					break;
				case 'cancel':
					audio.src = '../sokoban/sound/cancel.ogg'; 
					break;
				default:
					break;
			}
			var playPromise = audio.play();

            if (playPromise) {
                playPromise.then(() => {
                    // 音频加载成功
                    // 音频的播放需要耗时
                    setTimeout(() => {
                        // 后续操作
                        //console.log("play.");
                    }, audio.duration * 1000); // audio.duration 为音频的时长单位为秒

                }).catch((e) => {
                    // 音频加载失败
                });
            }	
		}
		
		
		
    	$("#move").click(function(){
		  $("#rank").slideDown();
		});
						
    	$("#rank").click(function(){
		  $("#rank").slideUp();
		});

		function voicePlay(){
			document.getElementById("myaudio").play();
		}
	
		//小人移动
		function go(dir){
			$("#rank").slideUp();
			var p1,p2;			
			direction=dir;
			switch (dir)
			{
			case "up":
				curMan = up;
				//获取小人前面的两个坐标位置来井行判断小人是否能够移动
				p1 = new Point(perPosition.x-1,perPosition.y);
				p2 = new Point(perPosition.x-2,perPosition.y);
				break;
			case "down":
				curMan = down;
				p1 = new Point(perPosition.x+1,perPosition.y);
				p2 = new Point(perPosition.x+2,perPosition.y);
				break;
			case "left":
				curMan = left;
				p1 = new Point(perPosition.x,perPosition.y-1);
				p2 = new Point(perPosition.x,perPosition.y-2);
				break;
			case "right":
				curMan = right;
				p1 = new Point(perPosition.x,perPosition.y+1);
				p2 = new Point(perPosition.x,perPosition.y+2);
				break;
			}
			//如果小人能够移动的话，更新游戏数据，并重绘地图
			if (Trygo(p1,p2))
			{	
				backstep=0;
				moveTimes ++;
				showMoveInfo();
			
				var tempmove="tempmove"+iCurlevel;			
				sessionStorage.setItem(tempmove, moves);
			
				//重绘地板
				InitMap();
				//重绘当前更新了数据的地图			
				DrawMap(curMap);
				//console.log(curMap);				
				maparray.push(JSON.stringify(curMap));//当前地图进栈
				
				var tempmap="tempmap"+iCurlevel;
				var tempstr=JSON.stringify(maparray);
				sessionStorage.setItem(tempmap, tempstr);
				console.log("移动",moveTimes,"步");
			}
			//如果移动完成了进入下一关
			if (checkFinish())
			{
				//定时器解决渲染地图后再弹窗
				setTimeout(()=>{
						  notice('pass');
						  showPass();
						  NextLevel(1);
				},0)
				
			}
		}
		var backstep=0;
		function showBack(){
				console.log(backstep);
				var tempmap="tempmap"+iCurlevel;
				//console.log(tempmap);
				var tempstr= sessionStorage.getItem(tempmap);	
				if(tempstr!=""&& tempstr!=null){
					var temparray=JSON.parse(tempstr);
					var tempcount=temparray.length;
					
					if(tempcount>backstep){
						backstep++;
						moveTimes--;
						console.log("撤消",backstep,"步");
						//console.log(temparray);
						console.log(tempcount,backstep);
						var curtemp=temparray[tempcount-backstep];
						curMap=JSON.parse(curtemp);
						//重绘地板
						InitMap();
						//重绘当前更新了数据的地图			
						DrawMap(curMap);
						//console.log("记录地图：");
						//console.log(curMap);	
						maparray.pop();
						movearray.pop();
						showTable();
						notice('cancel');
					}
					else{
						console.log("没有可撤消记录");
					}
				}
		}
		
		//判断是否推成功
		function checkFinish(){
			for (var i=0;i<curMap.length ;i++ )
			{
				for (var j=0;j<curMap[i].length ;j++ )
				{
					//当前移动过的地图和初始地图井行比较，如果初始地图上的陷井参数在移动之后不是箱子的话就指代没推成功
					if (curLevel[i][j] == 2 && curMap[i][j] != 3 || curLevel[i][j] == 5 && curMap[i][j] == 2)
					{
						return false;
					}
				}
			}
			return true;
		}
		//判断小人是否能够移动
		function Trygo(p1,p2){
			if (!begin){
				begin=true;
				voicePlay();
			}
			if(p1.x<0) return false;//如果超出地图的上边，不通过
			if(p1.y<0) return false;//如果超出地图的左边，不通过
			if(p1.x>curMap.length) return false;//如果超出地图的下边，不通过
			if(p1.y>curMap[0].length) return false;//如果超出地图的右边，不通过
			if(curMap[p1.x][p1.y]==1)
			{
					notice('pop');
					return false;//如果前面是墙，不通过
			}
			if (curMap[p1.x][p1.y]==3 || curMap[p1.x][p1.y]==5)
			{//如果小人前面是箱子那就还需要判断箱子前面有没有障碍物(箱子/墙)
				if (curMap[p2.x][p2.y]==1 || curMap[p2.x][p2.y]==3|| curMap[p2.x][p2.y]==5)
				{
					notice('pop');
					return false;
				}
				//如果判断不成功小人前面的箱子前进一步
				curMap[p2.x][p2.y] = 3;//更改地图对应坐标点的值
				//console.log(curMap[p2.x][p2.y]);
								
				//console.log(moves);
				if(curMap[p2.x][p2.y]==2){
					notice('ok');
				}else{				
					notice('tui');				
				}
			}
			else{
				notice('zou');
			}
			if(Backset){
				step=step-1;
			}			
			else{
				step=step+1;			
				movearray.push(direction);//推荐成功
				var movemsg="</br>"+step+"："+direction;
				moves=moves+movemsg;
			}
			showTable();
			
			//如果都没判断成功小人前进一步
			curMap[p1.x][p1.y] = 4;//更改地图对应坐标点的值
			//如果小人前进了一步，小人原来的位置如何显示
			var v = curLevel[perPosition.x][perPosition.y];
			if (v!=2)//如果刚开始小人位置不是陷井的话
			{
				if (v==5)//可能是5 既有箱子又陷井
				{
					v=2;//如果小人本身就在陷井里面的话移开之后还是显示陷井
				}else{
					v=0;//小人移开之后之前小人的位置改为地板
				}
			}
			//重置小人位置的地图参数
			curMap[perPosition.x][perPosition.y] = v;
			//如果判断小人前进了一步，更新坐标值
			perPosition = p1;
			//如果小动了 返回true 指代能够移动小人
			return true;
		}
		//判断是否推成功
		//与键盘上的上下左右键关联
		function doKeyDown(event){
			switch (event.keyCode)
			{
			case 37://左键头
			case 65:
				go("left");
				break;
			case 38://上键头
			case 87:
				go("up");
				break;
			case 39://右箭头
			case 68:
				go("right");
				break;
			case 40://下箭头
			case 83:
				go("down");
				break;
			}

		}
		//完善关卡数据及游戏说明
		function showPass(){
			//var tempmap="tempmap"+iCurlevel;
			//console.log(tempmap);
			//sessionStorage.removeItem(tempmap);
			sessionStorage.clear();
			msg.innerHTML = "恭喜过关！！";
			// 存储游戏进度
			var process=iCurlevel+1;
			localStorage.setItem("sokoban", process);
			var way="save"+iCurlevel;			
			localStorage.setItem(way, moves);			
			console.log("保存游戏进度："+process);
            SaveLevel();
			//console.log("保存移动记录："+moves);
			step=0;//推箱步数
			moves="第"+process+"关 移动记录：";//移动记录
		}
		//完善关卡数据及游戏说明
		function showMoveInfo(){
			msg.innerHTML = "第" + (iCurlevel+1) +"/100关 移动次数: "+ moveTimes;
		}
		//游戏说明
		var showhelp = false;
		function showHelp(){
			showhelp = !showhelp;
			if (showhelp)
			{
				msg.innerHTML = "用键盘上的上、下、左、右键移动小人，把箱子全部推到小球的位置即可过关。箱子只可向前推，不能往后拉，并且小人一次只能推动一个箱子。";
			}else{
				showMoveInfo();
			}
		}

		//克隆二维数组
		function copyArray(arr){
			var b=[];//每次移动更新地图数据都先清空再添加新的地图
			for (var i=0;i<arr.length ;i++ )
			{
				b[i] = arr[i].concat();//链接两个数组
			}
			return b;
		}
		
		
		function automove(){			
			var movearray=[];
			var way="save"+iCurlevel;	
			//console.log(way);
			var oldmove= localStorage.getItem(way);

			//console.log(oldmove);
            if(levelsave==iCurlevel && movenote!=""){
                oldmove=movenote;            
            }
			if(oldmove!=""&& oldmove!=null){
				console.log("开始自动播放：");
				var str=oldmove.split("</br>");	
				var n=str.length;
				var i=0;
				var test=setInterval(function(){
					var cmds=str[i].split("：");					
					var cmdmove=cmds[1];
					console.log(cmdmove);
					if(cmdmove!=""&& cmdmove!=null){
						go(cmdmove);				
						console.log(i+":"+cmdmove+":"+n);	
					}		
					if(i>n-2){
						clearInterval(test);
						console.log("结束自动播放");
					}			
					i=i+1;
				},1000);			
			}
			else{
				var tempmove="tempmove"+iCurlevel;			
				var tempstore= sessionStorage.getItem(tempmove);
				console.log("临时存储："+tempstore);				
				if(tempstore!=""&& tempstore!=null){
					oldmove=tempstore;
					NextLevel(0);
					var str=oldmove.split("</br>");	
					var n=str.length;
					var i=0;
					var test=setInterval(function(){
						var cmds=str[i].split("：");					
						var cmdmove=cmds[1];
						if(cmdmove!=""&& cmdmove!=null){
							go(cmdmove);				
							console.log(i+":"+cmdmove+":"+n);	
						}		
						if(i>n-2){
							clearInterval(test);
							console.log("结束自动播放");
						}			
						i=i+1;
					},500);	
				}
				else{											
					console.log("没有记录");					
				}
			}
		}

		var docurl = document.URL;
		var ipurl = docurl.substring(0, docurl.lastIndexOf("/"));
		
        //自动保存成绩
        function SaveLevel() {            
            var formData = new FormData();
            formData.append('Note', moves);
            formData.append('Level', iCurlevel);
            var saveurl = ipurl + "/push.ashx?Title=sokoban&Level=" + iCurlevel+"&Step="+moveTimes;
            $.ajax({
                url: saveurl,
                type: "POST",
                cache: false,
                data: formData,
                dataType: "html",
                processData: false,
                contentType: false
            }).done(function (res) {
                console.log(res);
            }).fail(function (res) {
                console.log("保存失败");
            }); 
        }


	</script>
</html>





