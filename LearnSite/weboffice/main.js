
/****************************************************
*
*		关闭页面时调用此函数，关闭文件 
*
****************************************************/
function window_onunload() {
	try{
		var webObj=document.getElementById("WebOffice1");
		webObj.Close();
	}catch(e){
	//	alert("异常\r\nError:"+e+"\r\nError Code:"+e.number+"\r\nError Des:"+e.description);
	}
}
/****************************************************
*
*					新建文档
*
****************************************************/
function newDoc() {
	try{
		var webObj=document.getElementById("WebOffice1");
		var doctype=document.getElementById("doctype").value;
		webObj.LoadOriginalFile("", doctype);
	}catch(e){
		alert("异常\r\nError:"+e+"\r\nError Code:"+e.number+"\r\nError Des:"+e.description);
	}
}

/****************************************************
*
*			关闭页面时调用此函数，关闭文件 
*
****************************************************/
function window_onunload() {
	try{
		var webObj=document.getElementById("WebOffice1");
		webObj.Close();
	}catch(e){
		alert("异常\r\nError:"+e+"\r\nError Code:"+e.number+"\r\nError Des:"+e.description);
	}
}
/****************************************************
*
*				 解除文档保护 
*
****************************************************/
function UnProtect() {
	try{
		var webObj=document.getElementById("WebOffice1");
		webObj.ProtectDoc(0,1, document.all.docPwd.value);
	}catch(e){
		alert("异常\r\nError:"+e+"\r\nError Code:"+e.number+"\r\nError Des:"+e.description);
	}
}
/****************************************************
*			
*				设置文档保护 
*
****************************************************/
function ProtectFull() {
	try{
		var webObj=document.getElementById("WebOffice1");
		webObj.ProtectDoc(1,1, document.all.docPwd.value);
	}catch(e){
		alert("异常\r\nError:"+e+"\r\nError Code:"+e.number+"\r\nError Des:"+e.description);
	}
}


/****************************************************
*
*					禁止复制
*
/****************************************************/
function notCopy() {
	try{
		var webObj=document.getElementById("WebOffice1");
		webObj.SetSecurity(0x04); 
	}catch(e){
		alert("异常\r\nError:"+e+"\r\nError Code:"+e.number+"\r\nError Des:"+e.description);
	}
}
/****************************************************
*
*					恢复允许复制
*
/****************************************************/
function okCopy() {
	try{
		var webObj=document.getElementById("WebOffice1");
		webObj.SetSecurity(0x04 + 0x8000); 
	}catch(e){
		alert("异常\r\nError:"+e+"\r\nError Code:"+e.number+"\r\nError Des:"+e.description);
	}
}
/****************************************************
*
*					禁止拖动
*
/****************************************************/
function notDrag() {
	try{
		var webObj=document.getElementById("WebOffice1");
		webObj.SetSecurity(0x08); 
	}catch(e){
		alert("异常\r\nError:"+e+"\r\nError Code:"+e.number+"\r\nError Des:"+e.description);
	}
}
/****************************************************
*
*					恢复拖动
*
/****************************************************/
function okDrag() {
	try{
		var webObj=document.getElementById("WebOffice1");
		webObj.SetSecurity(0x08 + 0x8000); 
	}catch(e){
		alert("异常\r\nError:"+e+"\r\nError Code:"+e.number+"\r\nError Des:"+e.description);
	}

}

/****************************************************
*
*					打开本地文件
*
/****************************************************/
function docOpen() {
	try{
		var webObj=document.getElementById("WebOffice1");
		webObj.LoadOriginalFile("open", "doc");
	}catch(e){
		alert("异常\r\nError:"+e+"\r\nError Code:"+e.number+"\r\nError Des:"+e.description);
	}
}

/****************************************************
*
*					隐藏菜单
*
/****************************************************/
function notMenu() {
	try{
		var webObj=document.getElementById("WebOffice1");
		webObj.SetToolBarButton2("Menu Bar",1,8);
	}catch(e){
		alert("异常\r\nError:"+e+"\r\nError Code:"+e.number+"\r\nError Des:"+e.description);
	}
}
/****************************************************
*
*					显示菜单
*
/****************************************************/
function okMenu() {
	try{
		var webObj=document.getElementById("WebOffice1");
		webObj.SetToolBarButton2("Menu Bar",1,11);
	}catch(e){
		alert("异常\r\nError:"+e+"\r\nError Code:"+e.number+"\r\nError Des:"+e.description);
	}
}
/****************************************************
*
*					隐藏常用工具栏
*
/****************************************************/
function notOfter() {
	try{
		var webObj=document.getElementById("WebOffice1");
		webObj.SetToolBarButton2("Standard",1,8);
	}catch(e){
		alert("异常\r\nError:"+e+"\r\nError Code:"+e.number+"\r\nError Des:"+e.description);
	}
}
/****************************************************
*
*					显示常用工具栏
*
/****************************************************/
function okOfter() {
	try{
		var webObj=document.getElementById("WebOffice1");
		webObj.SetToolBarButton2("Standard",1,11);
	}catch(e){
		alert("异常\r\nError:"+e+"\r\nError Code:"+e.number+"\r\nError Des:"+e.description);
	}
}
/****************************************************
*
*					隐藏格式工具栏
*
/****************************************************/
function notFormat() {
	try{
		var webObj=document.getElementById("WebOffice1");
		webObj.SetToolBarButton2("Formatting",1,8);
	}catch(e){
		alert("异常\r\nError:"+e+"\r\nError Code:"+e.number+"\r\nError Des:"+e.description);
	}
}
/****************************************************
*
*					显示格式工具栏
*
/****************************************************/
function okFormat() {
	try{
		var webObj=document.getElementById("WebOffice1");
		webObj.SetToolBarButton2("Formatting",1,11);
	}catch(e){
		alert("异常\r\nError:"+e+"\r\nError Code:"+e.number+"\r\nError Des:"+e.description);
	}
}    

/****************************************************
*
*					全屏
*
/****************************************************/
function bToolBar_FullScreen_onclick() {
	try{
		var webObj=document.getElementById("WebOffice1");
		webObj.FullScreen = true;
	}catch(e){
		alert("异常\r\nError:"+e+"\r\nError Code:"+e.number+"\r\nError Des:"+e.description);
	}
}
/****************************************************
*
*		设置weboffice自带工具栏显示或隐藏
*
/****************************************************/
function bToolBar_onclick() {
	try{
		var webObj=document.getElementById("WebOffice1");
		webObj.ShowToolBar =  !webObj.ShowToolBar;
	}catch(e){
		alert("异常\r\nError:"+e+"\r\nError Code:"+e.number+"\r\nError Des:"+e.description);
	}
}

/****************************************************
*
*		设置weboffice自带工具栏显示或隐藏
*
/****************************************************/
function NobToolBar() {
	try{
		var webObj=document.getElementById("WebOffice1");
		webObj.ShowToolBar =  false;
	}catch(e){
		alert("异常\r\nError:"+e+"\r\nError Code:"+e.number+"\r\nError Des:"+e.description);
	}
}
/****************************************************
*
*			Office2007菜单隐藏和恢复
*			----开始菜单隐藏
*
/****************************************************/
function beginMenu_onclick()
{
	try{
		var webObj=document.getElementById("WebOffice1");
		webObj.HideMenuAction(1,0x100000);
		webObj. HideMenuAction(5,0);//激活设置
	}catch(e){
		alert("异常\r\nError:"+e+"\r\nError Code:"+e.number+"\r\nError Des:"+e.description);
	}
}
/****************************************************
*
*			Office2007菜单隐藏和恢复
*			---插入菜单隐藏
*
/****************************************************/
function insertMenu_onclick()
{
	try{
		var webObj=document.getElementById("WebOffice1");
		webObj.HideMenuAction(1,0x200000);
		webObj. HideMenuAction(5,0);//激活设置

	}catch(e){
		alert("异常\r\nError:"+e+"\r\nError Code:"+e.number+"\r\nError Des:"+e.description);
	}
}
/****************************************************
*			
*			Office2007菜单隐藏和恢复
*			---页面菜单隐藏
*
/****************************************************/
function pageMenu_onclick()
{
	try{
		var webObj=document.getElementById("WebOffice1");
		webObj.HideMenuAction(1,0x400000);
		webObj. HideMenuAction(5,0);//激活设置
	
	}catch(e){
		alert("异常\r\nError:"+e+"\r\nError Code:"+e.number+"\r\nError Des:"+e.description);
	}
}
/****************************************************
*			Office2007菜单隐藏和恢复
*			--引用菜单隐藏
*
/****************************************************/
function adducMenu_onclick()
{
	try{
		var webObj=document.getElementById("WebOffice1");
		webObj.HideMenuAction(1,0x800000);
		webObj. HideMenuAction(5,0);//激活设置

	}catch(e){
		alert("异常\r\nError:"+e+"\r\nError Code:"+e.number+"\r\nError Des:"+e.description);
	}
}
/****************************************************
*			Office2007菜单隐藏和恢复
*			---邮件菜单隐藏
*
/****************************************************/
function	emailMenu_onclick()
{
	try{
		var webObj=document.getElementById("WebOffice1");
		webObj.HideMenuAction(1,0x1000000);
		webObj. HideMenuAction(5,0);//激活设置
	}catch(e){
		alert("异常\r\nError:"+e+"\r\nError Code:"+e.number+"\r\nError Des:"+e.description);
	}
}
/****************************************************
*			Office2007菜单隐藏和恢复
*			---审阅菜单隐藏
*
/****************************************************/
function	checkMenu_onclick()
{
	try{
		var webObj=document.getElementById("WebOffice1");
		webObj.HideMenuAction(1,0x2000000);
		webObj. HideMenuAction(5,0);//激活设置
	}catch(e){
		alert("异常\r\nError:"+e+"\r\nError Code:"+e.number+"\r\nError Des:"+e.description);
	}
}
/****************************************************
*			Office2007菜单隐藏和恢复
*			---视图菜单隐藏
*
/****************************************************/
function	viewMenu_onclick()
{
	try{
		var webObj=document.getElementById("WebOffice1");
		webObj.HideMenuAction(1,0x4000000);
		webObj. HideMenuAction(5,0);//激活设置
	}catch(e){
		alert("异常\r\nError:"+e+"\r\nError Code:"+e.number+"\r\nError Des:"+e.description);
	}
}
/****************************************************
*			Office2007菜单隐藏和恢复
*			---开发工具菜单隐藏
*
/****************************************************/
function	empolderMenu_onclick()
{
	try{
		var webObj=document.getElementById("WebOffice1");
		webObj.HideMenuAction(1,0x8000000);
		webObj. HideMenuAction(5,0);//激活设置
	}catch(e){
		alert("异常\r\nError:"+e+"\r\nError Code:"+e.number+"\r\nError Des:"+e.description);
	}
}
/****************************************************
*			Office2007菜单隐藏和恢复
*			---加载项菜单隐藏
*
/****************************************************/
function	loadMenu_onclick()
{
	try{
		var webObj=document.getElementById("WebOffice1");
		webObj.HideMenuAction(1,0x10000000);
		webObj. HideMenuAction(5,0);//激活设置
	}catch(e){
		alert("异常\r\nError:"+e+"\r\nError Code:"+e.number+"\r\nError Des:"+e.description);
	}
}
/****************************************************
*			Office2007菜单隐藏和恢复
*			---全部菜单隐藏
*
/****************************************************/
function	allHideMenu_onclick()
{
	try{
		var webObj=document.getElementById("WebOffice1");
		webObj.HideMenuAction(1,0x100000+0x200000+0x400000+0x800000+0x1000000+0x2000000+0x4000000+0x8000000+0x10000000);
		webObj. HideMenuAction(5,0);//激活设置
	}catch(e){
		alert("异常\r\nError:"+e+"\r\nError Code:"+e.number+"\r\nError Des:"+e.description);
	}
}
/****************************************************
*			Office2007菜单隐藏和恢复
*			---复制无效
*
/****************************************************/
function nullityCopy_onclick()
{
	try{
		var webObj=document.getElementById("WebOffice1");
		webObj. HideMenuAction(1,0x2000);
		webObj. HideMenuAction(5,0);//激活设置
	}catch(e){
		alert("异常\r\nError:"+e+"\r\nError Code:"+e.number+"\r\nError Des:"+e.description);
	}
}
/****************************************************
*			Office2007菜单隐藏和恢复
*			---粘贴无效
*
/****************************************************/
function nullityAffix_onclick()
{
	try{
		var webObj=document.getElementById("WebOffice1");
		webObj. HideMenuAction(1,0x1000);
		webObj. HideMenuAction(5,0);//激活设置
	}catch(e){
		alert("异常\r\nError:"+e+"\r\nError Code:"+e.number+"\r\nError Des:"+e.description);
	}
}
/****************************************************
*			Office2007菜单隐藏和恢复
*	---恢复至设置之前状态（菜单显示，复制，粘贴可用）
*
/****************************************************/
function affixCopy_onclick()
{
	try{
		var webObj=document.getElementById("WebOffice1");
		webObj. HideMenuAction(6,0);
	}catch(e){
		alert("异常\r\nError:"+e+"\r\nError Code:"+e.number+"\r\nError Des:"+e.description);
	}
}
