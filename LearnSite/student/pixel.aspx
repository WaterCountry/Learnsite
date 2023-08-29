<%@ page language="C#" autoeventwireup="true" validaterequest="false" enableviewstatemac="false" inherits="Student_pixel, LearnSite" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Pixel Art Maker 像素艺术画</title>
  <meta name="viewport" content="width=device-width, initial-scale=1"/>
  <link rel="stylesheet" href="../pixelartmaker/style.css"/>  
</head>
<body>
  <div id="pick">
    <h1 >
	<img id="logo" src="../pixelartmaker/logo.png"  alt=""/>   
	Pixel Art Maker 像素画 <input type="color" id="colorPicker"/>&nbsp;	&nbsp;&nbsp;&nbsp;	
	<button id="savebtn"  class="savetext" >保存</button>&nbsp;	&nbsp;&nbsp;
	<button id="returnbtn"  class="savetext" >返回</button>
     </h1>
  </div>

  <form id="sizePicker" action="">
  画布尺寸
  <input type="number" id="input_height" name="height" min="1" max="24" value="38"/> x
  <input type="number" id="input_width" name="width" min="1" max="32" value="54"/>
  <input type="submit"  value="创建画布"/>
  拾取颜色
   </form>

<table id="palette"></table>
<div id="pix" runat="server" >
<table id="pixel_canvas"></table>
</div>
<script type="text/javascript" >
    var id = "<%=Id %>";
    function returnurl() {
        if (confirm('确定要返回吗，记得先保存。') == true) {
            window.location.href = "<%=Fpage %>"
        }
    }
</script>
<script src='../pixelartmaker/jquery.min.js' type="text/javascript" ></script>
<script src="../pixelartmaker/html2canvas.min.js" type="text/javascript" ></script>
<script src="../pixelartmaker/pixelartmaker.js" type="text/javascript" ></script>
</body>
</html>

