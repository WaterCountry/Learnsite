<%@ page language="C#" autoeventwireup="true" inherits="Student_codeproject, LearnSite" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Scratch3在线编程</title>
    <script src="../js/jquery-1.8.2.min.js" type="text/javascript"></script>
<script type="text/javascript">
    window.scratchConfig = {
      logo: {
        show: true
        , url: "../scratch/logo.png"
        , handleClickLogo: () => {
        }
      }, 
      menuBar: {
        color: 'hsla(215, 100%, 65%, 1)'
      }, 
      handleVmInitialized: (vm) => {
        window.vm = vm
        console.log("VM初始化完毕")
        
      },
      handleDefaultProjectLoaded:() => {
          window.scratch.loadProject("<%=sbfile %>", () => { 
             console.log("项目加载完毕")
             window.scratch.setProjectName("<%=sbtitle %>")
          })
      },
      //若使用官方素材库请删除本配置项
    }

  </script>

</head>
<body>
   
  <div id="scratch">
    加载中……
  </div>
    <script src="../Scratch/lib.min.js" type="text/javascript"></script>
    <script src="../Scratch/chunks/gui.js" type="text/javascript"></script>

</body>
</html>