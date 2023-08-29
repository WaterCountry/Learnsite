<%@ page language="C#" autoeventwireup="true" inherits="Student_coding, LearnSite" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Scratch3在线编程</title>
    <script src="../js/jquery.min.js" type="text/javascript"></script>
	<style>
        ::-webkit-scrollbar { width: 6px; } 
        ::-webkit-scrollbar-track {border-radius: 3px; } 
        ::-webkit-scrollbar-thumb { 
            border-radius: 3px; 
            height: 30px; 
            background-color: #eee; 
        }
	</style>
<script>
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
      shareButton: {
        show: true,
        buttonName: "立即保存",
        handleClick: () => {
          window.scratch.getProjectCoverBlob(cover => {
              //TODO 获取到作品截图
              var projectName = window.scratch.getProjectName()
              var projectCover=cover
              console.log(projectName);

              window.scratch.getProjectFile(file => {              
                console.log(projectCover)
                //TODO 获取到项目文件
                console.log(file)
                //TODO 获取到项目文件
                var id = "<%=Id %>";
                var urls = 'uploadproject.ashx?id=' + id;
                var formData = new FormData();
                formData.append('cover', projectCover);
                formData.append('file', file);
                formData.append('title', projectName);

                $.ajax({
                    url: urls,
                    type: 'POST',
                    cache: false,
                    data: formData,
                    processData: false,
                    contentType: false
                }).done(function (res) {
                    alert("保存成功！");
                    console.log(res)
                }).fail(function (res) {
                    alert("保存失败！");
                    console.log(res)
                }); 
            
              })
          
          })
        }
      }, 
      profileButton: {
        show: true,
        buttonName: "查看学案",
        handleClick:()=>{
          //查看学案
          $("#mcontext").slideToggle();
        }
      }, 
      taskButton: {
        show: true,
        buttonName: "返回学案",
        handleClick:()=>{
          //返回学案
          window.location.href="<%=Fpage %>"
        }
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

    $(window).bind('beforeunload',function(){return '确定离开当前页面吗？';} );

  </script>

</head>
<body>
   
  <div id="scratch">
    加载中……
  </div>
    <script src="../scratch/lib.min.js" type="text/javascript"></script>
    <script src="../scratch/chunks/gui.js" type="text/javascript"></script>

   <div id="mcontext" style="display: none; background: #F9F9F9; overflow-y: auto; overflow-x: hidden;
        position: absolute;  width: 620px; height: 450px; z-index: 999; left:312px;
        bottom: 0px; ">
        <div style="margin:10px; ">
        <h4><%=Titles%></h4>
        <%=Mcontents %><br />
        </div>
    </div>
</body>
</html>