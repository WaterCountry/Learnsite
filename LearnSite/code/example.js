		const  example1=[
		"参考示例一:输入输出语句\n\r",
		"print('精彩的编程世界')\n",
		"name = input('请输入您的姓名:')\n",
		"print('欢迎光临',name)\n"
		].join("");
		const  example2=[
		"参考示例二:运算符和表达式\n\r",
		"print('数学运算')\n",
		"a = 1\n",
		"b = 2\n",		
		"s = a+b\n",
		"print('两数之和为',s)\n"
		].join("");		
		const  example3=[
		"参考示例三:选择分支语句\n\r",
		"print('猜年龄')\n",
		"n = input('请输入年龄:')\n",
		"n = int(n)\n",
		"if n>12:\n",
		"    print('你猜大了！')\n",
		"elif n==12:\n",
		"    print('你猜中了！')\n",
		"else:\n",
		"    print('你猜小了！')"
		].join("");	
		const  example4=[
		"参考示例四:条件循环语句\n\r",
		"print('倒计时')\n",
		"n = 5\n",
		"while n>0:\n",
		"    print(n)\n",
		"    n=n-1\n"
		].join("");
		const  example5=[
		"参考示例五:循环选择分支\n\r",
		"print('密码解锁')\n",		
		"while True:\n",
		"    pwd = input('请输入密码:')\n",
		"    if pwd=='abc123':\n",
		"        print('正确')\n",
		"        break\n",
		"    else:\n",
		"        print('错误')\n",
		"        continue"
		].join("");
		var mycodes=new Array();
		mycodes[0]=example1;
		mycodes[1]=example2;
		mycodes[2]=example3;
		mycodes[3]=example4;
		mycodes[4]=example5;
		
		var coden=0;
		var maxn=mycodes.length;
		
		var codeplate=document.getElementById("codeplace");
		//codeplate.innerText  = example3;
		function showexample(){
			codeplate.innerText=mycodes[coden%maxn]
		}
		
		$("#prev").click(function(){
			coden=coden-1;
			if(coden<0){
				coden=0;
			}
			codeplate.innerText=mycodes[coden%maxn]			
		});
		
		
		$("#next").click(function(){
			coden=coden+1;
			codeplate.innerText=mycodes[coden%maxn]			
		});
		
		